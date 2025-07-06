using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class User_UserAllProducts : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["Agriculture"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            BindProducts();
        }
    }
    private void BindProducts()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["Agriculture"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ProductName, Price, Description, ImagePath FROM ProductDetails", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Columns.Contains("ImagePath"))
                {
                    rptProducts.DataSource = dt;
                    rptProducts.DataBind();
                }
                else
                {
                    // Optionally show error or log this.
                    Response.Write("<script>alert('ImagePath column not found in the result set');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
            }
            finally
            {
                con.Close();
            }
        }
    }
    protected void btnAddToCart_Click(object sender, EventArgs e)
    {

        Button btn = (Button)sender;
        int productId = Convert.ToInt32(btn.CommandArgument);
        hfproductid.Value = productId.ToString();

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Agriculture"].ConnectionString))
        {
            string query = "SELECT ProductName, ShopName, Price FROM Product WHERE ProductID = @ProductID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ProductID", productId);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                lblProductName.Text = rdr["ProductName"].ToString();
                lblShopName.Text = rdr["ShopName"].ToString();
                lblPrice.Text = rdr["Price"].ToString();
            }
            con.Close();
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", @"
    var myModal = new bootstrap.Modal(document.getElementById('addToCartModal'));
    myModal.show();
", true);

    }

    protected void btnSaveCart_Click(object sender, EventArgs e)
    {
        int productid = Convert.ToInt32(hfproductid.Value);
        //int quantity = 1;
        //int.TryParse(txtModalQuantity.Text.Trim(), out quantity);
        //if (quantity < 1) quantity = 1;




        // Initialize
        int userId = 0;
        string role = "";

        // Check if logged in as User or Farmer
        if (Session["UserRole"] != null)
        {
            role = Session["UserRole"].ToString().ToLower();

            if (role == "user")
            {
                userId = Convert.ToInt32(Session["UserID"]);
            }
            else if (role == "farmer")
            {
                userId = Convert.ToInt32(Session["FarmerID"]);  // Assuming FarmerID is stored separately
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Invalid role.');", true);
                return;
            }
        }



        using (SqlConnection con = new SqlConnection(conStr))
        {
            con.Open();

            string query = "SELECT ProductName, ShopName, Price, FarmerID FROM Product WHERE ProductID = @ProductID";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ProductID", productid);

            SqlDataReader dr = cmd.ExecuteReader();

            string productName = "";
            string shopName = "";
            decimal price = 0;
            int farmerId = 0;

            if (dr.Read())
            {
                productName = dr["ProductName"].ToString();
                shopName = dr["ShopName"].ToString();
                price = Convert.ToDecimal(dr["Price"]);
                farmerId = Convert.ToInt32(dr["FarmerID"]);
            }
            dr.Close();

            // decimal total = price * quantity;

            string checkQuery = "SELECT COUNT(*) FROM Cart WHERE UserID=@UserID AND ProductID=@ProductID";
            SqlCommand checkCmd = new SqlCommand(checkQuery, con);
            checkCmd.Parameters.AddWithValue("@UserID", userId);
            checkCmd.Parameters.AddWithValue("@ProductID", productid);

            int count = (int)checkCmd.ExecuteScalar();

            if (count > 0)
            {

            }
            else
            {
                string insertQuery = @"INSERT INTO Cart (UserID, ProductID, ProductName, ShopName, Price, AddedDate, FarmerID)
                                       VALUES (@UserID, @ProductID, @ProductName, @ShopName, @Price, GETDATE(), @FarmerID)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                insertCmd.Parameters.AddWithValue("@UserID", userId);
                insertCmd.Parameters.AddWithValue("@ProductID", productid);
                insertCmd.Parameters.AddWithValue("@ProductName", productName);
                insertCmd.Parameters.AddWithValue("@ShopName", shopName);
                // insertCmd.Parameters.AddWithValue("@Quantity", quantity);
                insertCmd.Parameters.AddWithValue("@Price", price);
                // insertCmd.Parameters.AddWithValue("@Total", total);
                insertCmd.Parameters.AddWithValue("@FarmerID", farmerId);
                insertCmd.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, GetType(), "popup", "alert('Product added to cart!');", true);

                //txtModalQuantity.Text = "1";

            }

        }




    }
}