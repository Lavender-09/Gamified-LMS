<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="MoProf_bse25aft1_250429135.profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">


    <asp:Label ID="lblmsg" CssClass="offset-md-1" runat="server" Text="" ForeColor="Red"></asp:Label>

    <div class="container my-5">
        <div class="row justify-content-center">
            <div class="col-lg-8 col-md-10">

                <div class="card shadow-lg border-0 rounded-4">

                    <div class="card-header bg-primary text-white text-center rounded-top-4 py-4">
                        <h2 class="mb-0 fw-bold">
                            <i class="fas fa-user me-2"></i>User Profile
                        </h2>
                    </div>
                    <div class="row">
                        <div class=" col-lg-8 col-md-10 offset-md-1">
                            <br />
                            <div id="container">
                                <div id="leftSide">
                                    <fieldset>
                                        <legend>Profile Details</legend>
                                        <asp:Repeater
                                            ID="rptimg" runat="server">
                                            <ItemTemplate>
                                                <asp:Image ID="ImageButton1" runat="server" Width="193px" Height="195px"
                                                    ImageUrl='<%# Eval("profilepic", "~/images/{0}")%>' />

                                            </ItemTemplate>
                                        </asp:Repeater>

                                        <div class="mt-2 mb-3">
    <asp:FileUpload ID="fupNewProfilePic" runat="server" CssClass="form-control mb-2" />
    <asp:Button ID="btnUploadProfilePic" runat="server" Text="Upload Profile Picture"
        CssClass="btn btn-outline-primary btn-sm" OnClick="btnUploadProfilePic_Click" />
    <asp:Label ID="lblPicMsg" runat="server" CssClass="d-block mt-1" />
