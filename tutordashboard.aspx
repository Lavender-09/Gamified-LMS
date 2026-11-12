<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="tutordashboard.aspx.cs" Inherits="MoProf_bse25aft1_250429135.tutordashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

<div class="d-flex justify-content-between align-items-center mb-3">
    <h4>My Subjects</h4>
    <asp:HyperLink ID="hlAddSubject" runat="server" CssClass="btn btn-success btn-sm"
        NavigateUrl="~/addtutorsubject.aspx">
        <i class="bi bi-plus-circle"></i> Add Subject
    </asp:HyperLink>
</div>

<div class="row">
    <asp:ListView ID="lvMySubjects" runat="server">
        <ItemTemplate>
            <div class="col-md-4 mb-3">
                <a href='<%# "tutorsubjectdetails.aspx?ts_id=" + Eval("TS_Id") %>' class="text-decoration-none text-dark">
                    <div class="card h-100 shadow-sm">
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Sub_Name") %></h5>
                            <p class="card-title"><strong><%# Eval("Grade_Name") %></strong></p>
                            <p class="card-text text-muted mb-1"><%# Eval("Sub_Desc") %></p>
                            <p class="card-text mb-1"><strong>Fees:</strong> Rs <%# Eval("Fees") %></p>
                            <p class="card-text mb-1">
                                <span class="badge <%# (Eval("TS_Availability").ToString() == "Available") ? "bg-success" : "bg-secondary" %>">
                                    <%# Eval("TS_Availability") %>
                                </span>
                            </p>
                            <p class="card-text"><small class="text-muted">
                                Enrolled: <%# Eval("Curr_Enroll") %> / <%# Eval("Max_Enroll") %>
                            </small>
                            </p>
                            <asp:LinkButton ID="lnkarchive" CssClass="btn btn-warning" runat="server" OnClientClick="return confirm('Are you sure you want to archive class?')" OnClick="lnkarchive_Click" CommandArgument='<%#Eval("TS_Id") %>'>Archive Class</asp:LinkButton>
                            <p class="card-text align-content-end"></p>
                        </div>
                    </div>
                </a>
            </div>
        </ItemTemplate>
        <EmptyDataTemplate>
            <p class="text-muted">You haven't added any subjects yet.</p>
        </EmptyDataTemplate>
    </asp:ListView>
</div>

    <div>
        <h4>Archived Subjects</h4>
</div>

<div class="row">
    <asp:ListView ID="lvArchSubjects" runat="server">
        <ItemTemplate>
            <div class="col-md-4 mb-3">
                    <div class="card h-100 shadow-sm">
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Sub_Name") %></h5>
                            <p class="card-title"><strong><%# Eval("Grade_Name") %></strong></p>
                            <p class="card-text text-muted mb-1"><%# Eval("Sub_Desc") %></p>
                            <p class="card-text mb-1"><strong>Fees:</strong> Rs <%# Eval("Fees") %></p>
                            <p class="card-text mb-1">
                                <span class="badge <%# (Eval("TS_Availability").ToString() == "Available") ? "bg-success" : "bg-secondary" %>">
                                    <%# Eval("TS_Availability") %>
                                </span>
                            </p>
                            <p class="card-text"><small class="text-muted">
                                Enrolled: <%# Eval("Curr_Enroll") %> / <%# Eval("Max_Enroll") %>
                            </small>
                            </p>
                            <asp:LinkButton ID="lnkunarchive" CssClass="btn btn-success" runat="server" OnClientClick="return confirm('Are you sure you want to unarchive class?')" OnClick="lnkunarchive_Click" CommandArgument='<%#Eval("TS_Id") %>'>Unarchive Class</asp:LinkButton>
                            <p class="card-text align-content-end"></p>
                        </div>
                    </div>
                </a>
            </div>
        </ItemTemplate>
        <EmptyDataTemplate>
            <p class="text-muted">No classes archived.</p>
        </EmptyDataTemplate>
    </asp:ListView>
</div>

</asp:Content>
