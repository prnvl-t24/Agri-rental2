using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


public partial class Farmer_PaymentGateway : System.Web.UI.Page
{
    string connStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btnPayNow_Click(object sender, EventArgs e)
    {
        string rentId = Session["RentID"]?.ToString();

        if (string.IsNullOrEmpty(rentId))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sessionError", "alert('Session expired. Please try again.');", true);
            return;
        }

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            // Step 1: Check payment and request status
            string checkQuery = "SELECT PaymentStatus, Status FROM RentProduct WHERE RentID = @RentID";
            using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
            {
                cmd.Parameters.AddWithValue("@RentID", rentId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string paymentStatus = reader["PaymentStatus"]?.ToString();
                        string requestStatus = reader["Status"]?.ToString();

                        if (paymentStatus == "Paid")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alreadyPaid", "alert('You have already paid for this product.');", true);
                            return;
                        }

                        if (requestStatus == "Pending")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "notAccepted", "alert('Payment is not allowed. Your request is still pending approval.');", true);
                            return;
                        }

                        if (requestStatus == "Rejected")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "rejected", "alert('Your order is rejected. You are not allowed to Proceed Further.');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "notFound", "alert('Request not found.');", true);
                        return;
                    }
                }
            }

            // Step 2: Update payment info
            string updateQuery = @"UPDATE RentProduct 
                           SET PaymentDate = @PaymentDate,
                               PaymentMethod = @PaymentMethod,
                               PaymentStatus = @PaymentStatus,
                               Status = 'Paid'
                           WHERE RentID = @RentID";

            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
            {
                cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@PaymentMethod", "Card");
                cmd.Parameters.AddWithValue("@PaymentStatus", "Paid");
                cmd.Parameters.AddWithValue("@RentID", rentId);

                cmd.ExecuteNonQuery();
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Payment successful!'); window.location='RequsetPage.aspx';", true);
    }

    //protected void btnDelete_Command(object sender, CommandEventArgs e)
    //{
    //    string[] keys = e.CommandArgument.ToString().Split('|');

    //    if (keys.Length != 3)
    //    {
    //        // Invalid data
    //        return;
    //    }

    //    string farmerId = keys[0];
    //    string productId = keys[1];
    //    string vendorId = keys[2];

    //    using (SqlConnection conn = new SqlConnection(connStr))
    //    {
    //        conn.Open();

    //        string deleteQuery = @"DELETE FROM RentProduct 
    //                           WHERE FarmerID = @FarmerID AND ProductID = @ProductID AND VendorID = @VendorID";

    //        using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
    //        {
    //            cmd.Parameters.AddWithValue("@FarmerID", farmerId);
    //            cmd.Parameters.AddWithValue("@ProductID", productId);
    //            cmd.Parameters.AddWithValue("@VendorID", vendorId);

    //            cmd.ExecuteNonQuery();
    //        }
    //    }

    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "deleteSuccess", "showSuccess();", true);
    //}


    protected void Delete_Click(object sender, EventArgs e)
    {
        string rentId = Session["RentID"]?.ToString();

        if (string.IsNullOrEmpty(rentId))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sessionError", "alert('Session expired. Please try again.');", true);
            return;
        }

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            // Check if already paid
            string statusQuery = "SELECT PaymentStatus FROM RentProduct WHERE RentID = @RentID";
            using (SqlCommand cmd = new SqlCommand(statusQuery, conn))
            {
                cmd.Parameters.AddWithValue("@RentID", rentId);
                var status = cmd.ExecuteScalar()?.ToString();

                if (string.Equals(status, "Paid", StringComparison.OrdinalIgnoreCase))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "paidPopup",
                        "alert('You can\\'t cancel the order because you already paid for it.');", true);
                    return;
                }
            }

            // Delete the record
            string deleteQuery = "DELETE FROM RentProduct WHERE RentID = @RentID";
            using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
            {
                cmd.Parameters.AddWithValue("@RentID", rentId);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "deleted", "alert('Request deleted successfully.'); window.location='RequsetPage.aspx';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "notFound", "alert('No matching request found.');", true);
                }
            }
        }
    }
}



