using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using MODEL;
using VMODEL;

namespace DAL
{
    public class ProductAttrKeyDAL : BaseDAL<ProductAttrKey>,IProductAttrKeyDAL
    {
        //ShopEntities entities = new ShopEntities();
        //public void Add(ProductAttrKey attrKey)
        //{
        //    entities.ProductAttrKey.Add(attrKey);
        //    entities.SaveChanges();
        //}

        //public int DeLete(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<ProductAttrKey> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        //public List<ProductAttrKeyVModel> GetByCatecoryID(int categoryID, bool isSku)
        //{
        //    return entities.ProductAttrKeyVModel.Where(x => x.ProductCategoryID == categoryID).ToList();
        //}
        public List<ProductAttrKey> GetByCatecoryID(int cateroryId)
        {
            return entities.ProductAttrKey.Where(x => x.ProductCategoryID == cateroryId).ToList();
        }
        DbContextTransaction IBaseDAL<ProductAttrKey>.BeginTran()
        {
            throw new NotImplementedException();
        }




        //public ProductAttrKey GetOne(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Update(ProductAttrKey model)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
