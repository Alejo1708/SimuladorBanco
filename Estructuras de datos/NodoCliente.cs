namespace SimuladorBanco
{
    // Nodo basico para construir la lista enlazada de clientes
    public class NodoCliente
    {
        // Aqui se guarda el objeto con los datos de la persona
        public Cliente Dato { get; set; }
        public NodoCliente Siguiente { get; set; } //puntero que conecta con el proximo nodo en la lista

        // Cuando nace un nodo, sabe que cliente guarda, pero todavia 
        // no sabe quien va despues, por eso Siguiente empieza como nulo.
        public NodoCliente(Cliente dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }
}