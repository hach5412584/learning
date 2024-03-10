<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="topic.aspx.cs" Inherits="learningEX.topic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Branch-and-Bound</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            display: flex;
            align-items: center;
            justify-content: center;
            height: 100vh;
            background-color: #f4f4f4;
        }

        .container {
            width: 90vw; /* 設定寬度為螢幕寬度的90% */
            height: 90vh; /* 設定高度為螢幕高度的90% */
            overflow: hidden; /* 隱藏超出容器的部分 */
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
            background-color: #fff;
            display: flex;
            flex-direction: row;
            justify-content: center; /* 水平置中 */
            align-items: center; /* 垂直置中 */
        }

        .Image-container {
            flex: 1; /* 佔據 1/2 的空間 */
            flex-basis: 60%; /* 設定初始寬度為60% */
            padding: 20px;
            display: flex; /* 將容器設置為 flexbox */
            justify-content: center; /* 水平居中 */
            align-items: center; /* 垂直居中 */
            height: 100%; /* 設置高度為父容器的100% */
        }

            .Image-container img {
                max-width: 100%; /* 圖片最大寬度為Image-container的寬度 */
                max-height: 100%; /* 圖片最大高度為Image-container的高度 */
            }

        .datatextbutton-container {
            flex: 1; /* 佔據父容器的所有空間 */
            flex-basis: 40%; /* 設定初始寬度為 40% */
            padding: 20px;
            height: 100%; /* 設置高度為父容器的100% */
            text-align: center;
            overflow: hidden; /* 超出容器的部分隱藏 */
            display: flex; /* 將子元素放置在一個 flex 容器中 */
            flex-direction: column; /* 子元素垂直排列 */
        }

        .data-container {
            flex: 8; /* 填滿剩餘空間 */
            word-wrap: break-word;
            padding: 20px;
            text-align: left;
        }

        .input-container {
            flex: 1; /* 填滿剩餘空間 */
            padding: 20px;
        }

        .button-container {
            flex: 1; /* 填滿剩餘空間 */
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
            overflow: hidden; /* 超出容器的部分隱藏 */
        }

        .detailedexplanation {
            font-size: 25px;
            color: red;
            font-weight: bold;
            word-break: break-all; /* 在單詞內部進行換行 */
            overflow: hidden;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="Image-container">
                <asp:Image ID="imgTopic" runat="server" />
            </div>

            <div class="datatextbutton-container">
                <div class="data-container">
                    <div id="questiondata" runat="server" class="data"></div>
                    <div id="detailedexplanationtext" runat="server" class="detailedexplanation"></div>
                </div>

                <div class="input-container">
                    <asp:TextBox runat="server" ID="inputAns" CssClass="text-input" placeholder="輸入答案：" />
                </div>
                <div class="button-container">
                    <input type="button" id="btnPrev" runat="server" value="上一頁" onserverclick="btnPrev_Click" />
                    <div id="pageload" runat="server"></div>
                    <input type="button" id="btnNext" runat="server" value="下一頁" onserverclick="btnNext_Click" /><br>
                    <asp:Label ID="lblOverallAccuracy" runat="server" Text=""></asp:Label>
                    <input type="button" id="btnChangeTopic" runat="server" value="修改題目" onserverclick="ChangeTopic_Click" />
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
