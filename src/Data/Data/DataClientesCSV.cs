using Modelos;

namespace Data
{
    public class DataClientesCSV : IData<Cliente>
    {
        string _file = "../../clientes.csv";
        public void Guardar(List<Cliente> cliente)
        {

            List<string> data = new() { };
            cliente.ForEach(cliente =>
            {
            var str = $"{cliente.id_cliente},{cliente.nombre},{cliente.apellido},{cliente.direccion}";
            data.Add(str);
            });
            File.WriteAllLines(_file, data);
        }
        public List<Cliente> Leer()
        {
            return File.ReadAllLines(_file)
                .Select(Cliente.cliente)
                .ToList();
        }
    }
}