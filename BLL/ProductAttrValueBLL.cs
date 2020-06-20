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
    public class ProductAttrValueBLL : BaseBLL<ProductAttrValue, ProductAttrValueDAL>, IProductAttrValueBLL
    {
        public List<ProductAttrValue> GetAllByAttrKeyID(int attrKeyID)
        {
            return dal.GetAllByAttrKeyID(attrKeyID);
        }

        IBLL.DbContextTransaction IBaseBLL<ProductAttrValue>.BeginTran()
        {
            throw new NotImplementedException();
        }
    }
}
