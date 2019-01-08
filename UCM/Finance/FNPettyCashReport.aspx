<%@ Page Title="" Language="C#" MasterPageFile="~/UCMLogged.Master" AutoEventWireup="true" CodeBehind="FNPettyCashReport.aspx.cs" Inherits="UCM.Finance.FNPettyCashReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>UCM :: Petty Cash Report</title>
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
            <h2>Finance - Petty Cash Report</h2>
        </div>
        <div class="row clearfix">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="card">
                    <div class="card body">
                        <div class="row clearfix">
                            <div class="col-md-2">
                                <div class="form-group form-float">
                                    <div class="form-line">
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                        <label class="form-label">From</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group form-float">
                                    <div class="form-line">
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                        <label class="form-label">To</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group form-float">
                                    <div class="form-line">
                                        <asp:DropDownList ID="dropCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="butGenerateReport" runat="server" Text="Search" CssClass="btn bg-teal waves-effect" OnClick="butGenerateReport_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row clearfix">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="card">
                    <div class="body">

                        <div class="row clearfix">
                            <div class="col-sm-12">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <script type="text/javascript">
                                            Sys.Application.add_load(bindEvents);
                                        </script>
                                        <h2 class="card-inside-title">Report: 
                                            <asp:Label ID="lblReportMessasge" runat="server" CssClass="right"></asp:Label></h2>
                                        
                                        <asp:GridView ID="gridPettyCashReport" runat="server" AutoGenerateColumns="true"
                                            CssClass="table table-bordered table-striped table-hover table-condensed" OnRowDataBound="gridPettyCashReport_RowDataBound" OnPreRender="gridPettyCashReport_PreRender">
                                            </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="butGenerateReport" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
