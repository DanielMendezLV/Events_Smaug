var ViewEventos = function () {
    var self = this;


    //Defino las listas y el error
    self.listaEventos = ko.observableArray();

    self.listaTipos = ko.observableArray();
    self.listaTiposEdit = ko.observableArray();
    self.abrirNuevo = ko.observable();

    self.xTipo = ko.observableArray();
    self.xTipoEdit = ko.observableArray();
    self.xLugar = ko.observableArray();
    self.xLugarEdit = ko.observableArray();

    self.listaLugares = ko.observableArray();
    self.listaLugaresEdit = ko.observableArray();


    self.dato = ko.observableArray();
    self.editEventoFoto = ko.observableArray();
    self.idLugarSelect = ko.observableArray();
    self.idTipoSelect = ko.observableArray();


    self.detail = ko.observableArray();
    self.detailLugar = ko.observableArray();
    self.detailTipo = ko.observableArray();
    self.edit = ko.observableArray();
    self.error = ko.observableArray();

    self.detail(null);
    self.edit(null);
    self.xLugar(null);
    self.xTipo(null);
    self.xTipoEdit(null);
    self.xLugarEdit(null);
    self.usuarioLogeado = ko.observable();
    // lo guardo en editar el dato si funciona
    var eventosUri = '/api/Eventoes/';
    var tiposUri = '/api/Tipoes/';
    var lugarsUri = '/api/Lugars/';


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

    //Obtiene las listas 

    function getAllEventos() {
        ajaxHelper(eventosUri, 'GET').done(function (data) {
            self.listaEventos(data);
        });
    }

    function getAllTipos() {
        ajaxHelper(tiposUri, 'GET').done(function (data) {

            self.listaTipos(data);
            self.listaTiposEdit(data);
        });
    }

    function getAllLugares() {
        ajaxHelper(lugarsUri, 'GET').done(function (data) {
            self.listaLugares(data);
            self.listaLugaresEdit(data);
        });
    }

    getAllEventos();
    getAllLugares();
    getAllTipos();



    self.getEventoDetail = function (item) {
        ajaxHelper(eventosUri + item.IdEvento, 'GET').done(function (data) {
            self.detail(data);
            //alert(JSON.stringify(data));
        });

        //alert(ko.toJSON(self.detail()));
        ajaxHelper(tiposUri + item.TipoId, 'GET').done(function (data) {
            self.detailTipo(data);
            //alert(JSON.stringify(data));
        });

        ajaxHelper(lugarsUri + item.LugarId, 'GET').done(function (data) {
            self.detailLugar(data);
            //alert(JSON.stringify(data));
        });

    }

    self.lugarListaEdit = ko.observableArray();
    self.tipoListaEdit = ko.observableArray();

    self.getEventoEditar = function (item) {
        ajaxHelper(eventosUri + item.IdEvento, 'GET').done(function (data) {
            self.edit(data);
            self.idLugarSelect(item.LugarId);
            self.idTipoSelect(item.TipoId);
        });

        ajaxHelper(lugarsUri + item.LugarId, 'GET').done(function (data) {
            self.dato(data);
            self.getLugar();
            //alert(JSON.stringify(data));
        });

        ajaxHelper(tiposUri + item.TipoId, 'GET').done(function (data) {
            self.dato(data);
            self.getTipo();
            //alert(JSON.stringify(data));
        });

    }




    self.getLugar = function () {
        //alert(id);
        self.lugarListaEdit(self.dato());

        ko.utils.arrayForEach(self.listaLugares(), function (item) {
            if (item.IdLugar != self.dato().IdLugar) {
                var other = [self.dato(), item];
                self.lugarListaEdit(other);
            }
        });
    }

    self.getTipo = function () {
        //alert(id);
        self.tipoListaEdit(self.dato());

        ko.utils.arrayForEach(self.listaTipos(), function (item) {
            if (item.IdTipo != self.dato().IdTipo) {
                var other = [self.dato(), item];
                self.tipoListaEdit(other);
            }
        });
    }



    self.getEventoDelete = function (item) {
        ajaxHelper(eventosUri + item.IdEvento, 'DELETE').done(function (data) {
            self.listaEventos.remove(item)
            self.detail(null);
        });
    }


    self.newEvento = {
        Nombre: ko.observable(),
        Fecha: ko.observable(),
        EntradasDisponibles: ko.observable(),
        Foto: ko.observable(),
        LugarId: ko.observable(),
        TipoId: ko.observable(),
        Lugar: ko.observable(),
        Tipo: ko.observable()
    }






    self.addEvento = function (formElement) {

        var files = $("#inputFile").get(0).files;
        var data = new FormData();
        var estado = false;

        for (i = 0; i < files.length; i++) {
            data.append("file" + i, files[i]);
            estado = true;
        }

        if (estado) {
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

            var file1 = self.newEvento.Foto();
            if (file1.lastIndexOf('\\') >= 0) {
                file1 = file1.substr(file1.lastIndexOf('\\') + 1);
            }

            if (self.idLugarSelect() != null && self.idTipoSelect() != null) {
                var evento = {
                    Nombre: self.newEvento.Nombre,
                    Fecha: self.newEvento.Fecha,
                    EntradasDisponibles: self.newEvento.EntradasDisponibles,
                    Foto: file1,
                    LugarId: self.idLugarSelect(),
                    TipoId: self.idTipoSelect(),
                    Lugar: ko.observable(),
                    Tipo: ko.observable()
                };
                //alert(ko.toJSON(evento));

                ajaxHelper(eventosUri, 'POST', evento).done(function (item) {

                    location.reload();

                });

                document.getElementById("form-agregar-eventos").reset();
                self.abrirNuevo(null);
            } else {
                alert("Lugar / Tipo no ingre")
            }

        } else {
            alert("No ha subido ninguna imagen");
        }

    }

    self.abrirAgregar = function () {
        self.abrirNuevo(true);
    }


    self.editEvento = function () {
        //alert(ko.toJSON(self.edit()));
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
            var file1 = self.editEventoFoto();
            if (file1.lastIndexOf('\\') >= 0) {
                file1 = file1.substr(file1.lastIndexOf('\\') + 1);
            }

            self.edit().Foto = file1;

        }

        if (self.edit().TipoId != self.idTipoSelect()) {
            self.edit().TipoId = self.idTipoSelect();
        }

        if (self.edit().LugarId != self.idLugarSelect()) {
            self.edit().LugarId = self.idLugarSelect();
        }

        ajaxHelper(eventosUri + self.edit().IdEvento, 'PUT', self.edit()).done(function (item) {
            location.reload();
        });
    }

    self.sendTipo = function (item) {
        self.idTipoSelect(item.IdTipo);
        self.tipoFloat = item;
        self.listaTipos([]);
        self.listaTipos.push(self.tipoFloat);
        self.xTipo(true);
    }


    self.sendTipoEdit = function (item) {
        self.idTipoSelect(item.IdTipo);
        self.tipoFloat = item;
        self.listaTiposEdit([]);
        self.listaTiposEdit.push(self.tipoFloat);
        self.xTipoEdit(true);
    }


    self.pressXTipo = function () {
        self.listaTipos([]);
        getAllTipos();
        self.idTipoSelect(null);
        self.xTipo(null);
    }

    self.pressXTipoEdit = function () {
        self.listaTiposEdit([]);
        getAllTipos();
        self.idTipoSelect(null);
        self.xTipoEdit(null);
    }


    self.sendLugar = function (item) {
        self.idLugarSelect(item.IdLugar);
        self.lugarFloat = item;
        self.listaLugares([]);
        self.listaLugares.push(self.lugarFloat);
        self.xLugar(true);
        // alert(ko.toJSON(item));
    }

    self.sendLugarEdit = function (item) {
        self.idLugarSelect(item.IdLugar);
        self.lugarFloat = item;
        self.listaLugaresEdit([]);
        self.listaLugaresEdit.push(self.lugarFloat);
        self.xLugarEdit(true);
        // alert(ko.toJSON(item));
    }



    self.pressXLugar = function () {
        self.listaLugares([]);
        getAllLugares();
        self.idLugarSelect(null);
        self.xLugar(null);
    }



    self.pressXLugarEdit = function () {
        self.listaLugaresEdit([]);
        getAllLugares();
        self.idLugarSelect(null);
        self.xLugarEdit(null);
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


ko.applyBindings(new ViewEventos());