</div>

                                        <div class="form">
                                            <asp:Label ID="lblFname" runat="server" Text="First Name" AssociatedControlID="txtFname"></asp:Label>
                                            <div class="div_texbox">
                                                <asp:TextBox ID="txtFname" runat="server"
                                                    CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqFname" ControlToValidate="txtFname" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                            </div>
                                            <asp:Label ID="lblLname" runat="server" Text="Last Name" AssociatedControlID="txtLname"></asp:Label>
                                            <div class="div_texbox">
                                                <asp:TextBox ID="txtLname" runat="server"
                                                    CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqLname" ControlToValidate="txtLname" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                            </div>

                                            <asp:Label ID="lblgender" runat="server" Text="Gender" AssociatedControlID="txtgender"></asp:Label>
                                            <div class="div_texbox">
                                                <asp:TextBox ID="txtgender" runat="server"
                                                    CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvgender" ControlToValidate="txtgender" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>

                                            </div>

                                            <asp:Label ID="lblEmail" runat="server" Text="Email" AssociatedControlID="txtEmail"></asp:Label>
                                            <div class="div_texbox">

                                                <asp:TextBox ID="txtEmail" runat="server"
                                                    CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqEmail" ControlToValidate="txtEmail" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator><br />
                                                <asp:RegularExpressionValidator ID="RegEmail" runat="server" ControlToValidate="txtEmail"
                                                    ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$" ErrorMessage="Not Valid"></asp:RegularExpressionValidator>
                                            </div>
                                            <asp:Label ID="lblphone" runat="server" Text="Phone" AssociatedControlID="txtPhone"></asp:Label>
                                            <div class="div_texbox">
                                                <asp:TextBox ID="txtPhone" runat="server"
                                                    CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvphone" ControlToValidate="txtPhone" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                            </div>

                                            <asp:Label ID="lblresults" runat="server" Text="Upload Results" AssociatedControlID="txtfilename"></asp:Label>
                                            <div class="div_texbox">
                                                <asp:TextBox ID="txtfilename" runat="server" />
                                                <br />
                                                <br />
                                                <asp:FileUpload ID="upDoc" runat="server" />
                                                <br />
                                                <br />
                                                <asp:Button ID="btnAdd" Text="Upload Document" CssClass="btn btn-outline-success" runat="server" OnClick="btnAdd_Click" />
                                            </div>

                                            <asp:Label ID="lblregdate" runat="server" Text="Registration Date" AssociatedControlID="txtregdate"></asp:Label>
                                            <div class="div_texbox">
                                                <asp:TextBox ID="txtregdate" runat="server"
                                                    CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvregdate" ControlToValidate="txtregdate" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                            </div>

                                            <asp:Label ID="lbldob" runat="server" Text="Date of Birth" AssociatedControlID="txtdob"></asp:Label>
                                            <div class="div_texbox">
                                                <asp:TextBox ID="txtdob" runat="server"
                                                    CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvdob" ControlToValidate="txtdob" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                            </div>

                                            <asp:Label ID="lblgrade" runat="server" Text="Grade" AssociatedControlID="ddlgrade"></asp:Label>
                                            <div class="div_texbox">
                                                <asp:DropDownList ID="ddlgrade" runat="server"
                                                    CssClass="form-control" OnSelectedIndexChanged="ddlgrade_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvgrade" ControlToValidate="ddlgrade" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                            </div>
                                            <asp:Label ID="lbllvl" runat="server" Text="Level" AssociatedControlID="txtlvl"></asp:Label>
                                            <div class="div_texbox">
                                                <asp:TextBox ID="txtlvl" runat="server"
                                                    CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvlvl" ControlToValidate="txtlvl" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                            </div>

                                            <div class="mb-3">
                                                <div class="form-check-inline">
                                                    <asp:RadioButton ID="radavail" GroupName="availability" runat="server" />
                                                    <asp:Label ID="lblavail" runat="server" AssociatedControlID="radavail"
                                                        CssClass="form-check-label">Available</asp:Label>
                                                </div>
                                                <div class="form-check-inline">
                                                    <asp:RadioButton ID="radunavail" GroupName="availability" runat="server" />
                                                    <asp:Label ID="lblunavail" runat="server" AssociatedControlID="radunavail"
                                                        CssClass="form-check-label">Unavailable</asp:Label>
                                                </div>
                                            </div>

                                            <asp:Label ID="lbltitle" runat="server" Text="Title" AssociatedControlID="ddlTitle"></asp:Label>
                                            <div class="div_texbox">
                                                <asp:DropDownList ID="ddlTitle" runat="server">
                                                    <asp:ListItem Text="Select Title" Value="-1" />
                                                    <asp:ListItem Text="Mr" Value="Mr" />
                                                    <asp:ListItem Text="Mrs" Value="Mrs" />
                                                    <asp:ListItem Text="Miss" Value="Miss" />
                                                    <asp:ListItem Text="Ms" Value="Ms" />
                                                    <asp:ListItem Text="Dr" Value="Dr" />
                                                    <asp:ListItem Text="Professor" Value="Professor" />
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvtitle" ControlToValidate="ddlTitle" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                            </div>
                                            <asp:Label ID="lblquals" runat="server" Text="Qualifications" AssociatedControlID="txtquals"></asp:Label>
                                            <div class="div_texbox">
                                                <asp:TextBox ID="txtquals" runat="server"
                                                    CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvquals" ControlToValidate="txtquals" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                            </div>

                                            <asp:Label ID="lblskills" runat="server" Text="Skills" AssociatedControlID="txtskills"></asp:Label>
                                            <div class="div_texbox">
                                                <asp:TextBox ID="txtskills" runat="server"
                                                    CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvskills" ControlToValidate="txtskills" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                            </div>

                                            <asp:Label ID="lblexp" runat="server" Text="Experience" AssociatedControlID="txtexp"></asp:Label>
                                            <div class="div_texbox">
                                                <asp:TextBox ID="txtexp" runat="server"
                                                    CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvexp" ControlToValidate="txtexp" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                            </div>

                                        </div>
                                        <hr />
                                        </fieldset>
                                        <fieldset>

                                            <legend>Account Information</legend>

                                            <div class="form">
                                                <asp:Label ID="lbluname" runat="server" Text="Username" AssociatedControlID="txtuname"></asp:Label>
                                                <div class="div_texbox">
                                                    <asp:TextBox ID="txtuname" runat="server"
                                                        CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvuname" ControlToValidate="txtuname" runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                </div>
                                                
                                                <div class="div_texbox">
                                                    
                                                    <asp:HyperLink ID="hyChangePass" runat="server" NavigateUrl="~/changepassword.aspx" CssClass="btn btn-outline-secondary">Change Password</asp:HyperLink>

                                                </div>
                                            </div>

                                        </fieldset>

                                        <fieldset>
                                            <asp:Button ID="btnEdit" runat="server"
                                                Text="Edit Profile"
                                                CssClass="btn btn-outline-success btn-block" OnClick="btnEdit_Click" />

                                            <asp:Button ID="btnSubmit" runat="server"
                                                Text="Save Changes"
                                                CssClass="btn btn-outline-success btn-block" OnClick="btnSubmit_Click" />

                                            <asp:Button ID="btnCancel" runat="server"
                                                Text="Cancel"
                                                CssClass="btn btn-outline-secondary btn-block" OnClick="btnCancel_Click" />
                                        </fieldset>
                                        <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
