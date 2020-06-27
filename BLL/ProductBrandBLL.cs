using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using IBLL;
using MODEL;

namespace BLL
{
    public class ProductBrandBLL : BaseBLL<ProductBrand, ProductBrandDAL>, IProductBrandBLL
    {
        public override int Add(ProductBrand productBrand)
        {
            int result = 0;
            var tran = dal.BeginTran();
            try
            {
                //1.插入商品表（product表）
                dal.Add(productBrand);
                result += SaveChange();//相当于预提交，  默认情况下，调用SaveChange会开启一个事务
                
                tran.Commit();//总提交
            }
            catch (Exception)
            {

                tran.Rollback();
            }

            return result;
        }

        public override int DeLete(int id)
        {

            //1.删除商品表（product表）
            dal.DeLete(id);
            int result = SaveChange();
            return result;
        }

        

        public override int Update(ProductBrand productBrand)
        {
            dal.Update(productBrand);
            SaveChange();
            
            int result = SaveChange();
            return result;
        }

        DbContextTransaction IBaseBLL<ProductBrand>.BeginTran()
        {
            throw new NotImplementedException();
        }
    }
}
