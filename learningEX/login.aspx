<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="learningEX.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html" />
    <meta charset="UTF-8" />
    <title>登錄頁面</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>

    <style>
        .bd-placeholder-img {
            font-size: 1.125rem;
            text-anchor: middle;
            -webkit-user-select: none;
            -moz-user-select: none;
            user-select: none;
        }

        @media (min-width: 768px) {
            .bd-placeholder-img-lg {
                font-size: 3.5rem;
            }
        }
    </style>
    <link href="css/signin.css" rel="stylesheet">
</head>
<body class="text-center">
    <main class="form-signin">
        <form id="loginForm" runat="server" class="form-signin">
            <h1 class="h3 mb-3 fw-normal">歡迎登錄</h1>

            <div class="form-floating">
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Placeholder="用戶名" Required="true"></asp:TextBox>
                <label for="floatingInput">使用者名稱</label>
            </div>
            <div class="form-floating mt-3">
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" Placeholder="密碼" Required="true"></asp:TextBox>
                <label for="floatingPassword">密碼</label>
            </div>
            <asp:Button ID="btnLogin" CssClass="btn btn-lg btn-primary btn-block" runat="server" Text="登錄" OnClick="btnLogin_Click" />
            <asp:Label ID="lblMessage" runat="server" CssClass="mt-3" Text="" ForeColor="Red"></asp:Label>
        </form>
    </main>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        /*$(document).ready(function() {
            $("#loginForm").submit(function(e) {
                e.preventDefault();
                var username = $("#txtUsername").val(); // 獲取使用者名稱
                var password = $("#txtPassword").val(); // 獲取密碼

                $.ajax({
                    type: "POST",
                    url: "login.aspx/Login",
                    data: JSON.stringify({ username: username, password: password }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(response) { 
                        if (response.d) {
                            alert("登錄成功");
                        } else {
                            alert("登錄失敗！");
                        }
                        
                    },
                    error: function(error) {
                        alert("發生錯誤：" + error.responseText);
                    }
                });
            });
        });*/
    </script>
</body>
</html>

