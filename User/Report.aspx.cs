using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

public partial class User_Report : System.Web.UI.Page
{
    string cs = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        if (!IsPostBack)
        {
            LoadSummary();
            LoadGridData();
           
                LoadGridData();  // Load all data initially or you can load default date range
            
        }
    }

    private void LoadSummary()
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("../Home/VendorLogin.aspx");
            return;
        }
        int vendorId = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = @"
            SELECT 
                COUNT(*) AS TotalRentals,
                SUM(CAST(rp.TotalAmount AS FLOAT)) AS TotalRevenue
            FROM RentProduct rp
            INNER JOIN Product p ON rp.ProductID = p.ProductID
            WHERE p.VendorID = @VendorID";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@VendorID", vendorId);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                lblTotalRentals.Text = reader["TotalRentals"].ToString();
                lblTotalRevenue.Text = "₹" + reader["TotalRevenue"].ToString();
            }
            reader.Close();
        }
    }

    private void LoadGridData()
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("../Home/VendorLogin.aspx");
            return;
        }

        int vendorId = Convert.ToInt32(Session["UserID"]);
        string cs = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = @"
            SELECT 
                rp.RentID, 
                rp.ProductID, 
                p.ProductName,
                rp.FarmerID, 
                fr.FullName AS FarmerName, 
                fr.MobileNumber,
                rp.FromDate, 
                rp.ToDate, 
                rp.RentDays,
                rp.TotalAmount, 
                rp.Status, 
                rp.PaymentMethod
            FROM RentProduct rp
            INNER JOIN Farmer_Registration fr ON rp.FarmerID = fr.FarmerID
            INNER JOIN Product p ON rp.ProductID = p.ProductID
            WHERE p.VendorID = @VendorID  -- 🔐 Filter by current vendor
            ORDER BY rp.RentID DESC";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@VendorID", vendorId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
    private void LoadGridData(DateTime? fromDate = null, DateTime? toDate = null)
    {
        if (Session["UserID"] == null)
        {
            Response.Redirect("../Home/VendorLogin.aspx");  // or your vendor login page
            return;
        }

        int vendorId = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = new SqlConnection(cs))
        {
            string query = @"
            SELECT 
                rp.RentID,
                p.ProductName,
                fr.FullName AS FarmerName,
                fr.MobileNumber,
                rp.FromDate,
                rp.ToDate,
                rp.TotalAmount,
                rp.Status,
                rp.PaymentMethod
            FROM RentProduct rp
            INNER JOIN Farmer_Registration fr ON rp.FarmerID = fr.FarmerID
            INNER JOIN Product p ON rp.ProductID = p.ProductID
            WHERE p.VendorID = @VendorID  -- Filter by VendorID in Product table
              AND (@FromDate IS NULL OR rp.FromDate >= @FromDate)
              AND (@ToDate IS NULL OR rp.ToDate <= @ToDate)
            ORDER BY rp.FromDate";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@VendorID", vendorId);
            cmd.Parameters.AddWithValue("@FromDate", (object)fromDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ToDate", (object)toDate ?? DBNull.Value);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();

            // Update summary labels
            lblTotalRentals.Text = dt.Rows.Count.ToString();
            decimal totalRevenue = 0;
            foreach (DataRow row in dt.Rows)
            {
                totalRevenue += Convert.ToDecimal(row["TotalAmount"]);
            }
            lblTotalRevenue.Text = totalRevenue.ToString("C");
        }
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DateTime fromDate, toDate;

        bool isFromDateValid = DateTime.TryParse(txtFromDate.Text, out fromDate);
        bool isToDateValid = DateTime.TryParse(txtToDate.Text, out toDate);

        if (isFromDateValid && isToDateValid)
        {
            LoadGridData(fromDate, toDate);
        }
        else
        {
            // Show error message if dates are invalid
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please enter valid From and To dates.');", true);
        }
    }
}
