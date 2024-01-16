using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace learningEX
{
    public partial class ans_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 獲取在 topic.aspx 中回答的小題資料（示例中假設你有一個方法 GetAnswerList 返回這些資料）
            Dictionary<string, string> answerResults = Session["answerResults"] as Dictionary<string, string>;

            // 判斷 Session 中是否有資料
            if (answerResults != null)
            {
                // 使用 answerResults 資料，例如將其綁定到 GridView
                BindAnswerResults(answerResults);
            }
        }
        private void BindAnswerResults(Dictionary<string, string> answerResults)
        {
            // 將 answerResults 資料綁定到 GridView
            gvAnswers.DataSource = answerResults.Select(kvp => new { QuestionID = kvp.Key, Result = kvp.Value });
            gvAnswers.DataBind();
        }

    }
}