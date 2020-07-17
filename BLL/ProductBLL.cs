using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;
using DAL;
using IBLL;
using IDAL;
using System.Data.Entity;
using Shop.Models;

namespace BLL
{
    public class ProductBLL : BaseBLL<Product, ProductDAL>, IProductBLL
    {
        IProductSkuDAL skuDAL = new ProductSkuDAL();
        IProductAttrDAL attrDAL = new ProductAttrDAL();
        IProductAttrKeyBLL attrBLL = new ProductAttrKeyBLL();

        IProductSkuImgDAL skuImgDAL = new ProductSkuImgDAL();

        public int Add(Product product, List<ProductSku> skuList, List<ProductAttr> attrList, List<ProductSkuImg> skuImgs)
        {
            int result = 0;
            var tran = dal.BeginTran();
            try
            {
                //1.插入商品表（product表）
                dal.Add(product);
                result += SaveChange();//相当于预提交，  默认情况下，调用SaveChange会开启一个事务

                //2.插入productsku表
                foreach (var sku in skuList)
                {
                    sku.ProductID = product.ID;
                    skuDAL.Add(sku);
                }
                //3.插入productAttr表
                foreach (var attr in attrList)
                {
                    attr.ProuductID = product.ID;
                    attrDAL.Add(attr);
                }
                //4.插入ProductSkuImg表
                foreach (var skuImg in skuImgs)
                {
                    skuImg.ProductID = product.ID;
                    skuImgDAL.Add(skuImg);
                }
                result += SaveChange();//相当于预提交，  默认情况下，调用SaveChange会开启一个事务
                tran.Commit();//总提交
            }
            catch (Exception ex)
            {
                //throw ex.InnerException;
                tran.Rollback();
            }

            return result;
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
        //小程序商品详情接口
        public ProductVModel GetFullInfoByID(int id)
        {
            var product = dal.GetOne(id);
            var skus = attrBLL.GetByCatecoryID(product.ProductCategoryID.Value, true);
            var productSkus = skuDAL.Search(x => x.ProductID == product.ID);

            return new ProductVModel()
            {
                Product = product,
                Skus = skus,
                ProductSkus = productSkus
            };
        }

        public int Update(Product product, List<ProductSku> skuList, List<ProductAttr> attrList)
        {
            dal.Update(product);
            SaveChange();
            //2.修改sku表
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
            
            //3.修改attr表
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
            int result = SaveChange();
            return result;
        }

        System.Data.Entity.DbContextTransaction IBaseBLL<Product>.BeginTran()
        {
            throw new NotImplementedException();
        }
    }
}