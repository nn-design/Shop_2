using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using COMMON;
using Constant;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using System.Configuration;

namespace Img.Controllers
{
    public class ImgController : Controller
    {
        string ak = ConfigurationManager.AppSettings["AccessKey"];
        string sk = ConfigurationManager.AppSettings["SecretKey"];
        string qiNiuScope = ConfigurationManager.AppSettings["QiNiuScope"];
        // GET: Img
        public ActionResult Upload()
        {
            List<string> fileNames = new List<string>();
            var files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];

                //1.判断文件是否存在 
                if (file == null)
                {
                    return Json(new { state = false, msg = "图片不存在" });
                }
                //2.判断上传文件的类型（.jpg .jpeg .png .gif）,类型检测不通过，直接返回

                byte[] bytes = Common.StreamToBytes(file.InputStream);

                FileExtension fileExtension = FileHelper.CheckTextFile(bytes);
                if (fileExtension == FileExtension.VALIDFILE)
                {
                    return Json(new { state = false, msg = "上传的文件已损坏" });
                }
                if (!(fileExtension == FileExtension.GIF || fileExtension == FileExtension.JPG || fileExtension == FileExtension.PNG))
                {
                    return Json(new { state = false, msg = "上传的文件类型有误" });
                }

                //3.生成一个新的文件名：20200317084945444（yyyyMMddHHmmssfffff）+五位随机数+后缀名
                DateTime dt = DateTime.Now;
                //yyyy：四位数的年份，MM：两位数的月份，dd：两位数的日期，HH：24小时制的时间,mm:两位数的分钟,ss:两位数的秒，f:代表随机数
                //2020-03-18:yyyy-MM-dd
                //2020年03月18日:yyyy年MM月dd日
                dt.ToString("yyyyMMddHHmmssfffff");
                //获取随机数
                Random random = new Random();
                int randowNum = random.Next(0, 999999);
                //获取文件后缀名
                string extension = Path.GetExtension(file.FileName);
                //Guid（全球统一标识符）理论上可以产生全宇宙唯一的值
                // Guid.NewGuid().ToString("N")
                string newFileName = Guid.NewGuid().ToString("N") + randowNum.ToString() + extension;

                //3.将图片保存到服务器
                //F:\Asp.net\Shop\All Shop\shop\实现ajax的添加\Shop\Shop\CategoryImages  :绝对路径
                //file.SaveAs(@"F:\Asp.net\Shop\All Shop\shop\实现ajax的添加\Shop\Shop\CategoryImages\"+newFileName);
                //根据相对路径获取绝对路径

                var imgType = Request["imgType"];//获取图片类型
                string upLoadPath = null;
                if (WebConstants.ImgPathDicts.ContainsKey(imgType))
                {
                    upLoadPath = WebConstants.ImgPathDicts[imgType];
                }
                else
                {
                    return Json(new { state = false });
                }
                string fullPath = upLoadPath + newFileName;
                file.SaveAs(Server.MapPath(fullPath));

                //4 将图片上传到云服务器（七牛云）

                //1.定义好其中鉴权对象mac
                Mac mac = new Mac(ak, sk);
                //2.简单上传的凭证
                PutPolicy putPolicy = new PutPolicy();
                putPolicy.Scope = qiNiuScope;//空间命名
                string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());
                //3.构建配置类
                Config config = new Config();
                // 空间对应的机房
                config.Zone = Zone.ZONE_CN_North;//空间区域
                // 是否使用https域名
                config.UseHttps = false;
                // 上传是否使用cdn加速
                config.UseCdnDomains = true;
                //4.字节数据上传
                FormUploader fu = new FormUploader(config);
                HttpResult result = fu.UploadData(bytes, fullPath, token,null);

                fileNames.Add("http://localhost:56568"+fullPath);

            }
            return Json(fileNames);



        }
    }
}