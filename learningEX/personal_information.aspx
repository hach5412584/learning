<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="personal_information.aspx.cs" Inherits="learningEX.personal_information" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <div>
        <label for="fullName">更改個人資料</label>
        <form id="profileForm">
            <label for="fullName">姓名：</label>
            <input type="text" id="fullName" name="fullName" required><br>
            <br>
            <label for="email">電子信箱：</label>
            <input type="text" id="email" name="email" required><br>
            <br>
            <label for="fullName">使用者名稱：</label>
            <input type="text" id="username" name="username" required><br>
            <br>
            <label for="fullName">學號：</label>
            <input type="text" id="studentID" name="studentID" required><br>
            <br>
            <!-- 其他基本資料項目... -->

            <input type="submit" value="儲存">
        </form>
        <form id="passwordForm">
            <label for="oldPassword">舊密碼：</label>
            <input type="password" id="oldPassword" name="oldPassword" required><br>
            <br>

            <label for="newPassword">新密碼：</label>
            <input type="password" id="newPassword" name="newPassword" required><br>
            <br>

            <label for="confirmPassword">確認新密碼：</label>
            <input type="password" id="confirmPassword" name="confirmPassword" required><br>
            <br>

            <input type="submit" value="更改密碼">
        </form>
        <table id="answerHistory">
            <!-- 表頭 -->
            <tr>
                <th>日期</th>
                <th>題目</th>
                <th>答案</th>
            </tr>

            <!-- 填入資料 -->
            <tr>
                <td>2023-10-30</td>
                <td><a href="question1.html">題目1</a></td>
                <td><a href="answerA.html">答案A</a></td>
            </tr>

            <!-- 其他作答紀錄... -->
        </table>
    </div>
</body>
</html>
