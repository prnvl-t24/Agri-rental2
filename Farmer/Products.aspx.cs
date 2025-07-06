using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System;

public partial class Farmer_Products : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            decimal maxPrice = GetMaxProductPrice(); // Query your DB
            ClientScript.RegisterStartupScript(this.GetType(), "maxPriceScript", $"var maxSliderPrice = {maxPrice};", true);
           
            BindProducts();
        }
    }
  
    private void BindProducts()
    {
        using (SqlConnection conn = new SqlConnection(conStr))
        {
            string query = @"
        SELECT 
            P.*, 
            V.FullNameOrBusinessName AS VendorName,
            C.CategoryName
        FROM 
            Product P
        INNER JOIN Vendor_Registration V ON P.VendorID = V.UserID
        INNER JOIN Equipment_Category C ON P.CategoryID = C.CategoryID";

            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            rptProducts.DataSource = dt;
            rptProducts.DataBind();
        }
    }
    protected void btnRent_Command(object sender, CommandEventArgs e)
    {
        string[] args = e.CommandArgument.ToString().Split('|');
        if (args.Length < 1)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid command arguments');", true);
            return;
        }

        int productId;
        if (!int.TryParse(args[0], out productId))
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid product ID');", true);
            return;
        }

        int quantity = 1;
        if (args.Length > 1)
            int.TryParse(args[1], out quantity);

        decimal price = 0m;
        if (args.Length > 2)
            decimal.TryParse(args[2], out price);

        if (Session["FarmerID"] == null)
        {
            Response.Redirect("~/Home/Login.aspx");
            return;
        }

        int farmerId = Convert.ToInt32(Session["FarmerID"]);
        int vendorId = 0;

        string connStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            // Check for existing pending request
            string checkQuery = @"SELECT COUNT(*) FROM RentProduct 
                              WHERE ProductID = @ProductID AND FarmerID = @FarmerID AND Status = 'Pending'";
            using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@ProductID", productId);
                checkCmd.Parameters.AddWithValue("@FarmerID", farmerId);
                int existing = (int)checkCmd.ExecuteScalar();

                if (existing > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "alert('You have already requested this product for renting!');", true);
                    return;
                }
            }

            // Get VendorID and Price from Product table
            string productQuery = "SELECT VendorID, Price FROM Product WHERE ProductID = @ProductID";
            using (SqlCommand cmd = new SqlCommand(productQuery, conn))
            {
                cmd.Parameters.AddWithValue("@ProductID", productId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        vendorId = Convert.ToInt32(reader["VendorID"]);
                        price = Convert.ToDecimal(reader["Price"]);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Product not found');", true);
                        return;
                    }
                }
            }

            // Calculate total amount (if needed)
            decimal totalAmount = price * quantity;

            // *** NO insertion here ***

            // Just redirect to RentRequest.aspx with product id
            Response.Redirect("RentRequest.aspx?pid=" + productId);
        }
    }

    private decimal GetMaxProductPrice()
    {
        decimal maxPrice = 100000;

        using (SqlConnection conn = new SqlConnection(conStr))
        {
            string query = "SELECT ISNULL(MAX(Price), 0) FROM Product";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                maxPrice = Convert.ToDecimal(result);
            }
        }

        return maxPrice;
    }



}




