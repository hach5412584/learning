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

        }

        protected void ConfirmButton_Click(object sender, EventArgs e)
        {
            int.TryParse(ITEM.Text, out itemCount);
            int.TryParse(InCapacity.Text, out capacity);
            Session["ItemCount"] = itemCount;
            Session["Capacity"] = capacity;
            Response.Redirect("Writetopic.aspx");
        }


    }
}

