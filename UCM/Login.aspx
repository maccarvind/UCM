<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UCM.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>UCM :: Sign In</title>
    <!-- Favicon-->
    <link rel="icon" href="favicon.ico" type="image/x-icon">

    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,700&subset=latin,cyrillic-ext" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" type="text/css">

    <!-- Bootstrap Core Css -->
    <link href="/plugins/bootstrap/css/bootstrap.css" rel="stylesheet">

    <!-- Waves Effect Css -->
    <link href="/plugins/node-waves/waves.css" rel="stylesheet" />

    <!-- Animation Css -->
    <link href="/plugins/animate-css/animate.css" rel="stylesheet" />

    <!-- Custom Css -->
    <link href="/css/style.css" rel="stylesheet">
</head>
<body class="login-page">
    <form id="form1" runat="server">
        <div class="login-box">
            <div class="logo">
                <a href="javascript:void(0);">UCM<b>Control</b></a>
                <small></small>
            </div>

            <div class="card">
                <div class="body">
                    <div class="msg">Sign in</div>
                    <div class="errmsg">
                        &nbsp;
                   <asp:Label ID="lblMessage" runat="server"></asp:Label>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon">
                            <i class="material-icons">person</i>
                        </span>
                        <div class="form-line">
                            <asp:TextBox ID="txtUserName" runat="server" Placeholder="User Name" CssClass="form-control" required autofocus></asp:TextBox>
                        </div>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon">
                            <i class="material-icons">lock</i>
                        </span>
                        <div class="form-line">
                            <asp:TextBox ID="txtPassword" runat="server" Placeholder="Password" CssClass="form-control" required TextMode="Password"></asp:TextBox>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-8">
                        </div>
                        <div class="col-xs-4">
                            <asp:Button ID="btnSubmit" CssClass="btn btn-block bg-teal waves-effect" runat="server" Text="Sign In" OnClick="btnSubmit_Click" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <!-- Jquery Core Js -->
        <script src="/plugins/jquery/jquery.min.js"></script>

        <!-- Bootstrap Core Js -->
        <script src="/plugins/bootstrap/js/bootstrap.js"></script>

        <!-- Waves Effect Plugin Js -->
        <script src="/plugins/node-waves/waves.js"></script>

        <!-- Validation Plugin Js -->
        <script src="/plugins/jquery-validation/jquery.validate.js"></script>

        <!-- Custom Js -->
        <script src="/js/admin.js"></script>
        <script src="/js/pages/examples/sign-in.js"></script>
    </form>
</body>
</html>
