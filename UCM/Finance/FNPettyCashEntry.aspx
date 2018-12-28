<%@ Page Title="" Language="C#" MasterPageFile="~/UCMLogged.Master" AutoEventWireup="true" CodeBehind="FNPettyCashEntry.aspx.cs" Inherits="UCM.Finance.FNPettyCashEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>UCM :: Petty Cash</title>
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
            <h2>Finance - Petty Cash Entry</h2>
        </div>


        <div class="row clearfix">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="card">

                    <div class="card body">
                        <div class="row clearfix">
                            <div class="col-md-3">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <script type="text/javascript">
                                            Sys.Application.add_load(bindEvents);
                                        </script>
                                        <div class="input-group">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtTranDate" runat="server" CssClass="form-control datepicker" placeholder="Tran. Date"></asp:TextBox>
                                            </div>
                                            <asp:LinkButton ID="lnkGetDate" runat="server" CssClass="input-group-addon" OnClick="lnkGetDate_Click">
                                            <i class="material-icons">send</i>
                                            </asp:LinkButton>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-9">
                                <ul class="nav nav-tabs tab-col-teal" role="tablist">

                                    <li role="presentation">
                                        <a href="#cashin" id="cashInLink" data-toggle="tab">
                                            <i class="material-icons">add_to_queue</i> Cash In
                                        </a>
                                    </li>
                                    <li role="presentation">
                                        <a href="#cashout" id="cashOutLink" data-toggle="tab">
                                            <i class="material-icons">remove_from_queue</i> Cash Out
                                        </a>
                                    </li>

                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <ul class="nav nav-tabs">
                                                <li class="nav nav-tabs right">
                                                    <a style="color: #222 !important;">
                                                        <i class="material-icons">date_range</i>
                                                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                                                    </a>
                                                </li>
                                            </ul>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="lnkGetDate" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>

                                </ul>

                            </div>
                        </div>
                        <div class="row clearfix">

                            <div class="col-md-12">
                                <div class="tab-content">
                                    <div role="tabpanel" class="tab-pane fade" id="cashin">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <script type="text/javascript">
                                                    Sys.Application.add_load(bindEvents);
                                                </script>
                                                <div class="row clearfix">
                                                    <div class="col-md-3">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:DropDownList ID="dropCashInCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:TextBox ID="txtCashInRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <label class="form-label">Remarks</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:TextBox ID="txtCashInAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <label class="form-label">Amount</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:HiddenField ID="hidEntryID" runat="server" />
                                                            <asp:Button ID="butCashIn" runat="server" Text="Add Entry" CssClass="btn bg-teal waves-effect" OnClick="butCashIn_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="lblCashInMessage" runat="server" CssClass="form-error"></asp:Label>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div role="tabpanel" class="tab-pane fade" id="cashout">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <script type="text/javascript">
                                                    Sys.Application.add_load(bindEvents);
                                                </script>
                                                <div class="row clearfix">
                                                    <div class="col-md-3">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:DropDownList ID="dropCashOutCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:DropDownList ID="dropHeadOfExp" runat="server" CssClass="form-control"  AutoPostBack="true"
                                                                    OnSelectedIndexChanged="dropHeadOfExp_SelectedIndexChanged"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:DropDownList ID="dropNatureOfExp" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <asp:Button ID="butCashOut" runat="server" Text="Add Entry" CssClass="btn bg-teal waves-effect" OnClick="butCashOut_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-2">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:TextBox ID="txtBillDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                                <label class="form-label">Bill Date</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:TextBox ID="txtBillNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <label class="form-label">Bill No</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:TextBox ID="txtVendor" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <label class="form-label">Vendor</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:TextBox ID="txtCashOutRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <label class="form-label">Remarks</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <div class="form-group form-float">
                                                            <div class="form-line">
                                                                <asp:TextBox ID="txtCashOutAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <label class="form-label">Amount</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row clearfix">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="lblCashOutMessage" runat="server" CssClass="form-error"></asp:Label>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
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
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <h2 class="card-inside-title">Entry List: 
                                    <asp:Label ID="lblEntryMessasge" runat="server" CssClass="right"></asp:Label></h2>
                                    <div class="row clearfix">
                                        <div class="col-sm-12">

                                            <asp:GridView ID="gridPettyCashEntries" runat="server" DataKeyNames="ID" AutoGenerateColumns="false"
                                                CssClass="table table-bordered table-striped table-hover table-condensed">
                                                <Columns>
                                                    <asp:BoundField HeaderText="ID" DataField="ID" />
                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <%# UCMHelper.DataFormatter.SafeDate(Eval("TranDate")).ToString("dd-MMM-yyyy") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Company" DataField="Company" />
                                                    <asp:BoundField HeaderText="Head" DataField="ExpHeadName" />
                                                    <asp:BoundField HeaderText="Nature" DataField="ExpNatureName" />
                                                    <asp:TemplateField HeaderText="Details">
                                                        <ItemTemplate>
                                                            <%# (UCMHelper.DataFormatter.SafeDate(Eval("BillDate")) != UCMHelper.DataFormatter.SafeDate("1/1/1900")) ? UCMHelper.DataFormatter.SafeDate(Eval("BillDate")).ToString("dd-MMM-yyyy") : "" %>
                                                            <%# Eval("Vendor")%>
                                                            <%# Eval("BillNo")%>
                                                            <%# Eval("Remarks")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cr" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <%# (Eval("CrDr").ToString() == "C" ? UCMHelper.DataFormatter.SafeDouble(Eval("Amount")).ToString("F")  : "0.00")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dr" ItemStyle-HorizontalAlign="Right">
                                                        <ItemTemplate>
                                                            <%# (Eval("CrDr").ToString() == "D" ? UCMHelper.DataFormatter.SafeDouble(Eval("Amount")).ToString("F")  : "0.00")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="SELECT" CommandArgument='<%# Eval("ID")%>'
                                                                OnClick="lnkEdit_Click">
                                                                <i class="material-icons col-teal" onclick="expenseDisplay('<%# Eval("CrDr")%>')">edit</i>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DELETE" CommandArgument='<%# Eval("ID")%>'
                                                                Visible='<%# (UCMHelper.DataFormatter.SafeInt(Eval("Approved")) <= 0)%>'
                                                                OnClick="lnkDelete_Click">
                                                            <i class="material-icons col-red">delete_forever</i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="lnkGetDate" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="butCashIn" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="butCashOut" EventName="Click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function expenseDisplay(inputVal) 
        {
            if (inputVal == 'C')
                $("#cashInLink").click();
            else
                $("#cashOutLink").click();
        }
    </script>
</asp:Content>
