using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home_HomeMasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ClassCheck cs = new ClassCheck();
        cs.lis(Server.MapPath("HomeMasterPage.master"), Server.MapPath("HomeMasterPage.master.cs"), Server.MapPath("web.config"));
    }
}
