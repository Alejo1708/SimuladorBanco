using System;

namespace SimuladorBanco
{
    // Esta clase representa la fila fisica del banco. 
    // Funciona bajo la regla de que el primero en entrar es el primero en salir.
    public class ColaAtencion
    {
        // En una cola necesitamos saber quien es el primero para poder atenderlo
        // y quien es el ultimo (para poner a los nuevos detras de el).
        private NodoCola frente;
        private NodoCola final;

        public ColaAtencion()
        {
            frente = null;
            final = null;
        }

        // Agrega una persona al final de la fila.
        public void Encolar(Cliente cliente)
        {
            NodoCola nuevoNodo = new NodoCola(cliente);

            // Si no hay nadie, el que llega es el primero y tambien el ultimo.
            if (frente == null)
            {
                frente = nuevoNodo;
                final = nuevoNodo;
            }
            else
            {
                // Si ya hay fila, el que estaba de ultimo ahora apunta al nuevo,
                // y luego actualizamos nuestra variable 'final' para que sea el nuevo.
                final.Siguiente = nuevoNodo;
                final = nuevoNodo;
            }
        }

        // Llama a la siguiente persona de la fila y la saca de la cola.
        public Cliente Desencolar()
        {
            // Si la fila esta vacia, no hay a quien atender.
            if (EstaVacia()) return null;

            // Tomamos los datos de la persona que esta al frente.
            Cliente clienteAtendido = frente.Dato;
            
            // El que estaba detras de el ahora pasa a ser el nuevo frente.
            frente = frente.Siguiente; 

            // Si al sacar al cliente nos quedamos sin frente, significa que la fila
            // quedo totalmente vacia, asi que el final tambien debe ser nulo.
            if (frente == null)
            {
                final = null; 
            }

            return clienteAtendido;
        }

        // Muestra en pantalla el orden actual de la fila sin alterarla.
        public void MostrarCola()
        {
            if (EstaVacia())
            {
                Console.WriteLine("La cola de atencion esta vacia.");
                return;
            }

            Console.WriteLine("--- Clientes en espera ---");
            NodoCola actual = frente;
            int turno = 1;
            
            // Vamos saltando de nodo en nodo hasta llegar al final de la fila.
            while (actual != null)
            {
                Console.WriteLine($"Turno {turno}: {actual.Dato.NombreCompleto} (Cedula: {actual.Dato.Cedula})");
                actual = actual.Siguiente;
                turno++;
            }
        }

        // Un metodo simple para preguntar si la cola no tiene a nadie.
        public bool EstaVacia()
        {
            return frente == null;
        }
    }
}