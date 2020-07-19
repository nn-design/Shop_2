using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;
using VMODEL;

namespace IBLL
{
    public interface IProductAttrKeyBLL:IBaseBLL<ProductAttrKey>
    {
        //List<ProductAttrKeyVModel> GetByCatecoryID(int categoryID, bool isSku);
        List<ProductAttrKey> GetByCategoryID(int categoryId);
        List<ProductAttrKeyVModel> GetByCatecoryID(int categoryID, bool isSku);
        int Update(ProductAttrKey attrKey, List<ProductAttrValue> attrValues);
    }
}
