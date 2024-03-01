<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Writetopic.aspx.cs" Inherits="learningEX.Writetopic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>输入物品信息</h2>

            <asp:Literal ID="ResultLiteral" runat="server"></asp:Literal>
            <asp:Panel ID="ItemPanel" runat="server">
                <!-- 在此处动态生成输入框 -->
            </asp:Panel>
            <input type="button" id="btnPrev" runat="server" value="提交"  onserverclick="SubmitButton_Click" />
        </div>
    </form>
</body>
</html>
