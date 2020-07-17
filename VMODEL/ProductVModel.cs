using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MODEL;
using VMODEL;

namespace Shop.Models
{
    public class ProductVModel
    {
        public Product Product { get; set; }
        public List<ProductAttrKeyVModel> Skus { get; set; }
        public List<ProductSku> ProductSkus { get; set; }

        public List<ProductAttr> Attrs { get; set; }

        public List<ProductSkuImg> ProductSkuImg { get; set; }
    }
}