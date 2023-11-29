<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="personal_information.aspx.cs" Inherits="learningEX.personal_information" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>個人資料</title>

    <!-- 引入 Bootstrap CSS -->
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="container mt-5"> <!-- 使用 Bootstrap container 並添加 margin-top -->

    <div>
        <h2 class="mb-4 text-center">更改個人資料</h2> <!-- 使用 Bootstrap 的標題樣式和 text-center 使其置中 -->

        <!-- 第一個表單 -->
        <form id="profileForm" class="mb-4">
            <div class="form-group">
                <label for="fullName">姓名：</label>
                <input type="text" id="fullName" name="fullName" class="form-control" required>
            </div>
            <div class="form-group">
                <label for="email">電子信箱：</label>
                <input type="text" id="email" name="email" class="form-control" required>
            </div>
            <div class="form-group">
                <label for="username">使用者名稱：</label>
                <input type="text" id="username" name="username" class="form-control" required>
            </div>
            <div class="form-group">
                <label for="studentID">學號：</label>
                <input type="text" id="studentID" name="studentID" class="form-control" required>
            </div>
            <!-- 其他基本資料項目... -->
            <button type="submit" class="btn btn-primary">儲存</button>
        </form>

        <!-- 第二個表單 -->
        <form id="passwordForm" class="mb-4">
            <div class="form-group">
                <label for="oldPassword">舊密碼：</label>
                <input type="password" id="oldPassword" name="oldPassword" class="form-control" required>
            </div>
            <div class="form-group">
                <label for="newPassword">新密碼：</label>
                <input type="password" id="newPassword" name="newPassword" class="form-control" required>
            </div>
            <div class="form-group">
                <label for="confirmPassword">確認新密碼：</label>
                <input type="password" id="confirmPassword" name="confirmPassword" class="form-control" required>
            </div>
            <!-- 其他密碼相關項目... -->
            <button type="submit" class="btn btn-primary">更改密碼</button>
        </form>

        <!-- 答案歷史表格 -->
        <table id="answerHistory" class="table table-bordered">
            <!-- 表頭 -->
            <thead class="thead-dark">
                <tr>
                    <th>日期</th>
                    <th>題目</th>
                    <th>答案</th>
                </tr>
            </thead>
            <!-- 填入資料 -->
            <tbody>
                <tr>
                    <td>2023-10-30</td>
                    <td><a href="question1.html">題目1</a></td>
                    <td><a href="answerA.html">答案A</a></td>
                </tr>
                <!-- 其他作答紀錄... -->
            </tbody>
        </table>
    </div>

    <!-- 引入 Bootstrap JS，注意要在 body 結尾處引入 -->
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
</body>
</html>
