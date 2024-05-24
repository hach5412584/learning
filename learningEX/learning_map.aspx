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
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
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
                        <a class="nav-link" href="learning_scale2.aspx">學習動機量表</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" href="logout.aspx">登出</a>
                    </li>
                </ul>
            </div>
        </nav>
        <div class="container mt-5">
            <div class="row">
                <!-- 左側部分 -->
                <div class="col-md-6">
                    <canvas id="myChart" width="1600" height="800"></canvas>
                </div>
                <!-- 右側部分 -->
                <div class="col-md-6">
                    <!-- 上半部分 -->
                    <div class="row">
                        <div class="col-md-12" id="barChartContainer" style="display: none;">
                            <canvas id="myChartTopRight" width="400" height="200"></canvas>
                        </div>
                    </div>
                    <!-- 下半部分 -->
                    <div class="row">
                        <div class="col-md-12" id="BottombarChartContainer" style="display: none;">
                            <canvas id="myChartBottomRight" width="400" height="200"></canvas>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row mt-3">
                <div class="col-md-12" id="BottomContainer" style="display: none;">
                    <canvas id="myChartBottom" width="1600" height="800"></canvas>
                </div>
            </div>
        </div>
    </form>
    <script>
        //first
        var ctx = document.getElementById('myChart').getContext('2d');
        var myChart = new Chart(ctx, {
            type: 'radar',
            data: {
                labels: ['演算法', '影像視覺', '項目3', '項目4', '項目5', '項目6'],
                datasets: [{
                    label: '平均正確率',
                    data: [0, 0, 0, 0, 0, 0], // 根據你的數據提供相應的正解率
                    
                    borderColor: 'rgba(75, 192, 192, 1)',
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    r: {
                        ticks: {
                            min: 0,
                            max: 100,
                            stepSize: 20,
                            font: {
                                size: 18 // 设置刻度字体大小
                            }
                        },
                        pointLabels: {
                            font: {
                                size: 24 // 设置标签字体大小
                            }
                        }
                    }
                },
                plugins: {
                    legend: {
                        labels: {
                            font: {
                                size: 24 // 设置图例字体大小
                            }
                        }
                    }
                },
                onClick: function (event, elements) {
                    var activePoints = myChart.getElementsAtEventForMode(event, 'nearest', { intersect: false }, false);
                    console.log('Click event:', event);
                    console.log('Elements:', activePoints);
                    if (activePoints.length) {
                        var firstPoint = activePoints[0];
                        var label = myChart.data.labels[firstPoint.index];
                        console.log('Clicked label:', label);
                        document.getElementById('barChartContainer').style.display = 'block';
                        getAccuracyChartTopRight(firstPoint.index);
                    }
                }

            }
        });
        //sec
        var barCtx = document.getElementById('myChartTopRight').getContext('2d');
        var myChartTopRight = new Chart(barCtx, {
            type: 'bar',
            data: {
                labels: [], // 可根據需要動態更新
                datasets: [
                    {
                        label: '平均正確率',
                        data: [], // 初始數據
                        backgroundColor: 'rgba(75, 192, 192, 0.2)',
                        borderColor: 'rgba(75, 192, 192, 1)',
                        borderWidth: 1
                    },
                    {
                        label: '作答次數',
                        data: [],
                        backgroundColor: 'rgba(153, 102, 255, 0.2)',
                        borderColor: 'rgba(153, 102, 255, 1)',
                        borderWidth: 1
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    y: {
                        beginAtZero: true,
                        max: 100
                    }
                },
                plugins: {
                    legend: {
                        labels: {
                            font: {
                                size: 24
                            }
                        }
                    }
                },
                onClick: function (event, elements) {
                    var activePoints = myChartTopRight.getElementsAtEventForMode(event, 'nearest', { intersect: false }, false);
                    console.log('Click event:', event);
                    console.log('Elements:', activePoints);
                    if (activePoints.length) {
                        var firstPoint = activePoints[0];
                        var label = myChartTopRight.data.labels[firstPoint.index];
                        document.getElementById('BottombarChartContainer').style.display = 'block';
                        getAccuracyChartBottomRight(label);
                    }
                }
            }
        });
        //thr
        var BottomRightbarCtx = document.getElementById('myChartBottomRight').getContext('2d');
        var myChartBottomRight = new Chart(BottomRightbarCtx, {
            type: 'bar',
            data: {
                labels: [], // 可根據需要動態更新
                datasets: [
                    {
                        label: '平均正確率',
                        data: [], // 初始數據
                        backgroundColor: 'rgba(75, 192, 192, 0.2)',
                        borderColor: 'rgba(75, 192, 192, 1)',
                        borderWidth: 1
                    },
                    {
                        label: '作答次數',
                        data: [],
                        backgroundColor: 'rgba(153, 102, 255, 0.2)',
                        borderColor: 'rgba(153, 102, 255, 1)',
                        borderWidth: 1
                    }
                ]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    y: {
                        beginAtZero: true,
                        max: 100
                    }
                },
                plugins: {
                    legend: {
                        labels: {
                            font: {
                                size: 24
                            }
                        }
                    }
                },
                onClick: function (event, elements) {
                    var activePoints = myChartBottomRight.getElementsAtEventForMode(event, 'nearest', { intersect: false }, false);
                    console.log('Click event:', event);
                    console.log('Elements:', activePoints);
                    if (activePoints.length) {
                        var firstPoint = activePoints[0];
                        var label = myChartBottomRight.data.labels[firstPoint.index];
                        document.getElementById('BottomContainer').style.display = 'block';
                        getAccuracyChartBottom(label);
                    }
                }
            }
        });
        //for
        var BottombarCtx = document.getElementById('myChartBottom').getContext('2d');
        var myChartBottom = new Chart(BottombarCtx, {
            type: 'bar',
            data: {
                labels: [], // 可根據需要動態更新
                datasets: [{
                    label: '近5次歷史正確率',
                    data: [], // 初始數據
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    y: {
                        beginAtZero: true,
                        max: 100
                    }
                },
                plugins: {
                    legend: {
                        labels: {
                            font: {
                                size: 24
                            }
                        }
                    }
                }
            }
        });
        //first
        function getAccuracyDataAndUpdateChart() {
            $.ajax({
                type: "POST",
                url: "learning_map.aspx/GetAccuracyData",
                data: '{}', // 空的 JSON 請求體
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var accuracyArray = response.d;
                    updateChartWithAccuracy(accuracyArray);
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error('Error:', errorThrown);
                }
            });
        }
        //sec
        function getAccuracyChartTopRight(index) {
            $.ajax({
                type: "POST",
                url: "learning_map.aspx/GetAccuracyChartTopRight",
                data: JSON.stringify({ index: index }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var accuracyData = response.d;
                    console.log(accuracyData);
                    updateBarChart(accuracyData);
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error('Error:', errorThrown);
                }
            });
        }
        //thr
        function getAccuracyChartBottomRight(index) {
            $.ajax({
                type: "POST",
                url: "learning_map.aspx/GetAccuracyChartBottomRight",
                data: JSON.stringify({ index: index }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var accuracyData = response.d;
                    console.log(accuracyData);
                    updateBottomRight(accuracyData);
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error('Error:', errorThrown);
                }
            });
        }
        //for
        function getAccuracyChartBottom(index) {
            $.ajax({
                type: "POST",
                url: "learning_map.aspx/GetAccuracyChartBottom",
                data: JSON.stringify({ index: index }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var accuracyData = response.d;
                    console.log(accuracyData);
                    updateBottom(accuracyData);
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error('Error:', errorThrown);
                }
            });
        }

        function updateChartWithAccuracy(accuracyData) {
            myChart.data.datasets[0].data = accuracyData;
            myChart.update();
        }

        function updateBarChart(accuracyData) {
            var labels = accuracyData.map(item => item.TopicCategory);
            var avgAccuracyData = accuracyData.map(item => item.AvgAccuracy);
            var answerCountData = accuracyData.map(item => item.AnswerCount);

            myChartTopRight.data.labels = labels;
            myChartTopRight.data.datasets[0].data = avgAccuracyData;
            myChartTopRight.data.datasets[1].data = answerCountData;
            myChartTopRight.update();
        }

        function updateBottomRight(accuracyData) {
            var labels = accuracyData.map(item => item.TopicSubcategory);
            var avgAccuracyData = accuracyData.map(item => item.AvgAccuracy);
            var answerCountData = accuracyData.map(item => item.AnswerCount);

            myChartBottomRight.data.labels = labels;
            myChartBottomRight.data.datasets[0].data = avgAccuracyData;
            myChartBottomRight.data.datasets[1].data = answerCountData;
            myChartBottomRight.update();
        }

        function updateBottom(accuracyData) {
            var labels = accuracyData.map(item => item.TopicSubcategory);
            var data = accuracyData.map(item => item.Accuracy);

            myChartBottom.data.labels = labels;
            myChartBottom.data.datasets[0].data = data;
            myChartBottom.update();
        }

        // 在頁面加載時執行
        $(document).ready(function() {
            getAccuracyDataAndUpdateChart();
        });

        $(document).ready(function() {
            $('#algorithmType').on('click', function(e) {
                e.preventDefault();
                getWeakestTopicsQuestion('Algorithm');
            });

            $('#imageRecognitionType').on('click', function(e) {
                e.preventDefault();
                getWeakestTopicsQuestion('ImageRecognition');
            });
        });

        function getWeakestTopicsQuestion(questionType) {
            $.ajax({
                type: "POST",
                url: "learning_map.aspx/GetWeakestTopicsQuestion",
                data: JSON.stringify({ questionType: questionType }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(data) {
                    if (data) {
                         var topicData = JSON.parse(data.d);
                        // 跳转到出题页面，假设出题页面的 URL 模板为 "question_page.aspx?topic={topicName}"
                        window.location.href = "topic.aspx?topicname=" + encodeURIComponent(topicData.TopicName) + "&topictype=" + encodeURIComponent(questionType) + "&topiccategory=" + encodeURIComponent(topicData.TopicCategory) + "&topicsubcategory=" + encodeURIComponent(topicData.TopicSubcategory);
                    } else {
                        alert('无法获取最弱的题目类型，请稍后再试。');
                    }
                },
                error: function(xhr, textStatus, errorThrown) {
                    console.error('Error:', errorThrown);
                }
            });
        }
    </script>
</body>
</html>
