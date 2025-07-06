using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;


public partial class User_UserDefault : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string Constr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(Constr)) // Use the connection string here
            {
                string query = "select FullNameOrBusinessName, Photo from Vendor_Registration where UserID = @vi";
                SqlCommand cmd = new SqlCommand(query, con);

                if (Session["UserID"] != null)
                {
                    cmd.Parameters.AddWithValue("@vi", Session["UserID"].ToString());
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        lblshow.Text = dr["FullNameOrBusinessName"].ToString();
                        string rawPath = dr["Photo"].ToString();

                        if (!string.IsNullOrEmpty(rawPath))
                        {
                            imgFarmer.ImageUrl = ResolveUrl("~/User/Vendorimg/" + rawPath);
                        }
                        else
                        {
                            imgFarmer.ImageUrl = ResolveUrl("~/User/Vendorimg/img.jpg");
                        }
                    }
                    dr.Close();
                }
                else
                {
                    Response.Redirect("~/Home/UserLogin.aspx"); // Redirect to login if session is null
                }
            }
        }
    }
}