<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="userdetails.aspx.cs" Inherits="MoProf_bse25aft1_250429135.userdetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">


        <div class="container my-5">
            <div class="row justify-content-center">
                <div class="col-lg-6 col-md-8">

                    <div class="card-header bg-primary text-white text-center rounded-top-4 py-4">
                        <h2 class="mb-0 fw-bold">
                            <i class="fas fa-user me-2"></i>Detailed User View
                        </h2>
                    </div>

                    <div class="card shadow-lg border-0 rounded-4 px-4">


                        <asp:DetailsView ID="DetailsView1" runat="server"
                            AutoGenerateRows="false" CssClass="w-100" GridLines="None"
                            BorderWidth="0" CellSpacing="0">

                            <Fields>

                                <asp:TemplateField HeaderText="Profile Picture">
                                    <ItemTemplate>
                                        <div class="text-center pt-4">
                                            <asp:Image ID="imgProfile" runat="server"
                                                ImageUrl='<%# "~/images/" + Eval("ProfilePic") %>'
                                                CssClass="rounded-circle shadow-sm"
                                                Width="120" Height="120" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Full Name">
                                    <ItemTemplate>
                                        <h3 class="text-center fw-bold mt-3 mb-0">
                                            <%# Eval("F_Name") %> <%# Eval("L_Name") %>
                                        </h3>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <p class="text-center text-muted mb-4">
                                            <i class="fas fa-envelope me-2"></i><%# Eval("Email") %>
                                        </p>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="User Role">
                                    <ItemTemplate>
                                        <div class="px-4 py-2 border-top">
                                            <span class="fw-semibold"><i class="fas fa-briefcase me-2"></i>User Role</span>
                                            <p class="mb-0"><%# Eval("Type_Name") %></p>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Username">
                                    <ItemTemplate>
                                        <div class="px-4 py-2 border-top">
                                            <span class="fw-semibold">Username</span>
                                            <p class="mb-0"><%# Eval("Username") %></p>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Login">
                                    <ItemTemplate>
                                        <div class="px-4 py-2 border-top">
                                            <span class="fw-semibold"><i class="fas fa-calendar me-2"></i>Last Login</span>
                                            <p class="mb-0"><%# Eval("lastlog") %></p>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Contact Number">
                                    <ItemTemplate>
                                        <div class="px-4 py-2 border-top">
                                            <span class="fw-semibold"><i class="fas fa-phone me-2"></i>Contact Number</span>
                                            <p class="mb-0"><%# Eval("Phone") %></p>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Date Of Birth">
                                    <ItemTemplate>
                                        <div class="px-4 py-2 border-top">
                                            <span class="fw-semibold">Date Of Birth</span>
                                            <p class="mb-0"><%# Eval("Birthdate") %></p>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Gender">
                                    <ItemTemplate>
                                        <div class="px-4 py-2 border-top">
                                            <span class="fw-semibold">Gender</span>
                                            <p class="mb-0"><%# Eval("Gender") %></p>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="District">
                                    <ItemTemplate>
                                        <div class="px-4 py-2 border-top">
                                            <span class="fw-semibold"><i class="fas fa-map me-2"></i>District</span>
                                            <p class="mb-0"><%# Eval("dis_name") %></p>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Registration Date">
                                    <ItemTemplate>
                                        <div class="px-4 py-2 border-top">
                                            <span class="fw-semibold"><i class="fas fa-calendar me-2"></i>Registration Date</span>
                                            <p class="mb-0"><%# Eval("Registration") %></p>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approved">
                                    <ItemTemplate>
                                        <div class="px-4 py-2 border-top">
                                            <span class="fw-semibold"><i class="fas fa-check-circle me-2"></i>Approved</span>
                                            <p class="mb-0"><%# Eval("Approval_Status") %></p>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Fields>



                        </asp:DetailsView>
                    </div>
                </div>
            </div>
        </div>



</asp:Content>
