using Data;
using Library;

namespace Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            var repositorio= new Data.DataClientesCSV();
            var repositorio2 = new Data.DataPedidoCSV();
            var view=new Vista();
            var sistema=new Panaderia(repositorio, repositorio2);
            var controlador=new Controlador(view, sistema);
            controlador.Run();
        }
    }
}
