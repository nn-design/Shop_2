using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace IDAL
{
    public interface IProductCategoryDAL: IBaseDAL<ProductCategory>
    {
        //void Add(ProductCategory category);
        ///// <summary>
        ///// 分类的删除
        ///// </summary>
        ///// <param name="id">主键</param>
        ///// <returns></returns>
        //int DeLete(int id);
        //int Update(ProductCategory category);
        //ProductCategory GetOne(int id);
        //List<ProductCategory> GetAll();
        ////为获取某一结点下的所有子节点
        List<ProductCategory> GetSub(int id);//id为父节点id
       
    }
}
