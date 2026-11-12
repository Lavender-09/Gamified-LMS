<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="courses.aspx.cs" Inherits="MoProf_bse25aft1_250429135.courses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

        <div>
            <h3>List of Courses</h3>
            <asp:GridView
                ID="GrdView2"
                CellPadding="20" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="10" OnPageIndexChanging="GrdView2_PageIndexChanging"
                runat="server">

                <Columns>

                    <asp:HyperLinkField DataTextField="Sub_Name" DataNavigateUrlFields="Sub_Id"
                        DataNavigateUrlFormatString="coursedetails.aspx?id={0}" ControlStyle-CssClass="btn btn-outline-success" HeaderText="Subject Name" />

                    <asp:TemplateField HeaderText="Description">
                        <ItemTemplate>
                            <p>
                                <%#Eval("Sub_Desc", "{0:c}") %>
                            </p>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Grade Level">
                        <ItemTemplate>
                            <h5>
                                <%#Eval("Grade_Name") %>
                            </h5>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
        </div>


</asp:Content>
