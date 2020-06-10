using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace IDAL
{
    public interface IProductAttrValueDAL:IBaseDAL<ProductAttrValue>
    {
        List<ProductAttrValue> GetAllByAttrKeyID(int attrKeyID);
        
    }
}
