<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMasterPage.master" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="User_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
           <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<script type="text/javascript">
    $(document).ready(function () {
        $("#<%= txtSearch.ClientID %>").on("keyup", function () {
            var value = $(this).val().toLowerCase();

            $("#<%= GridView1.ClientID %> tr").filter(function (index) {
                // Skip the header row
                if (index === 0) return;
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
            });
        });
    });
</script>
    <br />
    <br />
                <h5 class="mb-4 text-success">Vendor Rental Report</h5>

      <div class="d-flex justify-content-center mb-3">
    <div class="input-group" style="max-width: 400px;">
<asp:TextBox ID="txtSearch" runat="server" CssClass="form-control mb-3" placeholder="Search...." />
        <span class="input-group-text bg-primary text-white">
            <i class="fa fa-search"></i>
        </span>
    </div>
</div>
    <div class="row mb-4">
    <div class="col-md-3">
        <asp:Label ID="lblFromDate" runat="server" Text="From Date:" CssClass="form-label fw-bold" />
        <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date" CssClass="form-control" placeholder="dd-MM-yyyy" />
        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
            ErrorMessage="From Date required" ForeColor="Red" Display="Dynamic" />
    </div>
    <div class="col-md-3">
        <asp:Label ID="lblToDate" runat="server"  Text="To Date:" CssClass="form-label fw-bold" />
        <asp:TextBox ID="txtToDate" TextMode="Date" runat="server" CssClass="form-control" placeholder="dd-MM-yyyy" />
        <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
            ErrorMessage="To Date required" ForeColor="Red" Display="Dynamic" />
    </div>
    <div class="col-md-3 align-self-end">
        <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="btn btn-primary" OnClick="btnFilter_Click" />
    </div>
</div>
        <div class="container mt-5">

            <div class="row mb-4">
                <div class="col-md-4">
                    <label class="form-label fw-bold">Total Rentals:</label>
                    <asp:Label ID="lblTotalRentals" runat="server" CssClass="form-control bg-light" />
                </div>
                <div class="col-md-4">
                    <label class="form-label fw-bold">Total Revenue:</label>
                    <asp:Label ID="lblTotalRevenue" runat="server" CssClass="form-control bg-light" />
                </div>
             
            </div>

            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="RentID" HeaderText="Rent ID" />
                    <asp:BoundField DataField="ProductName" HeaderText="Product" />
                    <asp:BoundField DataField="FarmerName" HeaderText="Farmer" />
                    <asp:BoundField DataField="MobileNumber" HeaderText="Farmer Contact" />

                    <asp:BoundField DataField="FromDate" HeaderText="From Date" DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="ToDate" HeaderText="To Date" DataFormatString="{0:dd-MM-yyyy}" />
                  
                    <asp:BoundField DataField="TotalAmount" HeaderText="Amount (₹)" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:BoundField DataField="PaymentMethod" HeaderText="PaymentMethod" />
                </Columns>
            </asp:GridView>
        </div>
  
    </asp:Content>