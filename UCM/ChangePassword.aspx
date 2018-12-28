<%@ Page Title="" Language="C#" MasterPageFile="~/UCMLogged.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="UCM.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>UCM :: Change Password</title>
    <script type="text/javascript">
        function bindEvents() {
            $(function () {
              
                $.AdminBSB.dropdownMenu.activate();
                $.AdminBSB.input.activate();
                $.AdminBSB.select.activate();

            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="block-header">
            <h2>CHANGE PASSWORD</h2>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <script type="text/javascript">
                    Sys.Application.add_load(bindEvents);
                </script>
                <div class="row clearfix">
                    <div class="col-sm-6">
                        <div class="card">
                            <div class="body">
                                <h2 class="card-inside-title">Old Password</h2>
                                <div class="row clearfix">
                                    <div class="col-sm-12">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" CssClass="form-control" required autofocus></asp:TextBox>
                                                <label class="form-label">Old Password </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <h2 class="card-inside-title">New Password</h2>
                                <div class="row clearfix">
                                    <div class="col-sm-12">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control" required autofocus></asp:TextBox>
                                                <label class="form-label">New Password </label>
                                            </div>
                                        </div>

                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtRetypeNewPassword" runat="server" TextMode="Password" CssClass="form-control" required autofocus></asp:TextBox>
                                                <label class="form-label">Retype New Password </label>
                                            </div>
                                        </div>
                                        <asp:Label ID="lblMessage" runat="server" CssClass="form-error"></asp:Label>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Change Password" CssClass="btn right bg-teal waves-effect" OnClick="btnSubmit_Click" />

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
