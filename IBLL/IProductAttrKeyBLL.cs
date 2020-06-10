using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace IBLL
{
    public interface IProductAttrKeyBLL:IBaseBLL<ProductAttrKey>
    {
        List<ProductAttrKey> GetByCatecoryID(int cateroryId);
        int Update(ProductAttrKey attrKey, List<ProductAttrValue> attrValues);
    }
}
