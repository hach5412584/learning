<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="learning_scale.aspx.cs" Inherits="learningEX.learning_scale" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="feedbackForm" runat="server">
        <label>學習成效：</label>
        <input type="radio" name="rating" value="1" />1
        <input type="radio" name="rating" value="2" />2
        <input type="radio" name="rating" value="3" />3
        <input type="radio" name="rating" value="4" />4
        <input type="radio" name="rating" value="5" />5
        <br />
        <br />

        <label>難易度：</label>
        <input type="radio" name="difficulty" value="1" />1
        <input type="radio" name="difficulty" value="2" />2
        <input type="radio" name="difficulty" value="3" />3
        <br />
        <br />

        <label>滿意度：</label>
        <input type="radio" name="satisfaction" value="1" />1
        <input type="radio" name="satisfaction" value="2" />2
        <input type="radio" name="satisfaction" value="3" />3
        <input type="radio" name="satisfaction" value="4" />4
        <input type="radio" name="satisfaction" value="5" />5
        <br />
        <br />

        <label>學習意願：</label>
        <input type="radio" name="intention" value="1" />1
        <input type="radio" name="intention" value="2" />2
        <input type="radio" name="intention" value="3" />3
        <input type="radio" name="intention" value="4" />4
        <input type="radio" name="intention" value="5" />5
        <br />
        <br />

        <label>建議：</label>
        <textarea name="suggestion" id="suggestion" rows="4" cols="50"></textarea>
        <br />
        <br />

        <input type="button" runat="server" id="submitBtn" value="提交" />
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
