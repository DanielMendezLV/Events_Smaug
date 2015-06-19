var ViewModel = function () {
    //se crean las variables que nos ayudan a llmar a la api
    var self = this;
    var LugaresURi = '/api/Lugars/';
    var AsientoURI = '/api/Asientos/';
    var SeccionURI = '/api/Seccions/';
    var UsuarioURI = '/api/Usuarios/';
    var Evento_Seccion = '/api/Evento_Seccion/'
    var EventoURI = '/api/Eventoes/';
    var EntradaURI = '/api/Entradas/';
    var Factura_EntradaURI = '/api/Factura_Entrada/';
    var FacturaURI = '/api/Facturas/';
    var cont = 0;
     //se crean las variables observables
    self.usuarioLogeado = ko.observable();
    self.error = ko.observable();
    self.ListaFactura = ko.observableArray();
    self.ListaEntrada = ko.observableArray();
    self.Det = ko.observable();


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
    
    //Funcion que obtiene las facturas 
    function getAll() {
       // alert("hola");
       // ajaxHelper(Factura_EntradaURI, 'GET').done(function (data) {
       //     alert("hi");
       //     ko.utils.arrayForEach(data, function (fac) {
       //         alert("he");
       //     });
       // });
     //  ajaxHelper(EntradaURI, 'GET').done(function (data) {
     //      ko.utils.arrayForEach(data, function (ent) {
     //
     //          if (ent.IdEntrada == 118 || ent.IdEntrada == 119) {
     //              alert("a");
     //          } else {
     //              alert("c");
     //          }
     //      
     //      });
     //  });
       //  ajaxHelper(FacturaURI, 'GET').done(function (data) {
       //      ko.utils.arrayForEach(data, function (fac) {
       //          if (fac.IdFactura == 69) {
       //  
       //          } else {
       //              ajaxHelper(FacturaURI + fac.IdFactura, 'DELETE').done(function (data) {
       //  
       //              });
       //          }
       //      });           
       //   });
        ajaxHelper(FacturaURI, 'GET').done(function (data) {
            ko.utils.arrayForEach(data, function (fac) {
                //pongo 1 por que no puedo obtener todavia el id del usuario que se logea este debera ser remplazado por este usuario 
                //alert(ko.toJSON(self.usuarioLogeado/
                if (fac.UsuarioId == self.usuarioLogeado().IdUsuario) {
                    self.ListaFactura.push(fac);
                }
            });
        });


    }
    self.Detalle = function (item) {
        ajaxHelper(Factura_EntradaURI, 'GET').done(function (data) {
            ko.utils.arrayForEach(data, function (fac) {
                if (fac.FacturaId == item.IdFactura) {
                    ajaxHelper(EntradaURI, 'GET').done(function (data) {
                        self.ListaEntrada.removeAll();
                        ko.utils.arrayForEach(data, function (ent) {
                            if (ent.IdEntrada == fac.EntradaId) {
                                ajaxHelper(SeccionURI + ent.SeccionId, 'GET').done(function (data) {
                                    var newEnt = {
                                        IdEntrada: ent.IdEntrada,
                                        Fecha: ent.Fecha,
                                        Precio: data.Precio 
                                    }

                                    self.ListaEntrada.push(newEnt);
                                });
                                
                            }
                        });
                        
                    });
                }
              
            });
        });
        self.Det(item);
    }
    getAll();

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

ko.applyBindings(new ViewModel());