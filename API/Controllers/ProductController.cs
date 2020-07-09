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
    }
}
