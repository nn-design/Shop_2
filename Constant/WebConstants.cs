using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constant
{
    public class WebConstants
    {
        //用来存放图片类型和上传路径的映射关系
        public static Dictionary<string, string> ImgPathDicts = new Dictionary<string, string>();
        static WebConstants()//静态构造函数，只执行一次（当访问静态变量或者创建实例执行时），一般用来初始化静态变量
        {
            ImgPathDicts.Add("ProductCategoryImage", "/Upload/Product/CategoryImages/");
            ImgPathDicts.Add("ProductMainImg", "/Upload/Product/ProductMainImg/");
            ImgPathDicts.Add("ProductSlideImgs", "/Upload/Product/ProductSlideImgs/");
            ImgPathDicts.Add("ProductDetail", "/Upload/Product/ProductDetail/");

            ImgPathDicts.Add("BrandLogo", "/Upload/Product/BrandLogo/");
            ImgPathDicts.Add("BrandSpecialImg", "/Upload/Product/BrandSpecialImg/");



        }
    }
}
