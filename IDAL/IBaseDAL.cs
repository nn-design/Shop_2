using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace IDAL
{
    public interface IBaseDAL<T>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        void Add(T model);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        void DeLete(int id);
    
        void DeLete(T model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        void Update(T model);
        //void DeLete<T>(T model) where T : BaseModel, new();

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetOne(int id);
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();
        List<T> Search(Expression<Func<T, bool>> where);
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="isDesc"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        List<T> Search<TKey>(int pageSize, int pageIndex, bool isDesc, Func<T, TKey> orderkey, Expression<Func<T, bool>> where, out int count);
        /// <summary>
        /// 获得总条数（分页）
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        int GetCount(Func<T, bool> where);
        int SaveChange();
        DbContextTransaction BeginTran();//开启一个事务
    }
}
