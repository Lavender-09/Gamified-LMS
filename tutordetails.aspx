<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="tutordetails.aspx.cs" Inherits="MoProf_bse25aft1_250429135.tutordetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">


        <div class="container my-5">
            <div class="row justify-content-center">
                <div class="col-lg-6 col-md-8">

                    <div class="card-header bg-primary text-white text-center rounded-top-4 py-4">
                        <h2 class="mb-0 fw-bold">
                            <i class="fas fa-user me-2"></i>Detailed Tutor View
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

                                <asp:TemplateField HeaderText="Tutor Name">
                                    <ItemTemplate>
                                        <h3 class="text-center fw-bold mt-3 mb-0">
                                            <%# Eval("Title") %> <%# Eval("F_Name") %> <%# Eval("L_Name") %>
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

                                <asp:TemplateField HeaderText="Experience">
                                    <ItemTemplate>
                                        <div class="px-4 py-2 border-top">
                                            <span class="fw-semibold"><i class="fas fa-briefcase me-2"></i>Experience</span>
                                            <p class="mb-0"><%# Eval("Tut_Experience") %></p>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Skills">
                                    <ItemTemplate>
                                        <div class="px-4 py-2 border-top">
                                            <span class="fw-semibold"><i class="fas fa-screwdriver-wrench me-2"></i>Skills</span>
                                            <p class="mb-0"><%# Eval("Tut_Skills") %></p>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qualifications">
                                    <ItemTemplate>
                                        <div class="px-4 py-2 border-top">
                                            <span class="fw-semibold"><i class="fas fa-graduation-cap me-2"></i>Qualifications</span>
                                            <p class="mb-0"><%# Eval("Tut_Quals") %></p>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Availability">
                                    <ItemTemplate>
                                        <div class="px-4 py-2 border-top">
                                            <span class="fw-semibold"><i class="fas fa-clock me-2"></i>Availability</span>
                                            <p class="mb-0"><%# Eval("Tut_Availability") %></p>
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

                            </Fields>



                        </asp:DetailsView>

                        <div class="mb-3">
    <h5>Average Rating</h5>
    <asp:Label ID="lblAvgStars" runat="server" CssClass="fs-3" style="color:gold;" />
    <asp:Label ID="lblAvgRatingText" runat="server" CssClass="text-muted ms-2" />
</div>
                                        <asp:Panel ID="pnlRecommend" runat="server" CssClass="card mt-4 p-3">
    <h5>Rate this Tutor</h5>

    <asp:Panel ID="pnlAlreadyRated" runat="server" Visible="false" CssClass="alert alert-info">
        You've already submitted a recommendation for this tutor.
    </asp:Panel>

    <asp:Panel ID="pnlRateForm" runat="server">
        <div class="star-rating mb-3" id="starRating">
            <span class="star" data-value="1">&#9733;</span>
            <span class="star" data-value="2">&#9733;</span>
            <span class="star" data-value="3">&#9733;</span>
            <span class="star" data-value="4">&#9733;</span>
            <span class="star" data-value="5">&#9733;</span>
        </div>
        <asp:HiddenField ID="hdnRating" runat="server" Value="0" />

        <asp:TextBox ID="txtTestimonial" runat="server" CssClass="form-control mb-2"
            TextMode="MultiLine" Rows="3" placeholder="Share your experience..." />

        <asp:LinkButton ID="btnSubmitRec" runat="server" CssClass="btn btn-primary"
            OnClick="btnSubmitRec_Click" OnClientClick="return validateRating();">
            Submit Recommendation
        </asp:LinkButton>
        <asp:Label ID="lblRecMsg" runat="server" CssClass="d-block mt-2" />
    </asp:Panel>
</asp:Panel>

                    </div>
                </div>
            </div>
        </div>


<style>
    .star-rating .star { font-size: 2rem; cursor: pointer; color: #ccc; }
    .star-rating .star.filled { color: gold; }
</style>

    <script>
document.addEventListener("DOMContentLoaded", function() {
    var stars = document.querySelectorAll("#starRating .star");
        var hiddenField = document.getElementById("<%= hdnRating.ClientID %>");

        stars.forEach(function (star) {
            star.addEventListener("click", function () {
                var value = parseInt(this.getAttribute("data-value"));
                hiddenField.value = value;
                stars.forEach(function (s) {
                    s.classList.toggle("filled", parseInt(s.getAttribute("data-value")) <= value);
                });
            });
        });
    });

    function validateRating() {
        var hiddenField = document.getElementById("<%= hdnRating.ClientID %>");
        if (parseInt(hiddenField.value) < 1) {
            alert("Please select a star rating before submitting.");
            return false;
        }
        return true;
    }
</script>
</asp:Content>
