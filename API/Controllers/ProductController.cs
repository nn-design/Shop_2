using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using API.Models;
using BLL;
using IBLL;
using MODEL;
using Shop.Models;

namespace API.Controllers
{
    public class ProductController : BaseApiController<Product, IProductBLL>
    {
        public override IProductBLL Bll { get => new ProductBLL(); }

        public override ResponsMessage<PageModel<Product>> PostPager(SearchVModel search)
        {
            try
            {
                int count;
                var list = Bll.Search(

                    search.pageSize,
                    search.pageIndex,
                    false,
                    x => x.ID,
                    x => x.ProductTitle.Contains(search.keyWord),
                    out count
                    );
                PageModel<Product> page = new PageModel<Product>()
                {

                    count = count,
                    data = list
                };
                return Success(page);

            }
            catch (Exception ex)
            {

                return Error<PageModel<Product>>("在查询单条数据过程中出现异常");
            }
        }
        public override ResponsMessage<Product> Get(int id)
        {
            var list = Bll.GetOne(id);
            return new ResponsMessage<Product>()
            {
                Code = 200,
                Message = "请求成功",
                Data = list
            };
        }
        [Route("api/Product/{ID}")]
        public ResponsMessage<ProductVModel> GetOne(int id)
        {
            try
            {
                List<ProductAttr> attrs;
                List<ProductSku> skus;
                //默认情况下，c#方法返回值只有一个，为了弥补这种缺陷，应使用out参数变相增加返回值
                //out参数：必须在方法体内为其赋值
                var product = Bll.GetOne(id, out attrs, out skus);
                ProductVModel productVModel=new ProductVModel()
                {
                    Product = product,
                    Attrs = attrs,
                    Skus = skus,
                };
                return Success(productVModel);
            }
            catch (Exception)
            {

                return Error<ProductVModel>("在查询单条数据过程中出现异常");
            }
        }
    }
}
