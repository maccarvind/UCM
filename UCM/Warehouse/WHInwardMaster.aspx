<%@ Page Title="" Language="C#" MasterPageFile="~/UCMLogged.Master" AutoEventWireup="true" CodeBehind="WHInwardMaster.aspx.cs" Inherits="UCM.Warehouse.WHInwardMaster" %>

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
            <h2>Warehouse - Inward Details</h2>
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
                            <div class="col-sm-2">
                                <div class="form-group form-float">
                                    <div class="form-line">
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="datepicker form-control"></asp:TextBox>
                                        <label class="form-label">From</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-2">
                                <div class="form-group form-float">
                                    <div class="form-line">
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="datepicker form-control"></asp:TextBox>
                                        <label class="form-label">To</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:DropDownList ID="dropSearchOrigin" runat="server" CssClass="form-control show-tick">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-5">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox ID="txtSearchRef" runat="server" CssClass="form-control"></asp:TextBox>
                                        <label class="form-label">Reference</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row clearfix">
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:DropDownList ID="dropSearchVendor" runat="server" CssClass="form-control show-tick">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-5">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:DropDownList ID="dropSearchFabricSort" runat="server" CssClass="form-control show-tick">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
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
                        <h2 class="card-inside-title">Warehouse Inward Search Results</h2>
                        <div class="row clearfix">
                            <div class="col-sm-12">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>

                                        <asp:GridView ID="gridInward" runat="server" DataKeyNames="ID" OnSelectedIndexChanged="gridInward_SelectedIndexChanged" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-striped table-hover table-condensed">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Inward Details">
                                                    <ItemTemplate>
                                                        <a href="WHPieceCR.aspx?ID=<%# Eval("ID")%>">
                                                        <%# UCMHelper.DataFormatter.SafeDate(Eval("InwdDate")).ToString("dd-MMM-yyyy") %>/<%# Eval("InwdRef")%>/<%# Eval("Name")%>/<%# Eval("VenName")%>
                                                            </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Origin" DataField="OriginName" />
                                                <asp:ButtonField CommandName="Select" Text="&lt;i data-toggle='modal' data-target='#largeModal' class=&quot;material-icons col-teal&quot;&gt;edit&lt;/i&gt;" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="butSearch" EventName="Click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </div>

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
                                <h4 class="modal-title" id="largeModalLabel">Warehouse - Inward Entry</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row clearfix">
                                    <div class="col-sm-3">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtDate" runat="server" CssClass="datepicker form-control"></asp:TextBox>
                                                <label class="form-label">Date*</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:DropDownList ID="dropOrigin" runat="server" CssClass="form-control show-tick">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:DropDownList ID="dropFabricSort" runat="server" CssClass="form-control show-tick" data-live-search="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-4">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:DropDownList ID="dropVendor" runat="server" CssClass="form-control show-tick" data-live-search="true">
                                                    <asp:ListItem Text="-Select-"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtReference" runat="server" CssClass="form-control"></asp:TextBox>
                                                <label class="form-label">Reference</label>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtNoOfItems" runat="server" CssClass="form-control"></asp:TextBox>
                                                <label class="form-label">No. of Items*</label>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtApproxMts" runat="server" CssClass="form-control"></asp:TextBox>
                                                <label class="form-label">Approx. Mts.</label>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="row clearfix">
                                    <div class="col-sm-4 align-center">
                                        <div class="switch" id="divapprove" runat="server">
                                            <label>
                                                <asp:CheckBox ID="chkApprove" runat="server" /><span class="lever"></span>Approved</label>
                                        </div>

                                    </div>
                                    <div class="col-sm-4 align-left">
                                        <asp:Label ID="lblMessage" runat="server" CssClass="form-error"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:HiddenField ID="txtID" runat="server" />
                                        <asp:Button ID="btnAddEntry" runat="server" Text="Add Entry" CssClass="btn btn-link bg-teal waves-effect" OnClick="btnAddEntry_Click" />
                                        <button type="button" class="btn btn-link bg-teal waves-effect" data-dismiss="modal">Close</button>

                                    </div>
                                </div>


                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gridInward" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="lnkButton" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
