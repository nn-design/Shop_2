﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
using BLL;
using IBLL;
using MODEL;
using Shop.Models;

namespace API.Controllers
{
    public class ProductCategoryController : ApiController
    {
        public  IProductCategoryBLL Bll
        {
            get
            {
                return new ProductCategoryBLL();
            }
        }
        public ResponsMessage<List<ProductCategoryVModel>> GetAll()
        {
            //获取全部分类列表
            //var list = bll.GetAll();

            //递归生成json数据
            //1.获取所有一级节点（分类）
            var rootList = Bll.GetSub(0);
            List<ProductCategoryVModel> list = new List<ProductCategoryVModel>();
            foreach (var category in rootList)
            {
                ProductCategoryVModel vModel = new ProductCategoryVModel();
                vModel.ID = category.ID;
                vModel.Name = category.Name;
                vModel.PID = category.PID;
                vModel.OrderNum = category.OrderNum;
                vModel.Img = category.Img;
                vModel.children = new List<ProductCategoryVModel>();//初始化子节点集合
                GetSub(vModel);
                list.Add(vModel);
            }

            //构造返回json对象{"draw":  ,"data": }

            return new ResponsMessage<List<ProductCategoryVModel>>()
            {
                Code = 200,
                Message = "请求成功",
                Data = list
            };
        }
        //获取父节点的所有子节点
        private void GetSub(ProductCategoryVModel parentNode)
        {
            var subList = Bll.GetSub(parentNode.ID);

            if (subList.Count() == 0)//递归终止条件
            {
                return;
            }
            //判断子节点下是否还包含子结点
            foreach (var category in subList)
            {
                ProductCategoryVModel vModel = new ProductCategoryVModel();
                vModel.ID = category.ID;
                vModel.Name = category.Name;
                vModel.PID = category.PID;
                vModel.OrderNum = category.OrderNum;
                vModel.Img = category.Img;
                vModel.children = new List<ProductCategoryVModel>();//初始化子节点集合
                GetSub(vModel);//开始递归
                parentNode.children.Add(vModel);

            }
        }
    }
}
