<%@ Page Title="" Language="C#" MasterPageFile="~/Home/HomeMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<asp:Content  ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="text-center mb-4">
<%--        <img src="../Images/logo.jpg" alt="AgriRental Logo" class="rounded-circle mb-3" style="width: 100px; height: 100px; border: 3px solid #2d6a4f;" />--%>
        <h2>Welcome to AgriRental 🚜</h2>
        <p class="lead">Empowering Farmers by Connecting Them with Trusted Machine Vendors Across India.</p>
    </div>

    <!-- Carousel -->
    <div id="agriCarousel" class="carousel slide mb-5" data-bs-ride="carousel">
        <div class="carousel-inner rounded shadow">
            <div class="carousel-item active">
                <img src="../Images/img7.png" class="d-block w-100" alt="Farm Equipment Rental">
            </div>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#agriCarousel" data-bs-slide="prev">
            <span class="carousel-control-prev-icon"></span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#agriCarousel" data-bs-slide="next">
            <span class="carousel-control-next-icon"></span>
        </button>
    </div>

    <!-- Features Section -->
<%--    <div class="row text-center mb-5">
        <div class="col-md-4">
            <img src="../Images/agri1.jpg" class="img-fluid mb-2" style="height: 120px;" />
            <h5>Rent Machines Easily</h5>
            <p>Farmers can browse and rent a wide range of agricultural equipment directly from verified vendors.</p>
        </div>
        <div class="col-md-4">
            <img src="../Images/agri2.jpg" class="img-fluid mb-2" style="height: 120px;" />
            <h5>Vendor Marketplace</h5>
            <p>Vendors list their machines, set prices, and manage rentals efficiently from one central platform.</p>
        </div>
        <div class="col-md-4">
            <img src="../Images/agri3.jpg" class="img-fluid mb-2" style="height: 120px;" />
            <h5>24x7 Support</h5>
            <p>Get help whenever you need it — whether you're a farmer or a vendor, we’ve got your back.</p>
        </div>
    </div>--%>

    <!-- Call to Action -->
    <div class="text-center p-4 bg-success text-white rounded shadow">
        <h4>Boost Your Farm with the Right Equipment</h4>
        <p>Join AgriRental today and connect with vendors offering modern, efficient farming tools at affordable rental prices.</p>
<%--        <a href="VendorRegistration.aspx" class="btn btn-outline-light">Register as Vendor</a>--%>
    </div>
</asp:Content>

