namespace SimuladorBanco
{
    // Este nodo es exclusivo para guardar las transacciones pasadas.
    // En lugar de guardar un Cliente, guarda un objeto Transaccion.
    public class NodoPila
    {
        public Transaccion Dato { get; set; }
        public NodoPila Siguiente { get; set; }

        public NodoPila(Transaccion dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }
}