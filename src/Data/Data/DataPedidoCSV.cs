using Modelos;

namespace Data
{
    public class DataPedidoCSV : IData<Pedido>
    {
        string _file = "../../pedidos.csv";
        public void Guardar(List<Pedido> pedido)
        {

            List<string> data = new() { };
            pedido.ForEach(pedido =>
            {
            var str = $"{pedido.id_cliente},{pedido.pan},{pedido.cantidad},{pedido.entregado},{pedido.pagado},{pedido.precio},{pedido.fecha}";
            data.Add(str);
            });
            File.WriteAllLines(_file, data);
        }
        public List<Pedido> Leer()
        {
            return File.ReadAllLines(_file)
                .Select(Pedido.pedido)
                .ToList();

        }
    }
}