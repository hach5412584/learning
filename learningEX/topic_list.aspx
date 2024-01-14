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
