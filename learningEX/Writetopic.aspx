<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Writetopic.aspx.cs" Inherits="learningEX.Writetopic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h2>输入物品信息</h2>

            <asp:Literal ID="ResultLiteral" runat="server"></asp:Literal>
            <asp:Panel ID="ItemPanel" runat="server">
                <!-- 在此处动态生成输入框 -->
            </asp:Panel>
            <input type="button" id="btnPrev" runat="server" class="btn btn-primary" value="提交"  onserverclick="SubmitButton_Click" />
        </div>
    </form>
</body>
</html>
