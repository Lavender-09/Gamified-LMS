<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="searchuser.aspx.cs" Inherits="MoProf_bse25aft1_250429135.searchuser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <div class="container my-5">
        <div class="row justify-content-center">
            <div class="col-lg-8 col-md-10">

                <div class="card shadow-lg border-0 rounded-4">

                    <div class="card-header bg-primary text-white text-center rounded-top-4 py-4">
                        <h2 class="mb-0 fw-bold">
                            <i class="fas fa-search me-2"></i>Search For Users
                        </h2>
                    </div>

                    <div class="card-body p-4">
                        <div class="my-3">
                            <asp:TextBox ID="txtuid" Placeholder="User ID" OnTextChanged="txtuid_TextChanged"
                                CssClass="form-control" runat="server" Visible="false" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="my-3">
                            <asp:TextBox ID="txtfname" Placeholder="First Name"
                                CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="my-3">
                            <asp:TextBox ID="txtlname" Placeholder="Last Name"
                                CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="my-3">
                            <asp:TextBox ID="txtuname" Placeholder="Username"
                                CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="my-3">
                            <asp:TextBox ID="txtreg" Placeholder="Registration Date"
                                CssClass="form-control" runat="server" AutoPostBack="true" TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="my-3">
                            <asp:TextBox ID="txtapproval" Placeholder="Approval Status"
                                CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="my-3">
                            <asp:TextBox ID="txtfrozen" Placeholder="Frozen Status"
                                CssClass="form-control" runat="server" AutoPostBack="true"></asp:TextBox>
                        </div>
                        <div class="my-3">
                            <asp:DropDownList ID="ddlType" CssClass="form-control"
                                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="my-3">
                            <asp:DropDownList ID="ddlDistrict" CssClass="form-control"
                                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
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

                        <asp:GridView ID="gvs" runat="server" CellPadding="10" AutoGenerateColumns="false"
                            CssClass="border border-0 mt-4 bg-light" HeaderStyle-HorizontalAlign="Center"
                            AllowPaging="true" PageSize="5" OnPageIndexChanging="gvs_PageIndexChanging">

                            <Columns>
                                <asp:HyperLinkField DataTextField="Type_Name" DataNavigateUrlFields="U_Id"
                                    ControlStyle-CssClass="btn btn-outline-success"
                                    DataNavigateUrlFormatString="userdetails.aspx?id={0}" HeaderText="User Role" />

                                <asp:BoundField DataField="F_Name" HeaderText="First Name" />
                                <asp:BoundField DataField="L_Name" HeaderText="Last Name" />
                                <asp:BoundField DataField="Username" HeaderText="Username" />
                                <asp:BoundField DataField="Reg_Date" HeaderText="Registration Date" />
                                <asp:BoundField DataField="Approval_Status" HeaderText="Approval Status" />
                                <asp:BoundField DataField="Dis_Name" HeaderText="District Name" />
                                <asp:BoundField DataField="is_Frozen" HeaderText="Frozen Status" />

                            </Columns>
                        </asp:GridView>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
