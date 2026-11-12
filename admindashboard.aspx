<%@ Page Title="" Language="C#" MasterPageFile="~/moprofmaster.Master" AutoEventWireup="true" CodeBehind="admindashboard.aspx.cs" Inherits="MoProf_bse25aft1_250429135.admindashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <ul class="nav nav-tabs" id="dashboardTabs" role="tablist">
    <li class="nav-item">
        <a class="nav-link active" id="tutors-tab" data-bs-toggle="tab" href="#tutors" role="tab">Tutor Applications</a>
    </li>
    <li class="nav-item">
        <a class="nav-link" id="subjects-tab" data-bs-toggle="tab" href="#subjects" role="tab">Subjects</a>
    </li>
        <li class="nav-item">
        <a class="nav-link" id="categories-tab" data-bs-toggle="tab" href="#categories" role="tab">Categories</a>
    </li>
            <li class="nav-item">
        <a class="nav-link" id="districts-tab" data-bs-toggle="tab" href="#districts" role="tab">Districts</a>
    </li>
            <li class="nav-item">
        <a class="nav-link" id="analytics-tab" data-bs-toggle="tab" href="#analytics" role="tab">Analytics</a>
    </li>
</ul>


    <div class="tab-content mt-3" id="dashboardTabsContent">


        <!--Tutor Applications Tab -->
    <div class="tab-pane fade show active" id="tutors" role="tabpanel">
       
    <h2>Approve / Reject Tutors: </h2>
    <asp:GridView ID="gvs" ClientIDMode="Static" CssClass="table table-striped
