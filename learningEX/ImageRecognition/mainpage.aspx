<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mainpage.aspx.cs" Inherits="learningEX.ImageRecognition.mainpage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>影像處理首頁</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <style>
        .list-group-item:hover {
            background-color: #7700FF;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
            <h1 class="text-center mb-4">影像處理之範例</h1>
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="list-group">
                        <a href="Grayscale.aspx" class="list-group-item list-group-item-action active">灰階化</a>
                        <a href="BandW.aspx" class="list-group-item list-group-item-action active">負片轉換</a>
                        <a href="filter.aspx" class="list-group-item list-group-item-action active">平滑濾波與中值濾波</a>
                        <a href="EdgeDetection.aspx" class="list-group-item list-group-item-action active">邊緣偵測</a>
                        <!-- 子項目追加位置 -->
                    </div>
                </div>
            </div>
        </div>
       
        <div class="container mt-5">
            <div class="row justify-content-start">
                <div class="col-md-6">
                    <asp:Button ID="Button2" runat="server" Text="回到學習地圖" OnClick="Button1_Click" class="btn btn-primary" />
                </div>
            </div>
        </div>
    </form>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
