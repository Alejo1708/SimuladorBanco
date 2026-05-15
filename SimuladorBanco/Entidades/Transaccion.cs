namespace SimuladorBanco
{
    // Esta clase se necesita para poder guardar el historial en la pila y, si el usuario 
    // se equivoca, saber exactamente que cantidad y a que cuenta devolverle el dinero.
    public class Transaccion
    {
        public string Tipo { get; set; } // Guardara palabras clave como "Deposito" o "Retiro"
        public string NumeroCuenta { get; set; }
        public decimal Monto { get; set; }

        // Constructor para inicializar la transaccion en el momento en que ocurre.
        public Transaccion(string tipo, string numeroCuenta, decimal monto)
        {
            Tipo = tipo;
            NumeroCuenta = numeroCuenta;
            Monto = monto;
        }
    }
}