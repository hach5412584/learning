<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="topic_list.aspx.cs" Inherits="learningEX.topic_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <asp:GridView ID="gvQuestions" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
                <Columns>
                    <asp:TemplateField HeaderText="題目名稱">
                        <ItemTemplate>
                            <a href='<%# "topic.aspx?topicname=" + Eval("Topicname") + "&topictype=" + Eval("Topictype") %>'>
                                <asp:Label ID="lblTopicName" runat="server" Text='<%# Eval("Topicname") %>'></asp:Label>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Topictype" HeaderText="題目類型" SortExpression="Topictype" />
                </Columns>
            </asp:GridView>
        </div>
    </form>

</body>
</html>
