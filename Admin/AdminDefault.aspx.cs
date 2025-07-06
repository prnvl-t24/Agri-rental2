using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AdminDefault : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Fetch and display stats (replace with real DB calls)
            lblTotalFarmers.Text = GetTotalFarmers().ToString();
            lblPendingFarmers.Text = GetPendingFarmers().ToString();
            lblTotalProducts.Text = GetTotalProducts().ToString();
            lblTotalTransactions.Text = GetTotalTransactions().ToString();
        }
    }
    private int GetTotalFarmers()
    {
        // Sample DB logic
        return 150;
    }

    private int GetPendingFarmers()
    {
        return 12;
    }

    private int GetTotalProducts()
    {
        return 65;
    }

    private int GetTotalTransactions()
    {
        return 32;
    }
}