<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="topic_list.aspx.cs" Inherits="learningEX.topic_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="gvQuestions" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="QuestionName" HeaderText="題目名稱" SortExpression="QuestionName" />
                    <asp:BoundField DataField="QuestionType" HeaderText="題目類型" SortExpression="QuestionType" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
