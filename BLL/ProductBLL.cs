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
        public override int DeLete(int id)
        {

            //1.删除商品表（product表）
            dal.DeLete(id);
            //2.删除productsku表(先查后删除)
            var skus = skuDAL.Search(x => x.ProductID == id);
            foreach (var sku in skus)
            {
                skuDAL.DeLete(sku);
            }
            //2.删除productAttr表(先查后删除)
            var attrs = attrDAL.Search(x => x.ProuductID == id);
            foreach (var attr in attrs)
            {
                attrDAL.DeLete(attr);
            }
            int result = SaveChange();
            return result;
        }
        public Product GetOne(int id, out List<ProductAttr> attrs, out List<ProductSku> skus)
        {
            //默认情况下，c#方法返回值只有一个，为了弥补这种缺陷，应使用out参数变相增加返回值
            //out参数：必须在方法体内为其赋值
            var product = dal.GetOne(id);
            skus = skuDAL.Search(x => x.ProductID == id);
            attrs = attrDAL.Search(x => x.ProuductID == id);
            return product;
        }

        public int Update(Product product, List<ProductSku> skuList, List<ProductAttr> attrList)
        {
            dal.Update(product);
            //foreach (var sku in skuList)
            //{
            //    sku.ProductID = product.ID;
            //    skuDAL.DeLete(sku);
            //}
            var skus = skuDAL.Search(x => x.ProductID == product.ID);
            foreach (var sku in skus)
            {
                skuDAL.DeLete(sku);
            }
            foreach (var sku in skuList)
            {
                sku.ProductID = product.ID;
                skuDAL.Add(sku);
            }

            //foreach (var attr in attrList)
            //{
            //    attr.ProuductID = product.ID;
            //    attrDAL.DeLete(attr);
            //}
            //2.删除productAttr表(先查后删除)
            var attrs = attrDAL.Search(x => x.ProuductID == product.ID);
            foreach (var attr in attrs)
            {
                attrDAL.DeLete(attr);
            }
            foreach (var attr in attrList)
            {
                attr.ProuductID = product.ID;
                attrDAL.Add(attr);
            }
            //修改，分两步，首先是删除所有的数据，再重新添加
            //IProductSkuDAL productSkuDal = new ProductSkuDAL();
            //var SkuList = productSkuDal.GetProductID(product.ID);
            //foreach (var sku in SkuList)
            //{
            //    productSkuDal.DeLete(sku);//对ProductAttrKey表执行delete语句
            //}
            //foreach (var item in attrValues)
            //{
            //    attrValueDal.Add(item);//对ProductAttrKey表执行insert语句
            //}


            return SaveChange();
        }
    }
}