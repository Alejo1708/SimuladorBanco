namespace SimuladorBanco
{
    // Este es el nodo que forma la fila de atencion. es igual al nodo de cliente pero se hace separado
    // para que la estructura de la cola sea independiente.
    public class NodoCola
    {
        public Cliente Dato { get; set; }
        public NodoCola Siguiente { get; set; }

        public NodoCola(Cliente dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }
}