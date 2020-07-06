using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;
using IDAL;
using System.Data.Entity;

namespace DAL
{
    public class ProductDAL : BaseDAL<Product>, IProductDAL
    {
        DbContextTransaction IBaseDAL<Product>.BeginTran()
        {
            throw new NotImplementedException();
        }
    }
}
