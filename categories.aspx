<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="categories.aspx.cs" Inherits="MoProf_bse25aft1_250429135.categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <div>
        <h3>List of Categories</h3>
        <asp:GridView
            ID="GrdView1"
            CellPadding="20" AutoGenerateColumns="false"
            AllowPaging="true" PageSize="5" OnPageIndexChanging="GrdView1_PageIndexChanging"
            runat="server">

            <Columns>

<asp:HyperLinkField DataTextField="Cat_Name" DataNavigateUrlFields="Cat_Id"
                    DataNavigateUrlFormatString="categorydetails.aspx?id={0}" ControlStyle-CssClass="btn btn-outline-success" HeaderText="Category Name" />

                <asp:TemplateField HeaderText="Description">
                    <ItemTemplate>
                        <h5>
                            <%#Eval("Cat_Desc", "{0:c}") %>
                        </h5>
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>

        </asp:GridView>
    </div>

</asp:Content>
