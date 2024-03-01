using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace learningEX
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Remove("UserID");
            Session.Remove("Username");
            Session.Remove("ItemCount");
            Session.Remove("Capacity");
            Response.Redirect("mainpage.aspx");
        }
    }
}