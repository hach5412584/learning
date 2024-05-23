using System;
using System.Collections.Generic;
using System.Web.UI.WebControls; // 添加使用TextBox控件的命名空间
using System.Linq; // 添加 Linq 命名空间
using System.Web.UI;

namespace learningEX
{
    public partial class InputItem : System.Web.UI.Page
    {
        int itemCount; // 假设用户希望输入N个物品      
        int capacity;// 背包容量
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["TopicID"] = Session["TopicID"] as string;
                if (ViewState["TopicID"].ToString().Trim() == "2")
                {
                    Session["ItemCount"] = 6;
                    Response.Redirect("WriteTopicShortPath.aspx");
                }



                SetControlVisibility();
            }
        }
        
        private void SetControlVisibility()
        {


            if (ViewState["TopicID"].ToString() .Trim() == "1")
            {
                Div1.Visible = true;
                Div2.Visible = false;
            }

            else if (ViewState["TopicID"].ToString().Trim() == "2")
            {
                Div1.Visible = false;
                Div2.Visible = true;
            }

        }

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (ViewState["TopicID"].ToString().Trim() == "1")
            {
                int.TryParse(ITEM1.Text, out itemCount);
                int.TryParse(InCapacity.Text, out capacity);
                if (itemCount > 0 && capacity > 0)
                {
                    Session["ItemCount"] = itemCount;
                    Session["Capacity"] = capacity;
                    Response.Redirect("Writetopic.aspx");
                }
                else if (itemCount <= 0 || capacity <= 0)
                {
                    lblMessage.Text = "※輸入錯誤 必須大於0 請重新輸入";
                }
            }           
        }
        
    }
}

