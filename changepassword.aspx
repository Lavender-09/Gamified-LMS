<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="changepassword.aspx.cs" Inherits="MoProf_bse25aft1_250429135.changepassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <div class="container my-5">
        <div class="row justify-content-center">
            <div class="col-lg-6">
                <div class="card shadow-lg border-0 rounded-4">
                    <div class="card-header bg-primary text-white text-center rounded-top-4 py-4">
                        <h3 class="mb-0"><i class="fas fa-lock me-2"></i>Change Password</h3>
                    </div>
                    <div class="card-body p-4">
                        <div class="mb-3">
                            <label>Current Password</label>
                            <asp:TextBox ID="txtOldPass" runat="server" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="rfvOld" runat="server" ControlToValidate="txtOldPass"
                                ErrorMessage="Required" ForeColor="Red" Display="Dynamic" />
                        </div>
                        <div class="mb-3">
                            <label>New Password</label>
                            <asp:TextBox ID="txtNewPass" runat="server" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator ID="rfvNew" runat="server" ControlToValidate="txtNewPass"
                                ErrorMessage="Required" ForeColor="Red" Display="Dynamic" />
                        </div>
                        <div class="mb-3">
                            <label>Confirm New Password</label>
                            <asp:TextBox ID="txtConfirmPass" runat="server" TextMode="Password" CssClass="form-control" />
                            <asp:CompareValidator ID="cvConfirm" runat="server" ControlToCompare="txtNewPass" ControlToValidate="txtConfirmPass"
                                Operator="Equal" ErrorMessage="Passwords do not match" ForeColor="Red" Display="Dynamic" />
                        </div>

                        <asp:Button ID="btnChangePass" runat="server" Text="Update Password"
                            CssClass="btn btn-primary w-100" OnClick="btnChangePass_Click" />

                        <asp:Label ID="lblMsg" runat="server" CssClass="d-block mt-3" />
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
