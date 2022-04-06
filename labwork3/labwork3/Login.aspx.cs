using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace labwork3
{
    public partial class Login : System.Web.UI.Page
    {
        private Dictionary<string, string> users = new()
        {
            {"user1", "password1" },
            {"user2", "password2"},
            {"user3", "password3"}
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string userName = NameTextBox.Text;
            string password = PasswordInput.Value.ToString().Trim();
            if (users.Contains(new KeyValuePair<string, string>(userName, password)))
            {
                Session["user"] = userName;
                Server.Transfer("MainPage.aspx");
            }
            else
            {
                Response.Write("<script>alert('Invalid username or password')</script>");
                NameTextBox.Text = "";
            }
        }
    }
}