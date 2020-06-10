using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using IBLL;
using IDAL;
using MODEL;

namespace BLL
{
    public class ProductAttrKeyBLL : BaseBLL<ProductAttrKey, ProductAttrKeyDAL>, IProductAttrKeyBLL
    {
        public List<ProductAttrKey> GetByCatecoryID(int cateroryId)
        {
            return dal.GetByCatecoryID(cateroryId);
        }
        public  int Update(ProductAttrKey attrKey,List<ProductAttrValue> attrValues)
        {
            
            //1.对attrKey修改
            dal.Update(attrKey);//对ProductAttrKey表执行update语句
            if (attrKey.EnterType==2)
            {
                //2.对attrValues的修改，分两步，首先是删除所有的数据，再重新添加
                IProductAttrValueDAL attrValueDal = new ProductAttrValueDAL();
                var attrValueList = attrValueDal.GetAllByAttrKeyID(attrKey.ID);
                foreach (var item in attrValueList)
                {
                    attrValueDal.DeLete(item);//对ProductAttrKey表执行delete语句
                }
                foreach (var item in attrValues)
                {
                    attrValueDal.Add(item);//对ProductAttrKey表执行insert语句
                }
            }
            
            //事务执行的过程：两种情况

            //1.当sql语句全部执行成功后，然后提交事务（将数据的修改刷新到磁盘，将数据进行永久的修改）

            //2.当其中一条sql语句出像异常，对事物进行回滚操作（对之前执行过的sql语句进行反操作）

            //提交事务
            int result = SaveChange();
            return result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="attrKey"></param>
        /// <param name="attrValues"></param>
        /// <returns></returns>
        public int DeLete(ProductAttrKey attrKey, List<ProductAttrValue> attrValues)
        {
            IProductAttrValueDAL attrValueDal = new ProductAttrValueDAL();
            var attrValueList = attrValueDal.GetAllByAttrKeyID(attrKey.ID);
            foreach (var item in attrValueList)
            {
                attrValueDal.DeLete(item);//对ProductAttrKey表执行delete语句
            }

            //事务执行的过程：两种情况

            //1.当sql语句全部执行成功后，然后提交事务（将数据的修改刷新到磁盘，将数据进行永久的修改）

            //2.当其中一条sql语句出像异常，对事物进行回滚操作（对之前执行过的sql语句进行反操作）

            //提交事务
            int result = SaveChange();
            return result;
        }

    }
}
