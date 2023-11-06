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
    <form id="loginForm" runat="server">
        <label for="username">用戶名：</label>
        <asp:TextBox ID="txtUsername" runat="server" Required="true"></asp:TextBox><br><br>

        <label for="password">密碼：</label>
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Required="true"></asp:TextBox><br><br>

        <asp:Button ID="btnLogin" runat="server" Text="登錄" OnClick="btnLogin_Click" />
        <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
    </form>

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

