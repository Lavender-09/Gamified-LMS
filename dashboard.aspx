<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="MoProf_bse25aft1_250429135.dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

<div class="card shadow-sm mb-4">
    <div class="card-body">
        <div class="d-flex justify-content-between align-items-center mb-2">
            <h5 class="mb-0"><asp:Label ID="lblLevelName" runat="server" /></h5>
            <span class="text-muted"><asp:Label ID="lblXpText" runat="server" /></span>
        </div>
        <div class="progress" style="height: 20px;">
            <asp:Literal ID="litXpBar" runat="server" />
        </div>
    </div>
</div>

    <div class="d-flex justify-content-between align-items-center mb-3">
        <h4>My Enrolled Subjects</h4>
    </div>

    <div class="row">
        <asp:ListView ID="lvMySubjects" runat="server" OnItemCommand="lvMySubjects_ItemCommand">
            <LayoutTemplate>
    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
</LayoutTemplate>
            <ItemTemplate>
                <div class="col-md-4 mb-3">
                        <div class="card h-100 shadow-sm">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("Sub_Name") %></h5>
                                <p class="card-title"><strong><%# Eval("Grade_Name") %></strong></p>
                                <p class="card-text text-muted mb-1"><%# Eval("Sub_Desc") %></p>
                                <p class="card-text mb-1"><strong>Tutor: </strong><%#Eval("TutorName") %></p>
                                <div class="d-flex justify-content-between mt-2">
                                    <asp:HyperLink CssClass="btn btn-primary btn-sm" NavigateUrl='<%# "~/tutorsubjectdetails.aspx?ts_id=" + Eval("TS_Id") %>' runat="server">View Class</asp:HyperLink>
                                    <asp:LinkButton runat="server" CssClass="btn btn-outline-danger btn-sm"
                                        CommandName="leaveClass" CommandArgument='<%# Eval("TS_Id") %>'
                                        OnClientClick="return confirm('Are you sure you want to leave this class?');">Leave Class</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                </div>
            </ItemTemplate>
            <EmptyDataTemplate>
                <p class="text-muted">You haven't enrolled in any subjects yet.</p>
            </EmptyDataTemplate>            
        </asp:ListView>
    </div>

    <div>
        <h4>Archived Subjects</h4>
        <p class="text-muted small">These classes have been archived by the tutor and are no longer active.</p>
    </div>

    <div class="row">
    <asp:ListView ID="lvArchSubjects" runat="server">
        <ItemTemplate>
            <div class="col-md-4 mb-3">
                <div class="card h-100 shadow-sm bg-light">
                    <div class="card-body">
                        <h5 class="card-title text-muted"><%# Eval("Sub_Name") %></h5>
                        <p class="card-title"><strong><%# Eval("Grade_Name") %></strong></p>
                        <p class="card-text text-muted mb-1"><%# Eval("Sub_Desc") %></p>
                        <p class="card-text mb-1"><strong>Tutor:</strong> <%# Eval("TutorName") %></p>
                        <span class="badge bg-secondary">Archived</span>
                    </div>
                </div>
            </div>
        </ItemTemplate>
        <EmptyDataTemplate>
            <p class="text-muted">No archived subjects.</p>
        </EmptyDataTemplate>
        </asp:ListView>
    </div>

</asp:Content>
