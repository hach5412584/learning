using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace learningEX
{
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
    }
}