using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyWebApplication
{
    public partial class TestPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Change Title by Server " + DateTime.Now.ToString();
            TextBox1.Text = "Change Text by Server " + DateTime.Now.ToString();
        }
    }
}