using System;

namespace Modelos
{
    public class Cliente
    {
        public Guid id_cliente { get; set; }
        public string nombre { get; set; }

        public string apellido { get; set; }

        public string direccion { get; set; }


        public string ToCSV() => $"{id_cliente},{nombre},{apellido},{direccion}";

        public static Cliente cliente(string _file)
        {
            var campos = _file.Split(",");
            return new Cliente
            {
                id_cliente = Guid.Parse(campos[0]),
                nombre = (campos[1]),
                apellido = (campos[2]),
                direccion = (campos[3]),
            };
        }
        public override string ToString()
        {
            return $"{nombre} {apellido}, {direccion}";
        }
    }



    public class Pan
    {
        public enum Tipo
        {
            Bagguette,
            Integral,
            Centeno,
            Maiz,
        }

        public Tipo tipo { get; set; }

        public decimal precio
        {
            get
            {
                decimal precio = tipo switch
                {
                    Tipo.Bagguette =>0.50m,
                    Tipo.Integral => 1.00m,
                    Tipo.Centeno => 2.00m,
                    Tipo.Maiz => 3.00m
                };
                return precio;
            }
        }
    }
    public class Pedido
    {
        public Guid id_cliente { get; set; }

        public Pan pan { get; set; }

        public int cantidad { get; set; }
        
        public Boolean entregado { get; set; } = false;

        public Boolean pagado { get; set; } = false;        
        public DateTime fecha { get; set; }
        public decimal precio
        {
            get
            {
                return  pan.precio * cantidad;
            }
            set{}
        }


        public string ToCSV() => $"{id_cliente},{pan.tipo},{cantidad},{entregado},{pagado}";

        public static Pedido pedido(string _file)
        {
            var campos = _file.Split(",");
            return new Pedido
            {
                id_cliente = Guid.Parse(campos[0]),
                pan = new Pan { tipo = (Pan.Tipo)Enum.Parse(typeof(Pan.Tipo), campos[1])},
                cantidad = Int32.Parse(campos[2]),
                entregado = Boolean.Parse(campos[3]),
                pagado = Boolean.Parse(campos[4]),
                fecha = DateTime.Parse(campos[5]),
                precio = Decimal.Parse(campos[6])
            };
        }
        public override string ToString()
        {
            return $"{pan.tipo}, {cantidad}, {precio}, {fecha},(Entregado) {entregado}, (Pagado) {pagado}";
        }
    }
}
