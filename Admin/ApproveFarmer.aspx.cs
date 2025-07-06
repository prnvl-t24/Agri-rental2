using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class Admin_ApproveFarmer : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["Agriculture"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFarmers();
        }
    }
    private void LoadFarmers()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = "SELECT FarmerID, FullName, Email, PhoneNumber, Status FROM Farmer_Registration WHERE Status='Pending'";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvFarmers.DataSource = dt;
            gvFarmers.DataBind();
        }
    }

    protected void gvFarmers_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Approve" || e.CommandName == "Reject")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvFarmers.Rows[index];
            string farmerID = gvFarmers.DataKeys[index]["FarmerID"].ToString();
            string newStatus = e.CommandName == "Approve" ? "Approved" : "Rejected";

            using (SqlConnection con = new SqlConnection(conStr))
            {
                string updateQuery = "UPDATE Farmer_Registration SET Status=@Status WHERE FarmerID=@FarmerID";
                SqlCommand cmd = new SqlCommand(updateQuery, con);
                cmd.Parameters.AddWithValue("@Status", newStatus);
                cmd.Parameters.AddWithValue("@FarmerID", farmerID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            lblMessage.Text = "Farmer ID {farmerID} has been {newStatus}.";
            LoadFarmers();
        }
    }
}