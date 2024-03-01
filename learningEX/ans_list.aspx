<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ans_list.aspx.cs" Inherits="learningEX.ans_list" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <!-- 在 GridView 上套用 Bootstrap 的樣式類別 -->
            <asp:GridView ID="gvAnswers" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover">
                <Columns>
                    <asp:BoundField DataField="QuestionID" HeaderText="題號" SortExpression="QuestionID" />
                    <asp:BoundField DataField="Result" HeaderText="是否正確" SortExpression="Result" />
                    <asp:TemplateField HeaderText="詳解">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlDetails" runat="server" Text="詳解" NavigateUrl='<%# $"DetailedExplanation.aspx?questionID={Eval("QuestionID")}" %>' CssClass="btn btn-primary btn-sm"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div class="text-center mt-3">
                <asp:Button ID="btnGoToSurvey" runat="server" CssClass="btn btn-primary" Text="前往學習問卷" OnClientClick="window.location.href='learning_scale.aspx'; return false;" />
            </div>

        </div>
    </form>
</body>
</html>
