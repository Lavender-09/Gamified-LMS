<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="studentdetails.aspx.cs" Inherits="MoProf_bse25aft1_250429135.studentdetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <div class="container my-4">
    <div class="row">
        <div class="col-md-4 text-center">
            <asp:Image ID="imgStuProfile" runat="server" CssClass="rounded-circle mb-3" Width="150" Height="150" />
            <h4><asp:Label ID="lblStuName" runat="server" /></h4>
            <p class="text-muted"><asp:Label ID="lblStuUsername" runat="server" /></p>
        </div>
        <div class="col-md-8">
            <table class="table">
                <tr><th>Email</th><td><asp:Label ID="lblStuEmail" runat="server" /></td></tr>
                <tr><th>Phone</th><td><asp:Label ID="lblStuPhone" runat="server" /></td></tr>
                <tr><th>Grade</th><td><asp:Label ID="lblStuGrade" runat="server" /></td></tr>
                <tr><th>District</th><td><asp:Label ID="lblStuDistrict" runat="server" /></td></tr>
                <tr><th>Last Login</th><td><asp:Label ID="lblStuLastLogin" runat="server" /></td></tr>
            </table>

            <h5 class="mt-4">Results</h5>
            <asp:Repeater ID="rptResults" runat="server">
                <ItemTemplate>
                    <div class="d-flex justify-content-between align-items-center border-bottom py-2">
                        
                        <asp:HyperLink ID="hyresults" CssClass="link-success text-decoration-underline" NavigateUrl='<%# Eval("Res_Upload") %>' runat="server" Text='<%# Eval("Res_File_Name") %>'></asp:HyperLink>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</div>

</asp:Content>
