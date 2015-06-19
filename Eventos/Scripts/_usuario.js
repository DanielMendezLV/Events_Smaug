var ViewUsuario = function () {
    var self = this;
    //Declaracion de Variables
    self.listaUsuarios = ko.observableArray();
    self.usuarioLogeado = ko.observable();
    var user;

    //Defino mi ruta de acceso
    var usuariosUri = '/api/Usuarios/';

    //Plantilla de Logeado

    self.usuarioLogear = {
        NickName: ko.observable(),
        Password: ko.observable()
    }


    //Ajax Helper
    function ajaxHelper(uri, method, data) {
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            //Strinfy convierte una cadena de tipo javascript a objetos tipo JSON
            data: data ? ko.toJSON(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    //Funcion que retorna todos los usuarios
    function getAllUsuarios() {
        ajaxHelper(usuariosUri, 'GET').done(function (data) {
            self.listaUsuarios(data);
        });
    }

    self.nuevoUsuario = {
        Nombre: ko.observable(),
        Apellido: ko.observable(),
        Nickname: ko.observable(),
        Password: ko.observable(),
        Direccion: ko.observable(),
        Correo: ko.observable(),
        Telefono: ko.observable()
    }


    self.addUsuario = function (formElement) {
        var roli;
        if (self.usuarioLogeado()) {
            roli = self.usuarioLogeado().RolId;
        } else {
            roli = 2;
        }
        var nuevoUsuario = {
            Nombre: self.nuevoUsuario.Nombre(),
            Apellido: self.nuevoUsuario.Apellido(),
            Nickname: self.nuevoUsuario.Nickname(),
            Password: self.nuevoUsuario.Password(),
            Direccion: self.nuevoUsuario.Direccion(),
            Correo: self.nuevoUsuario.Correo(),
            Telefono: self.nuevoUsuario.Telefono(),
            RolId: roli,
        };

        //alert(JSON.stringify(usuario));
        //AGREGA A LA BD LOS DATOS
        ajaxHelper(usuariosUri, 'POST', nuevoUsuario).done(function (item) {
            //SE ACTUALIZA LA LISTA DE LOS CONTACTOS
            self.listaUsuarios.push(item);
        });

        document.getElementById("form-agregar-usuarios").reset();
    }


    //Metodo en el cual el usuario se Logea
    self.Logear = function (formElement) {
        var usuarioL = {
            NickName: self.usuarioLogear.NickName(),
            Password: self.usuarioLogear.Password()
        }
        verificar = false;
        for (var i = 0 ; i < self.listaUsuarios().length; i++) {
            if (self.listaUsuarios()[i].Nickname == usuarioL.NickName & self.listaUsuarios()[i].Password == usuarioL.Password) {
                verificar = true;
                self.usuarioLogeado(self.listaUsuarios()[i])
                user = {
                    IdUsuario: self.usuarioLogeado().IdUsuario,
                    RolId: self.usuarioLogeado().RolId,
                    Nombre: self.usuarioLogeado().Nombre,
                    Apellido: self.usuarioLogeado().Apellido,
                    Nickname: self.usuarioLogeado().Nickname,
                    Telefono: self.usuarioLogeado().Telefono,
                    Correo: self.usuarioLogeado().Correo
                }
                crearCookie('usuario', JSON.stringify(user));
                var parsed = JSON.parse(leerCookie('usuario'));
                self.usuarioLogeado(parsed);

            }
        }
        if (verificar) {
            document.location.assign('/');
        } else {
            alert('Verifique sus credenciales');
        }
    }
    //Funcion que cierra la sesion
    self.CerrarSesion = function (item) {
        self.eliminarCookie('usuario');
        location.assign('/');
    }


    //Funccion que carga el usuario desde la cookie
    function cargarUsuario() {
        var parsed = JSON.parse(leerCookie('usuario'));
        if (parsed) {
            self.usuarioLogeado(parsed);
        }
    }

    //Opciones Cookie
    var crearCookie = function (key, value) {
        expires = new Date();
        expires.setTime(expires.getTime() + 31536000000);
        cookie = key + "=" + value + ";expires=" + expires.toUTCString() + "; path=/";;
        return document.cookie = cookie;
    }

    var leerCookie = function (key) {
        keyValue = document.cookie.match("(^|;) ?" + key + "=([^;]*)(;|$)");
        if (keyValue) {
            return keyValue[2];
        } else {
            return null;
        }
    }

    self.eliminarCookie = function (llave) {
        document.cookie = llave + '=;expires=Thu, 01 Jan 1970 00:00:01 GMT;' + ';path=/';
        location.reload();
    }

    getAllUsuarios();
    cargarUsuario();

}

ko.applyBindings(new ViewUsuario());