<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="learningEX.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html" />
    <meta charset="UTF-8"/>
    <title>登錄頁面</title>
</head>
<body>
    <h1>歡迎登錄</h1>
    <form id="loginForm">
        <label for="username">用戶名：</label>
        <input type="text" id="username" name="username" required><br><br>

        <label for="password">密碼：</label>
        <input type="password" id="password" name="password" required><br><br>

        <input type="submit" value="登錄">
    </form>
        <a href="register.aspx"><button>註冊</button></a>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function() {
            $("#loginForm").submit(function(e) {
                e.preventDefault();
                var username = $("#username").val();
                var password = $("#password").val();

                $.ajax({
                    type: "POST",
                    url: "loginAjax.aspx/Login",
                    data: JSON.stringify({ username: username, password: password }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(response) {
                        if (response.d) {
                            window.location.href = "mainpage.aspx";
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
        });
    </script>
</body>
</html>

