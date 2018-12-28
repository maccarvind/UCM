<%@ Page Title="" Language="C#" MasterPageFile="~/UCMLogged.Master" AutoEventWireup="true" CodeBehind="WHDeliveryPrePlan.aspx.cs" Inherits="UCM.Warehouse.WHDeliveryPlan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>UCM :: Delivery Plan </title>

    <!-- Multi Select Css -->
    <link href="/plugins/multi-select/css/multi-select.css" rel="stylesheet">

    <script type="text/javascript">

        function bindEvents() {

            $.AdminBSB.dropdownMenu.activate();
            $.AdminBSB.input.activate();
            $.AdminBSB.select.activate();

            $('.searchable').multiSelect({
                selectableOptgroup: false,
                selectableHeader: "<div class='input-group'><div class='form-line'><input type='text' class='search-input form-control' autocomplete='off' placeholder='search...'></div></div>",
                selectionHeader: "<div class='input-group'><div class='form-line'><input type='text' class='search-input form-control' autocomplete='off' placeholder='search..'></div><span id='spnSelectedItems' name='spnSelectedItems' class='input-group-addon'></span></div>",
                afterInit: function (ms) {
                    var that = this,
                        $selectableSearch = that.$selectableUl.prev().find('.search-input'),
                        $selectionSearch = that.$selectionUl.prev().find('.search-input'),
                        selectableSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selectable:not(.ms-selected)',
                        selectionSearchString = '#' + that.$container.attr('id') + ' .ms-elem-selection.ms-selected';

                    that.qs1 = $selectableSearch.quicksearch(selectableSearchString)
                    .on('keydown', function (e) {
                        if (e.which === 40) {
                            that.$selectableUl.focus();
                            return false;
                        }
                    });

                    that.qs2 = $selectionSearch.quicksearch(selectionSearchString)
                    .on('keydown', function (e) {
                        if (e.which == 40) {
                            that.$selectionUl.focus();
                            return false;
                        }
                    });
                },
                afterSelect: function () {
                    this.qs1.cache();
                    this.qs2.cache();
                    calculateSelectedCount();
                },
                afterDeselect: function () {
                    this.qs1.cache();
                    this.qs2.cache();
                    calculateSelectedCount();
                }
            });

            calculateSelectedCount();

        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <form id="Form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="block-header">
            <h2>Delivery Pre Plan</h2>
        </div>

        <div class="row clearfix">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="card">
                    <div class="body">
                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 align-right">

                                <asp:LinkButton ID="lnkButton" runat="server" OnClick="lnkButton_Click">
                                    <i class="material-icons col-teal md-36"  data-toggle="modal" data-target="#largeModal" >add_circle</i>
                                </asp:LinkButton>

                            </div>

                        </div>
                        <div class="row clearfix">
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:DropDownList ID="dropDeliveryPlan" runat="server" CssClass="form-control show-tick">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:DropDownList ID="dropInvoice" runat="server" CssClass="form-control show-tick">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                   <div class="form-line">
                                        <asp:TextBox ID="txtSearchRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                        <label class="form-label">Details</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <asp:Button ID="butSearch" runat="server" Text="Search" CssClass="btn bg-teal waves-effect" OnClick="butSearch_Click" />
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
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <h2 class="card-inside-title">Packing Search Results
                                    <asp:Label ID="lblSearchResult" runat="server"></asp:Label></h2>
                                <div class="row clearfix">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gridDeliveryPlan" runat="server" DataKeyNames="ID" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-striped table-hover table-condensed">
                                            <Columns>
                                                <asp:BoundField HeaderText="DP No." DataField="DPNo" />
                                                <asp:BoundField HeaderText="Invoice No" DataField="InvNo" />
                                                <asp:BoundField HeaderText="Packages" DataField="TotalPackages" />
                                                <asp:BoundField HeaderText="Meters" DataField="TotalMeters" />
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <%# (UCMHelper.DataFormatter.SafeInt(Eval("Approved")) == 1 ? "Approved" : "Open")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="SELECT" CommandArgument='<%# Eval("ID")%>'
                                                            OnClick="lnkEdit_Click">
                                                            <i data-toggle='modal' data-target='#largeModal' class="material-icons col-teal">edit</i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DELETE" CommandArgument='<%# Eval("ID")%>'
                                                            Visible='<%# !( (UCMHelper.DataFormatter.SafeInt(Eval("TotalPackages")) > 0) || (UCMHelper.DataFormatter.SafeInt(Eval("Approved")) > 0) )%>'
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
                                <asp:AsyncPostBackTrigger ControlID="butSearch" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

        </div>
        <!-- Large Size -->
        <div class="modal fade" id="largeModal" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <script type="text/javascript">
                            Sys.Application.add_load(bindEvents);
                        </script>
                        <div class="modal-content">
                            <div class="modal-header bg-teal">
                                <h4 class="modal-title" id="largeModalLabel">Package Selection 
                                    <asp:Label ID="lblInvoice" runat="server"></asp:Label>
                                </h4>

                            </div>
                            <div class="modal-body">
                                <div class="row clearfix">
                                    <div class="col-sm-6">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtDeliveryPlanNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                <label class="form-label">Delivery Plan No.*</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtDeliveryRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                <label class="form-label">Remarks</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="upPieceSelection" runat="server">
                                    <ContentTemplate>
                                        <div class="row clearfix">
                                            <div class="col-sm-12">
                                                <div class="card">
                                                    <div class="body">
                                                        <p class="font-bold font-underline">Package Selection</p>
                                                        <div class="row clearfix">
                                                            <div class="col-sm-12">
                                                                <asp:ListBox ID="lstPackageSelection" class="searchable ms" runat="server" SelectionMode="Multiple"></asp:ListBox>
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
                                        <div class="switch" id="divApprove" runat="server">
                                            <label>
                                                <asp:CheckBox ID="chkApprove" runat="server" /><span class="lever"></span>Approved</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 align-left">
                                        <asp:Label ID="lblMessage" runat="server" CssClass="form-error"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:HiddenField ID="hidDeliveryPlanID" runat="server" />
                                        <asp:Button ID="btnAddEntry" runat="server" Text="Add Entry" CssClass="btn btn-link bg-teal waves-effect" OnClick="btnAddEntry_Click" />
                                        <button type="button" class="btn btn-link bg-teal waves-effect" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gridDeliveryPlan" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="lnkButton" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <!-- Multi Select Plugin Quicksearch Js -->
    <script src="/plugins/jquery-quicksearch/jquery.quicksearch.js"></script>

    <!-- Multi Select Plugin Js -->
    <script src="/plugins/multi-select/js/jquery.multi-select.js"></script>

    <script type="text/javascript">
        function calculateSelectedCount() {
            var selectedValueArr = $('.searchable').val();
            if (selectedValueArr != null)
                $('#spnSelectedItems').text(selectedValueArr.length + ' No.');
            else
                $('#spnSelectedItems').text('0 No.');
        }
    </script>

</asp:Content>
