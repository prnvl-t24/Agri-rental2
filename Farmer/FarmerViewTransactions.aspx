<%@ Page Title="" Language="C#" MasterPageFile="~/Farmer/FarmerMasterPage.master" AutoEventWireup="true" CodeFile="FarmerViewTransactions.aspx.cs" Inherits="Farmer_FarmerViewTransactions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <style>
     /* Modern GridView Styling */
.table {
    width: 100%;
    margin-bottom: 1rem;
    color: #212529;
    background-color: #fff;
    border-collapse: collapse;
    border-radius: 8px;
    overflow: hidden;
}

.table th {
    background-color: #198754; /* Bootstrap success */
    color: white;
    text-align: center;
    padding: 12px;
}

.table td {
    text-align: center;
    padding: 10px;
    vertical-align: middle;
}

.table-bordered {
    border: 1px solid #dee2e6;
}

.table-bordered td,
.table-bordered th {
    border: 1px solid #dee2e6;
}

.table-striped tbody tr:nth-of-type(odd) {
    background-color: #f9f9f9;
}

.table-striped tbody tr:hover {
    background-color: #e2f0d9; /* Light green hover */
    transition: 0.3s;
}

 </style>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<script type="text/javascript">
    $(document).ready(function () {
        $("#<%= txtSearch.ClientID %>").on("keyup", function () {
            var value = $(this).val().toLowerCase();

            $("#<%= GridView1.ClientID %> tbody tr").each(function () {
                var rowText = $(this).text().toLowerCase();
                $(this).toggle(rowText.indexOf(value) !== -1);
            });
        });
    });
</script>
    <div class="table-responsive mt-4">
        <div class="d-flex justify-content-center mb-3">
    <div class="input-group" style="max-width: 400px;">
<asp:TextBox ID="txtSearch" runat="server" CssClass="form-control mb-3" placeholder="Search category..." />
        <span class="input-group-text bg-primary text-white">
            <i class="fa fa-search"></i>
        </span>
    </div>
</div>
      <div class="row mb-4">
    <div class="col-md-3">
        <asp:Label ID="lblFromDate" runat="server" Text="From Date:" CssClass="form-label fw-bold" />
        <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
            ErrorMessage="From Date required" ForeColor="Red" Display="Dynamic" />
    </div>
    <div class="col-md-3">
        <asp:Label ID="lblToDate" runat="server" Text="To Date:" CssClass="form-label fw-bold" />
        <asp:TextBox ID="txtToDate" runat="server" TextMode="Date" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtToDate"
            ErrorMessage="To Date required" ForeColor="Red" Display="Dynamic" />
    </div>
    <div class="col-md-3 align-self-end">
        <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="btn btn-primary" OnClick="btnFilter_Click" />
    </div>
</div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
        CssClass="table table-bordered table-striped">
        <Columns>
             <asp:BoundField DataField="RentID" HeaderText="Rent ID" />
        <asp:BoundField DataField="FarmerID" HeaderText="Farmer ID" />
        <asp:BoundField DataField="ProductName" HeaderText="Product" />
        <asp:BoundField DataField="FromDate" HeaderText="From Date" DataFormatString="{0:dd-MM-yyyy}" />
        <asp:BoundField DataField="ToDate" HeaderText="To Date" DataFormatString="{0:dd-MM-yyyy}" />
        <asp:BoundField DataField="TotalAmount" HeaderText="Amount" DataFormatString="{0:C}" />
        <asp:BoundField DataField="Status" HeaderText="Status" />
        <asp:BoundField DataField="PaymentMethod" HeaderText="Payment Method" />
        <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" />
        <asp:BoundField DataField="VendorMobile" HeaderText="Vendor Mobile" />
              </Columns>
    </asp:GridView>
        </div>
</asp:Content>

