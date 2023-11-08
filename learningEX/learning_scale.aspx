<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="learning_scale.aspx.cs" Inherits="learningEX.learning_scale" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" name="viewport" content="width=device-width, initial-scale=1" />
    <!-- 引入 Bootstrap 的 CSS 文件 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>
    <title></title>
</head>
<body>
    <form id="feedbackForm" runat="server" class="container mt-5">
        <div class="mb-3">
            <label class="form-label">學習成效：</label>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="rating" id="rating1" value="1">
                <label class="form-check-label" for="rating1">1</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="rating" id="rating2" value="2">
                <label class="form-check-label" for="rating2">2</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="rating" id="rating3" value="3">
                <label class="form-check-label" for="rating3">3</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="rating" id="rating4" value="4">
                <label class="form-check-label" for="rating4">4</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="rating" id="rating5" value="5">
                <label class="form-check-label" for="rating5">5</label>
            </div>
        </div>

        <div class="mb-3">
            <label class="form-label">難易度：</label>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="difficulty" id="difficulty1" value="1">
                <label class="form-check-label" for="difficulty1">1</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="difficulty" id="difficulty2" value="2">
                <label class="form-check-label" for="difficulty2">2</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="difficulty" id="difficulty3" value="3">
                <label class="form-check-label" for="difficulty3">3</label>
            </div>
        </div>

        <div class="mb-3">
            <label class="form-label">滿意度：</label>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="satisfaction" id="satisfaction1" value="1">
                <label class="form-check-label" for="satisfaction1">1</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="satisfaction" id="satisfaction2" value="2">
                <label class="form-check-label" for="satisfaction2">2</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="satisfaction" id="satisfaction3" value="3">
                <label class="form-check-label" for="satisfaction3">3</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="satisfaction" id="satisfaction4" value="4">
                <label class="form-check-label" for="satisfaction4">4</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="satisfaction" id="satisfaction5" value="5">
                <label class="form-check-label" for="satisfaction5">5</label>
            </div>
        </div>

        <div class="mb-3">
            <label class="form-label">學習意願：</label>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="intention" id="intention1" value="1">
                <label class="form-check-label" for="intention1">1</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="intention" id="intention2" value="2">
                <label class="form-check-label" for="intention2">2</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="intention" id="intention3" value="3">
                <label class="form-check-label" for="intention3">3</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="intention" id="intention4" value="4">
                <label class="form-check-label" for="intention4">4</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="intention" id="intention5" value="5">
                <label class="form-check-label" for="intention5">5</label>
            </div>
        </div>

        <div class="mb-3">
            <label class="form-label">建議：</label>
            <textarea class="form-control" id="suggestion" name="suggestion" rows="4" cols="50"></textarea>
        </div>

        <button type="button" runat="server" id="submitBtn" class="btn btn-primary">提交</button>
    </form>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#submitBtn").click(function (e) {
                e.preventDefault();
                var rating = $("input[name='rating']:checked").val();
                var difficulty = $("input[name='difficulty']:checked").val();
                var satisfaction = $("input[name='satisfaction']:checked").val();
                var intention = $("input[name='intention']:checked").val();
                var suggestion = $("#suggestion").val();
                $.ajax({
                    type: "POST",
                    url: "learning_scale.aspx/SubmitFeedback",
                    data: JSON.stringify({ rating: rating, difficulty: difficulty, satisfaction: satisfaction, intention: intention, suggestion: suggestion }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        alert("反饋已提交！");
                        // 清除表單
                        $("#feedbackForm")[0].reset();
                    },
                    error: function (error) {
                        alert("發生錯誤：" + error.responseText);
                    }
                });
            });
        });
    </script>
</body>
</html>
