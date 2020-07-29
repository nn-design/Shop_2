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
        [Route("api/auth/getToken")]
        [HttpPost]
        public ResponsMessage<string> GetToken(MemberVModel memberVModel)
        {
            //1、获取openid
            //c# 6.0字符串插值
            string url = $"https://api.weixin.qq.com/sns/jscode2session?appid={appId}&secret={appSecret}&js_code={memberVModel.code}&grant_type=authorization_code";
            // 服务端发起http请求（爬虫）
            HttpWebRequest request =(HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            //发起请求，返回一个http响应对象HttpWebRequest
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //获取响应流GetResponseStream
            Stream myResponseStream = response.GetResponseStream();
            //从响应流读取数据
            StreamReader myStreamReader = new StreamReader(myResponseStream, System.Text.Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            //关闭流
            myStreamReader.Close();
            myResponseStream.Close();
            //序列化：将对象转化为json串
            //反序列化：将json串转化为对象
            //var obj = JsonConvert.DeserializeObject<dynamic>(retString);
            //var openid = obj.openid.ToString();
            JObject jObject = JObject.Parse(retString);
            var openid = jObject["openid"].ToString();

            //2、根据openid检测数据库是否包含用户，没有则要将用户添加到数据库中
            //判断数据库中是否存在该用户（根据openid）查询
            //List<Member> list = new List<Member>();
             var list = Bll.Search(x => x.OpenId == openid);
            //如果没有，使用ef往member表中添加用户数据
            if (list.Count==0)
            {
                Member member = new Member();
                member = memberVModel.userInfo;
                member.OpenId = openid;
                Bll.Add(member);
            }
            // 3、生成token（最常用两种方式：md5,jwt）
            string yan = "@!~#$%^&$$@&";
            string time = DateTime.Now.ToString("yyyyMMddHHmmssfffff");
            string guid = Guid.NewGuid().ToString("N");
            string random = new Random().Next(10000, 99999).ToString();
            string nickName = memberVModel.userInfo.NickName;
            string str = yan + time + guid + random + nickName;
            string token = Md5Helper.Md5(Md5Helper.Md5(str));

            //4、将生成token存入redis
            // 声明一个链接
            var conn = ConnectionMultiplexer.Connect("192.168.178.188:6379,password=123456");
            // 指定操作的数据库
            var db = conn.GetDatabase(0);
            //写入数据
            db.StringSet(token, openid, DateTime.Now.AddDays(7) - DateTime.Now);
            
            return new ResponsMessage<string>()
            {
                Code = 200,
                Data = token
            };
        }
    }
}
