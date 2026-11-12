<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="userview.aspx.cs" Inherits="MoProf_bse25aft1_250429135.userview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">


        <div>
            <h3>List of Users</h3>
            <asp:GridView
                ID="grdviewtut"
                CellPadding="20" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="10" OnPageIndexChanging="grdviewtut_PageIndexChanging"
                runat="server">

                <Columns>

                    <asp:TemplateField HeaderText="User ID">
    <ItemTemplate>
        <p>
            <%#Eval("u_id") %>
        </p>
    </ItemTemplate>
</asp:TemplateField>

                    <asp:TemplateField HeaderText="Full Name">
    <ItemTemplate>
        <p>
            <%#Eval("F_Name") %>
            <%#Eval("L_Name") %>
        </p>
    </ItemTemplate>
</asp:TemplateField> 

                    <asp:HyperLinkField DataTextField="Email" DataNavigateUrlFields="u_Id"
    DataNavigateUrlFormatString="userdetails.aspx?id={0}" ControlStyle-CssClass="btn btn-outline-success" HeaderText="Email" />


                    <asp:TemplateField HeaderText="Contact Number">
                        <ItemTemplate>
                            <p>
                                <%#Eval("Phone", "{0:c}") %>
                            </p>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Frozen">
    <ItemTemplate>
        <p>
            <%#Eval("is_Frozen", "{0:c}") %>
        </p>
    </ItemTemplate>
</asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved">
    <ItemTemplate>
        <p>
            <%#Eval("Approval_Status", "{0:c}") %>
        </p>
    </ItemTemplate>
</asp:TemplateField>

                                        <asp:TemplateField HeaderText="DOB">
    <ItemTemplate>
        <h5>
            <%#Eval("Birthdate") %>
        </h5>
    </ItemTemplate>
</asp:TemplateField>

                    <asp:TemplateField HeaderText="User Role">
    <ItemTemplate>
        <h5>
            <%#Eval("Type_Name") %>
        </h5>
    </ItemTemplate>
</asp:TemplateField>

                    <asp:TemplateField HeaderText="Registration Date">
                        <ItemTemplate>
                            <h5>
                                <%#Eval("Registration") %>
                            </h5>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
        </div>


</asp:Content>
