using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace learningEX
{
    public partial class topic : System.Web.UI.Page
    {
        private const string ConnectionString = "Data Source=DESKTOP-VLAJAD1;Initial Catalog=Algorithm Image;User Id=test;Password=";
        private int itemsPerPage = 1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["CurrentPage"] = 1;
                BindGridView();
            }
        }

        protected void BindGridView()
        {
            try
            {
                int currentPage = (int)ViewState["CurrentPage"];
                int startRow = (currentPage - 1) * itemsPerPage + 1;
                int endRow = startRow + itemsPerPage - 1;

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    string query = "SELECT datatext, ans, Detailedexplanation, Image FROM (SELECT datatext, ans, Detailedexplanation, Image, ROW_NUMBER() OVER (ORDER BY ImageID) AS RowNum FROM dbo.Images) AS NumberedImages WHERE RowNum BETWEEN @StartRow AND @EndRow";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartRow", startRow);
                        command.Parameters.AddWithValue("@EndRow", endRow);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        GridViewImages.DataSource = reader;
                        GridViewImages.DataBind();

                        if (GridViewImages.Rows.Count > 0)
                        {
                            var lblDatatext = GridViewImages.Rows[0].FindControl("lblDatatext") as Label;
                            string datatextValue = HttpUtility.HtmlDecode(lblDatatext.Text);

                            // 將 datatext 寫入到 text 中
                            Text.InnerHtml = datatextValue;
                            // 當前頁面
                            pageload.InnerText = $"{currentPage}/{GetTotalPages()}";
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        private int GetTotalPages()
        {
            int totalRows = GetTotalRows();
            int totalPages = (int)Math.Ceiling((double)totalRows / itemsPerPage);
            return totalPages;
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            int currentPage = (int)ViewState["CurrentPage"];
            ViewState["CurrentPage"] = Math.Max(1, currentPage - 1);
            detailedexplanation.InnerHtml = "";
            BindGridView();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            int currentPage = (int)ViewState["CurrentPage"];
            int totalRows = GetTotalRows();
            int nextPage = currentPage + 1;

            detailedexplanation.InnerHtml = "";

            if ((nextPage - 1) * itemsPerPage < totalRows)
            {
                ViewState["CurrentPage"] = nextPage;
                BindGridView();
            }
        }

        private int GetTotalRows()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM dbo.Images";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    int totalRows = (int)command.ExecuteScalar();
                    connection.Close();
                    return totalRows;

                }
            }
        }

        protected string GetImageURL(object image)
        {
            byte[] bytes = (byte[])image;
            string base64String = Convert.ToBase64String(bytes);
            return $"data:image/png;base64,{base64String}";
        }

        protected void DetectInput_Click(object sender, EventArgs e)
        {
            string inputData = Request.Form["inputdata"];
            string correctAnswer = GetCorrectAnswer();
            string detailedExplanation = GetDetailedExplanation();
            if (correctAnswer != "")
            {
                if (inputData == correctAnswer)
                {
                    detailedexplanation.InnerHtml = "正確";
                }
                else
                {
                    detailedexplanation.InnerHtml = "正確答案：" + correctAnswer + "<br>" + detailedExplanation;
                }
            }
        }

        private string GetCorrectAnswer()
        {
            int currentPage = (int)ViewState["CurrentPage"];
            int startRow = (currentPage - 1) * itemsPerPage + 1;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT ans FROM (SELECT ans, ROW_NUMBER() OVER (ORDER BY ImageID) AS RowNum FROM dbo.Images) AS NumberedImages WHERE RowNum = @StartRow";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartRow", startRow);
                    connection.Open();
                    return command.ExecuteScalar()?.ToString();
                }
            }
        }

        private string GetDetailedExplanation()
        {
            int currentPage = (int)ViewState["CurrentPage"];
            int startRow = (currentPage - 1) * itemsPerPage + 1;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string query = "SELECT Detailedexplanation FROM (SELECT Detailedexplanation, ROW_NUMBER() OVER (ORDER BY ImageID) AS RowNum FROM dbo.Images) AS NumberedImages WHERE RowNum = @StartRow";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartRow", startRow);
                    connection.Open();
                    return command.ExecuteScalar()?.ToString();
                }
            }
        }
    }
}