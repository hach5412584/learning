using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace learningEX
{
    public partial class Ans : System.Web.UI.Page
    {
        string ConnectionString = "Data Source=DESKTOP-VLAJAD1;Initial Catalog=TopicDatabase;User Id=test;Password=;";
        string QuestionID = "1004";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TakeQuestion(QuestionID);
                TakeDetailedExplanationImageandtext(QuestionID);
                TakeAns(QuestionID);
            }
        }

        private string TakeQuestion(string QuestionID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT Questiondata FROM dbo.TopicQuestion WHERE questionID = @QuestionID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@QuestionID", QuestionID);

                        // 使用 ExecuteScalar 來取得單一值（這裡是 Ans）
                        object result = command.ExecuteScalar();
                        questiondata.InnerText = result.ToString();
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

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            
        }

    }
}