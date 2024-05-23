<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WriteTopicShortPath.aspx.cs" Inherits="learningEX.WriteTopicShortPath" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>
    <title>Graph Shortest Path</title>
</head>
<body>
    <form id="form1" runat="server">
       <div class="container mt-5">
    <h2>輸入頂點邊和距離</h2>
    <asp:PlaceHolder ID="VertexInputPlaceHolder" runat="server"></asp:PlaceHolder>
    <asp:Button ID="AddVertexButton" runat="server" CssClass="btn btn-primary mt-3" Text="生成資料" OnClick="AddVertexButton_Click" />
    <br /><br />
    <asp:Label ID="AddVertexResultLabel" runat="server" CssClass="mt-3" Text=""></asp:Label>
    <br /><br />
    <asp:Label ID="ShortestPathResultLabel" runat="server" CssClass="mt-3" Text=""></asp:Label>
    <asp:Button ID="ShowLogButton" runat="server" CssClass="btn btn-primary mt-3" Text="確認題組" OnClick="ShowLogButton_Click" />
    <asp:Label ID="LogLabel" runat="server" CssClass="mt-3" Text=""></asp:Label>
</div>
    </form>
</body>
</html>
