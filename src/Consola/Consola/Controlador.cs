using System;
using Library;
using System.Collections.Generic;
using Modelos;
using System.Linq;

namespace Consola
{
    class Controlador
    {
        private Vista _vista;
        private Panaderia _sistema;
        private Dictionary<string, Action> _casosDeUso;
        public Controlador(Vista vista, Panaderia sistema)
        {
            _vista = vista;
            _sistema = sistema;
            _casosDeUso = new Dictionary<string, Action>(){

                {"Añadir un cliente nuevo ",  AnadirCliente},
                {"Eliminar un cliente ",  EliminarCliente},
                {"Ver los clientes actuales ", MostrarClientes},
                {"Añadir un pedido ", AnadirPedido},
                {"Entregar un pedido ", EntregarPedido},
                {"Pagar un pedido ", PagarPedido},
                {"Ver los pedidos pendientes de pago", MostrarPedidos},
            };
        }
        public void Run()
        {
            _vista.LimpiarPantalla();
            // Acceso a las Claves del diccionario
            var menu = _casosDeUso.Keys.ToList<String>();

            while (true)
                try
                {
                    //Limpiamos
                    _vista.LimpiarPantalla();
                    // Menu
                    var key = _vista.TryObtenerElementoDeLista("Menu de Usuario", menu, "Seleciona una opción");
                    _vista.Mostrar("");
                    // Ejecución de la opción escogida
                    _casosDeUso[key].Invoke();
                    // Fin
                    _vista.MostrarYReturn("Pulsa <Return> para continuar");
                }
                catch { return; }
        }
        public void AnadirCliente()
        {
            try
            {
                var name = _vista.TryObtenerDatoDeTipo<string>("Nombre ");
                var lastName = _vista.TryObtenerDatoDeTipo<string>("Apellido ");
                var address = _vista.TryObtenerDatoDeTipo<string>("Direccion ");

                Cliente cliente = new Cliente
                {
                    id_cliente = Guid.NewGuid(),
                    nombre = name,
                    apellido = lastName,
                    direccion = address,
                    
                };
                _sistema.AnadirCliente(cliente);
            }
            catch (Exception e)
            {
                _vista.Mostrar($"UC: {e.Message}");
            }
        }

        public void MostrarClientes()
        {
        _vista.MostrarListaEnumerada<Cliente>("Clientes actuales: ", _sistema.cliente);
        }

        public void EliminarCliente()
        {
            try
            {
                MostrarClientes();
                var idc = _vista.TryObtenerValorEnRangoInt(1, _sistema.cliente.Count, "Cliente que vas a eliminar");
                var cliente = _sistema.cliente[idc - 1];
                _sistema.EliminarCliente(cliente);
                _vista.Mostrar($"Se ha eliminado con exito al cliente {cliente.nombre} {cliente.apellido}");
            }
            catch (Exception e)
            {
                _vista.Mostrar($"{e.Message}");
            }

        }
        public void AnadirPedido()
        {
                var cliente = _vista.TryObtenerElementoDeLista<Cliente>("Socios", _sistema.cliente, "");
                var _tipo = _vista.TryObtenerElementoDeLista<Pan.Tipo>("Tipos de pan ", _vista.EnumToList<Pan.Tipo>(), "Indica el tipo de pan");
                var cantidad = _vista.TryObtenerDatoDeTipo<int>("Cantidad ");
                var fecha = _vista.TryObtenerFecha("Para cuando? ");

                Pedido pedido = new Pedido
                {
                    id_cliente = cliente.id_cliente,
                    pan = new Pan{
                    tipo=_tipo
                    },
                    cantidad = cantidad,
                    fecha = fecha
                };
                _sistema.AnadirPedido(pedido);
        }
        public void MostrarPedidos()
        {
        _vista.MostrarListaEnumerada<Pedido>("Pedidos actuales: ", _sistema.pedido);
        }
        public void EntregarPedido()
        {
            try
            {
                MostrarPedidos();
                var idp = _vista.TryObtenerValorEnRangoInt(1, _sistema.pedido.Count, "Pedido a entregar ");
                var pedido = _sistema.pedido[idp - 1];
                _sistema.EliminarPedido(pedido);
                pedido.entregado = true;
                _sistema.AnadirPedido(pedido);
                _vista.Mostrar($"Se ha entregado el pedido exitosamente, se quedara pendiente de pago.");
            }
            catch (Exception e)
            {
                _vista.Mostrar($"{e.Message}");
            }
        } 
        public void PagarPedido()
        {
            try
            {
                MostrarPedidos();
                var idp = _vista.TryObtenerValorEnRangoInt(1, _sistema.pedido.Count, "Pedido que vas a pagar");
                var pedido = _sistema.pedido[idp - 1];
                pedido.pagado = true;
                _sistema.EliminarPedido(pedido);
                _vista.Mostrar($"Son {pedido.precio} euros por favor. Gracias.");
            }
            catch (Exception e)
            {
                _vista.Mostrar($"{e.Message}");
            }
        }

    }
}