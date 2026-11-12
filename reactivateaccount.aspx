<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="reactivateaccount.aspx.cs" Inherits="MoProf_bse25aft1_250429135.reactivateaccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <div class="container my-5">
    <div class="row justify-content-center">
        <div class="col-lg-6">
            <div class="card shadow-lg border-0 rounded-4">
                <div class="card-header bg-warning text-center rounded-top-4 py-4">
                    <h3 class="mb-0">Reactivate Your Account</h3>
                </div>
                <div class="card-body p-4">
                    <p>Your account is currently deactivated. Enter your username and password to reactivate it.</p>

                    <div class="mb-3">
                        <label>Username</label>
                        <asp:TextBox ID="txtRUsername" runat="server" CssClass="form-control" />
                    </div>
                    <div class="mb-3">
                        <label>Password</label>
                        <asp:TextBox ID="txtRPassword" runat="server" TextMode="Password" CssClass="form-control" />
                    </div>

                    <asp:Button ID="btnReactivate" runat="server" Text="Reactivate Account"
                        CssClass="btn btn-warning w-100" OnClick="btnReactivate_Click" />

                    <asp:Label ID="lblReactivateMsg" runat="server" CssClass="d-block mt-3" />
                </div>
            </div>
        </div>
    </div>
</div>

</asp:Content>
