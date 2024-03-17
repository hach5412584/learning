<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetailedExplanation.aspx.cs" Inherits="learningEX.DetailedExplanation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            text-align: left;
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
                    <p></p>
                    <div id="detailedexplanationtext" runat="server" class="detailedexplanation"></div>
                </div>

                <div class="button-container">
                    <input type="button" id="btnNext" runat="server" value="返回" onserverclick="btn_Click" />
                </div>          
            </div>
        </div>
    </form>
</body>
</html>
