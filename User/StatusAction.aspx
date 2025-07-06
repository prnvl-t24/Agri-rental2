<%@ Page Title="" Language="C#" MasterPageFile="~/User/UserMasterPage.master" AutoEventWireup="true" CodeFile="StatusAction.aspx.cs" Inherits="User_StatusAction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <br />
   <style>
.status-paid {
    background-color: #d4edda !important; /* Light green */
}

.status-rejected {
    background-color: #f8d7da !important; /* Light red */
}

.status-pending {
    background-color: #fff3cd !important; /* Light yellow */
}
</style>

       <div class="d-flex justify-content-center mb-3">
    <div class="input-group" style="max-width: 400px;">
<asp:TextBox ID="txtSearch" runat="server" CssClass="form-control mb-3" placeholder="Search category..." />
        <span class="input-group-text bg-primary text-white">
            <i class="fa fa-search"></i>
        </span>
    </div>
</div>
           <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

<script type="text/javascript">
    $(document).ready(function () {
        $("#<%= txtSearch.ClientID %>").on("keyup", function () {
            var value = $(this).val().toLowerCase();

            $("#rptBody tr").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
            });
        });
    });
</script>
    

            <table class="table table-bordered">
                <tr>
                     <th>Request ID</th>
        <th>Product </th>
    <%--    <th>Farmer ID</th>--%>
        <th>Farmer Name</th>           <!-- New -->
        <th>Mobile Number</th>          <!-- New -->
        <th>From Date</th>
        <th>To Date</th>
                    <th>Address</th>
       
                    <th> Amount</th>
                     <th>Status</th>
        <th>Action</th>
                </tr>
          <tbody id="rptBody">
         <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
       <ItemTemplate>
    <tr class='<%# "status-" + Eval("Status").ToString().ToLower().Trim() %>'>
        <td><%# Eval("RentID") %></td>
       <%-- <td><%# Eval("ProductID") %></td>--%>
        <td><%# Eval("ProductName") %></td>       <!-- New ProductName -->
  <%--      <td><%# Eval("FarmerID") %></td>--%>
        <td><%# Eval("FullName") %></td>          <!-- Farmer Name -->
        <td><%# Eval("MobileNumber") %></td>      <!-- Farmer Mobile -->
        <td><%# Eval("FromDate", "{0:dd-MM-yyyy}") %></td>
        <td><%# Eval("ToDate", "{0:dd-MM-yyyy}") %></td>
        <td><%# Eval("Address") %></td>
         <td><%# Eval("TotalAmount") %></td>

        <td><%# Eval("Status") %></td>
        <td>
            <asp:Button ID="btnAccept" runat="server" Text="Accept" CommandArgument='<%# Eval("RentID") %>' OnClick="btnAccept_Click" CssClass="btn btn-success btn-sm" />
            <asp:Button ID="btnReject" runat="server" Text="Reject" CommandArgument='<%# Eval("RentID") %>' OnClick="btnReject_Click" CssClass="btn btn-danger btn-sm" />
        </td>
    </tr>
</ItemTemplate>

         </asp:Repeater>
              </tbody>
                </table>
      <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:AgroRentalDB %>" 
    SelectCommand="
        SELECT TOP (1000)
            r.RentID,
          r.TotalAmount,
            r.ProductID,
            p.ProductName,
            r.FarmerID,
            f.FullName,
            f.MobileNumber,
          f.Address,
            r.FromDate,
            r.ToDate,
            r.Status,
            r.VendorID
        FROM RentProduct r
        INNER JOIN Farmer_Registration f ON r.FarmerID = f.FarmerID
        INNER JOIN Product p ON r.ProductID = p.ProductID
        WHERE r.VendorID = @VendorID ORDER BY  r.RentID DESC">
    <SelectParameters>
        <asp:SessionParameter Name="VendorID" SessionField="UserID" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>

         


   
</asp:Content>

