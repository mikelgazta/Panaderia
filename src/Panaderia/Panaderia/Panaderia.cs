using System;
using System.Collections.Generic;
using Data;
using Modelos;


namespace Library
{
    public class Panaderia
    {
        public List<Cliente> cliente { get; set; } = new();

        public List<Pedido> pedido { get; set; } = new();

        DataClientesCSV Repositorio;

        DataPedidoCSV Repositorio2;

        public Panaderia(DataClientesCSV repo, DataPedidoCSV repo2)
        {
            Repositorio = repo;
            Repositorio2 = repo2;
            cliente = Repositorio.Leer();
            pedido = Repositorio2.Leer();
        }
        public void AnadirCliente(Cliente c)
        {
            cliente.Add(c);
            Repositorio.Guardar(cliente);
        }
        public void EliminarCliente(Cliente c)
        {
            cliente.Remove(c);
            Repositorio.Guardar(cliente);
        }
        public void AnadirPedido(Pedido p)
        {
            pedido.Add(p);
            Repositorio2.Guardar(pedido);
        }
        public void EliminarPedido(Pedido p)
        {
            pedido.Remove(p);
            Repositorio2.Guardar(pedido);
        }
    }   
}