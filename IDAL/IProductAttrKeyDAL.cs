using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;
using VMODEL;

namespace IDAL
{
    public interface IProductAttrKeyDAL: IBaseDAL<ProductAttrKey>
    {
        List<ProductAttrKey> GetByCatecoryID(int cateroryId);
        //void Add(ProductAttrKey attrKey);
        //List<ProductAttrKeyVModel> GetByCatecoryID(int categoryID, bool isSku);
    }
}
