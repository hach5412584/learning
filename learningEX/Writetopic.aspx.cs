using System;
using System.Collections.Generic;
using System.Web.UI.WebControls; // 添加使用TextBox控件的命名空间
using System.Linq; // 添加 Linq 命名空間
using System.Web.UI;
using System.Data.SqlClient;
using System.Web;
using Newtonsoft.Json;

namespace learningEX
{
    public partial class Writetopic : System.Web.UI.Page
    {
        string connectionString = "Data Source=DESKTOP-VLAJAD1;Initial Catalog=TopicDatabase;User Id=test;Password=;"; // 替换为您的数据库连接字符串
        List<Item> items = new List<Item>(); // 用于存储物品信息
        private static int itemCount;
        private static int capacity;
        private static int userid;
        // 初始化最大獲利
        int maxProfit = 0;
        int shortcut = 0;
        int id = 1;
        string sort = "";
        string node1 = "";
        string node2 = "";
        string node3 = "";       
        string topicgroupid = "";
        int weightans = 0;
        string topicUrl = "";
        // 定義物品類別
        class Item
        {
            public int Weight { get; }
            public int Value { get; }

            public Item(int weight, int value)
            {
                Weight = weight;
                Value = value;
            }
            public double CostPerUnit => (double)Value / Weight; // 添加单位价格属性       
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            itemCount = (int)Session["ItemCount"];
            capacity = (int)Session["Capacity"];
            userid = 1;// (int)Session["UserID"];
            // 清空 ItemPanel 控件
            ItemPanel.Controls.Clear();
            // 重新生成 TextBox 控件
            for (int i = 0; i < itemCount; i++)
            {
                AddItemInput(i + 1); // 生成第 i 个物品的输入框
            }
            if (Session["TopicUrl"] != null)
            {
                // 取得 Session 中存儲的 URL
                topicUrl = Session["TopicUrl"].ToString();
            }
        }
       
        private void GetItemsFromTextBoxes()
        {
            // 清空 items 列表
            items.Clear();

            // 获取用户输入的物品信息并保存到列表中
            for (int i = 0; i < itemCount; i++)
            {
                TextBox weightTextBox = (TextBox)ItemPanel.FindControl($"WeightTextBox{i + 1}");
                TextBox valueTextBox = (TextBox)ItemPanel.FindControl($"ValueTextBox{i + 1}");

                if (weightTextBox != null && valueTextBox != null)
                {
                    int weight, value;
                    if (int.TryParse(weightTextBox.Text, out weight) && int.TryParse(valueTextBox.Text, out value))
                    {
                        System.Diagnostics.Debug.WriteLine($"成功获取到物品 {i + 1} 的重量：{weight}，价值：{value}");
                        items.Add(new Item(weight, value));
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"无法获取到物品 {i + 1} 的重量和价值");
                    }
                }
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            // 在每次提交按钮点击时，获取用户输入并保存到 items 列表
            GetItemsFromTextBoxes();
            main();
        }

        protected void main()
        {
            // 复制未排序的 items 到另一个列表
            List<Item> unsortedItems = new List<Item>(items);
            items = items.OrderByDescending(item => item.CostPerUnit).ToList();

            for (int i = 0; i < items.Count; i++)
            {
                int originalIndex = unsortedItems.IndexOf(items[i]) + 1; // 找到排序前的索引并加 1（因为索引从 0 开始）
                sort += originalIndex.ToString(); // 将索引转换为字符串并添加到 sort 字符串中
            }

            // 使用遞迴計算最大獲利
            List<List<int>> allNodes = new List<List<int>>(); // 存儲所有節點的索引
            List<int> selectedItems = KnapsackBranchAndBound(0, 0, 0, items, capacity, ref maxProfit, allNodes, ref shortcut,ref weightans);

            // 輸出最大獲利
            int totalValue = CalculateTotalValue(selectedItems, items);
            Response.Write("最大獲利：" + maxProfit);
            Response.Write("shortcut：" + shortcut);

            // 輸出所有節點
            Response.Write("<br/>所有節點: ");
            int num = 0;
            foreach (List<int> node in allNodes)
            {
                if ( node[0]== 0 && node[1]== 0 && num<2)
                {
                    if (num == 0)
                    {
                        node1 = node[2].ToString();
                    }
                    else
                    {
                        node3 = node[2].ToString();
                    }
                    num += 1;
                }
                Response.Write($"[{node[0]}, {node[1]}, {node[2]}] ");
            }
            node2 = string.Join(",", allNodes[1].ToArray());

            var question = new Dictionary<string, object>();
            question["question1"] = itemCount;
            question["question2"] = capacity;

            string questionJson = JsonConvert.SerializeObject(question);

            HttpCookie questionCookie = new HttpCookie("Question", questionJson);
            Response.Cookies.Add(questionCookie);

            var answers = new Dictionary<string, object>();
            answers["answer1"] = sort;
            answers["answer2"] = node1;
            answers["answer3"] = node2;
            answers["answer4"] = node3;
            answers["answer5"] = (maxProfit.ToString() + "," + weightans.ToString());
            answers["answer6"] = shortcut.ToString();
            

            string answersJson = JsonConvert.SerializeObject(answers);

            HttpCookie answersCookie = new HttpCookie("Answers", answersJson);
            Response.Cookies.Add(answersCookie);
            Response.Redirect(topicUrl);
            //checkTopicGroupID();
        }
       
