<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="payment.aspx.cs" Inherits="MoProf_bse25aft1_250429135.payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <div class="container mt-5" style="max-width: 400px;">

        <asp:Panel ID="pnlLogin" runat="server">
            <h4 class="mb-3">Payment Login</h4>
            <asp:TextBox ID="txtPayUsername" runat="server" CssClass="form-control mb-2" placeholder="Username">
            </asp:TextBox>
            <asp:TextBox ID="txtPayPassword" runat="server" CssClass="form-control mb-2" placeholder="Password" TextMode="Password">
            </asp:TextBox>
            <asp:LinkButton ID="btnPayLogin" runat="server" CssClass="btn btn-primary w-100" OnClick="btnPayLogin_Click">Continue</asp:LinkButton>
            <asp:Label ID="lblLoginMsg" runat="server" CssClass="d-block mt-2 text-danger"></asp:Label>
        </asp:Panel>

        <asp:Panel ID="pnlConfirm" runat="server" Visible="false">
            <h4 class="mb-3">Confirm Payment</h4>
            <p>Would you like to confirm payment for this lesson?</p>
            <asp:LinkButton ID="btnConfirmYes" runat="server" CssClass="btn btn-success me-2" OnClick="btnConfirmYes_Click">Yes</asp:LinkButton>
            <asp:LinkButton ID="btnConfirmNo" runat="server" CssClass="btn btn-outline-danger" OnClick="btnConfirmNo_Click">No</asp:LinkButton>
        </asp:Panel>

        <asp:Panel ID="pnlResult" runat="server" Visible="false">
            <div id="divResult" runat="server" class="alert"></div>
            <asp:HyperLink ID="lnkBack" runat="server" CssClass="btn btn-secondary">Back to Subject</asp:HyperLink>
        </asp:Panel>

    </div>

</asp:Content>
