using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
using IBLL;
using MODEL;

namespace API.Controllers
{
    public class BaseApiController<T, B> : ApiController
         where T : BaseModel, new()
         where B : IBaseBLL<T>
    {
        public virtual B Bll { get; set; }

        //查询所有数据
        public virtual ResponsMessage<List<T>>  Get()//泛型方法
        {
            try
            {
                return Success<List<T>>(Bll.GetAll()); //List<T>可去掉，可以根据Bll.GetAll()推算出来D
            }
            catch (Exception)
            {
                return Error<List<T>>("在查询多条数据过程中出现异常");
            }
           
        }
        //根据主键查询单条数据
        public virtual ResponsMessage<T> Get(int id)
        {
            try
            {
                return Success<T>(Bll.GetOne(id));//<T>可去掉，可以根据Bll.GetOne()推算出来D
            }
            catch (Exception)
            {
                return Error<T>("在查询单条数据过程中出现异常");
            }
        }

        public ResponsMessage<D> Success<D>(D data)
        {
            return new ResponsMessage<D>()
            {
                Code = 200,
                Message = "请求成功",
                Data = data
            };
        }
        public ResponsMessage<D> Error<D>(string message)
        {
            return new ResponsMessage<D>()
            {
                Code = 500,
                Message = "请求失败："+message,
               
            };
        }
    }
}

