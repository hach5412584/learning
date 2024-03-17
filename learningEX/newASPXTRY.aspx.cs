using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;


namespace learningEX
{
    public partial class newASPXTRY : System.Web.UI.Page
    {
        public class Item
        {
            public string Weight { get; set; }
            public string Value { get; set; }
        }

        List<Item> data = new List<Item>()
{
    new Item { Weight = "重量", Value = "價值" },
    new Item { Weight = "1", Value = "15" },
    new Item { Weight = "2", Value = "20" }
};
        protected void Page_Load(object sender, EventArgs e)
        {


            data.Add(new Item { Weight = "3", Value = "30" });
            Session["question_items"] = data;
            List<Item> questionItems = Session["question_items"] as List<Item>;
            // 生成表格
            foreach (var item in data)
            {
                TableRow row = new TableRow();

                TableCell weightCell = new TableCell();
                weightCell.Text = item.Weight;
                row.Cells.Add(weightCell);

                TableCell valueCell = new TableCell();
                valueCell.Text = item.Value;
                row.Cells.Add(valueCell);

                dynamicTable.Rows.Add(row);
            }

        }
    }
}