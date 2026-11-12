<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="adminlogin.aspx.cs" Inherits="MoProf_bse25aft1_250429135.adminlogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <%@ Register Src="logincontrols.ascx" TagPrefix="user" TagName="Login" %>


            <div class="container my-5">
<div class="row justify-content-center">
    <div class="col-lg-8 col-md-10">
        
        <div class="card shadow-lg border-0 rounded-4">
            
            <div class="card-header bg-primary text-white text-center rounded-top-4 py-4">
                <h2 class="mb-0 fw-bold">
                    <i class="fas fa-user me-2"></i>Admin Login
                </h2>
            </div>
            <br />
        <div class="col-lg-8 col-md-10 offset-md-1">

            <user:Login ID="userLogin" runat="server" ValidationGroup="usergroup" />

            <hr />
            <asp:PlaceHolder runat="server" ID="ErrorMessage"
                Visible="false">
                <p class="text-danger">
                    <asp:Literal runat="server" ID="FailureText" />
                </p>
            </asp:PlaceHolder>
            <%-- declare the login user control--%>
            <div class="my-3">
                <asp:Button ID="btnlogin" runat="server" Text="Sign In"
                    CssClass="col-12 btn btn-outline-success btn-lg"
                    ValidationGroup="usergroup" OnClick="btnlogin_Click"/>
            </div>
                    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
        </div>

            </div>
        </div>
    </div>
                </div>


</asp:Content>
