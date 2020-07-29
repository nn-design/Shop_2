using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;
using IBLL;
using DAL;
using IDAL;

namespace BLL
{
    public class MemberBLL : BaseBLL<Member, MemberDAL>, IMemberBLL
    {
        IMemberDAL memberDAL = new MemberDAL();
        public override int Add(Member model)
        {
            int result = 0;
            var tran = dal.BeginTran();
            try
            {
                dal.Add(model);
                result += SaveChange();//相当于预提交，  默认情况下，调用SaveChange会开启一个事务

                tran.Commit();//总提交
            }
            catch (Exception)
            {

                tran.Rollback();
            }

            return result;
        }
    }
}
