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
        //依赖注入核心思想（控制反转）：将创建具体对象的控制权移交给专门负责创建具体对象的容器（ioc容器）
        //IProductCategoryDAL _categoryDAL;
        //public ProductCategoryBLL(IProductCategoryDAL categoryDAL) 
        //{
        //    _categoryDAL = categoryDAL;
        //}

        //当一个类包含一个具体的类时会发生依赖耦合关系，ProductCategoryBLL和ProductCategoryDAL时强依赖关系
        //ProductCategoryDAL categoryDAL = new ProductCategoryDAL();

        //尽量依赖于抽象而不是依赖于具体实现

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
            var tran = this.BeginTran();
            int result = 0;
            string path = "";//定义path
            try
            {
                model.UpdateTime = DateTime.Now;
                if (model.PID == 0)
                {
                    path = model.ID.ToString();//获取自己的id添加进path
                }
                else
                {
                    var list = this.Search(x => x.ID == model.PID);
                    var parent = list.First();//查询父级分类数据
                    path = parent.Path + "," + model.ID;//父级分类额path和自己的添加进path
                }
                model.Path = path;
                dal.Update(model);
                result += this.SaveChange();
                tran.Commit();
                
                //System.InvalidOperationException:
                //“附加类型“MODEL.ProductCategory”的实体失败，

                //因为  相同类型的其他实体已具有相同的主键值。

                //在使用 "Attach" 方法或者将实体的状态设置为
                //    "Unchanged" 或 "Modified" 
                //    时如果图形中的任何实体具有冲突键值，
                //则可能会发生上述行为。
                //这可能是因为某些实体是新的并且尚未接收数据库生成的键值。
                //在此情况下，
                //使用 "Add" 方法或者 "Added" 实体状态跟踪该图形，然后将非新实体的状态相应设置为 "Unchanged" 或 "Modified"。”


                //ef上下文对象 默认情况下会对实体进行缓存（跟踪实体对象的状态）
                //当进行查询时会将数据库返回的树缓存起来，当再次亲环球相同的实体（主键相同 ID相同）时会直接从缓存中读取
                //缓存中只能存放唯一的实体（ID相同的只能存一个）
            }
            catch (Exception ex)
            {
                result = 0;
                tran.Rollback();
            }
           
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
