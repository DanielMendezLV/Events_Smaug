var ViewLugares = function () {
    var self = this;



    //Defino las listas y el error
    self.listaLugares = ko.observableArray();
    self.detail = ko.observableArray();
    self.edit = ko.observable();
    self.error = ko.observableArray();
    self.usuarioLogeado = ko.observable();
    self.editLugarFoto = ko.observableArray();
    self.abrirNuevo = ko.observable();

    // lo guardo en editar el dato si funciona
    var lugaresUri = '/api/Lugars/';


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
    function getAllLugares() {
        ajaxHelper(lugaresUri, 'GET').done(function (data) {
            self.listaLugares(data);
        });
    }

    getAllLugares();

    self.getLugarDetail = function (item) {
       
        ajaxHelper(lugaresUri + item.IdLugar, 'GET').done(function (data) {
            self.detail(data);
            //alert(JSON.stringify(data));
        });

    }

    self.getLugarEditar =  function(item){
        ajaxHelper(lugaresUri + item.IdLugar, 'GET').done(function (data) {
            self.edit(data);
        });
    }


    self.getLugarDelete =function(item){
        ajaxHelper(lugaresUri + item.IdLugar, 'DELETE').done(function (data) {
            self.listaLugares.remove(item)
            self.detail(null);
        });
    }

 

    self.editarLugar = function () {

        var files = $("#inputFileEdit").get(0).files;
        var data = new FormData();
        var estado = false;

        for (i = 0; i < files.length; i++) {
            data.append("file" + i, files[i]);
            estado = true
        };



        if (estado) {
            $.ajax({
                type: "POST",
                url: "/api/File",
                contentType: false,
                processData: false,
                data: data,
                success: function (result) {
                    if (result) {
                        $("#inputFileEdit").val('');

                    }
                }
            });
            var file1 = self.editLugarFoto();
            if (file1.lastIndexOf('\\') >= 0) {
                file1 = file1.substr(file1.lastIndexOf('\\') + 1);
            }
            self.edit().Foto = file1;
        }



        ajaxHelper(lugaresUri + self.edit().IdLugar, 'PUT', self.edit()).done(function (item) {
            getAllLugares();
            if (self.detail() != null) {
                self.detail(self.edit());
            }
            self.edit(null);
            document.getElementById("form-editar").reset();
        });
    }


    // PLantillas
    self.newLugar = {
        Nombre: ko.observable(),
        Direccion: ko.observable(),
        NoEntradas: ko.observable(),
        Foto: ko.observable(),
    }

    self.addLugar = function (formElement) {

        var files = $("#inputFile").get(0).files;
        var data = new FormData();

        for (i = 0; i < files.length; i++) {
            data.append("file" + i, files[i]);
        }


        $.ajax({
            type: "POST",
            url: "/api/File",
            contentType: false,
            processData: false,
            data: data,
            success: function (result) {
                if (result) {
                    $("#inputFile").val('');

                }
            }
        });

        var file1 = self.newLugar.Foto();
        if (file1.lastIndexOf('\\') >= 0) {
            file1 = file1.substr(file1.lastIndexOf('\\') + 1);
        }


        var lugar = {
            Nombre: self.newLugar.Nombre,
            Direccion: self.newLugar.Direccion,
            NoEntradas: self.newLugar.NoEntradas,
            Foto: file1
        };

        //AGREGA A LA BD LOS DATOS
        ajaxHelper(lugaresUri, 'POST', lugar).done(function (item) {
            //SE ACTUALIZA LA LISTA DE LOS CONTACTOS
            self.listaLugares.push(item);
        });

        document.getElementById("form-agregar-lugares").reset();
        self.abrirNuevo(null);
    }


    self.abrirAgregar = function() {
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

ko.applyBindings(new ViewLugares());