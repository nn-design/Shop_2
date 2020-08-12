<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test1.aspx.cs" Inherits="WebFormsDemo1.test1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <a href="test2.aspx?id=1">跳转到test2</a>
    <form id="form1" runat="server">
        <div>
            姓名：<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        </div>
        <div>
            性别：
            <asp:DropDownList ID="drpSex" runat="server">
                <asp:ListItem Text="男" Value="1"></asp:ListItem>
                <asp:ListItem Text="女" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            爱好：
            <asp:CheckBoxList ID="chkLike" runat="server">
                <%--<asp:ListItem Text="足球" Value="1"></asp:ListItem>
                <asp:ListItem Text="蓝球" Value="2"></asp:ListItem>
                <asp:ListItem Text="游泳" Value="3"></asp:ListItem>--%>
            </asp:CheckBoxList>
        </div>
        <div>
            学历：
            <asp:RadioButtonList ID="rdoEducation" runat="server">
                <asp:ListItem Text="小学" Value="1"></asp:ListItem>
                <asp:ListItem Text="初中" Value="2"></asp:ListItem>
                <asp:ListItem Text="高中" Value="3"></asp:ListItem>
                <asp:ListItem Text="专科" Value="4"></asp:ListItem>
                <asp:ListItem Text="本科" Value="5"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div>
            是否党员：<asp:CheckBox ID="chkIsDy" runat="server" />
        </div>
        <asp:Button ID="Button1" runat="server" Text="确认" OnClick="Button1_Click" />
    </form>
</body>
</html>
