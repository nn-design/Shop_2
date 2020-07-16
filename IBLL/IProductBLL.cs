using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;
using Shop.Models;

namespace IBLL
{
    public interface IProductBLL:IBaseBLL<Product>
    {
        int Add(Product product, List<ProductSku> skuList, List<ProductAttr> attrList);
        Product GetOne(int id, out List<ProductAttr> attrs, out List<ProductSku> skus);
        ProductVModel GetFullInfoByID(int id);
        
       int Update(Product product, List<ProductSku> skuList, List<ProductAttr> attrList);
    }
}
