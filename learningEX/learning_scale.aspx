<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="learning_scale.aspx.cs" Inherits="learningEX.learning_scale" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta http-equiv="Content-Type" name="viewport" content="width=device-width, initial-scale=1" />
    <!-- 引入 Bootstrap 的 CSS 文件 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>
    <title></title>
    <style>
        .question-container {
            margin-bottom: 2rem;
        }
        .question-divider {
            border-top: 4px solid #343a40; /* 加粗边框并使用更深的颜色 */
            margin: 2rem 0;
        }
        .custom-text {
            color: red;
            font-weight: bold;
            font-size: 24px;
        }
    </style>
</head>
<body>
    <form id="feedbackForm" runat="server" class="container mt-5">
        <div class="container">
        <div class="question-container">
            <label class="form-label">在演算法這門課程，我喜歡有挑戰性的教材，這樣我就可以學習新的內容。</label>
            <div class="row mb-2">
                <div class="col-2"></div>
                <div class="col text-center">1</div>
                <div class="col text-center">2</div>
                <div class="col text-center">3</div>
                <div class="col text-center">4</div>
                <div class="col text-center">5</div>
                <div class="col-2"></div>
            </div>
            <div class="row">
                <div class="col-2 text-end">強烈不同意</div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="new_content" id="new_content1" value="1">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="new_content" id="new_content2" value="2">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="new_content" id="new_content3" value="3">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="new_content" id="new_content4" value="4">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="new_content" id="new_content5" value="5">
                </div>
                <div class="col-2 text-start">強烈同意</div>
            </div>
        </div>
        <hr class="question-divider">

        <div class="question-container">
            <label class="form-label">在演算法這門課程，我喜歡能激發我好奇心的教材，即使內容很困難。</label>
            <div class="row mb-2">
                <div class="col-2"></div>
                <div class="col text-center">1</div>
                <div class="col text-center">2</div>
                <div class="col text-center">3</div>
                <div class="col text-center">4</div>
                <div class="col text-center">5</div>
                <div class="col-2"></div>
            </div>
            <div class="row">
                <div class="col-2 text-end">強烈不同意</div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="curiosity" id="curiosity1" value="1">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="curiosity" id="curiosity2" value="2">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="curiosity" id="curiosity3" value="3">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="curiosity" id="curiosity4" value="4">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="curiosity" id="curiosity5" value="5">
                </div>
                <div class="col-2 text-start">強烈同意</div>
            </div>
        </div>
        <hr class="question-divider">

        <div class="question-container">
            <label class="form-label">對我來說，演算法這門課程所帶給我最大的滿足感是來自於我嘗試著徹底地理解學習的內容。</label>
            <div class="row mb-2">
                <div class="col-2"></div>
                <div class="col text-center">1</div>
                <div class="col text-center">2</div>
                <div class="col text-center">3</div>
                <div class="col text-center">4</div>
                <div class="col text-center">5</div>
                <div class="col-2"></div>
            </div>
            <div class="row">
                <div class="col-2 text-end">強烈不同意</div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="try_to_understand" id="try_to_understand1" value="1">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="try_to_understand" id="try_to_understand2" value="2">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="try_to_understand" id="try_to_understand3" value="3">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="try_to_understand" id="try_to_understand4" value="4">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="try_to_understand" id="try_to_understand5" value="5">
                </div>
                <div class="col-2 text-start">強烈同意</div>
            </div>
        </div>
        <hr class="question-divider">

        <div class="question-container">
            <label class="form-label">如果有機會的話，在演算法這門課程，我會想要選擇做能讓我學到東西的作業，即使這作業並不保證我在成績上獲得加分。</label>
            <div class="row mb-2">
                <div class="col-2"></div>
                <div class="col text-center">1</div>
                <div class="col text-center">2</div>
                <div class="col text-center">3</div>
                <div class="col text-center">4</div>
                <div class="col text-center">5</div>
                <div class="col-2"></div>
            </div>
            <div class="row">
                <div class="col-2 text-end">強烈不同意</div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="learn_something" id="learn_something1" value="1">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="learn_something" id="learn_something2" value="2">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="learn_something" id="learn_something3" value="3">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="learn_something" id="learn_something4" value="4">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="learn_something" id="learn_something5" value="5">
                </div>
                <div class="col-2 text-start">強烈同意</div>
            </div>
        </div>
        <hr class="question-divider">

        <div class="question-container">
            <label class="form-label">在演算法這門課程取得好成績會讓我獲得最大的滿足感。</label>
            <div class="row mb-2">
                <div class="col-2"></div>
                <div class="col text-center">1</div>
                <div class="col text-center">2</div>
                <div class="col text-center">3</div>
                <div class="col text-center">4</div>
                <div class="col text-center">5</div>
                <div class="col-2"></div>
            </div>
            <div class="row">
                <div class="col-2 text-end">強烈不同意</div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="Good_results" id="Good_results1" value="1">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="Good_results" id="Good_results2" value="2">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="Good_results" id="Good_results3" value="3">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="Good_results" id="Good_results4" value="4">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="Good_results" id="Good_results5" value="5">
                </div>
                <div class="col-2 text-start">強烈同意</div>
            </div>
        </div>
        <hr class="question-divider">

        <div class="question-container">
            <label class="form-label">為了提高各科目的成績平均值，在演算法這門課程，我最關心的事情就是能否取得好成績。</label>
            <div class="row mb-2">
                <div class="col-2"></div>
                <div class="col text-center">1</div>
                <div class="col text-center">2</div>
                <div class="col text-center">3</div>
                <div class="col text-center">4</div>
                <div class="col text-center">5</div>
                <div class="col-2"></div>
            </div>
            <div class="row">
                <div class="col-2 text-end">強烈不同意</div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="improve_average" id="improve_average1" value="1">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="improve_average" id="improve_average2" value="2">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="improve_average" id="improve_average3" value="3">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="improve_average" id="improve_average4" value="4">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="improve_average" id="improve_average5" value="5">
                </div>
                <div class="col-2 text-start">強烈同意</div>
            </div>
        </div>
        <hr class="question-divider">

        <div class="question-container">
            <label class="form-label">如果可以的話，我想在演算法這門課程取得的成績能夠贏過大部份的學生。</label>
            <div class="row mb-2">
                <div class="col-2"></div>
                <div class="col text-center">1</div>
                <div class="col text-center">2</div>
                <div class="col text-center">3</div>
                <div class="col text-center">4</div>
                <div class="col text-center">5</div>
                <div class="col-2"></div>
            </div>
            <div class="row">
                <div class="col-2 text-end">強烈不同意</div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="Beat_most_students" id="Beat_most_students1" value="1">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="Beat_most_students" id="Beat_most_students2" value="2">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="Beat_most_students" id="Beat_most_students3" value="3">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="Beat_most_students" id="Beat_most_students4" value="4">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="Beat_most_students" id="Beat_most_students5" value="5">
                </div>
                <div class="col-2 text-start">強烈同意</div>
            </div>
        </div>
        <hr class="question-divider">

        <div class="question-container">
            <label class="form-label">我會想在演算法這門課程取得好成績，是因為我認為向家人、朋友或其他人展示我的能力是一件重要的事。</label>
            <div class="row mb-2">
                <div class="col-2"></div>
                <div class="col text-center">1</div>
                <div class="col text-center">2</div>
                <div class="col text-center">3</div>
                <div class="col text-center">4</div>
                <div class="col text-center">5</div>
                <div class="col-2"></div>
            </div>
            <div class="row">
                <div class="col-2 text-end">強烈不同意</div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="show_my_ability" id="show_my_ability1" value="1">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="show_my_ability" id="show_my_ability2" value="2">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="show_my_ability" id="show_my_ability3" value="3">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="show_my_ability" id="show_my_ability4" value="4">
                </div>
                <div class="col text-center">
                    <input class="form-check-input" type="radio" name="show_my_ability" id="show_my_ability5" value="5">
                </div>
                <div class="col-2 text-start">強烈同意</div>
            </div>
        </div>
    </div>

    <div class="d-flex justify-content-end mb-3">
        <span class="me-auto custom-text">繳交後才會記入答題歷史</span>
        <button type="button" runat="server" id="submitBtn" class="btn btn-primary btn-lg">提交</button>
    </div>
    <div class="question-container"></div>
    </form>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#submitBtn").click(function (e) {
                e.preventDefault();
                var new_content = $("input[name='new_content']:checked").val();
                var curiosity = $("input[name='curiosity']:checked").val();
                var try_to_understand = $("input[name='try_to_understand']:checked").val();
                var learn_something = $("input[name='learn_something']:checked").val();
                var Good_results = $("input[name='Good_results']:checked").val();
                var improve_average = $("input[name='improve_average']:checked").val();
                var Beat_most_students = $("input[name='Beat_most_students']:checked").val();
                var show_my_ability = $("input[name='show_my_ability']:checked").val();
                $.ajax({
                    type: "POST",
                    url: "learning_scale.aspx/SubmitFeedback",
                    data: JSON.stringify({
                        new_content: new_content,
                        curiosity: curiosity,
                        try_to_understand: try_to_understand,
                        learn_something: learn_something,
                        Good_results: Good_results,
                        improve_average: improve_average,
                        Beat_most_students: Beat_most_students,
                        show_my_ability: show_my_ability
                    }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        alert("反饋已提交！");
                        // 清除表單
                        $("#feedbackForm")[0].reset();
                        window.location.href = 'learning_map.aspx';
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
