using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;
using DAL;
using IBLL;
using IDAL;

namespace BLL
{
    public class ProductBLL:BaseBLL<Product,ProductDAL>,IProductBLL
    {
        IProductSkuDAL skuDAL = new ProductSkuDAL();
        IProductAttrDAL attrDAL = new ProductAttrDAL();
        public  int Add(Product product, List<ProductSku> skuList, List<ProductAttr> attrList)
        {
            //1.插入商品表（product表）
            dal.Add(product);
            SaveChange();
            //2.插入productsku表
            foreach (var sku in skuList)
            {
                sku.ProductID = product.ID;
                skuDAL.Add(sku);
            }
            //2.插入productAttr表
            foreach (var attr in attrList)
            {
                attr.ProuductID = product.ID;
                attrDAL.Add(attr);
            }
            return SaveChange();
        }
        public int DeLete(Product product, List<ProductSku> skuList, List<ProductAttr> attrList)
        {
            //1.删除商品表（product表）
            dal.DeLete(product);
            //2.删除productsku表
            foreach (var sku in skuList)
            {
                sku.ProductID = product.ID;
                skuDAL.DeLete(sku);
            }
            //2.删除productAttr表
            foreach (var attr in attrList)
            {
                attr.ProuductID = product.ID;
                attrDAL.DeLete(attr);
            }
            return SaveChange();
        }
    }
}