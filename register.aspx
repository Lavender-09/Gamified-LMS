<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="MoProf_bse25aft1_250429135.register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script>

         function val_pass(source, args) {
             if (args.Value.Length < 5 && args.Value.length > 10)
                 args.IsValid = false;
             else
                 args.IsValid = true;
         }

            </script> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

        <div class="container my-5">
    <div class="row justify-content-center">
        <div class="col-lg-8 col-md-10">
            
            <div class="card shadow-lg border-0 rounded-4">
                
                <div class="card-header bg-primary text-white text-center rounded-top-4 py-4">
                    <h2 class="mb-0 fw-bold">
                        <i class="fas fa-user-plus me-2"></i>Create Your Account
                    </h2>
                    <p class="mb-0 ">Join our learning community today</p>
                </div>

                    <div class="row">
        <div class=" col-lg-8 col-md-10 offset-md-1">

            <asp:ValidationSummary ID="ValidationSummary1" 
                runat="server" 
                HeaderText="Errors in the form are:" 
                 DisplayMode="BulletList" ForeColor="Red" 
                ShowMessageBox="true" ShowSummary="true"/>

            <br />
            <fieldset>
                <legend>Personal Details</legend>

                <div class="mb-3">
    <div class="form-check-inline">
        <asp:RadioButton ID="radmale" GroupName="gender" runat="server" />
        <asp:Label runat="server" AssociatedControlID="radmale"
            CssClass="form-check-label">Male</asp:Label>
    </div>
    <div class="form-check-inline">
        <asp:RadioButton ID="radfemale" GroupName="gender" runat="server" />
        <asp:Label runat="server" AssociatedControlID="radfemale"
            CssClass="form-check-label">Female</asp:Label>
    </div>
</div>


                <div class="mb-3">
                    <asp:Label runat="server"
                        CssClass="form-label">First name</asp:Label>
                    <asp:TextBox runat="server" ID="txtfname"
                        CssClass="form-control" />

                    <asp:RequiredFieldValidator ID="rfvfname" runat="server" 
                        ErrorMessage="First name is required" 
                        ControlToValidate="txtfname" ForeColor ="Red"
                        Display="Dynamic" Text="Missing" SetFocusOnError="true" >
                    </asp:RequiredFieldValidator>

                </div>
                <div class="mb-3">
                    <asp:Label runat="server"
                        CssClass="form-label">Last name</asp:Label>
                    <asp:TextBox runat="server" ID="txtlname"
                        CssClass="form-control" />

                    <asp:RequiredFieldValidator ID="rfvlname" runat="server" 
    ErrorMessage="Last name is required" 
    ControlToValidate="txtlname" ForeColor ="Red"
    Display="Dynamic" Text="Missing" SetFocusOnError="true" >
</asp:RequiredFieldValidator>

                </div>
                <div class="mb-3">
                    <asp:Label runat="server"
                        CssClass="form-label">Date of Birth</asp:Label>
                    <asp:TextBox runat="server" ID="txtdob" TextMode="Date"
                        CssClass="form-control" />

                    <asp:RequiredFieldValidator ID="rfvdob" runat="server" 
    ErrorMessage="Date of Birth is required" 
    ControlToValidate="txtdob" ForeColor ="Red"
    Display="Dynamic" Text="Missing" SetFocusOnError="true" >
</asp:RequiredFieldValidator>
                </div>

                <div class="mb-3">
    <asp:Label runat="server"
        CssClass="form-label">Profile Picture</asp:Label>
                    <asp:FileUpload ID="fupprofilepic" runat="server" />

</div>
                
            </fieldset>


            <fieldset>
                <hr />
                <legend>Contact Details</legend>
                <div class="mb-3">
                    <asp:Label runat="server"
                        CssClass="form-label">Email Address</asp:Label>
                    <asp:TextBox runat="server" ID="txtemail"
                        CssClass="form-control" />

                    <asp:RegularExpressionValidator ID="revemail" runat="server" 
                        ErrorMessage="Invalid Email" ForeColor="Red" ControlToValidate="txtemail"
                         Text="Invalid" Display="Dynamic" ValidationExpression="^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+[.])+[a-z]{2,5}$">
                    </asp:RegularExpressionValidator>

                                        <asp:RequiredFieldValidator ID="rfvemail" runat="server" 
    ErrorMessage="Email is required" 
    ControlToValidate="txtemail" ForeColor ="Red"
    Display="Dynamic" Text="Missing" SetFocusOnError="true" >
</asp:RequiredFieldValidator>

                </div>
                <div class="mb-3">
                    <asp:Label runat="server"
                        CssClass="form-label">Mobile Number</asp:Label>
                    <asp:TextBox runat="server" ID="txtmob"
                        CssClass="form-control" />

                                        <asp:RequiredFieldValidator ID="rfvphone" runat="server" 
    ErrorMessage="Contact Number is required" 
    ControlToValidate="txtmob" ForeColor ="Red"
    Display="Dynamic" Text="Missing" SetFocusOnError="true" >
