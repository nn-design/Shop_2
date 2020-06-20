using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using IBLL;
using IDAL;
using MODEL;

namespace BLL
{
    public class ProductCategoryBLL: BaseBLL<ProductCategory, ProductCategoryDAL>, IProductCategoryBLL
    {
        public override int Add(ProductCategory model)
        {
            model.CreateTime = DateTime.Now;
           dal.Add(model);
            return SaveChange();
        }
        public List<ProductCategory> GetSub(int id)
        {
            return dal.GetSub(id);
        }

        IBLL.DbContextTransaction IBaseBLL<ProductCategory>.BeginTran()
        {
            throw new NotImplementedException();
        }
    }
}
