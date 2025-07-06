using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;



public partial class Farmer_ViewStatus : System.Web.UI.Page
{
    string connStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadOrders();
            BindOrders();
        }
    }
    void LoadOrders()
    {
        if (Session["FarmerID"] == null) Response.Redirect("~/Home/FarmerLogin.aspx");

        string farmerId = Session["FarmerID"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM RentProduct WHERE FarmerID = @FarmerID", conn);
            da.SelectCommand.Parameters.AddWithValue("@FarmerID", farmerId);
            DataTable dt = new DataTable();
            da.Fill(dt);
            rptOrders.DataSource = dt;
            rptOrders.DataBind();
        }
    }

    protected void rptOrders_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Pay")
        {
            string[] args = e.CommandArgument.ToString().Split('|');

            if (args.Length < 3)
            {
                // Log or show a meaningful error
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid payment details. Please try again.');", true);
                return;
            }

            string rentId = args[0];
            string productId = args[1];
            decimal amount = 0;

            // Try parsing the amount, fallback to fetching from DB
            if (!decimal.TryParse(args[2], out amount))
            {
                // Optional fallback: Fetch from DB
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    string query = "SELECT Price FROM Product WHERE ProductID = @ProductID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        con.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                            amount = Convert.ToDecimal(result);
                    }
                }
            }

            hfRentID.Value = rentId;
            txtAmount.Text = amount.ToString("F2"); // Format to 2 decimal places
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
        }
    }


    protected void btnConfirmPayment_Click(object sender, EventArgs e)
    {
        string rentId = hfRentID.Value;
        string method = "Card";
        DateTime paymentDate = DateTime.Now;
        string amount = txtAmount.Text;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();
            string updateQuery = @"UPDATE RentProduct 
                                   SET PaymentStatus = 'Paid', 
                                       PaymentMethod = @Method, 
                                       PaymentDate = @Date, 
                                     
                                   WHERE RentID = @RentID";

            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
            {
                cmd.Parameters.AddWithValue("@Method", method);
                cmd.Parameters.AddWithValue("@Date", paymentDate);
                cmd.Parameters.AddWithValue("@RentID", rentId);
                cmd.ExecuteNonQuery();
            }
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Payment successful.');", true);
        LoadOrders();
        paymentModal.Visible = true;
    }
    private void BindOrders()
    {
        string farmerId = Session["FarmerID"].ToString(); // Adjust based on your logic

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString))
        {
            string query = @"
            SELECT r.RentID, r.ProductID, r.FromDate, r.ToDate, r.Status, r.PaymentStatus,
                   r.PaymentMethod, r.PaymentDate, p.Price AS PaymentAmount
            FROM RentProduct r
            INNER JOIN Product p ON r.ProductID = p.ProductID
            WHERE r.FarmerID = @FarmerID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FarmerID", farmerId);
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                rptOrders.DataSource = rdr;
                rptOrders.DataBind();
            }
        }
    }

}
