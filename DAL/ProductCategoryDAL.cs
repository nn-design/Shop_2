using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;
using IDAL;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace DAL
{
    //同时继承基类和接口是，积累写在接口前边，基类只能继承一个，接口可继承多个
    public class ProductCategoryDAL:BaseDAL<ProductCategory>,IProductCategoryDAL
    {
        //ShopEntities entities = new ShopEntities();
        //public void Add(ProductCategory category)
        //{

        //    entities.ProductCategory.Add(category);
        //    entities.SaveChanges();
        //}

        //public int DeLete(int id)
        //{
        //    ProductCategory category = new ProductCategory() { ID = id };
        //    DbEntityEntry entityEntry= entities.Entry(category);
        //    entityEntry.State = EntityState.Deleted;
        //    return entities.SaveChanges();
            
        //}

        //public List<ProductCategory> GetAll()
        //{
        //    return entities.ProductCategory.ToList();//获取全部分类
        //}

        //public ProductCategory GetOne(int id)
        //{
        //    return entities.ProductCategory.First(x => x.ID == id);
        //}
        //获取某一结点下的所有子节点
        public List<ProductCategory> GetSub(int id)
        {
            return entities.ProductCategory.Where(x => x.PID == id).ToList();//获取全部分类
        }


        public override void Update(ProductCategory model)
        {
            DbEntityEntry entityEntry = entities.Entry<ProductCategory>(model);
            entityEntry.State = EntityState.Modified;

            entityEntry.Property("CreateTime").IsModified = false;//时间不变不更新
            
        }

       
    }
}
