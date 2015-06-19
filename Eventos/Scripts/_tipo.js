var ViewTipos = function () {
    var self = this;



    //Defino las listas y el error
    self.listaTipos = ko.observableArray();
    self.detail = ko.observableArray();
    self.edit = ko.observableArray();
    self.error = ko.observableArray();
    self.usuarioLogeado = ko.observable();
    self.abrirNuevo = ko.observable();

    self.detail(null);
    self.edit(null);

    // lo guardo en editar el dato si funciona
    var tiposUri = '/api/Tipoes/';


    //Definicion de función que ejecuta las acciones
    function ajaxHelper(uri, method, data) {
        self.error('');
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            //Strinfy convierte una cadena de tipo javascript a objetos tipo JSON
            data: data ? (ko.toJSON(data)) : null

        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }

    //Obtiene Lugares
    function getAllTipos() {
        ajaxHelper(tiposUri, 'GET').done(function (data) {
            self.listaTipos(data);
        });
    }

    getAllTipos();

    self.getTipoDetail = function (item) {

        ajaxHelper(tiposUri + item.IdTipo, 'GET').done(function (data) {
            self.detail(data);
            //alert(JSON.stringify(data));
        });

    }

    self.getTipoEditar = function (item) {
        ajaxHelper(tiposUri + item.IdTipo, 'GET').done(function (data) {
            self.edit(data);
        });
    }


    self.getTipoDelete = function (item) {
        ajaxHelper(tiposUri + item.IdTipo, 'DELETE').done(function (data) {
            self.listaTipos.remove(item)
            self.detail(null);
            self.edit(null);
        });
    }


    //
    self.editarTipo = function () {
        ajaxHelper(tiposUri + self.edit().IdTipo, 'PUT', self.edit()).done(function (item) {
            getAllTipos();
            if (self.detail() != null) {
                self.detail(self.edit());
            }
            self.edit(null);
            document.getElementById("form-editar").reset();
        });
    }


    // PLantillas
    self.newTipo = {
        Nombre: ko.observable()
    }




    self.addTipo = function (formElement) {
        var tip = {
            Nombre: self.newTipo.Nombre,
        };

        //alert(JSON.stringify(usuario));
        //AGREGA A LA BD LOS DATOS
        ajaxHelper(tiposUri, 'POST', tip).done(function (item) {
            //SE ACTUALIZA LA LISTA DE LOS CONTACTOS
            self.listaTipos.push(item);
        });

        document.getElementById("form-agregar-tipos").reset();
        self.abrirNuevo(null);
    }

    self.abrirAgregar = function () {
        self.abrirNuevo(true);
    }

    self.CerrarSesion = function (item) {
        self.eliminarCookie('usuario');
        location.assign('/');
    }

    function cargarUsuario() {
        var parsed = JSON.parse(leerCookie('usuario'));
        if (parsed) {
            self.usuarioLogeado(parsed);
        }
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

    cargarUsuario();


}

ko.applyBindings(new ViewTipos());