</asp:RequiredFieldValidator>

                                    <asp:CompareValidator ID="cvmob" runat="server" ErrorMessage="Invalid Entry" 
                     Type="Integer" ControlToValidate="txtmob"
                    ForeColor ="Red" Text ="Invalid entry"  Operator="DataTypeCheck"
Display="Dynamic" SetFocusOnError="true" >

                </asp:CompareValidator>

                </div>
                                <div class="mb-3">
    <asp:DropdownList runat="server" ID="ddlDistrict"
        CssClass="form-control">
        
        </asp:DropdownList>

<asp:RequiredFieldValidator ID="rfvdistrict" runat="server" 
    ErrorMessage="District is required"
     InitialValue="-1" ControlToValidate="ddlDistrict" 
    ForeColor="Red" Display="Dynamic" 
    SetFocusOnError="true">


</asp:RequiredFieldValidator>
</div>
            </fieldset>

            <fieldset>
<hr />
                <legend>Register Details</legend>
                
                <div class="mb-3">
    <asp:Label runat="server" CssClass="form-label">Upload Results Document (optional)</asp:Label>
    <asp:FileUpload ID="upResults" runat="server" />
    <asp:TextBox ID="txtResultsFilename" runat="server" CssClass="form-control mt-1" placeholder="Custom filename (optional)" />
</div>
                                                                <div class="mb-3">
    <asp:DropdownList runat="server" ID="ddlGrade"
        CssClass="form-control">
        </asp:DropdownList>

<asp:RequiredFieldValidator ID="rfvgrade" runat="server" 
    ErrorMessage="Grade must be specified"
     InitialValue="-1" ControlToValidate="ddlGrade" 
    ForeColor="Red" Display="Dynamic" 
    SetFocusOnError="true">


</asp:RequiredFieldValidator>
</div>

                <div class="mb-3">
                    <asp:Label runat="server"
                        CssClass="form-label">Enter Username</asp:Label>
                    <asp:TextBox runat="server" ID="txtuname"
                        CssClass="form-control" />

                    <asp:RequiredFieldValidator ID="rfvuname" runat="server" 
    ErrorMessage="Username is required" 
    ControlToValidate="txtuname" ForeColor ="Red"
    Display="Dynamic" Text="Missing" SetFocusOnError="true" >
</asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="revuname" runat="server" 
                        ErrorMessage="Invalid Username: should contain 5-15 characters" ForeColor="Red" ControlToValidate="txtuname"
                         Text="Invalid" Display="Dynamic" ValidationExpression="\w{5,15}$">
                    </asp:RegularExpressionValidator>

                </div>
                <div class="mb-3">
                    <asp:Label runat="server"
                        CssClass="form-label">Enter Password</asp:Label>
                    <asp:TextBox runat="server" ID="txtpass" TextMode="Password"
                        CssClass="form-control" />
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Password should be 6-15 characters" ControlToValidate="txtpass"
                         ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate" ClientValidationFunction="val_pass"
                         ValidateEmptyText="true">

                    </asp:CustomValidator>


                </div>
                <div class="mb-3">
                    <asp:Label runat="server"
                        CssClass="form-label">Confirm Password</asp:Label>
                    <asp:TextBox runat="server" ID="txtcpass" TextMode="Password"
                        CssClass="form-control" />

                    <asp:CompareValidator ID="cvcpass" runat="server" ErrorMessage="Password needs to match" 
                        ControlToCompare="txtpass" ControlToValidate="txtcpass" Operator="Equal" 
                        ForeColor ="Red" Text ="Password does not match"
    Display="Dynamic" SetFocusOnError="true" >

                    </asp:CompareValidator>

                    <hr />

                </div>
            </fieldset>
            <fieldset>
                <div class="mb-3">
                    <asp:Button ID="btnRegister" Enabled="true" OnClick="btnRegister_Click" runat="server" CssClass="btn
btn-outline-primary btn-block"
                        Text="Register" />

                    <asp:Button ID="btnClear" runat="server" CssClass="btn
btn-outline-danger btn-block"
                        Text="Clear All"  CausesValidation="false" OnClick="btnClear_Click"/>

                    <asp:HyperLink ID="hyregistertutor" NavigateUrl="~/registertutor.aspx" CssClass="offset-md-1  link-primary text-decoration-underline" runat="server">Apply to become a Tutor</asp:HyperLink>

                    <br />
                    <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                </div>
            </fieldset>
        </div>
    </div>

                </div>
            </div>
        </div>
            </div>
</asp:Content>
