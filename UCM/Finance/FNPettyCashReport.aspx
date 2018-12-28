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
                                        <asp:GridView ID="gridPettyCashReport" runat="server" DataKeyNames="ID" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-striped table-hover table-condensed">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Head / Nature">
                                                    <ItemTemplate>
                                                        <%# ( (Eval("ExpType").ToString() == "HEAD" || Eval("CrDr").ToString() == "C")
                                                            ? "<b>"+Eval("ExpName")+"</b>"  
                                                            : " <i class=\"material-icons\" style=\"vertical-align: middle;\">keyboard_arrow_right</i> " + Eval("ExpName"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cr" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <%# (Eval("CrDr").ToString() == "C" 
                                                            ? "<b>" + UCMHelper.DataFormatter.SafeDouble(Eval("Total")).ToString("F") + "</b>"  
                                                            : "0.00")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dr" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <%# (Eval("CrDr").ToString() == "D" 
                                                            ? (Eval("ExpType").ToString() == "HEAD" 
                                                                ?  "<b>" + UCMHelper.DataFormatter.SafeDouble(Eval("Total")).ToString("F")  +"</b>"
                                                                : UCMHelper.DataFormatter.SafeDouble(Eval("Total")).ToString("F")  + "&nbsp;&nbsp;&nbsp;" )
                                                            : (Eval("ExpType").ToString() == "HEAD" 
                                                                ?  "<b>0.00</b>"
                                                                : "0.00" + "&nbsp;&nbsp;&nbsp;" )
                                                            )%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
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
