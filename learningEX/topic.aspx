<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="topic.aspx.cs" Inherits="learningEX.topic" %>

<%@ Import Namespace="System.Web.Script.Serialization" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>
    <title>Branch-and-Bound</title>
    <style>
        .body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            display: flex;
            align-items: center;
            justify-content: center;
            height: 100vh;
            width: 100vw;
            background-color: #f4f4f4;
        }

        .container {
            width: 100vw;
            height: 100vh;
            overflow: hidden;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
            /*background-color: #fff;*/
            display: flex;
            flex-direction: row;
            max-width: 100%; /* 最大寬度不超過螢幕寬度 */
            max-height: 100%;
            /* 水平和垂直置中 */
            justify-content: center;
            align-items: center;
        }

        .Image-container {
            flex: 1; /* 佔據 1/2 的空間 */
            flex-basis: 60%; /* 設定初始寬度為60% */
            padding: 20px;
            display: flex; /* 將容器設置為 flexbox */
            justify-content: center; /* 水平居中 */
            align-items: center; /* 垂直居中 */
            height: 100%; /* 設置高度為父容器的100% */
            overflow: auto; /*滾動條*/
        }

            .Image-container img {
                max-width: 100%; /* 圖片最大寬度為Image-container的寬度 */
                max-height: 100%; /* 圖片最大高度為Image-container的高度 */
            }

        .datatextbutton-container {
            flex: 1; /* 佔據父容器的所有空間 */
            flex-basis: 40%; /* 設定初始寬度為60% */
            font-size: 15px;
            height: 100%; /* 設置高度為父容器的100% */
            width: 100%;
            text-align: center;
            overflow: hidden; /* 超出容器的部分隱藏 */
            display: flex; /* 將子元素放置在一個 flex 容器中 */
            flex-direction: column; /* 子元素垂直排列 */
        }

        .data-container {
            flex: 6; /* 填滿剩餘空間 */
            word-wrap: break-word;
            padding: 20px;
            align-items: center; /* 垂直置中 */
        }

        .input-container {
            flex: 2; /* 填滿剩餘空間 */
            padding: 20px;
        }

        .button-container {
            flex: 2; /* 填滿剩餘空間 */
            display: flex;
            flex-direction: row; /* 橫向排列 */
            align-items: center;
            justify-content: center;
            padding: 20px;
        }

            .button-container input[type="button"] {
                flex-basis: 10%;
                margin: 0 20px; /* 設置左右間距為 10px，上下間距為 0 */
                padding: 10px;
                background-color: #4CAF50;
                color: #fff;
                border: none;
                border-radius: 4px;
                cursor: pointer;
            }

        .thumbnail {
            max-width: 100%;
            max-height: 100%;
        }

        .text-input {
            width: 100%;
            padding: 10px;
            box-sizing: border-box;
        }

        .button-input {
            width: 100%;
            padding: 10px;
            box-sizing: border-box;
            background-color: #4caf50;
            color: white;
            border: none;
            cursor: pointer;
        }

        .detailedexplanation-container {
            flex: 1;
            flex-basis: 10%;
            padding: 20px;
            text-align: left;
        }

        .data {
            font-size: 25px;
            color: #333;
            font-weight: bold;
            word-break: break-all; /* 在單詞內部進行換行 */
            text-align: left;
            justify-content: center; /* 水平置中 */
            align-items: center; /* 垂直置中 */
        }

        .detailedexplanation {
            font-size: 25px;
            color: red;
            font-weight: bold;
            word-break: break-all; /* 在單詞內部進行換行 */
            overflow: hidden;
        }


        /* 添加CSS样式来为表格添加边框 */
        #dynamicTable {
            border-collapse: collapse;
            width: 80%;
        }

            #dynamicTable th, #dynamicTable td {
                border: 1px solid black;
                padding: 8px;
                text-align: center;
                font-size: 18px;
            }

            #dynamicTable th {
                background-color: #f2f2f2;
            }
    </style>
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
        <div class="container">
            <div class="Image-container">
               <asp:Image ID="imgTopic" runat="server" />

                <asp:Table ID="dynamicTable" runat="server"></asp:Table>
            </div>

            <div class="datatextbutton-container">
                <div class="data-container">
                    <div id="questiondata" runat="server" class="data"></div>
                    <p></p>
                    <div id="userinputscope" runat="server" class="data"></div>                   
                </div>

                <div class="inputandcheckbutton-container">
                    <div class="input-container">
                        <asp:TextBox runat="server" ID="inputAns" CssClass="text-input" placeholder="輸入答案：" />
                    </div>

                </div>
                <div class="button-container">
                    <input type="button" id="btnPrev" runat="server" value="上一頁" onserverclick="btnPrev_Click" />
                    <div id="pageload" runat="server"></div>
                    <input type="button" id="btnNext" runat="server" value="下一頁" onserverclick="btnNext_Click" /><br>
                    <asp:Label ID="lblOverallAccuracy" runat="server" Text=""></asp:Label>
                    <input type="button" id="btnChangeTopic" runat="server" value="修改題目" onserverclick="ChangeTopic_Click" />
                    
                </div>
            </div>
        </div>
    </form>
</body>
</html>
