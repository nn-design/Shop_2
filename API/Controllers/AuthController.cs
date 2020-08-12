using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
using Newtonsoft.Json;
using COMMON;
using StackExchange.Redis;
using Newtonsoft.Json.Linq;
using IBLL;
using BLL;
using MODEL;
using JWT.Algorithms;
using JWT;
using JWT.Serializers;
using System.Configuration;

namespace API.Controllers
{
    public class AuthController : ApiController
    {
        public IMemberBLL Bll
        {
            get
            {
                return new MemberBLL();
            }
        }
        string appId = "wxb811562a9650fd58";
        string appSecret = "0db1106353373facd52d64c26793a3b9";
        // 自定义秘钥
        // jwt 的生成和解析都需要使用
        string secret = ConfigurationManager.AppSettings["JWTSecret"].ToString();
        IMemberBLL memberBLL = new MemberBLL();

        [Route("api/auth/getToken")]
        [HttpPost]
        public ResponsMessage<TokenVModel> GetToken(MemberVModel memberVModel)
        {
            //1、获取openid
            //c# 6.0字符串插值
            string url = $"https://api.weixin.qq.com/sns/jscode2session?appid={appId}&secret={appSecret}&js_code={memberVModel.code}&grant_type=authorization_code";
            // 服务端发起http请求（爬虫）
            //声明一个http请求对象HttpWebRequest
            string retString = "";
            HttpWebResponse response = null;
            Stream myResponseStream = null;
            StreamReader myStreamReader = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                //发起请求，返回一个http响应对象HttpWebRequest
                response = (HttpWebResponse)request.GetResponse();
                //获取响应流GetResponseStream
                myResponseStream = response.GetResponseStream();
                //从响应流读取数据
                myStreamReader = new StreamReader(myResponseStream, System.Text.Encoding.UTF8);
                retString = myStreamReader.ReadToEnd();
            }
            catch (Exception)
            {
                return new ResponsMessage<TokenVModel>
                {
                    Code = 500,
                    Message = "过去微信个人信息失败"
                };
            }
            finally
            {
                //关闭流
                if (myStreamReader!=null)
                {
                    myStreamReader.Close();
                }
                if (myResponseStream!=null)
                {
                    myResponseStream.Close();
                }
                if (response!=null)
                {
                    response.Close();
                }
            }


            //序列化：将对象转化为json串
            //反序列化：将json串转化为对象
            //var obj = JsonConvert.DeserializeObject<dynamic>(retString);
            //var openid = obj.openid.ToString();

            JObject jObject = JObject.Parse(retString);
            if (jObject["openid"]==null)
            {
                return new ResponsMessage<TokenVModel>
                {
                    Code = 500,
                    Message = "code不正确"
                };
            }
            var openid = jObject["openid"].ToString();

            //2、根据openid检测数据库是否包含用户，没有则要将用户添加到数据库中
            int uid = 0;
            //判断数据库中是否存在该用户（根据openid）查询
            var list = Bll.Search(x => x.OpenId == openid);
            //如果没有，使用ef往member表中添加用户数据
            if (list.Count == 0)
            {
                Member member = new Member();
                member = memberVModel.userInfo;
                member.OpenId = openid;
                Bll.Add(member);
                uid = member.ID;
            }
            else
            {
                uid = list[0].ID;
            }
            // 3、生成token（最常用两种方式：md5,jwt）

            string token, refreshToken;
            CreateToken(uid.ToString(), out token, out refreshToken);
            DateTime expire = DateTime.Now.AddMinutes(1);
            DateTime now = DateTime.Now;

            //使用 md5 来生成 token
            //string yan = "@!~#$%^&$$@&";
            //string time = DateTime.Now.ToString("yyyyMMddHHmmssfffff");
            //string guid = Guid.NewGuid().ToString("N");
            //string random = new Random().Next(10000, 99999).ToString();
            //string nickName = memberVModel.userInfo.NickName;
            //string str = yan + time + guid + random + nickName;
            //string token = Md5Helper.Md5(Md5Helper.Md5(str));

            //4、将生成token存入redis

            //// 声明一个链接
            //var conn = ConnectionMultiplexer.Connect("192.168.178.188:6379,password=123456");
            //// 指定操作的数据库
            //var db = conn.GetDatabase(0);
            ////写入数据
            //db.StringSet(token, openid, DateTime.Now.AddDays(7) - DateTime.Now);

            //refreshToken的过期时间要比token长，一般是两倍
            RedisHelper.Set(token, uid.ToString(), expire - now);
            RedisHelper.Set(refreshToken, uid.ToString(), expire.AddMinutes(6) - now);

            //5 返回token
            return new ResponsMessage<TokenVModel>()
            {
                Code = 200,
                Data = new TokenVModel() {
                    Token=token,
                    RefreshToken=refreshToken,
                    Expire=(int)(expire - now).TotalMilliseconds
                }
            };
        }
        [Route("api/auth/getTokenByRefreshToken")]
        [HttpGet]
        public ResponsMessage<TokenVModel> getTokenByRefreshToken(string rtoken)
        {
            //验证token是否过期
            var uid = RedisHelper.Get(rtoken);
            if (uid==null)
            {
                return new ResponsMessage<TokenVModel>()
                {
                    Code = 500,
                    Message = "RefreshToken失效，请重新授权"
                };
            }
            // 3、生成token
            string token, refreshToken;
            CreateToken(uid.ToString(), out token, out refreshToken);
            DateTime expire = DateTime.Now.AddDays(7);
            DateTime now = DateTime.Now;

            //4、将生成token存入redisrefreshToken的过期时间要比token长，一般是两倍
            RedisHelper.Set(token, uid.ToString(), expire - now);
            RedisHelper.Set(refreshToken, uid.ToString(), expire.AddDays(7) - now);

            //5 返回token
            return new ResponsMessage<TokenVModel>()
            {
                Code = 200,
                Data = new TokenVModel()
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    Expire = (int)(expire - now).TotalMilliseconds
                }
            };
        }
        private void CreateToken(string uid, out string token, out string refreshToken)
        {
            Random random = new Random();
            // 使用 JwtBuilder 来生成 token
            var payload = new Dictionary<string, string>//模拟到时候服务器想接受的数据
            {
                { "username",uid+random.Next(100000,999999).ToString()+ Guid.NewGuid().ToString("N")},
            };
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            token = encoder.Encode(payload, secret);

            payload = new Dictionary<string, string>
            {
                { "username",uid+random.Next(10000,99999).ToString()+ Guid.NewGuid().ToString("N")},
            };
            refreshToken = encoder.Encode(payload, secret);
        }

        [AuthFilter]
        [Route("api/auth/test")]
        [HttpGet]
        public string AuthTest()
        {
            return "Auth OK";
        }
    }
}
