using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace IBLL
{
    public interface IProductCategoryBLL: IBaseBLL<ProductCategory>
    {
        //void Add(ProductCategory category);

        ///// <summary>
        ///// 分类的删除
        ///// </summary>
        ///// <param name="id">主键</param>
        ///// <returns></returns>
        //int DeLete(int id);
        //ProductCategory GetOne(int id);
        //int Update(ProductCategory category);
        //List<ProductCategory> GetAll();
        List<ProductCategory> GetSub(int id);//id为父节点id
    }
}
