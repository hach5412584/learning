using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace learningEX
{
    public partial class mainpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"mainpageLoad");
            if (!IsPostBack)
            {
                if (Session["UserName"] != null)
                {
                    string username = Session["UserName"].ToString();
                    lblUserName.Text = "歡迎您，" + username + "！";
                }
            }
        }
    }
}