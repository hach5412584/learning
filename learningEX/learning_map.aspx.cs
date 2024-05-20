using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Services;

namespace learningEX
{
    public class AccuracyData
    {
        public List<string> Labels { get; set; }
        public List<double> Data { get; set; }
    }

    public partial class learningmap : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] != null && Session["UserID"] != null)
                {
                    string username = Session["UserName"].ToString();
                    string userID = Session["UserID"].ToString();
                    lblUserName.ForeColor = System.Drawing.Color.Gray;
                    lblUserName.Text = "歡迎您" + username;
                }
            }
        }
        [WebMethod]
        public static double[] GetAccuracyData()
        {
            double[] accuracyData = new double[6]; // 假設你有六個項目
            // 連接資料庫並執行查詢
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string query = "SELECT AVG(Accuracy) AS AverageAccuracy FROM UserAnswerHistory WHERE Topictype = 'Algorithm'";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                int index = 0;
                while (reader.Read() && index < 6)
                {
                    // 讀取每個項目的正確率，並將其添加到數據數組中
                    accuracyData[index++] = Math.Round(Convert.ToDouble(reader["AverageAccuracy"]),2);
                }
                reader.Close();
            }

            return accuracyData;
        }
        [WebMethod]
        public static AccuracyData GetAccuracyChartTopRight(int index)
        {
            AccuracyData accuracyData = new AccuracyData();
            accuracyData.Labels = new List<string>();
            accuracyData.Data = new List<double>();

            string topicType = "";

            // 根据 index 设置不同的 topicType
            switch (index)
            {
                case 0:
                    topicType = "Algorithm";
                    break;
                case 1:
                    topicType = "ImageRecognition";
                    break;
                // 添加其他 case 处理不同的 index 值
                default:
                    topicType = "DefaultTopic";
                    break;
            }

            HttpContext.Current.Session["labeltype"] = topicType;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string query = "SELECT TopicCategory, AVG(Accuracy) AS AverageAccuracy FROM UserAnswerHistory WHERE Topictype = @TopicType GROUP BY TopicCategory";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TopicType", topicType);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    accuracyData.Labels.Add(reader["TopicCategory"].ToString());
                    accuracyData.Data.Add(Math.Round(Convert.ToDouble(reader["AverageAccuracy"]), 2));
                }
                reader.Close();
            }

            return accuracyData;
        }


        [WebMethod]
        public static AccuracyData GetAccuracyChartBottomRight(int index)
        {
            AccuracyData accuracyData_Bot = new AccuracyData();
            accuracyData_Bot.Labels = new List<string>();
            accuracyData_Bot.Data = new List<double>();
            string topicType = HttpContext.Current.Session["labeltype"].ToString(); ;
            string TopicCategory = "";
            switch (topicType)
            {
                case "Algorithm":
                    switch (index)
                    {
                        case 0:
                            TopicCategory = "Divide and conquer";
                            break;
                        case 1:
                            TopicCategory = "";
                            break;
                        case 2:
                            TopicCategory = "";
                            break;
                        default:
                            break;
                    }
                    break;
                case "ImageRecognition":
                    switch (index)
                    {
                        case 0:
                            TopicCategory = "";
                            break;
                        case 1:
                            TopicCategory = "";
                            break;
                        case 2:
                            TopicCategory = "";
                            break;
                        default:
                            break;
                    }
                    break;
                // 添加其他 case 处理不同的 index 值
                default:
                    topicType = "DefaultTopic";
                    break;
            }

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string query = "SELECT TopicSubcategory, AVG(Accuracy) AS AverageAccuracy FROM UserAnswerHistory WHERE TopicCategory = @TopicCategory GROUP BY TopicSubcategory";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TopicCategory", TopicCategory);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    accuracyData_Bot.Labels.Add(reader["TopicSubcategory"].ToString());
                    accuracyData_Bot.Data.Add(Math.Round(Convert.ToDouble(reader["AverageAccuracy"]), 2));
                }
                reader.Close();
            }

            return accuracyData_Bot;
        }


    }
}