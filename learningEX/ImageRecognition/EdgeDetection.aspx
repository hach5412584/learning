<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EdgeDetection.aspx.cs" Inherits="learningEX.ImageRecognition.EdgeDetection" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>影像處理之邊緣擷取</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
</head>

<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <div class="container mt-5">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">邊緣擷取</h5>
                            <div class="form-group">
                                <label for="FileUpload1">選擇圖片：</label>
                                <asp:FileUpload ID="FileUpload1" runat="server"  />
                            </div>
                            <asp:Button ID="Button1" runat="server" Text="上傳灰階化後進行邊緣偵測" OnClick="Button1_Click" class="btn btn-primary" />
                            <asp:Label ID="Label1" runat="server" Text="請輸入門檻值0~255"/>
                            <asp:TextBox ID="TextBox1"  runat="server" Text="0" Width="100" Height="38"  />
                           
                        </div>
                    </div>
                </div>
            </div>
            <div class="row mt-3 justify-content-center">
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">原始圖片</h5>
                            <asp:Image ID="Image2" runat="server" AlternateText="原始圖片" />
                        </div>
                    </div>
                </div>            
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="card-title">轉換後的圖片</h5>
                            <asp:Image ID="Image1" runat="server"  AlternateText="轉換後的圖片" />
                        </div>
                    </div>
                </div>
            </div>
            <div style="width:110px;height:60px;border:3px;padding-top: 16px;">
            <a href="mainpage.aspx" class="list-group-item list-group-item-action active">回上一頁</a>
            </div>
        </div>
    </form>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
   
</body>
</html>
