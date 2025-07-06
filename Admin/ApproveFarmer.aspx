<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMasterPage.master" AutoEventWireup="true" CodeFile="ApproveFarmer.aspx.cs" Inherits="Admin_ApproveFarmer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="container mt-5">
            <h2 class="text-center text-success mb-4">Pending Farmer Approvals</h2>
            <asp:GridView ID="gvFarmers" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped text-center"
                OnRowCommand="gvFarmers_RowCommand" DataKeyNames="FarmerID">
                <Columns>
                    <asp:BoundField DataField="FarmerID" HeaderText="ID" />
                    <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                    <asp:ButtonField ButtonType="Button" CommandName="Approve" Text="Approve" ControlStyle-CssClass="btn btn-success btn-sm" />
                    <asp:ButtonField ButtonType="Button" CommandName="Reject" Text="Reject" ControlStyle-CssClass="btn btn-danger btn-sm" />
                </Columns>
            </asp:GridView>
            <asp:Label ID="lblMessage" runat="server" CssClass="text-center d-block mt-3 text-primary"></asp:Label>
        </div>
</asp:Content>

