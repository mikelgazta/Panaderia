using Modelos;
using System.Collections.Generic;

namespace Data
{
    public interface IData<T>
    {
        void Guardar(List<T> lista);

        List<T> Leer();
    }
}