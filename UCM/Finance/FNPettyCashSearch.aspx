<%@ Page Title="" Language="C#" MasterPageFile="~/UCMLogged.Master" AutoEventWireup="true" CodeBehind="FNPettyCashSearch.aspx.cs" Inherits="UCM.Finance.FNPettyCashSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>UCM :: Petty Cash Search</title>
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
            <h2>Finance - Petty Cash Search</h2>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <script type="text/javascript">
                    Sys.Application.add_load(bindEvents);
                </script>
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
                                    <div class="col-md-2">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:DropDownList ID="dropCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:DropDownList ID="dropHeadOfExp" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
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
                                </div>
                                <div class="row clearfix">
                                    <div class="col-md-3">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:DropDownList ID="dropCrDr" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="" Text="- Tran type -"></asp:ListItem>
                                                    <asp:ListItem Value="C" Text="Cash In"></asp:ListItem>
                                                    <asp:ListItem Value="D" Text="Cash Out"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                <label class="form-label">Remarks</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                                <label class="form-label">Amount</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button ID="butSearch" runat="server" Text="Search" CssClass="btn bg-teal waves-effect" OnClick="butSearch_Click" />
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
                                    <div class="col-md-7"><h2 class="card-inside-title">Entry List: </h2></div>
                                    <div class="col-md-3"><h2 class="card-inside-title"><asp:Label ID="lblEntryMessasge" runat="server" CssClass="right"></asp:Label></h2></div>
                                    <div class="col-md-2"><asp:Button ID="butApprove" runat="server" Text="Approve"  CssClass="btn bg-teal waves-effect" OnClick="butApprove_Click"/></div>
                                </div>
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
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"> 
                                                    <HeaderTemplate>
                                                        <input type="checkbox" id="checkAll" class="filled-in align-center" onclick="toggleCheck()" />
                                                        <label for="checkAll"></label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div  style='display: <%# (UCMHelper.DataFormatter.SafeInt(Eval("Approved")) > 0) ? "none" : "block"%>'>
                                                        <input type="checkbox" id="basic_checkbox_<%# Eval("ID")%>" name="expEntryCheckBox" value="<%# Eval("ID")%>" class="filled-in" onclick="enumerateValues()" />
                                                        <label for="basic_checkbox_<%# Eval("ID")%>"></label>

                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hidSelected" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function enumerateValues() {
            $('#<%=hidSelected.ClientID%>').val('');

            $('input[name="expEntryCheckBox"]:checked').each(function () {
                $('#<%=hidSelected.ClientID%>').val($('#<%=hidSelected.ClientID%>').val() + this.value + ',');
            });
        }

        function toggleCheck() {
            $('input[name="expEntryCheckBox"]').prop('checked', $('#checkAll').prop('checked'));
            enumerateValues();
        }
    </script>
</asp:Content>
