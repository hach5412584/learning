<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="topic.aspx.cs" Inherits="learningEX.topic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>翻頁式題本</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/turn.js/3/turn.js"></script>
</head>
<body>
    <div class="container">
        <div class="page">
            <div class="page-content">
                <h2>題目 1</h2>
                <p>這是題目的描述。</p>
                <button class="btn btn-primary next-page">下一頁</button>
            </div>
        </div>
        <div class="page">
            <div class="page-content">
                <h2>題目 2</h2>
                <p>這是第二題的描述。</p>
                <button class="btn btn-primary prev-page">上一頁</button>
                <button class="btn btn-primary next-page">下一頁</button>
            </div>
        </div>
        <!-- 其他題目頁面依此類推 -->
        <div class="page">
            <div class="page-content">
                <h2>最後一題</h2>
                <p>這是最後一題的描述。</p>
                <button class="btn btn-primary prev-page">上一頁</button>
                <button class="btn btn-primary submit">繳交</button>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            var pages = $('.page');

            // 初始化
            pages.hide().eq(0).show();

            // 上一頁
            $('.prev-page').click(function () {
                var currentPage = $(this).closest('.page');
                currentPage.hide();
                currentPage.prev().show();
            });

            // 下一頁
            $('.next-page').click(function () {
                var currentPage = $(this).closest('.page');
                currentPage.hide();
                currentPage.next().show();
            });

            // 繳交
            $('.submit').click(function () {
                alert('繳交成功，顯示正解並跳轉到學習量表頁面');
                // 在這裡你可以實現顯示正解和跳轉到學習量表頁面的相關邏輯
            });
        });
    </script>

</body>
</html>
