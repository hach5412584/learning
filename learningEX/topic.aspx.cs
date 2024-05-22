using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace learningEX
{
    public partial class topic : System.Web.UI.Page
    {

        string ConnectionString = "Data Source=DESKTOP-VLAJAD1;Initial Catalog=TopicDatabase;User Id=test;Password=;";
        string topicname = "NULL";
        string topictype = "NULL";
        string topiccategory = "NULL";
        string topicsubcategory = "NULL";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //初始化 ViewState["QuestionIndex"]，表示當前題目的索引
                Session["TopicUrl"] = Request.Url.ToString();
                ViewState["QuestionIndex"] = 0;
                ViewState["QuestionResults"] = new Dictionary<string, bool>(); // 初始化題目正確與否的 Dictionary
                topicname = Request.QueryString["topicname"];
                topictype = Request.QueryString["topictype"];
                topiccategory = Request.QueryString["topiccategory"];
                topicsubcategory = Request.QueryString["topicsubcategory"];
                Session["topicname"] = topicname;
                Session["topictype"] = topictype;
                Session["topiccategory"] = topiccategory;
                Session["topicsubcategory"] = topicsubcategory;
                TakeQuestion();                  
                Session["Correctcount"] = 0;
                if (topicname == "BranchandBound1" && Session["question_items"] != null && Session["answers_list"] != null)
                {
                    List<string> answers = Session["answers_list"] as List<string>;                  
                    List<Writetopic.Item> questionItems = Session["question_items"] as List<Writetopic.Item>;
                   /* if (answers != null)
                    {
                        foreach (string str in answers)
                        {
                            Label1.Text += "(" + str + "),";
                        }
                    }*/
                    if (questionItems != null)
                    {

                        // 遍歷 questionItems 並生成相應的頁面內容
                        /*foreach (Writetopic.Item item in questionItems)
                        {
                            // 根據 item 的屬性值動態生成頁面內容，例如：
                            Label label = new Label();
                            label.Text = $"Weight: {item.Weight}, Value: {item.Value}";
                            // 將 label 或其他控件添加到頁面中的適當位置
                            form1.Controls.Add(label);
                        }*/

                    }                  
                    Session.Remove("question_items");
                    Session.Remove("question_capacity");
                    Session.Remove("answers_list");
                }
            }
        }
        // 定義 TakeID 方法
        private void TakeID()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // 使用参数化查询以防止SQL注入
                    string query = "SELECT ID FROM dbo.TopicNum WHERE Topicname = @Topicname AND Topictype = @Topictype";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // 添加参数
                        command.Parameters.AddWithValue("@Topicname", topicname);
                        command.Parameters.AddWithValue("@Topictype", topictype);

                        // 执行查询
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // 如果有匹配的记录，获取topicID
                                string topicID = reader["ID"].ToString();
                                ViewState["TopicID"] = topicID;
                                Debug.WriteLine("TopicID found: " + topicID);

                            }
                            else
                            {
                                Debug.WriteLine("No matching record found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                // 处理异常
            }
        }

        private void TakeQuestion()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    TakeID();
                    string topicID = ViewState["TopicID"] as string;
                    int pageSize = 1; // 每頁顯示的題目數量                                     
                    int currentPage = (int)ViewState["QuestionIndex"];
                    Session["currentPage"] = currentPage;
                    int totalPages = GetTotalQuestions();
                    Modifybuttonswitch();//修改題目按鈕開關
                    pageload.InnerText = $"{currentPage + 1}/{totalPages}";
                    // 計算分頁的 OFFSET
                    int offset = currentPage * pageSize;
                    // 使用参数化查询以防止 SQL 注入
                    string query = "SELECT ImageID, questionID, Questiondata FROM dbo.TopicQuestion WHERE ID = @TopicID ORDER BY (SELECT NULL) OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
                    List<string[]> userinputdata = Session["UserInputData"] as List<string[]>; //表格
                    List<string> Userinputscope = Session["UserInputsCope"] as List<string>;

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TopicID", topicID);
                        command.Parameters.AddWithValue("@Offset", offset);
                        command.Parameters.AddWithValue("@PageSize", pageSize);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string imageID = reader["ImageID"].ToString();
                                string questionID = reader["questionID"].ToString();
                                string questionData = reader["Questiondata"].ToString();

                                ViewState["QuestionID"] = questionID;
                                questiondata.InnerText = questionData;
                                if (Session["UserInputData"] == null)
                                {
                                    TakeImage(topicID, imageID);
                                }
                                else
                                {
                                    if (userinputdata != null)
                                    {
                                        // 遍历 userinputdata 并生成表格
                                        foreach (var item in userinputdata)
                                        {
                                            TableRow row = new TableRow();
                                            foreach (var value in item)
                                            {
                                                TableCell cell = new TableCell();
                                                cell.Text = value;
                                                row.Cells.Add(cell);
                                            }
                                            dynamicTable.Rows.Add(row);
                                        }
                                        foreach (var item in Userinputscope)
                                        {
                                            userinputscope.InnerText = item + "\n";
                                        }
                                    }
                                }
                                Debug.WriteLine($" ImageID: {imageID}, questionID: {questionID}, Questiondata: {questionData}");
                            }
                            else
                            {
                                Debug.WriteLine("No matching record found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                // 处理异常
            }
        }

        private void Modifybuttonswitch()
        {
            if ((int)ViewState["QuestionIndex"] + 1 == 1) { btnChangeTopic.Visible = true; } else { btnChangeTopic.Visible = false; }
            if ((int)ViewState["QuestionIndex"] + 1 == GetTotalQuestions()) { btnNext.Value = "完成"; } else { btnNext.Value = "下一步"; }
        }

        private int GetTotalQuestions()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string topicID = ViewState["TopicID"] as string;
                    string query = "SELECT COUNT(*) FROM dbo.TopicQuestion WHERE ID = @TopicID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TopicID", topicID);

                        // 使用 ExecuteScalar 來取得總題數
                        object result = command.ExecuteScalar();

                        // 轉換結果為整數
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                // 處理異常，這裡你可以自行決定如何處理錯誤，例如返回一個默認值或拋出異常。
                return -1; // 這只是一個示例，實際應用中應根據情況修改
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            // 獲取當前索引
            int currentIndex = (int)ViewState["QuestionIndex"];
            int totalPages = GetTotalQuestions();
            if (currentIndex + 1 < totalPages)
            {
                /*
                // 更新索引
                currentIndex++;

                // 更新 ViewState
                ViewState["QuestionIndex"] = currentIndex;
                detailedexplanationtext.InnerText = "";
                // 獲取下一題
                TakeQuestion();
                */

                // 取得當前題目的相關信息
                string questionID = ViewState["QuestionID"] as string;
                string inputAnswer = inputAns.Text;  // 假設這是用戶輸入的答案
                string correctAnswer = TakeAns(questionID);  // 假設這是正確的答案
                string isCorrect = "NULL";
                // 判斷答案是否正確
                if (inputAnswer == correctAnswer)
                {
                    isCorrect = "答對";
                }
                else
                {
                    isCorrect = "答錯";
                }
                // 將答案相關信息存入 Session
                SaveAnswerInfoToSession(questionID, isCorrect);
                ViewState["QuestionIndex"] = ++currentIndex;
                TakeQuestion();
            }
            else
            {
                string questionID = ViewState["QuestionID"] as string;
                string inputAnswer = inputAns.Text;  // 用戶輸入的答案
                string correctAnswer = TakeAns(questionID);  // 正確的答案
                string isCorrect = "NULL";
                // 判斷答案是否正確
                if (inputAnswer == correctAnswer)
                {
                    isCorrect = "答對";
                }
                else
                {
                    isCorrect = "答錯";
                }
                // 將答案相關信息存入 Session
                SaveAnswerInfoToSession(questionID, isCorrect);
                Dictionary<string, string> answerResults = Session["AnswerResults"] as Dictionary<string, string>;
                List<string> values = answerResults.Values.ToList<string>();
                for (int i = 0; i < totalPages; i++)
                {
                    if (values[i] == "答對")
                    {
                        int count = Convert.ToInt32(Session["Correctcount"]);
                        count++;
                        Session["Correctcount"] = count;
                    }
                }
                float count_out = Convert.ToInt32(Session["Correctcount"]);
                double Accuracy = ((double)count_out / totalPages) * 100;
                Accuracy = Math.Round(Accuracy, 2);
                Session.Remove("Correctcount");
                Session["Accuracy"] = Accuracy;
                Session["topic_cilck"] = 1;
                Response.Redirect("~/ans_list.aspx");
            }
            inputAns.Text = "";
        }
        private void SaveAnswerInfoToSession(string questionID, string content)
        {
            // 從 Session 中獲取 AnswerResults 字典，如果不存在則創建一個新的
            Dictionary<string, string> answerResults = Session["AnswerResults"] as Dictionary<string, string>;

            if (answerResults == null)
            {
                answerResults = new Dictionary<string, string>();
            }

            // 更新或添加當前題的答案結果
            answerResults[questionID] = content;

            // 將更新後的字典存回 Session
            Session["AnswerResults"] = answerResults;
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            int currentIndex = (int)ViewState["QuestionIndex"];
            // 獲取當前索引
            if (currentIndex > 0)
            {
                // 更新 ViewState
                ViewState["QuestionIndex"] = --currentIndex;
                //  detailedexplanationtext.InnerText = "";
                // 獲取上一題
                TakeQuestion();
            }
            inputAns.Text = "";
        }
        private void TakeImage(string topicID, string imageID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT Image FROM dbo.TopicImage WHERE ID = @TopicID AND ImageID = @ImageID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TopicID", topicID);
                        command.Parameters.AddWithValue("@ImageID", imageID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                byte[] imageData = (byte[])reader["Image"];
                                string base64String = Convert.ToBase64String(imageData);
                                imgTopic.ImageUrl = "data:image/jpeg;base64," + base64String;
                            }
                            else
                            {
                                Debug.WriteLine("No matching record found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }

        private string TakeAns(string questionID)
        {
            try
            {
                if (Request.Cookies["Answers"] != null && Request.Cookies["Question"] != null)
                {
                    List<string> answers_string = Session["AnswersString"] as List<string>;
                    int currentPage = Convert.ToInt32(Session["currentPage"]);
                    return answers_string[currentPage];
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        string query = "SELECT Ans FROM dbo.TopicAns WHERE questionID = @QuestionID";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@QuestionID", questionID);

                            // 使用 ExecuteScalar 來取得單一值（這裡是 Ans）
                            object result = command.ExecuteScalar();
                            return result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 適當地處理例外，例如記錄錯誤、顯示錯誤訊息等
                return "發生錯誤：" + ex.Message;
            }
        }

        protected void ChangeTopic_Click(object sender, EventArgs e)
        {
            Session.Remove("question_items");
            Session.Remove("question_capacity");
            Session.Remove("answers_list");

            Response.Redirect("InputItem.aspx");
        }

        //目前用不到
        /*
        private void TakeDetailedExplanationImageandtext(string questionID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // 使用参数化查询以防止SQL注入
                    string query = "SELECT DetailedExplanationImage, DetailedExplanationText FROM dbo.TopicAns WHERE questionID = @QuestionID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // 添加参数
                        command.Parameters.AddWithValue("@QuestionID", questionID);

                        // 执行查询
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // 取得二進位圖片資料
                                byte[] detailedExplanationImageData = (byte[])reader["DetailedExplanationImage"];

                                // 轉換二進位圖片資料為 Base64 字串
                                string detailedExplanationImageBase64 = Convert.ToBase64String(detailedExplanationImageData);
                                imgTopic.ImageUrl = "data:image/jpeg;base64," + detailedExplanationImageBase64;

                                // 取得文字描述
                                string detailedExplanationText = reader["DetailedExplanationText"].ToString();
                                detailedexplanationtext.InnerText = detailedExplanationText;

                                // 在這裡你可以使用這些值，例如，將它們設置給後端的變數或進行其他處理
                                Debug.WriteLine($"DetailedExplanationText: {detailedExplanationText}");

                                // 將圖片 Base64 字串傳遞到前端，這樣你可以在前端使用它
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowImage", $"showImage('{detailedExplanationImageBase64}');", true);
                            }
                            else
                            {
                                Debug.WriteLine("No matching record found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                // 处理异常
            }
        }
        protected void CheckinputAns_Click(object sender, EventArgs e)
        {
            Dictionary<string, bool> questionResults = ViewState["QuestionResults"] as Dictionary<string, bool>;
            string inputans = inputAns.Text;
            string QuestionID = ViewState["QuestionID"] as string;
            string ANS = TakeAns(QuestionID);
            string resultString = "NULL";
            if (ANS == inputans)
            {
                string script = "alert('答案正確');";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", script, true);
                int currentIndex = (int)ViewState["QuestionIndex"];

                questionResults[QuestionID] = true;
                // 更新索引
                currentIndex++;

                // 更新 ViewState
                ViewState["QuestionIndex"] = currentIndex;
                detailedexplanationtext.InnerText = "";
                // 獲取下一題
                TakeQuestion();
            }
            else
            {
                string script = "alert('答案錯誤');";
                questionResults[QuestionID] = false;
                ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", script, true);
                //TakeDetailedExplanationImageandtext(QuestionID);
                TakeQuestion();
            }
            if (questionResults != null && questionResults.ContainsKey(QuestionID))
            {
                bool result = questionResults[QuestionID];

                // 將布林值轉換為對應的字串
                resultString = result ? "正確" : "錯誤";

                // 使用 resultString 進行後續處理，例如顯示在頁面上或執行其他邏輯
                Console.WriteLine($"Question {QuestionID} 的結果為：{resultString}");
            }
            else
            {
                // 處理字典為 null 或者不包含指定 QuestionID 的情況
                Console.WriteLine($"找不到 QuestionID 為 {QuestionID} 的結果");
            }
            UpdateOverallAccuracy();
        }
        private void UpdateOverallAccuracy()
            {
                if (ViewState["QuestionResults"] is Dictionary<string, bool> questionResults)
                {
                    if (questionResults.Count > 0)
                    {
                        int correctCount = questionResults.Count(kvp => kvp.Value);
                        int totalCount = questionResults.Count;

                        double overallAccuracy = (double)correctCount / totalCount * 100;

                        // 更新整體題組的正確率，你可以將其顯示在頁面上或進一步處理
                        lblOverallAccuracy.Text = $"整體正確率：{overallAccuracy}%";
                    }
                    else
                    {
                        // 沒有結果時的處理邏輯
                        lblOverallAccuracy.Text = "沒有結果";
                    }
                }
                else
                {
                    // 無法取得 QuestionResults 時的處理邏輯
                    lblOverallAccuracy.Text = "無法取得結果";
                }
            }
        */
    }
}