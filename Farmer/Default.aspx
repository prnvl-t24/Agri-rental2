<%@ Page Title="" Language="C#" MasterPageFile="~/Farmer/FarmerMasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Farmer_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <div class="container mt-4">
          <div class="d-flex justify-content align-items-center mb-3">
       <asp:Image ID="imgFarmer" runat="server" CssClass="rounded-circle border border-success shadow" 
    Width="100px" Height="100px" />    <h2 class="text-success">Welcome <asp:Label ID="lblshow" runat="server" Text=""></asp:Label></h2>


        <!-- Welcome Message -->
    

    <!-- Farmer Image -->
</div>
       </div>
        <!-- 🔄 Bootstrap Slider -->
        <div id="dashboardCarousel" class="carousel slide mb-5" data-bs-ride="carousel">
            <div class="carousel-inner rounded shadow">
                <div class="carousel-item active">
                    <img src="../Images/img2.jpg" class="d-block w-100" style="height: 350px; object-fit: cover;" alt="Slide 1">
                </div>
               <%-- <div class="carousel-item">
                    <img src="../Images/slider2.jpg" class="d-block w-100" style="height: 350px; object-fit: cover;" alt="Slide 2">
                </div>
                <div class="carousel-item">
                    <img src="../Images/slider3.jpg" class="d-block w-100" style="height: 350px; object-fit: cover;" alt="Slide 3">
                </div>
            </div>--%>
           <%-- <button class="carousel-control-prev" type="button" data-bs-target="#dashboardCarousel" data-bs-slide="prev">
                <span class="carousel-control-prev-icon"></span>
            </button>
            <button class="carousel-control-next" type="button" data-bs-target="#dashboardCarousel" data-bs-slide="next">
                <span class="carousel-control-next-icon"></span>
            </button>--%>
        </div>

        <!-- Cards Section (Dashboard Boxes) -->
        <div class="row g-4">
            <!-- existing cards here (same as your original code) -->
        </div>

    </div>
</asp:Content>

