<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="learning_map.aspx.cs" Inherits="learningEX.learningmap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ul>
                <asp:Label ID="lblUserName" runat="server"></asp:Label>
                <li class="active">介紹</li>
                <li class="active"><a href="personal_information.aspx">個人資料</a></li>
                <li class="active"><a href="learning_map.aspx">學習地圖</a></li>
                <li class="active"><a href="register.aspx">題目</a></li>
                <li class="active"><a href="learning_scale.aspx">自動補強出題</a></li>
                <li class="active">登出</li>
            </ul>
        </div>
    </form>
</body>
</html>
