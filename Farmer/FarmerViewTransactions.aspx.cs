using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Farmer_FarmerViewTransactions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        if (!IsPostBack)
        {
            int farmerId = Convert.ToInt32(Session["FarmerID"]);
            LoadFarmerReport();
        }
    }
    private void LoadFarmerReport()
    {
        string cs = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;
        using (SqlConnection con = new SqlConnection(cs))
        {
            int farmerId = Convert.ToInt32(Session["FarmerID"]);
            DateTime fromDate = new DateTime(1753, 1, 1);
            DateTime toDate = new DateTime(9999, 12, 31);

            string query = @"
        SELECT
            rp.RentID,
            rp.FarmerID,
            rp.ProductID,
            p.ProductName,
            rp.FromDate,
            rp.ToDate,
            rp.RentDays,
            rp.TotalAmount,
            rp.Status,
            rp.PaymentMethod,
            vr.FullNameOrBusinessName AS VendorName,
            vr.MobileNumber AS VendorMobile
        FROM RentProduct rp
        INNER JOIN Vendor_Registration vr ON rp.VendorID = vr.UserID
        INNER JOIN Product p ON rp.ProductID = p.ProductID
        WHERE rp.FarmerID = @FarmerID AND rp.FromDate >= @FromDate AND rp.ToDate <= @ToDate
        ORDER BY rp.RentID DESC";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FarmerID", farmerId);
                cmd.Parameters.AddWithValue("@FromDate", fromDate);
                cmd.Parameters.AddWithValue("@ToDate", toDate);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
    }




    protected void btnFilter_Click(object sender, EventArgs e)
    {
        DateTime fromDate, toDate;

        if (DateTime.TryParse(txtFromDate.Text, out fromDate) && DateTime.TryParse(txtToDate.Text, out toDate))
        {

            int farmerId = Convert.ToInt32(Session["FarmerID"]);

            string query = @"
            SELECT 
                rp.RentID,
                rp.FarmerID,
                rp.ProductID,
                p.ProductName,
                rp.FromDate,
                rp.ToDate,
                rp.RentDays,
                rp.TotalAmount,
                rp.Status,
                rp.PaymentMethod,
                vr.FullNameOrBusinessName AS VendorName,
                vr.MobileNumber AS VendorMobile
            FROM RentProduct rp
            INNER JOIN Vendor_Registration vr ON rp.VendorID = vr.UserID
            INNER JOIN Product p ON rp.ProductID = p.ProductID
            WHERE rp.FarmerID = @FarmerID AND rp.FromDate >= @FromDate AND rp.ToDate <= @ToDate
            ORDER BY rp.RentID DESC";

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@FarmerID", farmerId);
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);

                    SqlDataAdapter da = new SqlDataAdapter(cmd); // ✅ Pass SqlCommand here
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }
    }

}