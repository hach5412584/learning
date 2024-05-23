<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InputItem.aspx.cs" Inherits="learningEX.InputItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">
      <div class="container mt-5">
            <h2>資料輸入</h2>
            
            <asp:Panel ID="Div1" runat="server" CssClass="mb-3 hidden">
                <label for="InCapacity" class="form-label">背包承重</label>
                <asp:TextBox ID="InCapacity" runat="server" CssClass="form-control"></asp:TextBox>
                <label for="ITEM1" class="form-label">項目數量</label>
                <asp:TextBox ID="ITEM1" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="lblMessage1" runat="server" CssClass="mt-3" Text="" ForeColor="Red"></asp:Label>
            </asp:Panel>
            
            <asp:Panel ID="Div2" runat="server" CssClass="mb-3 hidden">
                <label for="ITEM2" class="form-label">節點項目</label>
                <asp:TextBox ID="ITEM2" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="lblMessage2" runat="server" CssClass="mt-3" Text="" ForeColor="Red"></asp:Label>
            </asp:Panel>
           <asp:Label ID="lblMessage" runat="server" CssClass="mt-3" Text="" ForeColor="Red"></asp:Label>
          <asp:Button ID="ConfirmButton" runat="server" Text="確認" CssClass="btn btn-primary" OnClick="ConfirmButton_Click" />
        </div>                  
    </form>
</body></html>
