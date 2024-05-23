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
        <nav class="navbar navbar-expand-lg navbar navbar-dark bg-dark">
            <a class="navbar-brand" href="#">學習地圖</a>
            <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>
            <div class="collapse navbar-collapse" id="navbarNav">
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item d-flex">
                        <asp:Label class="nav-link" ID="lblUserName" runat="server"></asp:Label>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="#">介紹</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="personal_information.aspx">個人資料</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="learning_map.aspx">學習地圖</a>
                    </li>
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" id="topicDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">題目</a>
                            <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                                <li><a class="dropdown-item" href="topic_list.aspx?questionType=Algorithm">演算法</a></li>
                                <li><a class="dropdown-item" href="topic_list.aspx?questionType=ImageRecognition">影像辨識</a></li>
                                <li><a class="dropdown-item" href="ImageRecognition/mainpage.aspx">線上影像處理</a></li>
                            </ul>
                    </li>
                    <li class="nav-item dropdown">
                        <a class="nav-link dropdown-toggle" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">自動出題</a>
                            <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
                                <li><a class="dropdown-item" href="#" id="algorithmType">演算法類型</a></li>
                                <li><a class="dropdown-item" href="#" id="imageRecognitionType">影像辨識類型</a></li>
                            </ul>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="logout.aspx">登出</a>
                    </li>
                </ul>
            </div>
        </nav>
        <div class="container mt-5">
            <asp:GridView ID="gvQuestions" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
                <Columns>
                    <asp:TemplateField HeaderText="題目名稱">
                        <ItemTemplate>
                            <a href='<%# "topic.aspx?topicname=" + Eval("Topicname") + "&topictype=" + Eval("Topictype") + "&topiccategory=" + Eval("TopicCategory") + "&topicsubcategory=" + Eval("TopicSubcategory") %>'>
                                <asp:Label ID="lblTopicName" runat="server" Text='<%# Eval("Topicname") %>'></asp:Label>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Topictype" HeaderText="題目類型" SortExpression="Topictype" />
                    <asp:BoundField DataField="TopicCategory" HeaderText="題目類別" SortExpression="TopicCategory" />
                    <asp:BoundField DataField="TopicSubcategory" HeaderText="題目子類別" SortExpression="TopicSubcategory" />
                </Columns>
            </asp:GridView>
        </div>
    </form>

</body>
</html>
