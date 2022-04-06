using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static labwork3.DataProvider;

namespace labwork3
{
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PreviousPage == null  || Session["user"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
                Session["permission"] = null;
                Session["school"] = null;
                ReadData();
                StudentsGridView.DataSource = Students.Values.OrderBy(v => v.Name).Select(v => new
                {
                    Прізвище = v.Name,
                    Школа = v.School
                });
                StudentsGridView.DataBind();


                ZNOsGridView.DataSource = ZNOsList.OrderBy(v => Students[v.Id].Name).Select(v => new
                {
                    Прізвище = Students[v.Id].Name,
                    Предмет = v.Subject,
                    Бал = v.Mark,
                    Дата = v.DateTime
                });
                ZNOsGridView.DataBind();

                var schools = new SortedSet<string>();
                foreach(var student in Students)
                {
                    schools.Add(student.Value.School);
                }
                foreach(var school in schools)
                {
                    SchoolDropDownList.Items.Add(school);
                }
            }
        }

        protected void Task2Button_Click(object sender, EventArgs e)
        {
            Session["permission"] = "true";
            Response.Redirect("Task2Results.aspx");
        }
    }
}