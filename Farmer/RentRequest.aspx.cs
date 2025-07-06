using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Farmer_RentRequest : System.Web.UI.Page
{
    string connStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //LBLALERT.Text = "If you want to cancel please cancel before submit otherwise the order will not get canceled onces submitted here";
            int pid = Convert.ToInt32(Request.QueryString["pid"]);
            hfProductID.Value = pid.ToString();

            string connStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Price FROM Product WHERE ProductID = @pid", conn);
                cmd.Parameters.AddWithValue("@pid", pid);
                object priceObj = cmd.ExecuteScalar();
                if (priceObj != null)
                {
                    decimal price = Convert.ToDecimal(priceObj);
                    hfTotalAmount.Value = price.ToString(); // this is used by JS
                }
                conn.Close();
            }
            if (Request.QueryString["pid"] != null && int.TryParse(Request.QueryString["pid"], out pid))
            {
                hfProductID.Value = pid.ToString();
                LoadProductDetails(); // << Load details including VendorID
            }

            // Set default RentTime and RequestDate
            HiddenField3.Value = DateTime.Now.TimeOfDay.ToString();
            HiddenField1.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // RequestDate
        }
    }

    private void LoadProductDetails()
    {
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT ProductName, Price, VendorID FROM Product WHERE ProductID = @pid", conn);
            cmd.Parameters.AddWithValue("@pid", hfProductID.Value);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                lblProduct.Text = reader["ProductName"].ToString();
                hfTotalAmount.Value = reader["Price"].ToString(); // For JS price/day
                HiddenField2.Value = reader["VendorID"].ToString(); // VendorID
            }
            reader.Close();
        }
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        CalculateAmount();
    }

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        CalculateAmount();
    }
    private void CalculateAmount()
    {
        DateTime fromDate, toDate;
        if (!DateTime.TryParse(txtFromDate.Text, out fromDate) ||
            !DateTime.TryParse(txtToDate.Text, out toDate))
        {
            lblAmount.Text = "0.00";
            return;
        }

        int days = (toDate - fromDate).Days + 1;
        if (days <= 0)
        {
            lblAmount.Text = "0.00";
            return;
        }
        // Convert pricePerDay string to decimal:
        decimal pricePerDay;
        if (!decimal.TryParse(hfTotalAmount.Value, out pricePerDay))
        {
            lblAmount.Text = "0.00";
            return;
        }

        decimal total = days * pricePerDay;
        lblAmount.Text = total.ToString("0.00");

        // IMPORTANT: do NOT overwrite pricePerDay hidden field with total.
        // Use a separate hidden field for total amount.
        hfTotalCalculatedAmount.Value = total.ToString();  // New hidden field for total amount
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int productId;
        if (!int.TryParse(hfProductID.Value, out productId))
        {
            Label1.Text = "Product ID is invalid.";
            return;
        }

        int vendorId;
        if (!int.TryParse(HiddenField2.Value, out vendorId))
        {
            Label1.Text = "Vendor ID is invalid.";
            return;
        }

        int farmerId;
        if (Session["FarmerID"] == null || !int.TryParse(Session["FarmerID"].ToString(), out farmerId))
        {
            Label1.Text = "Farmer ID is missing or invalid.";
            return;
        }

        DateTime fromDate, toDate;
        if (!DateTime.TryParse(txtFromDate.Text, out fromDate) || !DateTime.TryParse(txtToDate.Text, out toDate))
        {
            Label1.Text = "Invalid From or To date.";
            return;
        }

        if (toDate < fromDate)
        {
            Label1.Text = "Invalid date range: 'To' date cannot be earlier than 'From' date.";
            return;
        }



        decimal totalAmount;
        if (string.IsNullOrEmpty(hfTotalCalculatedAmount.Value) ||
            !decimal.TryParse(hfTotalCalculatedAmount.Value, out totalAmount))
        {
            Label1.Text = "Invalid Total Amount.";
            return;
        }

        // ❗ Check availability
        if (!IsProductAvailable(productId, fromDate, toDate))
        {
            ShowAlert("This product is already rented during the selected dates. Please choose another date range.");
            return;
        }

        try
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"INSERT INTO RentProduct 
                (ProductID, FarmerID, VendorID, FromDate, ToDate, RentTime, 
                Status, RequestDate, PaymentStatus, PaymentMethod, PaymentDate, 
                TotalAmount) 
                VALUES 
                (@ProductID, @FarmerID, @VendorID, @FromDate, @ToDate, @RentTime, 
                @Status, @RequestDate, @PaymentStatus, @PaymentMethod, @PaymentDate, 
                @TotalAmount)", conn);

                cmd.Parameters.AddWithValue("@ProductID", productId);
                cmd.Parameters.AddWithValue("@FarmerID", farmerId);
                cmd.Parameters.AddWithValue("@VendorID", vendorId);
                cmd.Parameters.AddWithValue("@FromDate", fromDate);
                cmd.Parameters.AddWithValue("@ToDate", toDate);
                cmd.Parameters.AddWithValue("@RentTime", TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")));
                // cmd.Parameters.AddWithValue("@RentDays", rentDays);
                cmd.Parameters.AddWithValue("@Status", "Pending");
                cmd.Parameters.AddWithValue("@RequestDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@PaymentStatus", "Pending");
                cmd.Parameters.AddWithValue("@PaymentMethod", DBNull.Value);
                cmd.Parameters.AddWithValue("@PaymentDate", DBNull.Value);
                cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);

                cmd.ExecuteNonQuery();
                // your logic here...
                Label1.Text = "Request submitted successfully!";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "showPopup();", true);

            }
        }
        catch (Exception ex)
        {
            Label1.Text = "Error: " + ex.Message;
        }
    }




    private bool IsProductAvailable(int productId, DateTime fromDate, DateTime toDate, int? excludeRentId = null)
    {
        bool isAvailable = true;

        using (SqlConnection con = new SqlConnection(connStr))
        {
            con.Open();

            string query = @"
                SELECT COUNT(*) FROM RentProduct
                WHERE ProductID = @ProductID
                  AND Status NOT IN ('Cancelled', 'Rejected')
                  AND (
                        (@FromDate BETWEEN FromDate AND ToDate) OR
                        (@ToDate BETWEEN FromDate AND ToDate) OR
                        (FromDate BETWEEN @FromDate AND @ToDate) OR
                        (ToDate BETWEEN @FromDate AND @ToDate)
                      )";

            if (excludeRentId.HasValue)
            {
                query += " AND RentID <> @ExcludeRentID";
            }

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@ProductID", productId);
                cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = fromDate;
                cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = toDate;

                if (excludeRentId.HasValue)
                    cmd.Parameters.AddWithValue("@ExcludeRentID", excludeRentId.Value);

                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                    isAvailable = false;
            }
        }
        return isAvailable;
    }

    private void ShowAlert(string message)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('{message}');", true);
    }
    //protected void btnCancel_Click(object sender, EventArgs e)
    //{





    //    string script = "alert('Your rent request has been cancelled successfully.'); window.location='Products.aspx';";
    //    ClientScript.RegisterStartupScript(this.GetType(), "alertRedirect", script, true);
    //    // Optionally clear form fields here
    //}

}


