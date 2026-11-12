<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="viewtutors.aspx.cs" Inherits="MoProf_bse25aft1_250429135.viewtutors" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">


        <div>
            <h3>List of Tutors</h3>
            <asp:GridView
                ID="grdviewtut"
                CellPadding="20" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="10" OnPageIndexChanging="grdviewtut_PageIndexChanging"
                runat="server">

                <Columns>

                    <asp:TemplateField HeaderText="Tutor Name">
    <ItemTemplate>
        <p>
            <%#Eval("Title") %>
            <%#Eval("F_Name") %>
            <%#Eval("L_Name") %>
        </p>
    </ItemTemplate>
</asp:TemplateField>

                    

                    <asp:HyperLinkField DataTextField="Email" DataNavigateUrlFields="tut_Id"
                        DataNavigateUrlFormatString="tutordetails.aspx?id={0}" ControlStyle-CssClass="btn btn-outline-success" HeaderText="Email" />

                    <asp:TemplateField HeaderText="Experience">
                        <ItemTemplate>
                            <p>
                                <%#Eval("Tut_Experience", "{0:c}") %>
                            </p>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Skills">
    <ItemTemplate>
        <p>
            <%#Eval("Tut_Skills", "{0:c}") %>
        </p>
    </ItemTemplate>
</asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qualifications">
    <ItemTemplate>
        <p>
            <%#Eval("Tut_Quals", "{0:c}") %>
        </p>
    </ItemTemplate>
</asp:TemplateField>

                    <asp:TemplateField HeaderText="Availability">
    <ItemTemplate>
        <h5>
            <%#Eval("Tut_Availability") %>
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
