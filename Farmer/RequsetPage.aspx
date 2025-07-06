<%@ Page Title="" Language="C#" MasterPageFile="~/Farmer/FarmerMasterPage.master" AutoEventWireup="true" CodeFile="RequsetPage.aspx.cs" Inherits="Farmer_RequsetPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <style>
        .request-card {
            margin-bottom: 30px;
        }

        .status-badge {
            font-size: 0.9rem;
            padding: 5px 10px;
            border-radius: 10px;
        }

        .status-pending {
            background-color: #ffc107;
            color: #000;
        }

        .status-accepted {
            background-color: #28a745;
            color: #fff;
        }

        .status-rejected {
            background-color: #dc3545;
            color: #fff;
        }

        .status-paid {
            background-color: #0d6efd;
            color: #fff;
        }
    </style>
    <script type="text/javascript">
    function confirmDelete() {
        return confirm("Are you sure you want to delete this order?");
    }

    function showSuccess() {
        alert("Order deleted successfully.");
    }
    </script>


    <h2 class="mb-4">My Rental Requests</h2>
    <div class="row">
        <asp:Repeater ID="rptRequestStatus" runat="server" >
            <ItemTemplate>
                <div class="col-md-4">
                    <div class="card request-card shadow-sm">
                        <img src='<%# "../User/PRODUCTIMG/" + Eval("Image") %>' class="card-img-top" alt="Product" style="height: 200px; object-fit: cover;" />
                        <div class="card-body">
                       <p class="card-text"><strong>Product By:</strong> <%# Eval("VendorName") %></p>
<p class="card-text"><strong>Vendor Contact:</strong> <%# Eval("VendorMobile") %></p>
<p class="card-text"><strong>Payable Amount:</strong> <%# Eval("PayableAmount") %></p>


                            <p class="card-text"><strong>Requested On:</strong> <%# Eval("RequestDate", "{0:dd MMM yyyy}") %></p>
                            <p class="card-text"><strong>From:</strong> <%# Eval("FromDate", "{0:dd MMM yyyy}") %></p>
                            <p class="card-text"><strong>To:</strong> <%# Eval("ToDate", "{0:dd MMM yyyy}") %></p>
                            <span class='status-badge <%# GetStatusCss(Eval("Status").ToString()) %>'>
                                <%# Eval("Status") %>
                            </span>
                            <br />
                            <br />
                          <%--<asp:LinkButton ID="btnDelete" runat="server"
    CommandName="DeleteOrder"
    CommandArgument='<%# Eval("FarmerID") + "|" + Eval("ProductID") + "|" + Eval("VendorID") %>'
    CssClass="btn btn-sm btn-danger"
    OnCommand="btnDelete_Command" OnClientClick="return confirmDelete();">
    Delete
</asp:LinkButton>
<asp:LinkButton ID="btnPay" runat="server"
                                CommandName="PayOrder"
                                CommandArgument='<%# Eval("FarmerID") + "|" + Eval("ProductID") + "|" + Eval("VendorID") %>'
                                CssClass="btn btn-sm btn-primary"
                                OnCommand="btnPay_Command">
                                Pay
                            </asp:LinkButton>

  --%>
          <asp:LinkButton ID="btnDelete" runat="server"
    CommandName="DeleteOrder"
CommandArgument='<%# Eval("RentID") + "|" + Eval("FarmerID") + "|" + Eval("ProductID") + "|" + Eval("VendorID") %>'
    CssClass="btn btn-sm btn-danger"
    OnCommand="btnDelete_Command" OnClientClick="return confirmDelete();">
    Delete
</asp:LinkButton>



<asp:LinkButton ID="btnPay" runat="server"
    CommandName="PayOrder"
CommandArgument='<%# Eval("RentID") + "|" + Eval("FarmerID") + "|" + Eval("ProductID") + "|" + Eval("VendorID") %>'
    CssClass="btn btn-sm btn-primary"
    OnCommand="btnPay_Command">
    Pay
</asp:LinkButton>


<!-- HiddenField to keep CommandArgument -->





                    </div>
                </div>
              

 </div>
            </ItemTemplate>
        </asp:Repeater>
        </div>
         <!-- Payment Modal -->
  
</asp:Content>

   
