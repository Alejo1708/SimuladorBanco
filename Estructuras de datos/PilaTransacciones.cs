namespace SimuladorBanco
{
    // Esta estructura permite deshacer acciones. 
    // Funciona con la logica de que el ultimo movimiento en registrarse es el primero 
    // en salir cuando queramos echar para atras una operacion.
    public class PilaTransacciones
    {
        // A diferencia de la cola, en la pila solo nos importa el elemento de mas arriba
        // Puntero que siempre indica cual es el elemento superior de la pila.
        private NodoPila cima;

        public PilaTransacciones()
        {
            cima = null;
        }

        // Coloca una nueva transaccion en la parte superior del historial
        public void Apilar(Transaccion transaccion)
        {
            NodoPila nuevoNodo = new NodoPila(transaccion);
            
            
            nuevoNodo.Siguiente = cima; // El nuevo nodo apunta al que antes estaba arriba
            
            cima = nuevoNodo; // Actualizamos el puntero para que el nuevo nodo sea la nueva cima
        }

        // Saca la transaccion mas reciente para deshacerla.
        public Transaccion Desapilar()
        {
            if (EstaVacia()) return null; // No hay historial.

            // Tomamos el dato del nodo superior.
            Transaccion transaccion = cima.Dato;
            
            // La cima baja un nivel, apuntando al nodo que estaba justo debajo, o sea que se actualiza la cima
            //para que baje al siguiente nodo 
            cima = cima.Siguiente;
            
            return transaccion;
        }

// Metodo de control para saber si la pila no tiene elementos
        public bool EstaVacia()
        {
            return cima == null;
        }
    }
}