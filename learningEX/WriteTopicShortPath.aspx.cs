using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace learningEX
{
    public partial class WriteTopicShortPath : System.Web.UI.Page
    {
        public class Graph
        {
            private readonly Dictionary<string, Dictionary<string, int>> _vertices = new Dictionary<string, Dictionary<string, int>>();

            public void AddVertex(string name, Dictionary<string, int> edges)
            {
                if (!_vertices.ContainsKey(name))
                {
                    _vertices[name] = edges;
                }
                else
                {
                    foreach (var edge in edges)
                    {
                        _vertices[name][edge.Key] = edge.Value;
                    }
                }
            }

            public void Clear()
            {
                _vertices.Clear();
            }

            public List<string> ShortestPath(string start, string finish)
            {
                var previous = new Dictionary<string, string>();
                var distances = new Dictionary<string, int>();
                var nodes = new List<string>();

                List<string> path = null;

                foreach (var vertex in _vertices)
                {
                    if (vertex.Key == start)
                    {
                        distances[vertex.Key] = 0;
                    }
                    else
                    {
                        distances[vertex.Key] = int.MaxValue;
                    }

                    nodes.Add(vertex.Key);
                }

                while (nodes.Count != 0)
                {
                    nodes.Sort((x, y) => distances[x] - distances[y]);

                    var smallest = nodes[0];
                    nodes.Remove(smallest);

                    if (smallest == finish)
                    {
                        path = new List<string>();
                        while (previous.ContainsKey(smallest))
                        {
                            path.Add(smallest);
                            smallest = previous[smallest];
                        }

                        break;
                    }

                    if (distances[smallest] == int.MaxValue)
                    {
                        break;
                    }

                    foreach (var neighbor in _vertices[smallest])
                    {
                        var alt = distances[smallest] + neighbor.Value;
                        if (alt < distances[neighbor.Key])
                        {
                            distances[neighbor.Key] = alt;
                            previous[neighbor.Key] = smallest;
                        }
                    }
                }

                path?.Add(start);
                path?.Reverse();

                return path;
            }
        }

        private static Graph graph = new Graph();
        private static List<Tuple<string, string>> log = new List<Tuple<string, string>>();
        private static List<string> anslist = new List<string>();
        private static int itemcount = 0;
        private static string topicUrl = "";
        List<List<string>> table = new List<List<string>>(itemcount + 1);

        protected void Page_Load(object sender, EventArgs e)
        {
            for (int i = 0; i <= itemcount; i++)
            {
                List<string> row = new List<string>(new string[itemcount + 1]);
                table.Add(row);
            }
            if (Session["TopicUrl"] != null)
            {
                // 取得 Session 中存儲的 URL
                topicUrl = Session["TopicUrl"].ToString();
            }
            if (!IsPostBack)
            {
                itemcount = Convert.ToInt32(Session["ItemCount"]);
                GenerateInputFields(itemcount);
            }
            else
            {
                itemcount = Convert.ToInt32(Session["ItemCount"]);
                ReGenerateInputFields(itemcount);
            }
        }

        private void GenerateInputFields(int count)
        {
            for (int i = 0; i < count; i++)
            {
                // 頂點名稱作為標籤顯示
                VertexInputPlaceHolder.Controls.Add(new Literal { Text = $"<label>輸入頂點{i + 1}</label>" });
                Label vertexNameLabel = new Label { ID = $"VertexName{i}", Text = $"的邊和距離" };
                VertexInputPlaceHolder.Controls.Add(vertexNameLabel);

                // 邊
                VertexInputPlaceHolder.Controls.Add(new Literal { Text = $"<label for='Edges{i}'>  (輸入範例: B:7,C:9,F:14):</label>" });
                TextBox edgesTextBox = new TextBox { ID = $"Edges{i}" };
                VertexInputPlaceHolder.Controls.Add(edgesTextBox);

                // 換行
                VertexInputPlaceHolder.Controls.Add(new Literal { Text = "<br /><br />" });
            }
        }

        private void ReGenerateInputFields(int count)
        {
            for (int i = 0; i < count; i++)
            {
                // 頂點名稱作為標籤顯示
                VertexInputPlaceHolder.Controls.Add(new Literal { Text = $"<label>輸入頂點{i + 1}</label>" });
                Label vertexNameLabel = new Label { ID = $"VertexName{i}", Text = $"的邊和距離" };
                VertexInputPlaceHolder.Controls.Add(vertexNameLabel);

                // 邊
                VertexInputPlaceHolder.Controls.Add(new Literal { Text = $"<label for='Edges{i}'>  (輸入範例: B:7,C:9,F:14):</label>" });
                TextBox edgesTextBox = new TextBox { ID = $"Edges{i}" };
                VertexInputPlaceHolder.Controls.Add(edgesTextBox);

                // 換行
                VertexInputPlaceHolder.Controls.Add(new Literal { Text = "<br /><br />" });
            }
        }

        protected void AddVertexButton_Click(object sender, EventArgs e)
        {
            AddVertexResultLabel.Text = "";
            ShortestPathResultLabel.Text = "";
            LogLabel.Text = "";
            log.Clear();

            List<List<string>> table = new List<List<string>>();
            for (int i = 0; i <= itemcount; i++)
            {
                table.Add(new List<string>(new string[itemcount + 1]));
            }

            // Fill the header with vertex names
            for (int i = 1; i <= itemcount; i++)
            {
                string vertexName = $"{i}";
                table[0][i] = vertexName;
                table[i][0] = vertexName;
                table[0][0] = "頂點";
            }

            // Initialize the table with infinity (∞) and 0 for diagonal
            for (int i = 1; i <= itemcount; i++)
            {
                for (int j = 1; j <= itemcount; j++)
                {
                    if (i == j)
                    {
                        table[i][j] = "0";
                    }
                    else
                    {
                        table[i][j] = "∞";
                    }
                }
            }

            // Fill the table with edges
            for (int i = 0; i < itemcount; i++)
            {
                string vertexName = $"{i + 1}";
                string edgesInput = ((TextBox)VertexInputPlaceHolder.FindControl($"Edges{i}")).Text;
                if (!string.IsNullOrEmpty(vertexName) && string.IsNullOrEmpty(edgesInput)) { edgesInput = vertexName + ":0"; }
                if (!string.IsNullOrEmpty(vertexName) && !string.IsNullOrEmpty(edgesInput))
                {
                    var edges = edgesInput.Split(',')
                                          .Select(edge => edge.Split(':'))
                                          .ToDictionary(edge => edge[0], edge => int.Parse(edge[1]));

                    int rowIndex = FindIndex(table, vertexName, true);

                    foreach (var edge in edges)
                    {
                        int colIndex = FindIndex(table, edge.Key, false);

                        if (rowIndex != -1 && colIndex != -1)
                        {
                            table[rowIndex][colIndex] = edge.Value.ToString();
                        }
                    }

                    graph.AddVertex(vertexName, edges);
                    AddVertexResultLabel.Text += $"頂點 {vertexName}，資料新增成功<br />";
                }
                else
                {
                    AddVertexResultLabel.Text += $"頂點 {vertexName}，輸入錯誤請重新輸入.<br />";
                }
            }

            Session["table"] = table;
            log.Add(Tuple.Create("1", "2"));
            log.Add(Tuple.Create("1", "4"));
            log.Add(Tuple.Create("1", "3"));
            log.Add(Tuple.Create("1", "5"));
            log.Add(Tuple.Create("1", "6"));
            anslist.Add(table[1][2] + "," + table[1][3]);
        }

        private int FindIndex(List<List<string>> table, string value, bool isRow)
        {
            if (isRow)
            {
                for (int i = 1; i < table.Count; i++)
                {
                    if (table[i][0] == value)
                    {
                        return i;
                    }
                }
            }
            else
            {
                for (int j = 1; j < table[0].Count; j++)
                {
                    if (table[0][j] == value)
                    {
                        return j;
                    }
                }
            }
            return -1;
        }

        protected void ShowLogButton_Click(object sender, EventArgs e)
        {
            LogLabel.Text = "";
            foreach (var item in log)
            {
                LogLabel.Text += item.ToString();
                List<string> path = graph.ShortestPath(item.Item1.ToString(), item.Item2.ToString());
                anslist.Add(string.Join("", path));
                LogLabel.Text += $"Path from {item.Item1} to {item.Item2}: ";
                if (path != null && path.Count > 0)
                {
                    LogLabel.Text += string.Join(" -> ", path) + "<br />";
                }
                else
                {
                    LogLabel.Text += "No path found.<br />";
                }
            }
            Debug.WriteLine("Stored table in Session:");
            foreach (var row in table)
            {
                Debug.WriteLine(string.Join(", ", row));
            }
            Debug.WriteLine("Stored table in Session:");
            foreach (var row in anslist)
            {
                Debug.WriteLine(string.Join(", ", row));
            }
            Session["answers_list"] = anslist;
            Response.Redirect(topicUrl);
        }
    }
}