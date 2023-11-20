<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="learning_map.aspx.cs" Inherits="learningEX.learningmap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" name="viewport" content="width=device-width, initial-scale=1" />
    <title></title>
    <script src="https://cdn.staticfile.org/Chart.js/3.9.1/chart.js"></script>
    <!-- 引入 Bootstrap 的 CSS 文件 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>

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
                    <li class="nav-item">
                        <a class="nav-link" href="topic.aspx">題目</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="learning_scale.aspx">自動補強出題</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="logout.aspx">登出</a>
                    </li>
                </ul>
            </div>
        </nav>
        <canvas id="myChart" width="400" height="400"></canvas>
    </form>
    <script>
        var ctx = document.getElementById('myChart').getContext('2d');
        var myChart = new Chart(ctx, {
            type: 'radar',
            data: {
                labels: ['項目1', '項目2', '項目3', '項目4', '項目5', '項目6'],
                datasets: [{
                    label: '答題正解率',
                    data: [80, 60, 70, 90, 50, 30], // 根據你的數據提供相應的正解率
                    borderColor: 'rgba(75, 192, 192, 1)',
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                }]
            },
            options: {
                responsive: true, // 设置图表为响应式，根据屏幕窗口变化而变化
                //maintainAspectRatio: false,// 保持图表原有比例
                scale: {
                    ticks: {
                        min: 0,
                        max: 100,
                        stepSize: 20
                    }
                }
            }
        });
    </script>
</body>
</html>
