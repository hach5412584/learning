using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace learningEX
{
    public partial class topic : System.Web.UI.Page
    {
        string ConnectionString = "Data Source=DESKTOP-VLAJAD1;Initial Catalog=TopicDatabase;User Id=test;Password=;";
        string topicname = "BranchandBound";
        string topictype = "Algorithm";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化 ViewState["QuestionIndex"]，表示當前題目的索引
                ViewState["QuestionIndex"] = 0;
                TakeQuestion();
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
                    // 獲取當前頁碼
                    int currentPage = (int)ViewState["QuestionIndex"];
                    int totalPages = GetTotalQuestions();
                    pageload.InnerText = $"{currentPage + 1}/{totalPages}";
                    // 計算分頁的 OFFSET
                    int offset = currentPage * pageSize;
                    // 使用参数化查询以防止 SQL 注入
                    string query = "SELECT ImageID, questionID, Questiondata FROM dbo.TopicQuestion WHERE ID = @TopicID ORDER BY (SELECT NULL) OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

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
                                TakeImage(topicID, imageID);

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
                // 更新索引
                currentIndex++;

                // 更新 ViewState
                ViewState["QuestionIndex"] = currentIndex;
                detailedexplanationtext.InnerText = "";
                // 獲取下一題
                TakeQuestion();
            }

        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            int currentIndex = (int)ViewState["QuestionIndex"];
            // 獲取當前索引
            if (currentIndex > 0)
            {
                currentIndex--;

                // 更新 ViewState
                ViewState["QuestionIndex"] = currentIndex;
                detailedexplanationtext.InnerText = "";
                // 獲取上一題
                TakeQuestion();
            }
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
            catch (Exception ex)
            {
                // 適當地處理例外，例如記錄錯誤、顯示錯誤訊息等
                return "發生錯誤：" + ex.Message;
            }
        }

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
            string inputans = inputAns.Text;
            string QuestionID = ViewState["QuestionID"] as string;
            string ANS = TakeAns(QuestionID);
            if (ANS == inputans)
            {
                string script = "alert('答案正確');";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", script, true);
                int currentIndex = (int)ViewState["QuestionIndex"];

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
                ClientScript.RegisterStartupScript(this.GetType(), "ShowMessage", script, true);
                TakeDetailedExplanationImageandtext(QuestionID);
            }
        }
    }
}