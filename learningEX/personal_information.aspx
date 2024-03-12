<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="personal_information.aspx.cs" Inherits="learningEX.personal_information" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>個人資料</title>

    <!-- 引入 Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>
</head>
<body class="container mt-5">
    <!-- 使用 Bootstrap container 並添加 margin-top -->
    <div>
        <h2 class="mb-4 text-center">個人資料</h2>
        <!-- 使用 Bootstrap 的標題樣式和 text-center 使其置中 -->

        <!-- 第一個表單 -->
        <form id="profileForm" class="mb-4" runat="server">
            <div class="form-group">
                <label for="fullName">姓名：</label>
                <asp:TextBox ID="fullName" runat="server" CssClass="form-control" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="email">電子信箱：</label>
                <asp:TextBox type="text" ID="email" name="email" class="form-control" runat="server" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="username">使用者名稱：</label>
                <asp:TextBox type="text" ID="username" name="username" class="form-control" runat="server" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="studentID">學號：</label>
                <asp:TextBox type="text" ID="studentID" name="studentID" class="form-control" runat="server" required></asp:TextBox>
            </div>
            <br>
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#confirmationModal">儲存</button>

            <div class="modal fade" id="confirmationModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">確認更改</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            您確定要儲存更改嗎？
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">取消</button>
                            <button type="button" class="btn btn-primary" id="saveChangesBtn">確定</button>
                        </div>
                    </div>
                </div>
            </div>

            <div id="notificationToast" class="toast" role="alert" aria-live="assertive" aria-atomic="true">
                <div class="toast-header">
                    <strong class="mr-auto">提示</strong>
                    <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
                <div class="toast-body" id="toastMessage"></div>
            </div>
            <br>
            <!-- 第二個表單 -->
            <div class="form-group">
                <label for="oldPassword">舊密碼：</label>
                <asp:TextBox type="password" ID="oldPassword" name="oldPassword" class="form-control" runat="server" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="newPassword">新密碼：</label>
                <asp:TextBox type="password" ID="newPassword" name="newPassword" class="form-control" runat="server" required></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="confirmPassword">確認新密碼：</label>
                <asp:TextBox type="password" ID="confirmPassword" name="confirmPassword" class="form-control" runat="server" required></asp:TextBox>
            </div>
            <br>
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#passwordModal">更改密碼</button>

            <div class="modal fade" id="passwordModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="ModalLabel">確認更改</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            您確定要更改密碼嗎？
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">取消</button>
                            <button type="button" class="btn btn-primary" id="changeButton">確定</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- 答案歷史表格 -->
            <asp:GridView ID="gvAnswerHistory" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="AnswerDate" HeaderText="作答日期" />
                    <asp:TemplateField HeaderText="題目名稱">
                        <ItemTemplate>
                            <a href='<%# "topic.aspx?topicname=" + Eval("Topicname") + "&topictype=" + Eval("Topictype") %>'>
                                <asp:Label ID="lblTopicName" runat="server" Text='<%# Eval("Topicname") %>'></asp:Label>
                            </a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Topictype" HeaderText="題目類型" />
                    <asp:TemplateField HeaderText="答案">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkReview" runat="server" Text="回顧" OnClick="lnkReview_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Accuracy" HeaderText="正確率" />
                    <asp:TemplateField HeaderText="HistoricalAnswers" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblHistoricalAnswers" runat="server" Text='<%# Eval("HistoricalAnswers") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </form>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#saveChangesBtn").click(function () {

                var fullName = $("#fullName").val();
                var email = $("#email").val();
                var username = $("#username").val();
                var studentID = $("#studentID").val();

                $.ajax({
                    type: "POST",
                    url: "personal_information.aspx/SaveUserData",
                    data: JSON.stringify({ fullName: fullName, email: email, username: username, studentID: studentID }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        // 處理成功回應 
                        var myModal = new bootstrap.Modal(document.getElementById('confirmationModal'));
                        setTimeout(function () {
                            myModal.hide();
                        }, 300);
                        showToast("資料儲存成功!");
                        console.log(response.d);
                    },
                    error: function (error) {
                        // 處理錯誤回應
                        console.log(error);
                        showToast("儲存資料時發生錯誤!");
                    }
                });
            });
        });

        $(document).ready(function () {
            $("#changeButton").click(function () {
                var oldPassword = $("#oldPassword").val();
                var newPassword = $("#newPassword").val();
                var confirmPassword = $("#confirmPassword").val();

                // 檢查新密碼和確認新密碼是否相符
                if (newPassword !== confirmPassword) {
                    showToast("新密碼和確認新密碼不相符");
                    return;
                }

                // 使用 Ajax 發送請求
                $.ajax({
                    type: "POST",
                    url: "personal_information.aspx/ChangePassword",
                    data: JSON.stringify({ oldPassword: oldPassword, newPassword: newPassword }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        // 處理成功回應
                        console.log(response.d);
                        var myModal = new bootstrap.Modal(document.getElementById('passwordModal'));
                        setTimeout(function () {
                            myModal.hide();
                        }, 300);
                        showToast(response.d);
                        console.log(response.d);
                    },
                    error: function (error) {
                        // 處理錯誤回應
                        console.log(error);
                        showToast(error);
                    }
                });
            });
        });

        function showToast(message) {
            var toast = new bootstrap.Toast(document.getElementById('notificationToast'));
            $("#toastMessage").text(message);
            toast.show();
        }

    </script>
</body>
</html>
