using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static labwork3.DataProvider;

namespace labwork3
{
    public partial class Task1Results : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["school"] == null)
            {
                Server.Transfer("MainPage.aspx");
            }
            else
            {
                Dictionary<string, List<ZNO>> studentAndSubjects = new Dictionary<string, List<ZNO>>();
                var school = Convert.ToString(Session["school"]);
                Session["school"] = null;
                foreach (var student in Students)
                {
                    if(student.Value.School == school)
                    {
                        if(ZNOs.ContainsKey(student.Key))
                        {
                            studentAndSubjects.Add(student.Value.Name, ZNOs[student.Key]);
                        }
                        else studentAndSubjects.Add(student.Value.Name, new List<ZNO>());
                    }
                }
                if (studentAndSubjects.Count > 0)
                {
                    Task1GridView.DataSource = studentAndSubjects.OrderBy(pair => pair.Key).Select(pair => new
                    {
                        Прізвище = pair.Key,
                        Предмети = ConvertToStringSubjects(pair.Value)
                    });
                }
                Task1GridView.DataBind();
          }
        }
        public static string ConvertToStringSubjects(List<ZNO> ZNOs)
        {
            string stringOfSubjects = "";
            if (ZNOs.Count > 0)
            {
                ZNOs = ZNOs.OrderBy(v => v.Subject).ToList();
                foreach (var zno in ZNOs)
                {
                    stringOfSubjects += zno.Subject + " ";
                }
            }
            else stringOfSubjects = "Учень не складав ЗНО із жодного предмету";
            return stringOfSubjects;
        }
    }
}