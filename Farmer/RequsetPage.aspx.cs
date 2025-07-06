using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Farmer_RequsetPage : System.Web.UI.Page
{
    string connStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadRequestStatus();
            BindRequestData();
        }
    }

    private void LoadRequestStatus()
    {
        int farmerId = Convert.ToInt32(Session["FarmerID"]);

        using (SqlConnection con = new SqlConnection(connStr))
        {
            string query = @"
SELECT 
    rp.RentID, rp.TotalAmount As PayableAmount, rp.FromDate, rp.FarmerID, rp.VendorID, rp.ProductID, rp.ToDate, rp.Status, rp.RequestDate,
    p.ProductName, p.Image,
    v.FullNameOrBusinessName AS VendorName,
    v.MobileNumber AS VendorMobile
FROM RentProduct rp
INNER JOIN Product p ON rp.ProductID = p.ProductID
INNER JOIN Vendor_Registration v ON rp.VendorID = v.UserID
WHERE rp.FarmerID = @FarmerID
ORDER BY rp.RequestDate DESC";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@FarmerID", farmerId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            rptRequestStatus.DataSource = dt;
            rptRequestStatus.DataBind();
        }
    }
    protected void btnDelete_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName != "DeleteOrder")
            return;
        string[] keys = e.CommandArgument.ToString().Split('|');
        if (keys.Length != 4)
        {
            return;
        }


        int rentId = Convert.ToInt32(keys[0]);
        int farmerId = Convert.ToInt32(keys[1]);
        int productId = Convert.ToInt32(keys[2]);
        int vendorId = Convert.ToInt32(keys[3]);

       
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string statusQuery = "SELECT PaymentStatus FROM RentProduct WHERE RentID = @RentID";
                using (SqlCommand cmd = new SqlCommand(statusQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@RentID", rentId);
                    var status = cmd.ExecuteScalar()?.ToString();

                    if (string.Equals(status, "paid", StringComparison.OrdinalIgnoreCase))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "paidPopup",
                            "alert('You can\\'t cancel the order because you already paid for it.');", true);
                        return;
                    }
                }

                string deleteQuery = "DELETE FROM RentProduct WHERE RentID = @RentID";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@RentID", rentId);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "deleteSuccess",
                            "alert('Order deleted successfully.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "deleteFail",
                            "alert('Order delete failed.');", true);
                    }
                }
            }

            LoadRequestStatus();
        
       
    }

    public string GetStatusCss(string status)
    {
        switch (status.ToLower())
        {
            case "pending":
                return "status-pending";
            case "accepted":
                return "status-accepted";
            case "rejected":
                return "status-rejected";
            case "paid":
                return "status-paid";
            default:
                return "status-pending";
        }
    }
    private void BindRequestData()
    {
        string farmerId = Session["FarmerID"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = @"
SELECT 
    rp.RentID,rp.TotalAmount As PayableAmount, rp.FromDate, rp.FarmerID, rp.VendorID, rp.ProductID, rp.ToDate, rp.Status, rp.RequestDate,
    p.ProductName, p.Image,
    v.FullNameOrBusinessName AS VendorName,
    v.MobileNumber AS VendorMobile
FROM RentProduct rp
INNER JOIN Product p ON rp.ProductID = p.ProductID
INNER JOIN Vendor_Registration v ON rp.VendorID = v.UserID
WHERE rp.FarmerID = @FarmerID
ORDER BY rp.RequestDate DESC";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@FarmerID", farmerId);
                DataTable dt = new DataTable();

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                    rptRequestStatus.DataSource = dt;
                    rptRequestStatus.DataBind();
                }
            }
        }
    }
    //protected void btnPay_Command(object sender, CommandEventArgs e)
    //{
    //    string[] keys = e.CommandArgument.ToString().Split('|');
    //    if (keys.Length != 3) return;

    //    string farmerId = keys[0];
    //    string productId = keys[1];
    //    string vendorId = keys[2];

    //    using (SqlConnection conn = new SqlConnection(connStr))
    //    {
    //        conn.Open();

    //        // First, check payment and request status
    //        string checkQuery = @"SELECT PaymentStatus, Status FROM RentProduct 
    //                          WHERE FarmerID = @FarmerID AND ProductID = @ProductID AND VendorID = @VendorID";

    //        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
    //        {
    //            checkCmd.Parameters.AddWithValue("@FarmerID", farmerId);
    //            checkCmd.Parameters.AddWithValue("@ProductID", productId);
    //            checkCmd.Parameters.AddWithValue("@VendorID", vendorId);

    //            using (SqlDataReader reader = checkCmd.ExecuteReader())
    //            {
    //                if (reader.Read())
    //                {
    //                    string paymentStatus = reader["PaymentStatus"]?.ToString();
    //                    string requestStatus = reader["Status"]?.ToString();

    //                    if (paymentStatus == "Paid")
    //                    {
    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alreadyPaid", "alert('You have already paid for this product.');", true);
    //                        return;
    //                    }

    //                    if (requestStatus == "Pending")
    //                    {
    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "notAccepted", "alert('Payment is not allowed. Your request is still pending approval.');", true);
    //                        return;
    //                    }
    //                    if (requestStatus == "Rejected")
    //                    {
    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "notAccepted", "alert('Your order is rejected. You are not allowed to Proceed Further.');", true);
    //                        return;
    //                    }
    //                }
    //                else
    //                {
    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "notFound", "alert('Request not found.');", true);
    //                    return;
    //                }
    //            }
    //        }

    //        // Now allow payment update
    //        string updateQuery = @"UPDATE RentProduct 
    //                           SET PaymentDate = @PaymentDate,
    //                               PaymentMethod = @PaymentMethod,
    //                               PaymentStatus = @PaymentStatus,
    //                               Status = 'Paid'
    //                           WHERE FarmerID = @FarmerID AND ProductID = @ProductID AND VendorID = @VendorID";

    //        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
    //        {
    //            cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
    //            cmd.Parameters.AddWithValue("@PaymentMethod", "Card");
    //            cmd.Parameters.AddWithValue("@PaymentStatus", "Paid");
    //            cmd.Parameters.AddWithValue("@FarmerID", farmerId);
    //            cmd.Parameters.AddWithValue("@ProductID", productId);
    //            cmd.Parameters.AddWithValue("@VendorID", vendorId);

    //            cmd.ExecuteNonQuery();
    //        }

    //        BindRequestData();
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "paymentSuccess", "alert('Payment successful.');", true);
    //    }
    //}

    //protected void btnPay_Command(object sender, CommandEventArgs e)
    //{
    //    string[] keys = e.CommandArgument.ToString().Split('|');
    //    if (keys.Length != 3) return;

    //    string farmerId = keys[0];
    //    string productId = keys[1];
    //    string vendorId = keys[2];

    //    using (SqlConnection conn = new SqlConnection(connStr))
    //    {
    //        conn.Open();

    //        string checkQuery = @"SELECT PaymentStatus, Status FROM RentProduct 
    //                          WHERE FarmerID = @FarmerID AND ProductID = @ProductID AND VendorID = @VendorID";

    //        using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
    //        {
    //            checkCmd.Parameters.AddWithValue("@FarmerID", farmerId);
    //            checkCmd.Parameters.AddWithValue("@ProductID", productId);
    //            checkCmd.Parameters.AddWithValue("@VendorID", vendorId);

    //            using (SqlDataReader reader = checkCmd.ExecuteReader())
    //            {
    //                if (reader.Read())
    //                {
    //                    string paymentStatus = reader["PaymentStatus"]?.ToString();
    //                    string requestStatus = reader["Status"]?.ToString();

    //                    if (paymentStatus == "Paid")
    //                    {
    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alreadyPaid", "alert('You have already paid for this product.');", true);
    //                        return;
    //                    }

    //                    if (requestStatus == "Pending")
    //                    {
    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "notAccepted", "alert('Payment is not allowed. Your request is still pending approval.');", true);
    //                        return;
    //                    }

    //                    if (requestStatus == "Rejected")
    //                    {
    //                        ScriptManager.RegisterStartupScript(this, this.GetType(), "rejected", "alert('Your order is rejected. You are not allowed to Proceed Further.');", true);
    //                        return;
    //                    }
    //                }
    //                else
    //                {
    //                    ScriptManager.RegisterStartupScript(this, this.GetType(), "notFound", "alert('Request not found.');", true);
    //                    return;
    //                }
    //            }
    //        }

    //        // Set sessions only after validationaa
    //        Session["FarmerID"] = farmerId;
    //        Session["ProductID"] = productId;
    //        Session["VendorID"] = vendorId;

    //        // Redirect to payment gateway
    //        Response.Redirect("PaymentGateway.aspx");
    //    }
    //}
    protected void btnPay_Command(object sender, CommandEventArgs e)
    {
        string[] values = e.CommandArgument.ToString().Split('|');
        if (values.Length < 4)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "invalidArgs", "alert('Invalid command argument.');", true);
            return;
        }

        // Parse values
        int rentId = int.Parse(values[0]);
        int farmerId = int.Parse(values[1]);
        int productId = int.Parse(values[2]);
        int vendorId = int.Parse(values[3]);

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string checkQuery = @"SELECT PaymentStatus, Status, FarmerID, ProductID, VendorID 
                              FROM RentProduct WHERE RentID = @RentID";

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

                        // Store needed data
                        Session["FarmerID"] = reader["FarmerID"].ToString();
                        Session["ProductID"] = reader["ProductID"].ToString();
                        Session["VendorID"] = reader["VendorID"].ToString();
                        Session["RentID"] = rentId;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "notFound", "alert('Request not found.');", true);
                        return;
                    }
                }
            }

            // Redirect to payment gateway
            Response.Redirect("PaymentGateway.aspx");
        }
    }

}