<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="learningEX.register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>註冊帳號</title>
</head>
<body>
    <h1>註冊帳號</h1>
        <form id="registerForm">
            <label for="username">用戶名：</label>
            <input type="text" id="username" name="username" required><br><br>

            <label for="password">密碼：</label>
            <input type="password" id="password" name="password" required><br><br>

            <label for="email">電子信箱：</label>
            <input type="email" id="email" name="password" required><br><br>

            <label for="name">姓名：</label>
            <input type="text" id="name" name="password" required><br><br>

            <label for="studentID">學號：</label>
            <input type="text" id="studentID" name="password" required><br><br>

            <input type="submit" value="註冊">
        </form>

        <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
        <script>
            $(document).ready(function() {
                $("#registerForm").submit(function(e) {
                    e.preventDefault();
                    var username = $("#username").val();
                    var password = $("#password").val();
                    var email = $("#email").val();
                    var name = $("#name").val();
                    var studentID = $("#studentID").val();

                    $.ajax({
                        type: "POST",
                        url: "register.aspx/Register",  // 這裡需要指向你的註冊方法
                        data: JSON.stringify({ username: username, password: password, email: email, name: name, studentID: studentID }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function(response) {
                            if (response.d == "註冊成功") {
                                // 註冊成功，重定向到登入介面
                                window.location.href = "login.aspx"; // 將 login.aspx 替換為你的登入頁面路徑
                                alert("註冊成功");
                            } else {
                                alert(response.d); // 顯示其他訊息
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
