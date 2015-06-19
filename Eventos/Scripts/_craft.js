var ViewCraft = function () {
    var self = this;
    self.error = ko.observable();
    self.listaEventos = ko.observableArray();
    self.listaSecciones = ko.observableArray();
    self.eventoSeleccionado = ko.observableArray();
    self.noBox = ko.observableArray();
    self.numeroSecciones = ko.observableArray();
    self.toConfig = ko.observableArray();
    self.detalleSeccion = ko.observableArray();
    self.listaDetalleSeccion = ko.observableArray();
    self.detalleEventoId = ko.observableArray();
    self.detail = ko.observableArray();
    self.listaFila = ko.observableArray();
    self.listaColumna = ko.observableArray();
    self.usuarioLogeado = ko.observable();

    self.detalleEventoId(null);
    self.eventoSeleccionado(null);
    self.detail(null);
    self.toConfig(null);
    // lo guardo en editar el dato si funciona
    var eventosUri = '/api/Eventoes/';
    var tiposUri = '/api/Tipoes/';
    var lugarsUri = '/api/Lugars/';
    var seccionesUri = '/api/Seccions/';
    var detalleUri = '/api/Evento_Seccion/';
    var asientosUri = '/api/Asientos/'
    var detallePersonalizadoUri = '/api/EventoSeccionP/';

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


    self.getConfigurar = function (item) {
        //alert(ko.toJSON(item));
        self.toConfig(item);
        //alert(item.Nombre())
    }

    self.getEventoSelecto = function (item) {
        self.eventoSeleccionado(item.IdEvento);
    }



    function getAllEventos() {
        ajaxHelper(eventosUri, 'GET').done(function (data) {
            self.listaEventos(data);
        });
    }


    self.crearSecciones = function () {
        if (self.numeroSecciones() < 10) {

            self.noBox([]);
            for (i = 0; i < self.numeroSecciones() ; i++) {
                self.seccion = ko.observable(
                    {
                        Id: ko.observable(i),
                        Nombre: ko.observable(""),
                        NoAsientos: ko.observable(0),
                        Precio: ko.observable(0),
                        Numeracion: ko.observable(""),
                        Fila: ko.observable(0),
                        Columna: ko.observable(0)
                    }
                );
                self.noBox.push(self.seccion);
            }
        }
    }

    self.calcular = function () {
        if (self.toConfig().Fila() != 0 && self.toConfig().NoAsientos() != 0) {
            // alert("llego aqui");
            var valor = self.toConfig().NoAsientos() / self.toConfig().Fila();
            self.toConfig().Columna(valor);

        }
    }

    self.ingresarSecciones = function () {
        //Aca se listan los las secciones que estan en el box
        if (self.eventoSeleccionado() != null) {
            ko.utils.arrayForEach(self.noBox(), function (item) {
                if (self.verficarCampos(item) == true) {
                    var seccion = {
                        Nombre: item().Nombre(),
                        NoAsientos: item().NoAsientos(),
                        Precio: item().Precio(),
                        Fila: item().Fila(),
                        Columna: item().Columna(),
                        Numeracion: item().Numeracion()
                    }

                    ajaxHelper(seccionesUri, 'POST', seccion).done(function (itemSeccion) {
                        self.ingresarDetalle(itemSeccion.IdSeccion);
                        self.ingresarAsientos(itemSeccion.IdSeccion, item().Numeracion(), item().NoAsientos());
                        location.reload();
                    });
                } else {
                    alert("Campos Vacios");
                }

            });
            alert("Secciones y Asientos Creados con exito");
        } else {
            alert("Seleccione un evento");
        }
    }


    self.verficarCampos = function (item) {
        if (item().Nombre() != "" && item().NoAsientos() != 0) {
            if (item().NoAsientos() != 0 && item().Precio() != 0) {
                if (item().Numeracion() != "" && item().Fila() != 0 && item().Columna() != 0) {
                    return true;
                }
            }
        }
        return false;
    }


    //Secciones_Evento
    self.ingresarDetalle = function (id) {
        var detalle = {
            EventoId: self.eventoSeleccionado(),
            SeccionId: id
        }

        ajaxHelper(detalleUri, 'POST', detalle).done(function (item) {
            // Si funcionaalert("listo ");

        })
    }

    self.ingresarAsientos = function (id, numeracion, noAsientos) {
        var cont = parseInt(noAsientos) + 1;
        //alert(cont);

        for (i = 1; i < cont ; i++) {
            var numeroAsiento = numeracion + i;
            var asiento = {
                Numero: numeroAsiento,
                SeccionId: id,
                Estado: false,

            };

            ajaxHelper(asientosUri, 'POST', asiento).done(function (itemAsiento) {

            });
        }
    }

    ///Manda a llamar las secciones del evento especifico, clickeado en el options bindig
    self.cargarSecciones = function () {
        if (self.detalleEventoId() != null) {
            self.lista = ko.observableArray();
            self.listaDetalleSeccion([]);
            ajaxHelper(detallePersonalizadoUri + self.detalleEventoId(), 'GET').done(function (data) {
                self.lista(data);

                ko.utils.arrayForEach(self.lista(), function (item) {
                    //alert(item.SeccionId);
                    ajaxHelper(seccionesUri + item.SeccionId, 'GET').done(function (itemSeccion) {
                        self.listaDetalleSeccion.push(itemSeccion);
                    });

                });


            });




        } else {
            alert("El objeto es nullo");
        }
    }



    getAllEventos();

    self.getAsientosDetail = function (item) {
        for (i = 0; i < item.Fila; i++) {
            var fila = ko.observable(
                {
                    Id: ko.observable(i),
                }
            );
            var arrayFila = [columna, self.listaFila];
            self.listaFila.push.apply(arrayFila);

        }



        for (cont = 0; cont < item.Columna; cont++) {
            var columna = ko.observable(
                {
                    Id: ko.observable(cont),
                }
            );
            var array = [columna, self.listaColumna];
            self.listaColumna.push.apply(array);
        }


    }




    self.getSeccionDelete = function (item) {
        ajaxHelper(detallePersonalizadoUri + item.IdSeccion, 'DELETE').done(function (data) {
            self.listaDetalleSeccion.remove(item)
            self.detail(null);
        });
    }

    self.getSeccionDetail = function (item) {
        self.detail(item);
    }

    self.getEventoForDetail = function (item) {
        self.detalleEventoId(item.IdEvento);
        self.cargarSecciones();
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


ko.applyBindings(new ViewCraft());