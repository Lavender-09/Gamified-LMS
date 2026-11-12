<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="categorydetails.aspx.cs" Inherits="MoProf_bse25aft1_250429135.categorydetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">   
            <asp:DetailsView ID="DetailsView1" runat="server"
        AutoGenerateRows="false">
        <Fields>

            <asp:TemplateField HeaderText="Category Name and Description">
                <ItemTemplate>
                    <strong>
                        <%#Eval("Cat_Name") %>
                    </strong>
                    <br />
                    <em>
                        <%#Eval("Cat_Desc") %>
                    </em>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:CommandField
                ControlStyle-CssClass="btn btn-success fa-pull-right m-1"
                ShowDeleteButton="True" ShowEditButton="True"
                ShowInsertButton="True"></asp:CommandField>

        </Fields>
        
        
        
    </asp:DetailsView>

</asp:Content>