        private void checkTopicGroupID()
        {
            // 生成随机的 6 位数字字符串
            Random random = new Random();
            topicgroupid = random.Next(000000, 999999).ToString();

            // 连接数据库并查询是否存在相同的 QuestionGroupID           
            string query = "SELECT COUNT(*) FROM dbo.TopicAnswer WHERE QuestionGroupID = @QuestionGroupID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@QuestionGroupID", topicgroupid);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();

                    // 如果存在相同的 QuestionGroupID，则重新生成随机字符串
                    if (count > 0)
                    {
                        checkTopicGroupID(); // 递归调用重新生成
                    }
                    // 如果不存在相同的 QuestionGroupID，则使用生成的字符串
                    else
                    {
                        // 执行其他操作，例如将 topicgroupid 存储到数据库
                        // 这里省略其他代码
                    }
                }
                catch (Exception ex)
                {
                    // 处理异常
                    Response.Write("Error: " + ex.Message);
                }
            }
            SQL();

        }

        private void SQL()
        {
           
            for (int i = 1; i <= 6; i++)
            {
               
                string ans="" ;
                if (i==1) { ans = sort; }
                else if (i == 2) { ans = node1; }
                else if (i == 3) { ans = node2; }
                else if (i == 4) { ans = node3; }
                else if (i == 5) { ans = maxProfit.ToString() + "," + weightans.ToString(); }
                else { ans = shortcut.ToString(); }

                InsertData(userid, id, topicgroupid, i, ans);
            }
        }

        private void InsertData(int Userid, int id, string topicgroupid, int ansID, string ans)
        {
            string query = "INSERT INTO  dbo.TopicAnswer (UserID, ID, QuestionGroupID, AnsID, Ans) VALUES (@UserID, @ID, @QuestionGroupID, @AnsID, @Ans)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserID", Userid);
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@QuestionGroupID", topicgroupid);
                command.Parameters.AddWithValue("@AnsID", ansID);
                command.Parameters.AddWithValue("@Ans", ans);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // 处理异常
                    Response.Write("Error: " + ex.Message);
                }
            }
        }

        private void AddItemInput(int index)
        {
            Label weightLabel = new Label();
            weightLabel.Text = $"物品 {index} 重量：";
            ItemPanel.Controls.Add(weightLabel);

            TextBox weightTextBox = new TextBox();
            weightTextBox.ID = $"WeightTextBox{index}";
            ItemPanel.Controls.Add(weightTextBox);

            Label valueLabel = new Label();
            valueLabel.Text = $" 价值：";
            ItemPanel.Controls.Add(valueLabel);

            TextBox valueTextBox = new TextBox();
            valueTextBox.ID = $"ValueTextBox{index}";
            ItemPanel.Controls.Add(valueTextBox);

            ItemPanel.Controls.Add(new LiteralControl("<br />")); // 每个物品后添加换行
        }


        // 遞迴計算最大獲利，同時存取所有節點
        private List<int> KnapsackBranchAndBound( int level, int weight, int profit, List<Item> items, int capacity, ref int maxProfit, List<List<int>> allNodes, ref int shortcut,ref int weightans)
        {
            // 加入當前節點到所有節點列表
            allNodes.Add(new List<int> { profit, weight, (int)Bound(level, weight, profit, items, capacity) });

            // 如果重量超過背包容量，直接返回
            if (weight > capacity)
                return new List<int>();

            // 如果已經考慮完所有物品，更新最大獲利並返回
            if (level == items.Count)
            {
                maxProfit = Math.Max(maxProfit, profit);
                shortcut = level;
                weightans =weight;
                return new List<int>(); // 返回空列表表示結束遞迴
            }

            // 計算上界
            double bound = Bound(level, weight, profit, items, capacity);

            // 如果上界小於等於已知最大值，剪枝
            if (bound <= maxProfit)
                return new List<int>();

            // 考慮將當前物品放入背包
            List<int> selectedItemsInclude = KnapsackBranchAndBound( level + 1, weight + items[level].Weight, profit + items[level].Value, items, capacity, ref maxProfit, allNodes, ref shortcut,ref weightans);

            // 不放入物品
            List<int> selectedItemsExclude = KnapsackBranchAndBound( level + 1, weight, profit, items, capacity, ref maxProfit, allNodes, ref shortcut, ref weightans);

            // 返回兩者中的最大值
            if (CalculateTotalValue(selectedItemsInclude, items) > CalculateTotalValue(selectedItemsExclude, items))
                return selectedItemsInclude;
            else
                return selectedItemsExclude;
        }

        // 計算上界
        private double Bound(int level, int weight, int profit, List<Item> items, int capacity)
        {
            double bound = profit;
            int totalWeight = weight;
            int i = level;

            while (i < items.Count && totalWeight + items[i].Weight <= capacity)
            {
                bound += items[i].Value;
                totalWeight += items[i].Weight;
                i++;
            }

            if (i < items.Count)
            {
                bound += (capacity - totalWeight) * (double)items[i].Value / items[i].Weight;
            }

            return bound;
        }

        // 計算已選擇節點的總值
        private int CalculateTotalValue(List<int> selectedItems, List<Item> items)
        {
            int totalValue = 0;
            foreach (int index in selectedItems)
            {
                totalValue += items[index].Value;
            }
            return totalValue;
        }
    }
}
