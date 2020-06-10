using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace IBLL
{
    public interface IProductBLL:IBaseBLL<Product>
    {
        int Add(Product product, List<ProductSku> skuList, List<ProductAttr> attrList);
        int DeLete(Product product, List<ProductSku> skuList, List<ProductAttr> attrList);
    }
}
