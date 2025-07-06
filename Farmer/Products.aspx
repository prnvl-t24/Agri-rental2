<%@ Page Title="" Language="C#" MasterPageFile="~/Farmer/FarmerMasterPage.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Farmer_Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style>
    .product-card { margin-bottom: 30px; }
    .modal-img { width: 100%; height: 200px; object-fit: cover; }
    .input-icon-small {
    padding: 2px 6px;
    font-size: 0.8rem;
    height: auto;
    line-height: 1;
}
    .product-card {
            margin-bottom: 30px;
        }
        .modal-img {
            width: 100%;
            height: 200px;
            object-fit: cover;
        }

</style>
    <!-- noUiSlider CSS -->
<link href="https://cdnjs.cloudflare.com/ajax/libs/noUiSlider/15.7.0/nouislider.min.css" rel="stylesheet" />
<!-- noUiSlider JS -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/noUiSlider/15.7.0/nouislider.min.js"></script>

  <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<script type="text/javascript">
    $(document).ready(function () {
        $('#<%= txtSearch.ClientID %>').on("keyup", function () {
            var value = $(this).val().toLowerCase();

            $(".product-card").each(function () {
                var cardText = $(this).text().toLowerCase();
                if (cardText.indexOf(value) > -1) {
                    $(this).closest(".col-md-4").show();
                } else {
                    $(this).closest(".col-md-4").hide();
                }
            });
        });
    });
</script>
<div class="container py-5">
   <center> <h3 class="mb-4">Available Products</h3></center>
     <div class="d-flex justify-content-center mb-3">
    <div class="input-group" style="max-width: 200px;">
<asp:TextBox ID="txtSearch" runat="server" CssClass="form-control mb-3" placeholder="Search Product..." />
       <span class="input-group-text bg-primary text-white input-icon-small">
    <i class="fa fa-search"></i>
</span>

    </div>
</div>
   <!-- Price Range Slider Card -->
   
<div class="card border-0 shadow rounded-4" style="background: linear-gradient(to right, #4b2c79, #b23a94); max-width: 320px; margin: 0 auto;">

    <div class="card-body p-3 d-flex align-items-center gap-2">
        <div class="rounded-circle bg-white p-2 shadow-sm">
            <i class="fa fa-filter text-dark fs-5"></i>
        </div>
        <select id="ddlPriceRange" class="form-select border-0 shadow-sm fw-semibold text-dark"
                style="background-color: #ffffff; border-radius: 10px; font-size: 0.9rem;">
           <option value="">🎯 Filter By Price</option>
            <option value="0">🟢 All Prices</option>
            <option value="1">💸 Under ₹1000</option>
            <option value="2">💰 ₹1001 - ₹5000</option>
            <option value="3">💼 ₹5001 - ₹10000</option>
            <option value="4">🏆 Above ₹10000</option>
        </select>
    </div>
</div>
    <br />

 <div class="row">
        <asp:Repeater ID="rptProducts" runat="server">
            <ItemTemplate>
                <div class="col-md-4">
                    <div class="card product-card">
                       
                        <img src='<%# "../User/PRODUCTIMG/" + Eval("image") %>' class="card-img-top" alt="Product Image" style="height: 200px; object-fit: cover;" />
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("ProductName") %></h5>
   <p><strong>Vendor:</strong> <%# Eval("VendorName") %></p>

                           <p class="card-text price" data-price='<%# Eval("Price") %>'>
    <strong>Price:</strong> ₹<%# Eval("Price") %>
</p>      <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#quickViewModal<%# Eval("ProductID") %>">
                                Quick View
                            </button>
                        </div>
                    </div>
                </div>

                <!-- Modal -->
                <div class="modal fade" id="quickViewModal<%# Eval("ProductID") %>" tabindex="-1" aria-labelledby="modalLabel<%# Eval("ProductID") %>" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="modalLabel<%# Eval("ProductID") %>"><%# Eval("ProductName") %></h5>
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body row">
                                <div class="col-md-6">
                                    <img src='<%# "../User/PRODUCTIMG/" + Eval("image") %>' class="modal-img" />
                                </div>
                                <div class="col-md-6">
                                    <p ><strong>Vendor:</strong> <%# Eval("VendorName") %></p>
                                    <p><strong>Description:</strong> <%# Eval("Description") %></p>
                                    <p><strong>Price:</strong> ₹<%# Eval("Price") %></p>
                                    <asp:LinkButton ID="btnRent" runat="server" CssClass="btn btn-success"
                                        CommandName="Rent" CommandArgument='<%# Eval("ProductID") %>'
                                        OnCommand="btnRent_Command">
                                        Rent Product
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>   
    </div>

<!-- Separate Repeater for Modals -->
<%--<asp:Repeater ID="rptModals" runat="server">
    <ItemTemplate>
        <div class="modal fade" id="quickViewModal<%# Eval("ProductID") %>" tabindex="-1" aria-labelledby="quickViewModalLabel<%# Eval("ProductID") %>" aria-hidden="true">
            <div class="modal-dialog modal-lg modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="quickViewModalLabel<%# Eval("ProductID") %>"><%# Eval("ProductName") %></h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <img src='<%# "../User/PRODUCTIMG/" + Eval("image") %>' class="modal-img mb-3" alt="Product Image" />
                        <p><strong>Description:</strong> <%# Eval("Description") %></p>
                        <div class="mb-3">
                            <asp:HiddenField ID="hdnPrice" runat="server" Value='<%# Eval("Price") %>' />
                            <div class="mb-2">
                                <p><strong>Total Amount: ₹</strong>
                                    <span id="totalAmount<%# Eval("ProductID") %>"><%# Eval("Price") %></span>
                                </p>
                            </div>
                            <asp:LinkButton ID="btnRent" runat="server" CssClass="btn btn-success"
                                CommandName="Rent" CommandArgument='<%# Eval("ProductID") %>'
                                OnCommand="btnRent_Command">Rent Product</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>--%>
   


<!-- Bootstrap 5 CSS -->

<!-- Bootstrap Bundle with Popper (required for modals) -->
<!-- Include noUiSlider JS -->
<script src="https://cdn.jsdelivr.net/npm/nouislider@15.6.1/dist/nouislider.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Text Search
            $('#<%= txtSearch.ClientID %>').on("keyup", function () {
            var value = $(this).val().toLowerCase();

            $(".product-card").each(function () {
                var cardText = $(this).text().toLowerCase();
                if (cardText.indexOf(value) > -1) {
                    $(this).closest(".col-md-4").show();
                } else {
                    $(this).closest(".col-md-4").hide();
                }
            });
        });

        // Price Range Filter
        $('#ddlPriceRange').on("change", function () {
            var selectedRange = $(this).val();

            $(".product-card").each(function () {
                var price = parseFloat($(this).find(".price").data("price"));

                var show = false;
                if (selectedRange === "0" || selectedRange === "") {
                    show = true;
                } else if (selectedRange === "1" && price <= 1000) {
                    show = true;
                } else if (selectedRange === "2" && price > 1000 && price <= 5000) {
                    show = true;
                } else if (selectedRange === "3" && price > 5000 && price <= 10000) {
                    show = true;
                } else if (selectedRange === "4" && price > 10000) {
                    show = true;
                }

                if (show) {
                    $(this).closest(".col-md-4").show();
                } else {
                    $(this).closest(".col-md-4").hide();
                }
            });
        });
    });
    </script>


</asp:Content>

