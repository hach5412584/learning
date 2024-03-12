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
            Session.Remove("topicUrl");
            Session.Remove("AnswersString");
            Session.Remove("currentPage");
            Session.Remove("question_items");
            Session.Remove("question_capacity");
            Session.Remove("answers_list");

            Response.Redirect("mainpage.aspx");

        }
    }
}