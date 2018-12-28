<%@ Page Title="" Language="C#" MasterPageFile="~/UCMLogged.Master" AutoEventWireup="true" CodeBehind="Sorts.aspx.cs" Inherits="UCM.Warehouse.Sorts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>UCM :: Sorts</title>
    <script type="text/javascript">
        function bindEvents() {
            $(function () {

                $.AdminBSB.dropdownMenu.activate();
                $.AdminBSB.input.activate();
                $.AdminBSB.select.activate();

                onChangeEvents();

            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <form id="Form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <script type="text/javascript">
                    Sys.Application.add_load(bindEvents);
                </script>
                <div class="block-header">
                    <h2>Fabric Sort Master</h2>
                </div>
                <div class="row clearfix">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                        <div class="card">
                            <div class="body">
                                <div class="row clearfix">
                                    <div class="col-sm-2">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtWarp" runat="server" CssClass="form-control" required></asp:TextBox>
                                                <label class="form-label">Warp*</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtWeft" runat="server" CssClass="form-control" required></asp:TextBox>
                                                <label class="form-label">Weft*</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtReed" runat="server" CssClass="form-control" required></asp:TextBox>
                                                <label class="form-label">Reed*</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtPic" runat="server" CssClass="form-control" required></asp:TextBox>
                                                <label class="form-label">Pic*</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtWidth" runat="server" CssClass="form-control" required></asp:TextBox>
                                                <label class="form-label">Width*</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtGSM" runat="server" CssClass="form-control"></asp:TextBox>
                                                <label class="form-label">GSM</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-sm-5">
                                        <div class="form-group form-float">
                                            <div class="form-line focused">
                                                <asp:TextBox ID="txtFabricName" runat="server" CssClass="form-control"></asp:TextBox>
                                                <label class="form-label">Name</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                        <asp:Label ID="lblMessage" runat="server" CssClass="form-error"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Add Details" CssClass="btn right bg-teal waves-effect" OnClick="btnSubmit_Click" />
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
                                <h2 class="card-inside-title">Fabric Sorts</h2>
                                <div class="row clearfix">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gridFabric" runat="server" DataKeyNames="ID" OnSelectedIndexChanged="gridFabric_SelectedIndexChanged" AutoGenerateColumns="false"
                                            CssClass="table table-bordered table-striped table-hover table-condensed">
                                            <Columns>
                                                <asp:BoundField HeaderText="ID" DataField="ID" Visible="false" />
                                                <asp:BoundField HeaderText="Fabric Sorts" DataField="Name" />
                                                <asp:ButtonField CommandName="Select" Text="&lt;i class=&quot;material-icons col-teal&quot;&gt;edit&lt;/i&gt;" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                        </asp:GridView>
                                        <asp:DataList ID="dlFabric" runat="server"></asp:DataList>
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
        onChangeEvents();
        function onChangeEvents() {
            $("#<%=txtWarp.ClientID%>").keyup(UpdateValue);
            $("#<%=txtWeft.ClientID%>").keyup(UpdateValue);
            $("#<%=txtReed.ClientID%>").keyup(UpdateValue);
            $("#<%=txtPic.ClientID%>").keyup(UpdateValue);
            $("#<%=txtWidth.ClientID%>").keyup(UpdateValue);
            $("#<%=txtGSM.ClientID%>").keyup(UpdateValue);
        }

        function UpdateValue() {
            var warp = $("#<%=txtWarp.ClientID%>").val();
            var weft = $("#<%=txtWeft.ClientID%>").val();
            var reed = $("#<%=txtReed.ClientID%>").val();
            var pic = $("#<%=txtPic.ClientID%>").val();
            var width = $("#<%=txtWidth.ClientID%>").val();
            var gsm = $("#<%=txtGSM.ClientID%>").val();


            $("#<% =txtFabricName.ClientID%>").val(warp + 'x' + weft + ' | ' + reed + 'x' + pic + ' | ' + width + '" (' + gsm + ' gsm)');
        }
    </script>
</asp:Content>
