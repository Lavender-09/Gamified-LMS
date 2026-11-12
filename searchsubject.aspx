<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="searchsubject.aspx.cs" Inherits="MoProf_bse25aft1_250429135.searchsubject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="container my-5">
        <div class="row justify-content-center">
            <div class="col-lg-8 col-md-10">

                <div class="card shadow-lg border-0 rounded-4">

                    <div class="card-header bg-primary text-white text-center rounded-top-4 py-4">
                        <h2 class="mb-0 fw-bold">
                            <i class="fas fa-search me-2"></i>Search For Courses
                        </h2>
                    </div>

                    <div class="card-body p-4">

                        <div class="my-3">
                            <asp:TextBox ID="txtsubid" Placeholder="Course Id" OnTextChanged="txtsubid_TextChanged"
                                CssClass="form-control" runat="server" AutoPostBack="true" Visible="false"></asp:TextBox>
                        </div>

                        <div class="mb-3">
                            <asp:Label runat="server" CssClass="form-label">Tutor Name</asp:Label>
                            <asp:TextBox ID="txttutorname" runat="server" CssClass="form-control" />
                        </div>

                        <div class="my-3">
                            <asp:TextBox ID="txtsubname" Placeholder="Course Name"
                                CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                        </div>

                        <div class="my-3">
                            <asp:DropDownList ID="ddlgrade" CssClass="form-control"
                                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlgrade_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <div class="my-3">
                            <asp:DropDownList ID="ddlcategory" CssClass="form-control"
                                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <div class="d-flex align-items-center justify-content-center gap-3 mt-3">
                            <asp:Button ID="btnSearch" runat="server"
                                Text="Search" CssClass="btn btn-primary btn-lg px-5" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnClear" runat="server"
                                Text="Clear" CssClass="btn btn-outline-secondary btn-lg px-5" OnClick="btnClear_Click" />
                        </div>

                    </div>

                    <div class="card-footer bg-light rounded-bottom-4 p-4">
                        <h5><i class="fas fa-list me-2"></i>Search Results</h5>

                        <asp:Label ID="lblmsg" runat="server" CssClass="d-block mb-2" />

<asp:GridView ID="gvs" runat="server" AutoGenerateColumns="false"
    CssClass="table table-striped" OnRowCommand="gvs_RowCommand"
    OnPageIndexChanging="gvs_PageIndexChanging" AllowPaging="true">
    <Columns>
        <asp:BoundField DataField="Sub_Name" HeaderText="Subject" />
        <asp:BoundField DataField="TutorName" HeaderText="Tutor" />
        <asp:BoundField DataField="Grade_Name" HeaderText="Grade" />
        <asp:BoundField DataField="Cat_Name" HeaderText="Category" />
        <asp:BoundField DataField="Fees" HeaderText="Fees" DataFormatString="Rs {0}" />
        <asp:TemplateField HeaderText="Enrollment">
            <ItemTemplate>
                <%# Eval("Curr_Enroll") %> / <%# Eval("Max_Enroll") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm"
                    CommandName="joinClass" CommandArgument='<%# Eval("TS_Id") %>'
                    Visible='<%# Convert.ToInt32(Eval("IsEnrolled")) == 0 && Convert.ToInt32(Eval("Curr_Enroll")) < Convert.ToInt32(Eval("Max_Enroll")) %>'>
                    Join Class
                </asp:LinkButton>
                <span class="badge bg-success" Visible='<%# Convert.ToInt32(Eval("IsEnrolled")) == 1 %>'>Enrolled</span>
                <span class="badge bg-secondary" Visible='<%# Convert.ToInt32(Eval("IsEnrolled")) == 0 && Convert.ToInt32(Eval("Curr_Enroll")) >= Convert.ToInt32(Eval("Max_Enroll")) %>'>Full</span>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
