using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static labwork3.DataProvider;

namespace labwork3
{
    public partial class Task2Results : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["permission"] == null)
            {
                Server.Transfer("MainPage.aspx");
            }
            else
            {
                Session["permission"] = null;
                Dictionary<string, List<ZNO>> studentAndSubjects = new Dictionary<string, List<ZNO>>();
                foreach (var student in Students)
                {
                    if (ZNOs.ContainsKey(student.Key) && ZNOs[student.Key].Count >= 3)
                    {
                        studentAndSubjects.Add(student.Value.Name, ZNOs[student.Key]);
                    }
                }
                Dictionary<string, float> mediumMark = new Dictionary<string, float>();
                foreach (var pair in studentAndSubjects)
                {
                    mediumMark.Add(pair.Key, MediumMarkOfZNOs(pair.Value));
                }
                mediumMark = mediumMark.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                int stopOfPrintResults = 0;
                foreach (var pair in mediumMark)
                {
                    if (stopOfPrintResults < Math.Min(mediumMark.Count, 3))
                    {
                        ResultTask2ListBox.Items.Add(pair.Key);
                    }
                    stopOfPrintResults++;
                }
            }
        }
        public float MediumMarkOfZNOs(List<ZNO> ZNOs)
        {
            ZNOs = ZNOs.OrderByDescending(v => v.Mark).ToList();
            float mediumOfMarks = 0;
            for(int i = 0; i < 3; i++)
            {
                mediumOfMarks += ZNOs[i].Mark;
            }
            mediumOfMarks /= 3;
            return mediumOfMarks;
        }
    }
}