using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class User_StatusAction : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        var btn = (System.Web.UI.WebControls.Button)sender;
        int rentId = Convert.ToInt32(btn.CommandArgument);

        UpdateRequestStatus(rentId, "Accepted");
        SendMessageToFarmer(rentId, "Your request is accepted. Please pay the amount.");

        Repeater1.DataBind();  // Refresh Repeater
    }
    protected void btnReject_Click(object sender, EventArgs e)
    {
        var btn = (System.Web.UI.WebControls.Button)sender;
        int rentId = Convert.ToInt32(btn.CommandArgument);

        UpdateRequestStatus(rentId, "Rejected");
        SendMessageToFarmer(rentId, "Your request is rejected.");

        Repeater1.DataBind();  // Refresh Repeater
    }

    private void UpdateRequestStatus(int rentId, string status)
    {
        string cs = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

        using (SqlConnection con = new SqlConnection(cs))
        {
            con.Open();

            // Step 1: Check existing status
            string currentStatusQuery = "SELECT Status FROM RentProduct WHERE RentID = @RentID";
            using (SqlCommand checkCmd = new SqlCommand(currentStatusQuery, con))
            {
                checkCmd.Parameters.AddWithValue("@RentID", rentId);
                object result = checkCmd.ExecuteScalar();

                string currentStatus = result != null ? result.ToString().ToLower() : "";

                // Prevent update if current status is "Paid" and trying to set to Rejected or Accepted
                if (currentStatus == "paid" &&
                   (status.ToLower() == "rejected" || status.ToLower() == "accepted"))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "statusBlocked",
                        "alert('Cannot change the status because it is already paid.');", true);
                    return;
                }
            }

            // Step 2: Proceed with update
            string updateQuery = "UPDATE RentProduct SET Status = @Status WHERE RentID = @RentID";
            using (SqlCommand cmd = new SqlCommand(updateQuery, con))
            {
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@RentID", rentId);
                cmd.ExecuteNonQuery();
            }

            // Optional: show confirmation message
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "statusUpdated",
                "alert('Request Accepted successfully.');", true);
        }
    }

    private void SendMessageToFarmer(int rentId, string message)
    {
        string cs = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

        int farmerId = 0;

        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = "SELECT FarmerID FROM RentProduct WHERE RentID = @RentID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@RentID", rentId);
                con.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    farmerId = Convert.ToInt32(result);
                }
            }
        }

        if (farmerId > 0)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string insertMsg = "INSERT INTO Messages (FarmerID, MessageText, SentDate) VALUES (@FarmerID, @MessageText, GETDATE())";
                using (SqlCommand cmd = new SqlCommand(insertMsg, con))
                {
                    cmd.Parameters.AddWithValue("@FarmerID", farmerId);
                    cmd.Parameters.AddWithValue("@MessageText", message);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

