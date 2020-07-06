using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;
using IDAL;
using System.Data.Entity;

namespace BLL
{
    /// <summary>
    /// T:MODEL类型
    /// D:DAL类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="D"></typeparam>
    public class BaseBLL<T,D>
        where T : BaseModel, new()
        where D:IBaseDAL<T>,new()
    {
        public D dal = new D();
        
        public virtual int Add(T model)
        {
            dal.Add(model);
            return SaveChange();
        }

        public virtual int DeLete(int id)
        {
            dal.DeLete(id);
            return SaveChange();
        }
        public virtual int DeLete(T model)
        {
            dal.DeLete(model);
            return SaveChange();
        }
        public virtual List<T> GetAll()
        {

            return dal.GetAll();
        }

        public virtual List<T> Search(Func<T, bool> where)
        {
            return dal.Search(where);
        }
        public virtual List<T> Search(int pageSize, int pageIndex, bool isDesc, Func<T, bool> where)
        {
            return dal.Search(pageSize, pageIndex, isDesc, where);
        }
        public virtual int GetCount(Func<T, bool> where)
        {
            return dal.GetCount(where);
        }
        public  T GetOne(int id)
        {
            return dal.GetOne(id);
        }
        public virtual int Update(T model)
        {
            dal.Update(model);
            return SaveChange();
        }
        public int SaveChange()
        {
            return dal.SaveChange();
        }
        public DbContextTransaction BeginTran()
        {
            return dal.BeginTran();//开启一个事务
        }
    }
}
