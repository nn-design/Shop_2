﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;
using IDAL;

namespace DAL
{
    public class ProductSkuDAL : BaseDAL<ProductSku>, IProductSkuDAL
    {
        DbContextTransaction IBaseDAL<ProductSku>.BeginTran()
        {
            throw new NotImplementedException();
        }
    }
}
