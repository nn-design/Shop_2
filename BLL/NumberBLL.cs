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
    public class NumberBLL : BaseBLL<Number, NumberDAL>, INumberBLL
    {
        INumberDAL numberDAL = new NumberDAL();
        public  string CreateNumber(int type)
        {
            //1.根据类型查询当前流水号
            //Type：1-销售订单

            int currentNumber = 1;
            var number = numberDAL.Search(x => x.Type == type).FirstOrDefault();
            if (number != null)
            {
                currentNumber = number.CurrentNumber.Value + 1;
                //将currentNumber更新到数据库
                number.CurrentNumber = currentNumber;
                numberDAL.Update(number);//执行update操作
            }
            else
            {
                //执行添加操作
                Number number1 = new Number();
                number1.Type = type;
                number1.CurrentNumber = currentNumber;
                numberDAL.Add(number1);
            }
            string dh = DateTime.Now.ToString("yyyyMMddHHmmssfffff") + new Random().Next(10000, 99999)+ currentNumber.ToString().PadLeft(5, '0');
            return dh;
        }
    }
}