table-bordered" OnPreRender="gvs_PreRender"
        runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="F_Name" HeaderText="First Name" />
            <asp:BoundField DataField="L_Name" HeaderText="Last Name" />
            <asp:BoundField DataField="Username" HeaderText="Username" />
            <asp:BoundField DataField="Reg_Date" HeaderText="Request Date & Time" />
            <asp:BoundField DataField="Approval_Status" HeaderText="Approval Status" />
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <%--store the movie id in the hiddenfield--%>
                    <%--store the user id in the LinkButtons--%>
                    <asp:LinkButton ID="lnkdeny" CssClass="btn btn-danger"
                        runat="server" OnClick="lnkdeny_Click" CommandArgument='<%#Eval("u_id") %>'>Deny Access</asp:LinkButton><br />
                    <br />
                    <asp:LinkButton ID="lnkgrant" CssClass="btn btn-success"
                        runat="server" OnClick="lnkgrant_Click" CommandArgument='<%#Eval("u_id") %>'>Grant Access</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>    
    </div>

        <!-- Subjects tab -->
    <div class="tab-pane fade" id="subjects" role="tabpanel">
        <h2>Manage Subjects: </h2>

        <asp:UpdatePanel ID="upSubjects" runat="server">
            <ContentTemplate>

                <div class="row">
                <div class="col-md-3">
                    <h5>Grades</h5>
                    <asp:Repeater ID="rptGrades" runat="server" OnItemCommand="rptGrades_ItemCommand">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CssClass="list-group-item list-group-item-action link-primary px-3 text-decoration-underline"
                                CommandName="selectGrade"
                                CommandArgument='<%# Eval("Grade_Id") %>'>
                                <%# Eval("Grade_Name") %>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                    <div class="col-md-9">
                    <div class="d-flex justify-content-between align-items-center">
                        <h5>Subjects <asp:Label ID="lblSelectedGrade" runat="server" CssClass="text-muted" /></h5>
                        <asp:LinkButton ID="btnAddSubject" runat="server" CssClass="btn btn-success btn-sm"
                            OnClick="btnAddSubject_Click">Add Subject
                        </asp:LinkButton>
                    </div>

                    <asp:GridView ID="gvSubjects" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-striped" OnRowCommand="gvSubjects_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Subject Name">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CommandName="editSubject" CssClass="link-primary text-decoration-underline"
                                        CommandArgument='<%# Eval("Sub_Id") %>'>
                                        <%# Eval("Sub_Name") %>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Sub_Desc" HeaderText="Description" />
                            <asp:BoundField DataField="Cat_Name" HeaderText="Category" />
                        </Columns>
                    </asp:GridView>

                        <asp:Panel ID="pnlSubjectForm" runat="server" Visible="false" CssClass="border p-3 mt-2">
                        <asp:HiddenField ID="hidSubId" runat="server" />
                        <div class="mb-2">
                            <label>Subject Name</label>
                            <asp:TextBox ID="txtSubName" runat="server" CssClass="form-control" />
                        </div>
                        <div class="mb-2">
                            <label>Description</label>
                            <asp:TextBox ID="txtSubDesc" runat="server" TextMode="MultiLine" CssClass="form-control" />
                        </div>
                        <div class="mb-2">
                            <label>Category</label>
                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-select"
                                DataTextField="Cat_Name" DataValueField="Cat_Id" />
                        </div>
                        <asp:LinkButton ID="btnSaveSubject" runat="server" CssClass="btn btn-primary"
                            OnClick="btnSaveSubject_Click">Save</asp:LinkButton>
                        <asp:LinkButton ID="btnCancelSubject" runat="server" CssClass="btn btn-secondary"
                            OnClick="btnCancelSubject_Click">Cancel</asp:LinkButton>
                            <asp:LinkButton ID="btnDeleteSubject" runat="server" CssClass="btn btn-danger"
                                OnClick="btnDeleteSubject_Click">Delete</asp:LinkButton>
                    </asp:Panel>
                </div>
            </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

                <!-- Categories tab -->
    <div class="tab-pane fade" id="categories" role="tabpanel">
        <h2>Manage Categories: </h2>

        <asp:UpdatePanel ID="upCategories" runat="server">
            <ContentTemplate>

                <div class="row">
                <div class="col-md-3 card shadow-lg border-0 rounded-4">
                    <h5>Categories</h5>
                    <asp:Repeater ID="rptCat" runat="server" OnItemCommand="rptCat_ItemCommand">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CssClass=""
                                CommandName="selectCategory"
                                CommandArgument='<%# Eval("Cat_Id") %>'>
                                <%# Eval("Cat_Name") %>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                    <div class="col-md-9">
                    <div class="d-flex justify-content-between align-items-center">
                        <h5>Categories <asp:Label ID="lblCategory" runat="server" CssClass="text-muted" /></h5>
                        <asp:LinkButton ID="btnAddCategory" runat="server" CssClass="btn btn-success btn-sm"
                            OnClick="btnAddCategory_Click">Add Category
                        </asp:LinkButton>
                    </div>

                        <asp:Panel ID="pnlCategory" runat="server" Visible="false" CssClass="border p-3 mt-2">
                        <asp:HiddenField ID="hidCatId" runat="server" />
                        <div class="mb-2">
                            <label>Category Name</label>
                            <asp:TextBox ID="txtcatname" runat="server" CssClass="form-control" />
                        </div>
                        <div class="mb-2">
                            <label>Description</label>
                            <asp:TextBox ID="txtcatdesc" runat="server" TextMode="MultiLine" CssClass="form-control" />
                        </div>
                        <asp:LinkButton ID="btnSaveCat" runat="server" CssClass="btn btn-primary"
                            OnClick="btnSaveCat_Click">Save</asp:LinkButton>
                        <asp:LinkButton ID="btnCancelCat" runat="server" CssClass="btn btn-secondary"
                            OnClick="btnCancelCat_Click">Cancel</asp:LinkButton>
                            <asp:LinkButton ID="btnDeleteCat" runat="server" CssClass="btn btn-danger"
                                OnClick="btnDeleteCat_Click">Delete</asp:LinkButton>
                    </asp:Panel>
                </div>
            </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>


                <!-- Districts tab -->
    <div class="tab-pane fade" id="districts" role="tabpanel">
        <h2>Manage Districts: </h2>

        <asp:UpdatePanel ID="upDistricts" runat="server">
            <ContentTemplate>

                <div class="row">
                <div class="col-md-3 card shadow-lg border-0 rounded-4">
                    <h5>Districts</h5>
                    <asp:Repeater ID="rptDis" runat="server" OnItemCommand="rptDis_ItemCommand">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CssClass=""
                                CommandName="selectDistrict"
                                CommandArgument='<%# Eval("Dis_Id") %>'>
                                <%# Eval("Dis_Name") %>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                    <div class="col-md-9">
                    <div class="d-flex justify-content-between align-items-center">
                        <h5>Districts <asp:Label ID="lblDistrict" runat="server" CssClass="text-muted" /></h5>
                        <asp:LinkButton ID="btnAddDistrict" runat="server" CssClass="btn btn-success btn-sm"
                            OnClick="btnAddDistrict_Click">Add District
                        </asp:LinkButton>
                    </div>

                        <asp:Panel ID="pnlDistrict" runat="server" Visible="false" CssClass="border p-3 mt-2">
                        <asp:HiddenField ID="hidDisId" runat="server" />
                        <div class="mb-2">
                            <label>District Name</label>
                            <asp:TextBox ID="txtdisname" runat="server" CssClass="form-control" />
                        </div>
                        
                        <asp:LinkButton ID="btnSaveDis" runat="server" CssClass="btn btn-primary"
                            OnClick="btnSaveDis_Click">Save</asp:LinkButton>
                        <asp:LinkButton ID="btnCancelDis" runat="server" CssClass="btn btn-secondary"
                            OnClick="btnCancelDis_Click">Cancel</asp:LinkButton>
                            <asp:LinkButton ID="btnDeleteDis" runat="server" CssClass="btn btn-danger"
                                OnClick="btnDeleteDis_Click">Delete</asp:LinkButton>
                    </asp:Panel>
                </div>
            </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>


        <!-- Analytics tab -->
    <div class="tab-pane fade" id="analytics" role="tabpanel">
        <h2>Analytics & Insights: </h2>

        <canvas id="pChart" width="400" height="200"></canvas>

        <script> function renderPieChart(data) {
            var ctx = document.getElementById('pChart').getContext('2d'); new Chart(ctx, {
                type: 'pie', data: data, options: {
                    plugins: { legend: { position: 'top' }, tooltip: { enabled: true } }, responsive: true, maintainAspectRatio: true, aspectRatio: 2, plugins: {
                        title: {
                            display: true, // Enables the title 
                            text: 'Number of Users', // Title text 
                            position: 'bottom', font: {
                                size: 20, // Font size 
                                family: 'Arial', // Font family 
                                weight: 'bold' // Font weight 
                            }, color: '#333', // Title color 
                            padding: {
                                top: 10, // Padding above the title 
                                bottom: 30 // Padding below the title 
                            }
                        }
                    }
} }); } </script>

        <canvas id="bChart" width="400" height="200"></canvas>

        <script>
            function renderBarChart(data) {
                var ctx = document.getElementById('bChart').getContext('2d'); new Chart(ctx, {
                    type: 'bar', data: data, options: {
                        scales: {
                            y: {
                                beginAtZero: true
                            }
                        }
                    }
                });
            }
        </script>

                <canvas id="bChart2" width="400" height="200"></canvas>

        <script>
            function renderBarChart2(data) {
                var ctx = document.getElementById('bChart2').getContext('2d'); new Chart(ctx, {
                    type: 'bar', data: data, options: {
                        scales: {
                            y: {
                                beginAtZero: true
                            }
                        }
                    }
                });
            }
        </script>


        </div>

</div>
        

</asp:Content>
