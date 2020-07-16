using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace VMODEL
{
    public class ProductAttrKeyVModel: ProductAttrKey
    {
        public List<string> AttrValues { get; set; }//接受前台数组类型的数据
    }
}
