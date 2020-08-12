using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsDemo1
{
    public class Like
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public partial class test1 : System.Web.UI.Page
    {
        //页面加载时会执行该方法
        //请求到达服务端后 首先执行页面加载的方法，如果有点击事件再执行相关方法
        protected void Page_Load(object sender, EventArgs e)
        {
            //回发：1 不是第一次请求 2 post请求
            //IsPostBack：当请求时挥发时为true，否则为false
            if (!IsPostBack)//!IsPostBack代表是第一次请求(只要是get请求就都算第一次请求)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            //从数据库中的拿值
            List<Like> likes = new List<Like>() {
                new Like(){
                    ID=1,
                    Name="足球"
                },
                new Like(){
                    ID=2,
                    Name="蓝球"
                },
                new Like(){
                    ID=3,
                    Name="游泳"
                },
            };

            chkLike.Items.Clear();
            foreach (var like in likes)
            {
                ListItem item = new ListItem();
                item.Text = like.Name;
                item.Value = like.ID.ToString();
                chkLike.Items.Add(item);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //获取文本框的值
            string name = txtName.Text;
            //获取下拉框的值
            string sex = drpSex.SelectedValue;
            //获取checkboxList选中的值
            string likes = "";
            foreach (ListItem item in chkLike.Items)
            {
                if (item.Selected)
                {
                    //获取单个值
                    //string val = item.Value;
                    likes += item.Value + ",";
                }
            }
            //获取RadioButtonList选中的值
            string education = rdoEducation.SelectedValue;
            //获取CheckBox是否被选中
            bool isDy= chkIsDy.Checked;

            //后台跳转并传递参数，Redirect要写相对地址
            Response.Redirect("test2.aspx?id=1");

            Response.Write(name);
        }
    }
}