<%@ Page Title="" Language="C#" MasterPageFile="~/UCMLogged.Master" AutoEventWireup="true" CodeBehind="WHStockInHand.aspx.cs" Inherits="UCM.Warehouse.WHStockInHand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>UCM :: Stock In Hand</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <form id="Form1" runat="server">
        <div class="block-header">
            <h2>Warehouse - Fabric Stock In Hand</h2>
        </div>
        <div class="row clearfix">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div class="card">
                    <div class="card body">

                <asp:Repeater ID="rptSIH" runat="server">
                    <HeaderTemplate>
                        <table class="table table-bordered table-striped table-hover table-condensed">
                            <tbody>
                                <tr>
                                    <th rowspan="2">Sno</th>
                                    <th rowspan="2">Fab. Sort</th>
                                    <th rowspan="2">Fresh</th>
                                    <th colspan="4" class="align-center">Checked (<i>Mtrs / Pcs</i>)</th>
                                    <th rowspan="2">Packed</th>
                                </tr>
                                <tr>
                                    <th>A - Grade</th>
                                    <th>B - Grade</th>
                                    <th>E - Grade</th>
                                    <th>T - Grade</th>
                                </tr>
                            
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Sno") %></td>
                            <td><%# Eval("Name") %></td>
                            <td class="align-right"><%# Eval("Fresh") %></td>
                            <td class="align-right"><%# Eval("1") %> / <%# Eval("Count:1") %></td>
                            <td class="align-right"><%# Eval("2") %> / <%# Eval("Count:2") %></td>
                            <td class="align-right"><%# Eval("3") %> / <%# Eval("Count:3") %></td>
                            <td class="align-right"><%# Eval("4") %> / <%# Eval("Count:4") %></td>
                            <td class="align-right"><%# Eval("Packed") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                    </div>
                </div>

            </div>
        </div>

    </form>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
