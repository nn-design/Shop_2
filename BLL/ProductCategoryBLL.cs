using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            //model.CreateTime = DateTime.Now;
            //dal.Add(model);
            //return SaveChange();


            var tran = this.BeginTran();
            int result = 0;
            string path = "";//定义path
            try
            {
                model.CreateTime = DateTime.Now;
                dal.Add(model);
                result += this.SaveChange();

                if (model.PID == 0)
                {
                    path = model.ID.ToString();
                }
                else
                {
                    var parent = this.Search(x => x.ID == model.PID).First();
                    path = parent.Path + "," + model.ID;
                }
                model.Path = path;
                dal.Update(model);
                result += this.SaveChange();
                tran.Commit();
            }
            catch (Exception ex)
            {
                result = 0;
                tran.Rollback();
            }
            return result;
        }
        public override int Update(ProductCategory model)
        {
            int result = 0;
            string path = "";//定义path
            model.UpdateTime = DateTime.Now;
            if (model.PID == 0)
            {
                path = model.ID.ToString();//获取自己的id添加进path
            }
            else
            {
                var parent = this.Search(x => x.ID == model.PID).First();//查询父级分类数据
                path = parent.Path + "," + model.ID;//父级分类额path和自己的添加进path
            }
            model.Path = path;
            dal.Update(model);
            result += this.SaveChange();
            return result;
        }
        public List<ProductCategory> GetSub(int id)
        {
            return dal.GetSub(id);
        }

        System.Data.Entity.DbContextTransaction IBaseBLL<ProductCategory>.BeginTran()
        {
            throw new NotImplementedException();
        }
    }
}
