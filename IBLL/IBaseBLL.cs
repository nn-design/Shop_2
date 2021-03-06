﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace IBLL
{
    public interface IBaseBLL<T>
    {
        int Add(T model);

        /// <summary>
        /// 分类的删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        int DeLete(int id);
        int DeLete(T model);
        T GetOne(int id);
        int Update(T model);
        List<T> GetAll();
        List<T> Search(Expression<Func<T, bool>> where);
        List<T> Search<TKey>(int pageSize, int pageIndex, bool isDesc, Func<T, TKey> orderkey, Expression<Func<T, bool>> where, out int count);
        int GetCount(Func<T, bool> where);
        int SaveChange();
        DbContextTransaction BeginTran();//开启一个事务
    }
    
}
