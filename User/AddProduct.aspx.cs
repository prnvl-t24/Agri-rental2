using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class User_AddProduct : System.Web.UI.Page
{
    string conStr = ConfigurationManager.ConnectionStrings["AgroRentalDB"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCategories();
            BindProductGrid();
        }
    }

    private void LoadCategories()
    {
        using (SqlConnection con = new SqlConnection(conStr))
        {
            SqlCommand cmd = new SqlCommand("SELECT CategoryID, CategoryName FROM Equipment_Category Where VendorID=@vi", con);
            con.Open();
            cmd.Parameters.AddWithValue("@vi", Session["UserID"].ToString());
            ddlCategory.DataSource = cmd.ExecuteReader();
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-- Select Category --", "0"));
        }
    }

    private void BindProductGrid()
    {
        int vendorId = Convert.ToInt32(Session["UserID"]);

        using (SqlConnection con = new SqlConnection(conStr))
        {
            string query = @"
            SELECT P.ProductID, P.ProductName, P.Description, P.Price,  C.CategoryName 
            FROM Product P 
            INNER JOIN Equipment_Category C ON P.CategoryID = C.CategoryID 
            WHERE P.VendorID = @VendorID
            ORDER BY P.ProductID DESC";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@VendorID", vendorId);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvProducts.DataSource = dt;
            gvProducts.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedIndex == 0)
        {
            lblMessage.Text = "Select category.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
            return;
        }
        int vendorId = Convert.ToInt32(Session["UserID"]);
        using (SqlConnection con = new SqlConnection(conStr))
        {
            if(fuphoto.HasFile)
            {
                string ppath = Server.MapPath("/User/PRODUCTIMG/"+fuphoto.FileName);
                fuphoto.SaveAs(ppath);
              
            }
            con.Open();
            SqlCommand cmd;

            if (hfProductID.Value == "")
            {
                // Insert
                cmd = new SqlCommand(@"INSERT INTO Product (CategoryID, ProductName, Description, Price,image,VendorID) 
                                       VALUES (@CatID, @Name, @Desc, @Price,@img,@vi)", con);
            }
            else
            {
                // Update
                cmd = new SqlCommand(@"UPDATE Product SET CategoryID=@CatID, ProductName=@Name, 
                                       Description=@Desc, Price=@Price,image=@img
                                       WHERE ProductID=@PID", con);
                cmd.Parameters.AddWithValue("@PID", hfProductID.Value);
            }

            cmd.Parameters.AddWithValue("@CatID", ddlCategory.SelectedValue);
            cmd.Parameters.AddWithValue("@Name", txtProductName.Text.Trim());
            cmd.Parameters.AddWithValue("@Desc", txtDescription.Text.Trim());
            cmd.Parameters.AddWithValue("@Price", txtPrice.Text.Trim());
            cmd.Parameters.AddWithValue("@img", fuphoto.FileName);
            cmd.Parameters.AddWithValue("@vi", vendorId);

            cmd.ExecuteNonQuery();
        }

        //lblMessage.Text = hfProductID.Value == "" ? "Product added!" : "Product updated!";
        //lblMessage.ForeColor = System.Drawing.Color.Green;
        string message = hfProductID.Value == "" ? "Product added successfully!" : "Product updated successfully!";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", $"alert('{message}');", true);

        ResetForm();
        BindProductGrid();
    }

    protected void gvProducts_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        GridViewRow row = gvProducts.Rows[index];
        string productId = gvProducts.DataKeys[index].Value.ToString();

        if (e.CommandName == "EditRow")
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Product WHERE ProductID=@PID and VendorID=@vi", con);
                cmd.Parameters.AddWithValue("@PID", productId);
                cmd.Parameters.AddWithValue("@vi", Session["UserID"].ToString());
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    hfProductID.Value = productId;
                    ddlCategory.SelectedValue = dr["CategoryID"].ToString();
                    txtProductName.Text = dr["ProductName"].ToString();
                    txtDescription.Text = dr["Description"].ToString();
                    txtPrice.Text = dr["Price"].ToString();

                    // Set image preview
                    string imageName = dr["image"].ToString();
                    if (!string.IsNullOrEmpty(imageName))
                    {
                        imgPreview.ImageUrl = "~/User/PRODUCTIMG/" + imageName;
                        imgPreview.Visible = true;
                    }
                    else
                    {
                        imgPreview.Visible = false;
                    }

                    btnSave.Text = "Update Product";
                    btnCancel.Visible = true;
                }
            }
        }
        else if (e.CommandName == "DeleteRow")
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Product WHERE ProductID=@PID", con);
                cmd.Parameters.AddWithValue("@PID", productId);
                con.Open();
                cmd.ExecuteNonQuery();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Product deleted successfully!');", true);

            BindProductGrid();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetForm();
    }

    private void ResetForm()
{
    hfProductID.Value = "";
    ddlCategory.SelectedIndex = 0;
    txtProductName.Text = "";
    txtDescription.Text = "";
    txtPrice.Text = "";
    imgPreview.Visible = false;

    btnSave.Text = "Add Product";
    btnCancel.Visible = false;
}

}



