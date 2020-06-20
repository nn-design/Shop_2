using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using MODEL;


namespace DAL
{
    // where T : class是对泛型T做了一个约束，必须是class,new()必须具有无参构造函数
    //T :  BaseModel必须继承BaseModel必须继承
    public class BaseDAL<T> where T :  BaseModel, new()
    {
        public ShopEntities entities = DBContextFactory.GetEntities();
        public virtual void Add(T model)
        {
            DbEntityEntry entityEntry = entities.Entry<T>(model);
            entityEntry.State = EntityState.Added;
        }

        public virtual void DeLete(int id)
        {
            T model = new T() { ID = id };
            DbEntityEntry entityEntry = entities.Entry<T>(model);
            entityEntry.State = EntityState.Deleted;
        }
        public virtual void DeLete(T model)
        {
            DbEntityEntry entityEntry = entities.Entry(model);
            entityEntry.State = EntityState.Deleted;
         
        }
        //引用类型：class，string 不能是int double bool
        public virtual List<T> GetAll()
        {
            return entities.Set<T>().ToList();
        }

        public virtual List<T> Search(Func<T, bool> where)
        {
            return entities.Set<T>().Where(where).ToList();
        }
        public virtual List<T> Search(int pageSize, int pageIndex, bool isDesc, Func<T, bool> where)
        {
            if (isDesc)
            {
                return entities.Set<T>().Where(where).OrderByDescending(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return entities.Set<T>().Where(where).OrderBy(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        public virtual int GetCount(Func<T, bool> where)
        {
            return entities.Set<T>().Where(where).Count();
        }
        public virtual T GetOne(int id)
        {
            return entities.Set<T>().First(x=>x.ID==id);
        }

        public virtual void Update(T model)
        {
            DbEntityEntry entityEntry = entities.Entry<T>(model);
            entityEntry.State = EntityState.Modified;
        }
        public int SaveChange()
        {
            return entities.SaveChanges();//事务提交
        }
        /// <summary>
        /// 开启一个事务
        /// </summary>
        /// <returns></returns>
        public System.Data.Entity.DbContextTransaction BeginTran()
        {
            var tran = entities.Database.BeginTransaction();
            return tran;
        }
        
    }
}
