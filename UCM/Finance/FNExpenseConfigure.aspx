<%@ Page Title="" Language="C#" MasterPageFile="~/UCMLogged.Master" AutoEventWireup="true" CodeBehind="FNExpenseConfigure.aspx.cs" Inherits="UCM.Finance.FNExpenseConfigure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>UCM :: Expense Configure </title>
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
            <h2>Expense Configure</h2>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <script type="text/javascript">
                    Sys.Application.add_load(bindEvents);
                </script>
                <div class="row clearfix">
                    <div class="col-md-12">
                        <div class="card">
                            <div class="body">
                                <div class="row clearfix">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <div class="form-line">
                                                <asp:DropDownList ID="dropExpType" runat="server" CssClass="form-control show-tick" OnSelectedIndexChanged="dropExpType_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtExpMasterName" runat="server" CssClass="form-control"></asp:TextBox>
                                                <label class="form-label">Exp. Name</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <asp:HiddenField ID="hidExpID" runat="server" />
                                            <asp:Button ID="butAddExpenseMaster" runat="server" Text="Add Exp. Detail" CssClass="btn bg-teal waves-effect" OnClick="butAddExpenseMaster_Click" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <asp:Label ID="lblMessage" runat="server" CssClass="form-error"></asp:Label>
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
                                <h2 class="card-inside-title">Expense Type: 
                                    <asp:Label ID="lblSearchResult" runat="server"></asp:Label></h2>
                                <div class="row clearfix">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gridExpMaster" runat="server" DataKeyNames="ID" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-striped table-hover table-condensed">
                                            <Columns>
                                                <asp:BoundField HeaderText="Exp. Name" DataField="ExpName" />
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="SELECT" CommandArgument='<%# Eval("ID")%>'
                                                            OnClick="lnkEdit_Click">
                                                            <i class="material-icons col-teal">edit</i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
