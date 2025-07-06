<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMasterPage.master" AutoEventWireup="true" CodeFile="UserAllProducts.aspx.cs" Inherits="User_UserAllProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 
<!-- Bootstrap 5 JS bundle (includes Popper) -->
     <style>
        .product-card {
            border: 1px solid #ddd;
            border-radius: 15px;
            padding: 16px;
            margin: 15px;
            text-align: center;
            box-shadow: 0 4px 10px rgba(0,0,0,0.1);
            background: #fff;
            transition: transform 0.3s ease;
        }

        .product-card:hover {
            transform: scale(1.03);
        }

        .product-image {
            max-width: 100%;
            height: 200px;
            object-fit: cover;
            border-radius: 12px;
        }

        .product-name {
            font-size: 20px;
            font-weight: bold;
            margin-top: 10px;
        }

        .product-price {
            color: green;
            font-size: 18px;
            margin: 5px 0;
        }

        .buy-button {
            display: inline-block;
            padding: 10px 20px;
            background: linear-gradient(45deg, #00c6ff, #0072ff);
            color: #fff;
            text-decoration: none;
            border-radius: 25px;
            font-weight: bold;
            transition: 0.3s ease;
        }

        .buy-button:hover {
            background: linear-gradient(45deg, #0072ff, #00c6ff);
            transform: scale(1.05);
        }

        .product-grid {
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
        }
    </style>
  <!-- Bootstrap 5 CSS -->
    <div class="product-grid">
        <asp:Repeater ID="rptProducts" runat="server" DataSourceID="SqlDataSource1"   >
            <ItemTemplate>
                <div class="product-card">
<img src='<%# "/Farmer/Productimg/" + Eval("ImagePath") %>' alt="Product Image" class="product-image" />

                    <div class="product-name"><%# Eval("ProductName") %></div>
                    <div class="product-price">₹ <%# Eval("Price") %></div>
<asp:Button ID="btnAddToCart" runat="server" Text="Add To Cart" 
    class="buy-button" 
    CommandArgument='<%# Eval("ProductID") %>' 
    CommandName="AddToCart" OnClick="btnAddToCart_Click"/>
         <a href='UserCart.aspx?UserID=<%# Eval("UserID") %>' class="buy-button">Go To Cart</a>

                    <a href='UserBuyProduct.aspx?ProductID=<%# Eval("ProductID") %>' class="buy-button">Buy Now</a>
      
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Agriculture %>" SelectCommand="SELECT * FROM [Product]"></asp:SqlDataSource>

        </div>


    <!-- Bootstrap Modal for Add to Cart -->
<!-- Add To Cart Modal -->
<div class="modal fade" id="addToCartModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Add to Cart</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <p><strong>Product:</strong> <asp:Label ID="lblProductName" runat="server" /></p>
        <p><strong>Price:</strong> ₹<asp:Label ID="lblPrice" runat="server" /></p>
        <p><strong>Shop:</strong> <asp:Label ID="lblShopName" runat="server" /></p>
        <%--<asp:TextBox ID="txtModalQuantity" runat="server" Text="1" CssClass="form-control" />--%>
        <asp:HiddenField ID="hfproductid" runat="server" />
      </div>
      <div class="modal-footer">
        <asp:Button ID="btnSaveCart" runat="server" CssClass="btn btn-primary" Text="Add to Cart" OnClick="btnSaveCart_Click" />
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>

</asp:Content>

