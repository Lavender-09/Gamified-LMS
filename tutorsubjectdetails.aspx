<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="tutorsubjectdetails.aspx.cs" Inherits="MoProf_bse25aft1_250429135.tutorsubjectdetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <ul class="nav nav-tabs" id="dashboardTabs" role="tablist">
        <li class="nav-item">
            <a class="nav-link active" id="announcements-tab" data-bs-toggle="tab" href="#announcements" role="tab">Announcements</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" id="materials-tab" data-bs-toggle="tab" href="#materials" role="tab">Materials</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" id="students-tab" data-bs-toggle="tab" href="#students" role="tab">Students</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" id="timetable-tab" data-bs-toggle="tab" href="#timetable" role="tab">Timetable</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" id="lessons-tab" data-bs-toggle="tab" href="#lessons" role="tab">Lessons</a>
        </li>
    </ul>


    <div class="tab-content mt-3" id="dashboardTabsContent">

        <!-- Announcements Tab -->
        <div class="tab-pane fade show active" id="announcements" role="tabpanel">

            <asp:UpdatePanel ID="upAnnouncements" runat="server">
                <ContentTemplate>

                    <asp:Panel ID="pnlPostAnnouncement" runat="server">
                        <div class="mb-4">
                            <asp:TextBox ID="txtAnnTitle" runat="server" CssClass="form-control mb-2" placeholder="Announcement title" />
                            <asp:TextBox ID="txtAnnContent" runat="server" CssClass="form-control mb-2" TextMode="MultiLine" Rows="3" placeholder="Write an announcement..." />
                            <asp:LinkButton ID="btnPostAnn" runat="server" CssClass="btn btn-primary" OnClick="btnPostAnn_Click">Post</asp:LinkButton>
                        </div>
                    </asp:Panel>

                    <asp:Repeater ID="rptAnnouncements" runat="server">
                        <ItemTemplate>
                            <div class="card mb-3 shadow-sm">
                                <div class="card-body">
                                    <h5 class="card-title"><%# Eval("Ann_Title") %></h5>
                                    <h6 class="card-subtitle mb-2 text-muted"><%# Eval("Ann_Date", "{0:dd MMM yyyy, HH:mm}") %></h6>
                                    <p class="card-text"><%# Eval("Content") %></p>
                                </div>
                            </div>
                        </ItemTemplate>

                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <!-- Materials Tab -->
        <div class="tab-pane fade" id="materials" role="tabpanel">

            <asp:UpdatePanel ID="upMaterials" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlUploadMaterial" runat="server">
                        <div class="mb-4">
                            <asp:FileUpload ID="fupMaterial" runat="server" CssClass="form-control mb-2" />
                            <asp:LinkButton ID="btnUploadMaterial" runat="server" CssClass="btn btn-primary" OnClick="btnUploadMaterial_Click">Upload</asp:LinkButton>
                        </div>
                    </asp:Panel>

                    <asp:Repeater ID="rptMaterials" runat="server">
                        <ItemTemplate>
                            <div class="d-flex justify-content-between align-items-center border-bottom py-2">
                                <a href='<%# GetMaterialUrl(Eval("Mat_File_Name")) %>' target="_blank">
                                    <%# Eval("Mat_File_Name") %>
                        </a>
                                <small class="text-muted"><%# Eval("Mat_Upload", "{0:dd MMM yyyy, HH:mm}") %></small>
                            </div>
                        </ItemTemplate>

                    </asp:Repeater>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUploadMaterial" />
                </Triggers>
            </asp:UpdatePanel>

        </div>

        <!-- Students Tab -->
        <div class="tab-pane fade" id="students" role="tabpanel">

            <asp:Repeater ID="rptEnrolledStudents" runat="server" OnItemCommand="rptEnrolledStudents_ItemCommand" OnItemDataBound="rptEnrolledStudents_ItemDataBound">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkStudentRow" runat="server" CssClass="text-decoration-none text-dark d-block"
                        CommandName="viewStudent" CommandArgument='<%# Eval("Stu_Id") %>'>
                <div class="d-flex align-items-center border-bottom py-2 px-2 student-row">
                    <img src='<%# ResolveUrl("~/images/") + (Eval("ProfilePic") ?? "reviwer1.jpg") %>'
                         class="rounded-circle me-3" width="45" height="45" style="object-fit:cover;" />
                    <div class="flex-grow-1">
                        <strong><%# Eval("F_Name") %> <%# Eval("L_Name") %></strong>
                    </div>
                    <small class="text-muted">
                        Last login: <%# Eval("Last_Login") != DBNull.Value ? Convert.ToDateTime(Eval("Last_Login")).ToString("dd MMM yyyy, HH:mm") : "Never" %>
                    </small>
                </div>
            </asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>

        </div>

        <!-- Timetable Tab -->
        <div class="tab-pane fade" id="timetable" role="tabpanel">

            <div class="text-center">
                <asp:Image ID="imgTimetable" runat="server" CssClass="img-fluid rounded shadow" Visible="false" />
                <asp:Label ID="lblNoTimetable" runat="server" CssClass="text-muted" Text="No timetable uploaded yet." Visible="false" />

            </div>
        </div>

        <!-- Lessons Tab -->
        <div class="tab-pane fade" id="lessons" role="tabpanel">

            <asp:UpdatePanel ID="upLessons" runat="server">
                <ContentTemplate>

                    <asp:Panel ID="pnlCreateLesson" runat="server">
                        <div class="mb-4">
                            <label>Lesson Date & Time</label>
                            <asp:TextBox ID="txtLessonDateTime" runat="server" TextMode="DateTimeLocal" CssClass="form-control mb-2" />
                            <asp:LinkButton ID="btnCreateLesson" runat="server" CssClass="btn btn-primary" OnClick="btnCreateLesson_Click">Schedule Lesson</asp:LinkButton>
                            <asp:Label ID="lblLessonMsg" runat="server" CssClass="d-block mt-2" />
                        </div>

                    </asp:Panel>

                    <asp:Repeater ID="rptLessons" runat="server" OnItemCommand="rptLessons_ItemCommand" OnItemDataBound="rptLessons_ItemDataBound">
                        <ItemTemplate>
                            <div class="d-flex justify-content-between align-items-center border-bottom py-2">
                                <div>
                                    <strong><%# Eval("Less_Date", "{0:dddd, dd MMM yyyy - HH:mm}") %></strong>
                                    <br />
                                    <small class="text-muted">Seats: <%# Eval("Curr_Seats") %> / <%# Eval("Max_Seats") %>
                                        <%# Convert.ToBoolean(Eval("Is_Available")) ? "" : " (Full)" %>
                    </small>
                                </div>

                                <asp:Panel ID="Panel1" runat="server" Visible='<%# IsTutorView() %>'>
                                    <asp:Repeater ID="rptPendingRequests" runat="server" OnItemCommand="rptPendingRequests_ItemCommand">
                                        <ItemTemplate>
                                            <div class="d-flex justify-content-between align-items-center ps-3 py-1">
                                                <small><%# Eval("F_Name") %> <%# Eval("L_Name") %> - pending</small>
                                                <div>
                                                    <asp:LinkButton ID="LinkButton1" runat="server"
                                                        CommandArgument='<%# Eval("Stu_Id") %>' CommandName="accept"
                                                        CssClass="btn btn-sm btn-success">Accept</asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton2" runat="server"
                                                        CommandArgument='<%# Eval("Stu_Id") %>' CommandName="decline"
                                                        CssClass="btn btn-sm btn-outline-danger">Decline</asp:LinkButton>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </asp:Panel>

                                <asp:Panel runat="server" Visible='<%# !IsTutorView() %>'>
                                    <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm"
                                        CommandName="bookSeat" CommandArgument='<%# Eval("Less_Id") %>'
                                        Visible='<%# Convert.ToBoolean(Eval("Is_Available")) && Eval("MyBookingStatus") == DBNull.Value %>'>
        Book Seat
                                    </asp:LinkButton>

                                    <span class="badge bg-warning text-dark" visible='<%# GetBookingStatus(Eval("MyBookingStatus")) == "Pending" %>'>Pending tutor approval
                                    </span>

                                    <asp:LinkButton runat="server" CssClass="btn btn-success btn-sm"
                                        CommandName="pay" CommandArgument='<%# Eval("Less_Id") %>'
                                        Visible='<%# GetBookingStatus(Eval("MyBookingStatus")) == "Booked" && Eval("MyPaymentId") == DBNull.Value %>'>
        Proceed to Payment
                                    </asp:LinkButton>

                                    <span class="badge bg-success" Visible='<%# GetBookingStatus(Eval("MyBookingStatus")) == "Booked" && Eval("MyPaymentId") != DBNull.Value %>'>Paid</span>

                                    <asp:LinkButton runat="server" CssClass="btn btn-outline-danger btn-sm"
                                        CommandName="cancelSeat" CommandArgument='<%# Eval("Less_Id") %>'
                                        OnClientClick="return confirm('Cancel your booking for this lesson?');"
                                        Visible='<%# Eval("MyBookingStatus") != DBNull.Value %>'>
        Cancel Booking
                                    </asp:LinkButton>
                                </asp:Panel>

                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
