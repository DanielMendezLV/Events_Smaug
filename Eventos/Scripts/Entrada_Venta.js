var ViewModel = function () {
    //se crean las variables que nos ayudan a llmar a la api
    var self = this;
    var LugaresURi =            '/api/Lugars/';
    var AsientoURI =            '/api/Asientos/';
    var SeccionURI =            '/api/Seccions/';   
    var UsuarioURI = '/api/Usuarios/';
    var Evento_Seccion = '/api/Evento_Seccion/'
    var EventoURI =             '/api/Eventoes/';
    var EntradaURI =            '/api/Entradas/';
    var Factura_EntradaURI =    '/api/Factura_Entrada/';
    var FacturaURI = '/api/Facturas/';
    var cont = 0;
    //se crean las variables observables
    self.usuarioLogeado = ko.observable();
    self.ayuda = ko.observableArray([{ name: "y" }]);
    self.Factura = ko.observable();
    self.Total = ko.observable();
    self.ListaFactura = ko.observableArray();
    self.Evento = ko.observable("--");
    self.Seccion = ko.observable();
    self.Asiento = ko.observable();
    self.CarrEntradas = ko.observableArray();
    self.CarrMuestraEntradas = ko.observableArray();
    self.error = ko.observable();
    
    self.entrada = {
        AsientoId: ko.observable(),
        //ya
        SeccionId: ko.observable(),
        //ya
        UsuarioId: ko.observable(),
        //ya
        EventoId: ko.observable(),
        //ya
        Fecha: ko.observable(),
        Asiento: ko.observable(),
        Seccion: ko.observable(),
        Evento: ko.observable(),
        Precio: ko.observable()
    }
    self.ListaEntradas = ko.observableArray();
    self.ListaEventos = ko.observableArray();
    self.ListaSecciones = ko.observableArray();
    self.listaAsientos = ko.observableArray();
    self.listaLugares = ko.observableArray();


    //Funcion que nos ayuda a usar ajax
    function ajaxHelper(uri, method, data) {
        self.error(''); // Clear error message
        return $.ajax({
            type: method,
            url: uri,
            dataType: 'json',
            contentType: 'application/json',
            data: data ? ko.toJSON(data) : null
        }).fail(function (jqXHR, textStatus, errorThrown) {
            self.error(errorThrown);
        });
    }
    self.evento   = {
        IdEvento: ko.observable(),
        Nombre: ko.observable(),
        Fecha: ko.observable(),
        EntradasDisponibles: ko.observable(),
        lugar: ko.observable(),
        direccion: ko.observable()
    }


    //Funcion que obtiene los eventos
    function getAll() {        
        ajaxHelper(EventoURI, 'GET').done(function (data) {
            self.ListaEventos(data);
        });        
    }


    //para obtener las secciones que tiene el evento que seleccione
    self.getSeccion = function (item) {
        self.Seccion(item.Nombre);
        self.Evento(null);
        self.Asiento(null);
        self.entrada.EventoId(item.IdEvento);
        self.entrada.Evento(item.Nombre);

        EventoSec = ko.observableArray();
        ajaxHelper(Evento_Seccion, 'GET').done(function (data) {
            self.ListaSecciones.removeAll();
            EventoSec(data);
            ko.utils.arrayForEach(EventoSec(), function (evse) {
                if (evse.EventoId == item.IdEvento) {
                    getSeccion(evse.SeccionId);
                }
            });
        });
    }

    //Este metodo sirve para obtener los asientos segun la seccion
    self.getAsiento = function (item) {
        self.entrada.Seccion(item.Nombre);
        ajaxHelper(SeccionURI + item.IdSeccion, 'GET').done(function (data) {
            self.entrada.Precio(data.Precio);
            self.entrada.SeccionId(data.IdSeccion);

        });
        Asients = ko.observableArray();
        ajaxHelper(AsientoURI, 'GET').done(function (data) {
            Asients(data);
            self.listaAsientos.removeAll();
            ko.utils.arrayForEach(Asients(), function (sec) {
                if (sec.SeccionId == item.IdSeccion) {
                    self.Evento(null);
                    self.Asiento(item.Nombre);
                    self.Seccion(null);
                    var secs = {
                        IdAsiento: sec.IdAsiento,
                        Numero: sec.Numero,
                        Precio: item.Precio,
                        Bool: ko.observable("vacio")
                    }
                    if (sec.Estado == true) {
                        secs.Bool("lleno");
                    }
                    self.listaAsientos.push(secs);
                }
            });
        });
    }

    //Esto sirve para haccer la factura. 
    self.Comprar = function (item) {
        var contador = 0;
        var Total = 0;
        var boole = true;
        ko.utils.arrayForEach(self.CarrEntradas(), function (carr) {
            contador++;
            Total = Total + carr.Precio();            
        });
        self.Total(Total);
        if (contador == 0) {
            alert("No ha Comprado nada");
        } else {
            var idEntrada;
            var idFactura;

            newFactura = {
                UsuarioId: self.usuarioLogeado().IdUsuario,
                Total: Total
            }
            //           ajaxHelper(FacturaURI, 'POST', )
            ajaxHelper(FacturaURI, 'POST', newFactura).done(function (item) {
                idFactura = item.IdFactura;
              

            
                ko.utils.arrayForEach(self.CarrEntradas(), function (carr) {
                    //carr.UsuarioId sale undifined pero esto se arreglara con lo del login :) 
                    newEntrada = {
                        AsientoId: carr.AsientoId,
                        SeccionId: carr.SeccionId(),
                        UsuarioId: self.usuarioLogeado().IdUsuario,
                        Fecha: carr.Fecha(),
                        EventoId: carr.EventoId
                    }
                    ajaxHelper(EntradaURI, 'POST', newEntrada).done(function (item) {
                       newDetalle = {
                            FacturaId: idFactura,
                            EntradaId:item.IdEntrada,
                            Precio: carr.Precio()
                        }
                        ajaxHelper(Factura_EntradaURI, 'POST', newDetalle).done(function (item) {

                        });
                        if (boole) {
                            ajaxHelper(EventoURI + carr.EventoId(), 'GET').done(function (data) {
                                data.EntradasDisponibles = data.EntradasDisponibles - contador;
                                ajaxHelper(EventoURI + carr.EventoId(), 'PUT', data).done(function (item) {
                                    
                                });
                            });
                            boole = false;
                        }
                        ajaxHelper(AsientoURI + carr.AsientoId, 'GET').done(function (data) {
                            data.Estado = true;

                            ajaxHelper(AsientoURI + carr.AsientoId, 'PUT', data).done(function (item) {
                               
                            });
                        });

                    });
                    self.Factura(idFactura + "  " + carr.Fecha());


                });
                self.Evento("h1");
                self.Asiento(null);
                self.Seccion(null);
                self.ListaFactura(self.CarrEntradas());
                self.CarrEntradas.removeAll();
              
            });
            
        }
    }

    //Este sirve para agregar la entrada al carrito 
    self.getEntrada = function (item) {
        if (item.Bool() == "vacio") {


            item.Bool("lleno");
            self.entrada.AsientoId(item.IdAsiento);
            self.entrada.Asiento(item.Numero);
            var f = new Date();
            var price = ko.observable();
            var IdSec = ko.observable();
            self.entrada.Fecha(f.getDate() + "/" + (f.getMonth() + 1) + "/" + f.getFullYear());

            cont = cont + 1;
            ajaxHelper(SeccionURI + self.entrada.SeccionId(), 'GET').done(function (data) {
                price(data.Precio);
                IdSec(data.IdSeccion);



                newItem = {
                    IDS: cont,
                    AsientoId: item.IdAsiento,
                    SeccionId: IdSec,
                    UsuarioId: self.entrada.UsuarioId,
                    EventoId: self.entrada.EventoId,
                    Fecha: self.entrada.Fecha,
                    Asiento: item.Numero,
                    Seccion: self.entrada.Seccion,
                    Evento: self.entrada.Evento,

                    Precio: price
                };

                self.CarrEntradas.push(newItem);
            });
            
        } else {
            alert("entrada ocupada");
        }
    }

    //este metodo sirve para eliminar del carrito
    self.Remove = function (item) {
        self.CarrEntradas.remove(item);
    }

    //esto metodo funciona para regresar a evento
    self.RegresarEvento = function (item) {
        self.Evento("h1");
        self.Asiento(null);
        self.Seccion(null);
    }

    //este metodo funciona para regresar a seccion
    self.RegresarSeccion = function (item) {
        self.Evento(null);
        self.Asiento(null);
        self.Seccion(self.entrada.Evento());
    }

    //aqui se le agregan los datos a el arraylist que se muestra 
    function getSeccion(id) {
        seccions = ko.observableArray();
        ajaxHelper(SeccionURI + id, 'GET').done(function (data) {
            self.ListaSecciones.push(data);
        });
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

    //donde se manda a llamar la funcion 
    getAll();

   
}

ko.applyBindings(new ViewModel());