using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class SearchVModel
    {
    //    keyWord:"",
    //pagenum:1,//当前的页码值
    //pagesize:10//

        public string keyWord { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}