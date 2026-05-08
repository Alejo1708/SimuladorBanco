namespace SimuladorBanco
{
    // La clase del Banco no hace validaciones ni operaciones complejas, simplemente sostiene en su 
    // interior las tres estructuras de datos que requiere el proyecto
    public class Banco
    {
        // Propiedades que representan los recursos del banco
        public ListaEnlazadaClientes Clientes { get; set; }
        public ColaAtencion Cola { get; set; }
        public PilaTransacciones Historial { get; set; }

        // Al momento de abrir las puertas del banco, se preparan las estructuras vacias
        public Banco()
        {
            Clientes = new ListaEnlazadaClientes();
            Cola = new ColaAtencion();
            Historial = new PilaTransacciones();
        }
    }
}