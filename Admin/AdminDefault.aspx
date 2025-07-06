<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminDefault.aspx.cs" Inherits="Admin_AdminDefault" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
        <h2 class="text-success mb-4">Welcome, Admin</h2>

        <div class="row g-4">
            <!-- Total Farmers -->
            <div class="col-md-3">
                <div class="card text-white bg-success shadow">
                    <div class="card-body">
                        <h5 class="card-title">Total Farmers</h5>
                        <h2><i class="bi bi-person-lines-fill me-2"></i><asp:Label ID="lblTotalFarmers" runat="server" Text="--"></asp:Label></h2>
                    </div>
                </div>
            </div>

            <!-- Pending Approvals -->
            <div class="col-md-3">
                <div class="card text-white bg-warning shadow">
                    <div class="card-body">
                        <h5 class="card-title">Pending Approvals</h5>
                        <h2><i class="bi bi-person-x-fill me-2"></i><asp:Label ID="lblPendingFarmers" runat="server" Text="--"></asp:Label></h2>
                    </div>
                </div>
            </div>

            <!-- Products -->
            <div class="col-md-3">
                <div class="card text-white bg-info shadow">
                    <div class="card-body">
                        <h5 class="card-title">Total Products</h5>
                        <h2><i class="bi bi-box-seam me-2"></i><asp:Label ID="lblTotalProducts" runat="server" Text="--"></asp:Label></h2>
                    </div>
                </div>
            </div>

            <!-- Transactions -->
            <div class="col-md-3">
                <div class="card text-white bg-danger shadow">
                    <div class="card-body">
                        <h5 class="card-title">Total Transactions</h5>
                        <h2><i class="bi bi-currency-rupee me-2"></i><asp:Label ID="lblTotalTransactions" runat="server" Text="--"></asp:Label></h2>
                    </div>
                </div>
            </div>
        </div>

        <!-- Chart or Report Area (optional) -->
        <div class="mt-5">
            <h4 class="text-secondary">Recent Activity</h4>
            <p>This section can be enhanced with recent logs or reports.</p>
        </div>
    </div>
</asp:Content>

