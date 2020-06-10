using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace IBLL
{
    public interface IProductAttrValueBLL:IBaseBLL<ProductAttrValue>
    {
        List<ProductAttrValue> GetAllByAttrKeyID(int attrKeyID);
       
    }
}
