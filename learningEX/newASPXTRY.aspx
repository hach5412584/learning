<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newASPXTRY.aspx.cs" Inherits="learningEX.newASPXTRY" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>表格</title>
 <style>
        /* 添加CSS样式来为表格添加边框 */
        #dynamicTable {
            border-collapse: collapse;
            width: 100%;
        }

        #dynamicTable th, #dynamicTable td {
            border: 1px solid black;
            font-size: 25px;
            padding: 8px;
            text-align: center;
        }

        #dynamicTable th {
            background-color: #f2f2f2;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Table ID="dynamicTable" runat="server"></asp:Table>
        </div>
    </form>
</body>
</html>
