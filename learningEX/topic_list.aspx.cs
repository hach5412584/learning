using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace learningEX
{
	public partial class topic_list : System.Web.UI.Page
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
                string questionType = Request.QueryString["questiontype"];

                if (!string.IsNullOrEmpty(questionType))
                {
                    DataTable dtQuestions = GetQuestionsByType(questionType);
                    DisplayQuestions(dtQuestions);
                }
            }
        }

        private void DisplayQuestions(DataTable questions)
        {
            gvQuestions.DataSource = questions;
            gvQuestions.DataBind();
        }

        private DataTable GetQuestionsByType(string questionType)
        {
            DataTable dtQuestions = new DataTable();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["topic"].ConnectionString))
            {
                connection.Open();

                // 使用參數化查詢以防 SQL 注入
                string query = "SELECT Topictype, Topicname, TopicCategory, TopicSubcategory FROM TopicNum WHERE Topictype = @Topictype";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Topictype", questionType);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dtQuestions);

                connection.Close();
            }

            return dtQuestions;
        }
    }
}