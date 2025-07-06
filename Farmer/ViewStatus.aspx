<%@ Page Title="" Language="C#" MasterPageFile="~/Farmer/FarmerMasterPage.master" AutoEventWireup="true" CodeFile="ViewStatus.aspx.cs" Inherits="Farmer_ViewStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <style>
        .modal { display: none; position: fixed; background-color: rgba(0,0,0,0.6); top: 0; left: 0; width: 100%; height: 100%; }
        .modal-content { background: #fff; margin: 10% auto; padding: 20px; width: 400px; border-radius: 8px; }
    </style>
                <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

                <table border="1">
                    <tr>
                        <th>Product ID</th>
                        <th>From</th>
                        <th>To</th>
                        <th>Status</th>
                        <th>Amount</th>
                        <th>Payment</th>
                        <th>Action</th>
                    </tr>
        <asp:Repeater ID="rptOrders" runat="server" OnItemCommand="rptOrders_ItemCommand">
            
            <ItemTemplate>
                <tr>
                    <td><%# Eval("ProductID") %></td>
                    <td><%# Eval("FromDate", "{0:dd/MM/yyyy}") %></td>
                    <td><%# Eval("ToDate", "{0:dd/MM/yyyy}") %></td>
                    <td><%# Eval("Status") %></td>
                 
                    <td><%# Eval("PaymentStatus") %></td>
                    <td>
                        <%# Eval("PaymentStatus").ToString() == "Paid" ? "Paid" : "" %>
                        <asp:Button ID="btnPay" runat="server" Text="Pay" CommandName="Pay" 
                               CommandArgument='<%# Eval("RentID") + "|" + Eval("ProductID") + "|" + Eval("PaymentAmount") %>'

                                    Visible='<%# Eval("PaymentStatus").ToString() != "Paid" %>' />
                    </td>
                </tr>
            </ItemTemplate>
            
        </asp:Repeater>
    </table>
    
        <!-- Payment Modal -->
        <div id="paymentModal" class="modal" runat="server">
            <div class="modal-content">
                <h3>Card Payment</h3>
                <asp:HiddenField ID="hfRentID" runat="server" />
                <asp:TextBox ID="txtCardNumber" runat="server" placeholder="Card Number"></asp:TextBox><br />
                <asp:TextBox ID="txtCardName" runat="server" placeholder="Cardholder Name"></asp:TextBox><br />
                <asp:TextBox ID="txtCVV" runat="server" placeholder="CVV" TextMode="Password"></asp:TextBox><br />
                <asp:TextBox ID="txtAmount" runat="server" ReadOnly="true"></asp:TextBox><br />
                <asp:Button ID="btnConfirmPayment" runat="server" Text="Confirm Payment" OnClick="btnConfirmPayment_Click" />
                <asp:Button ID="btnCancelPayment" runat="server" Text="Cancel" OnClientClick="document.getElementById('paymentModal').style.display='none'; return false;" />
            </div>
        </div>

    <script>
        function showModal() {
            document.getElementById('paymentModal').style.display = 'block';
        }
    </script>

</asp:Content>

