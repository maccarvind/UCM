<%@ Page Title="" Language="C#" MasterPageFile="~/UCMLogged.Master" AutoEventWireup="true" CodeBehind="WHPieceCR.aspx.cs" Inherits="UCM.Warehouse.WHPieceCR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>UCM :: Inward Master</title>
    <script type="text/javascript">

        function bindEvents() {

            $.AdminBSB.dropdownMenu.activate();
            $.AdminBSB.input.activate();
            $.AdminBSB.select.activate();

            $('.datepicker').bootstrapMaterialDatePicker({
                format: 'DD-MMM-YYYY',
                clearButton: true,
                weekStart: 1,
                time: false
            });

        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <form id="Form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="block-header">
            <h2>Warehouse - Piece Checking Report</h2>
        </div>
        <div class="row clearfix">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="card">
                    <div class="body">
                        <div class="row clearfix">
                            <div class="col-sm-5">
                                <div class="form-group form-float">
                                    <div class="form-line">
                                        <asp:DropDownList ID="dropInwardMaster" runat="server" CssClass="form-control show-tick" data-live-search="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="btnGetDetails" runat="server" Text="Get Details" CssClass="btn btn-link bg-teal waves-effect" OnClick="btnGetDetails_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row clearfix">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="card">
                            <div class="body">
                                <div class="row clearfix">
                                    <div class="col-sm-11">
                                        <h2 class="card-inside-title">
                                            <asp:Label ID="lblInwardMasterDetails" runat="server"></asp:Label>
                                        </h2>
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="lnkAddButton" runat="server" OnClick="lnkAddButton_Click">
                                            <i class="material-icons col-teal md-36">add_circle</i>
                                        </asp:LinkButton>
                                    </div>
                                </div>

                                <div class="row clearfix">
                                    <div class="col-sm-12">

                                        <asp:GridView ID="gridInwardItems" runat="server" DataKeyNames="ID" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-striped table-hover table-condensed"
                                            >
                                            <Columns>
                                                <asp:BoundField HeaderText="ID" DataField="ID" />
                                                <asp:TemplateField HeaderText="Details">
                                                    <ItemTemplate>
                                                        <%# UCMHelper.DataFormatter.SafeDate(Eval("CheckingDate")).ToString("dd-MMM-yyyy") %>/<%# Eval("Details")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Defects" DataField="Defects" />
                                                <asp:BoundField HeaderText="Meters" DataField="Meters" />
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <%# (UCMHelper.DataFormatter.SafeInt(Eval("Approved")) == 1 ? "Approved" : "Open")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton id="lnkEdit" runat="server" CommandName="SELECT" CommandArgument='<%# Eval("ID")%>'
                                                            OnClick="lnkEdit_Click">
                                                            <i data-toggle='modal' data-target='#largeModal' class="material-icons col-teal">edit</i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton id="lnkDelete" runat="server" CommandName="DELETE"  CommandArgument='<%# Eval("ID")%>'
                                                            Visible='<%# !( (UCMHelper.DataFormatter.SafeInt(Eval("Defects")) > 0) || (UCMHelper.DataFormatter.SafeInt(Eval("Meters")) > 0) )%>'
                                                            OnClick="lnkDelete_Click">
                                                            <i class="material-icons col-red">delete_forever</i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>

                                </div>

                            </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnGetDetails" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <!-- Large Size -->
        <div class="modal fade" id="largeModal" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-80-percent" role="document">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <script type="text/javascript">
                            Sys.Application.add_load(bindEvents);
                        </script>
                        <div class="modal-content">
                            <div class="modal-header bg-teal">
                                <h4 class="modal-title" id="largeModalLabel">Warehouse - Inward Item Checking Report</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row clearfix">
                                    <div class="col-sm-2">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtCheckingDate" runat="server" CssClass="datepicker form-control"></asp:TextBox>
                                                <label class="form-label">Checking Date</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtDetails" runat="server" CssClass="form-control"></asp:TextBox>
                                                <label class="form-label">Details / Markings</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                <label class="form-label">Remarks</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="upCheckingReport" runat="server">
                                    <ContentTemplate>
                                        <div class="row clearfix">
                                            <div class="col-sm-6">
                                                <div class="card">
                                                    <div class="body">
                                                        <p class="font-bold font-underline">Checking Report</p>
                                                        <div class="row clearfix">

                                                            <div class="col-sm-5">
                                                                <div class="form-group form-float form-group-sm">
                                                                    <div class="form-line">
                                                                        <asp:TextBox ID="txtCRMtrs" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <label class="form-label">Meters (,)</label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-5">
                                                                <div class="form-group form-float form-group-sm">
                                                                    <div class="form-line">
                                                                        <asp:DropDownList ID="dropDefect" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-sm-1">
                                                                <asp:LinkButton ID="lnkAddUpdateCR" runat="server" CssClass="btn bg-teal btn-circle waves-effect waves-circle waves-float" OnClick="lnkAddUpdateCR_Click">
                                                                    <i class="material-icons col-teal md-18">assignment_returned</i>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-sm-12">
                                                                <asp:GridView ID="gridCheckingReport" runat="server" DataKeyNames="ID" OnSelectedIndexChanged="gridCheckingReport_SelectedIndexChanged" AutoGenerateColumns="false"
                                                                    CssClass="table table-bordered table-striped table-hover table-condensed">
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="Meter" DataField="Meter" />
                                                                        <asp:BoundField HeaderText="Defect" DataField="EntName" />
                                                                        <asp:ButtonField CommandName="Select" Text="&lt;i class=&quot;material-icons col-red&quot;&gt;delete_forever&lt;/i&gt;" ItemStyle-HorizontalAlign="Center" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="card">
                                                    <div class="body">
                                                        <p class="font-bold font-underline">Pieces</p>
                                                        <div class="row clearfix">
                                                            <div class="col-sm-2">
                                                                <div class="form-group form-float form-group-sm">
                                                                    <div class="form-line">
                                                                        <asp:TextBox ID="txtLength" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <label class="form-label">Length</label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <div class="form-group form-float form-group-sm">
                                                                    <div class="form-line">
                                                                        <asp:TextBox ID="txtPieceMark" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <label class="form-label">Mark</label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-5">
                                                                <div class="form-group form-float form-group-sm">
                                                                    <div class="form-line">
                                                                        <asp:DropDownList ID="dropGrades" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <asp:LinkButton ID="lnkAddUpdatePieceMtrs" runat="server" CssClass="btn bg-teal btn-circle waves-effect waves-circle waves-float" OnClick="lnkAddUpdatePieceMtrs_Click">
                                                                    <i class="material-icons col-teal">assignment_returned</i>
                                                                </asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="row clearfix">
                                                            <div class="col-sm-12">
                                                                <asp:GridView ID="gridPieces" runat="server" DataKeyNames="ID" OnSelectedIndexChanged="gridPieces_SelectedIndexChanged" AutoGenerateColumns="false"
                                                                    CssClass="table table-bordered table-striped table-hover table-condensed">
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="#" DataField="ID" />
                                                                        <asp:BoundField HeaderText="Qlty." DataField="EntName" />
                                                                        <asp:BoundField HeaderText="Len." DataField="ActualLength" />
                                                                        <asp:BoundField HeaderText="Mark" DataField="PieceMark" />
                                                                        <asp:ButtonField CommandName="Select" Text="&lt;i class=&quot;material-icons col-red&quot;&gt;delete_forever&lt;/i&gt;" ItemStyle-HorizontalAlign="Center" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <div class="row clearfix">
                                    <div class="col-sm-4 align-center">
                                        <div class="switch" id="divapprove" runat="server">
                                            <label><asp:CheckBox ID="chkApprove" runat="server" /><span class="lever"></span>Approved</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 align-left">
                                        <asp:Label ID="lblMessage" runat="server" CssClass="form-error"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:HiddenField ID="txtInwardItemID" runat="server" />
                                        <asp:Button ID="btnUpdateEntry" runat="server" Text="Update Entry" CssClass="btn btn-link bg-teal waves-effect" OnClick="btnUpdateEntry_Click" />
                                        <button type="button" class="btn btn-link bg-teal waves-effect" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gridInwardItems" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
