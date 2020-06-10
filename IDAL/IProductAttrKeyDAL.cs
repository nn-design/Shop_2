using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace IDAL
{
    public interface IProductAttrKeyDAL: IBaseDAL<ProductAttrKey>
    {
        //void Add(ProductAttrKey attrKey);
        List<ProductAttrKey> GetByCatecoryID(int cateroryId);
    }
}
