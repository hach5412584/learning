<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InputItem.aspx.cs" Inherits="learningEX.InputItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>输入重量及項目數量</h2>
            <asp:Label ID="InputLabel1" runat="server" Text="背包承重  "></asp:Label>
            <asp:TextBox ID="InCapacity" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="InputLabel2" runat="server" Text="項目數量  "></asp:Label>
            <asp:TextBox ID="ITEM" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="ConfirmButton" runat="server" Text="確認" OnClick="ConfirmButton_Click" />
        </div>
    </form>
</body>
</html>
