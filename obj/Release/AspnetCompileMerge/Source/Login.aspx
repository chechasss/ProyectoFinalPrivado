<%@ Page Title="" Language="vb" MasterPageFile="~/home.Master" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="WebDIACO_V2.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Regreso" runat="server">
    <section class="content-header">
        <ol class="breadcrumb">
            <li><a href="inicio.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">Login</li>
        </ol>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-box">
        <div class="login-logo">
            <!--<a href="/inicio.aspx"><img src="../../dist/img/DIACO.png" class="user-image" alt="User Image"></a>-->
      </div><!-- /.login-logo -->
      <div class="login-box-body">
        <p class="login-box-msg">Iniciar Sesión</p>
          <div class="form-group has-feedback">
            <input type="text" class="form-control" placeholder="Usuario" id="txtUsr"/>
            <span class="glyphicon glyphicon-user form-control-feedback"></span>
          </div>
          <div class="form-group has-feedback">
            <input type="password" class="form-control" placeholder="Contraseña" id="txtPass"/>
            <span class="glyphicon glyphicon-lock form-control-feedback"></span>
          </div>
          <div class="row">
            <!--<div class="col-xs-8">    
              <div class="checkbox icheck">
                <label>
                  <input type="checkbox"> Remember Me
                </label>
              </div>-- >
            </div><!-- /.col -->
            <div class="col-xs-4">
              <button type="button" class="btn btn-primary btn-block btn-flat" onclick="IniciaSesion();" style="width: 116px;">Iniciar Sesion</button>
            </div><!-- /.col -->
          </div>
        
        <!--<div class="social-auth-links text-center">
          <p>- OR -</p>
          <a href="#" class="btn btn-block btn-social btn-facebook btn-flat"><i class="fa fa-facebook"></i> Sign in using Facebook</a>
          <a href="#" class="btn btn-block btn-social btn-google-plus btn-flat"><i class="fa fa-google-plus"></i> Sign in using Google+</a>
        </div><!-- /.social-auth-links -- >

        <a href="#">I forgot my password</a><br>
        <a href="register.html" class="text-center">Register a new membership</a>-->

      </div><!-- /.login-box-body -->
    </div><!-- /.login-box -->

    <script type="text/javascript">
        function IniciaSesion() {
            var usr = $("#txtUsr").val();
            var pass = $("#txtPass").val();
            $.ajax({
                type: "GET",
                cache: false,
                url: "Ajx/ajxFuncBD.aspx",
                data: {
                    pMetodo: "IniciaSesion",
                    pUser: usr,
                    pPass: pass
                },
                success: function (data) {
                    var uno = data.split("<");

                    var JRespuesta = JSON.parse(uno[0]);
                    if (JRespuesta.CodigoRespuesta == 1) {
                        window.location.href = "/PrincipalMenu.aspx";
                    } else if (JRespuesta.CodigoRespuesta == 2) {
                        alert("El usuario esta Inactivo");
                    } else if (JRespuesta.CodigoRespuesta == 3) {
                        alert("Contraseña Incorrecta");
                    } else if (JRespuesta.CodigoRespuesta == 0) {
                        alert("El Usuario no Existe");
                    }
                },
                error: function () {
                    alert("error en el inicio de sesion");
                }
            });
        }
    </script>

</asp:Content>
