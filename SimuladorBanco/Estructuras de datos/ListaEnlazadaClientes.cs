
using System;

namespace SimuladorBanco
{
    // Esta es una lista simple donde cada 
    // nodo conoce al siguiente. 
    public class ListaEnlazadaClientes
    {
        // La cabeza es el punto de partida. Si se pierde la cabeza, se pierde toda la lista.
        private NodoCliente cabeza;

        public ListaEnlazadaClientes()
        {
            cabeza = null; // Al principio, el banco esta vacio.
        }

        // Metodo para agregar un cliente al final de nuestra cadena.
        public void Insertar(Cliente nuevoCliente)
        {
            NodoCliente nuevoNodo = new NodoCliente(nuevoCliente);

            // Primer caso: la lista esta vacia. El nuevo es el primero y el unico.
            if (cabeza == null)
            {
                cabeza = nuevoNodo;
            }
            else
            {
                // Segundo caso: ya hay gente. Empezamos desde la cabeza y caminamos
                // nodo por nodo hasta encontrar al ultimo (el que no tiene a nadie despues).
                NodoCliente actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                // Enganchamos el nuevo nodo al final de la cadena.
                actual.Siguiente = nuevoNodo;
            }
        }

        // Busca a una persona por su cedula. Retorna el cliente si lo encuentra, 
        // o nulo si esa cedula no existe en el banco.
        public Cliente BuscarPorCedula(string cedula)
        {
            NodoCliente actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Cedula == cedula)
                {
                    return actual.Dato; // Lo encontramos, devolvemos el dato.
                }
                actual = actual.Siguiente; // Pasamos al siguiente para seguir buscando.
            }
            return null; 
        }

        // Igual que el metodo anterior, pero busca por numero de cuenta. 
        // Es importante para hacer depositos y retiros.
        public Cliente BuscarPorCuenta(string numeroCuenta)
        {
            NodoCliente actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.NumeroCuenta == numeroCuenta)
                {
                    return actual.Dato;
                }
                actual = actual.Siguiente;
            }
            return null;
        }

        // Imprime en pantalla a todos los clientes. Recorre la lista de inicio a fin.
        public void Recorrer()
        {
            if (cabeza == null)
            {
                Console.WriteLine("No hay clientes registrados.");
                return;
            }

            NodoCliente actual = cabeza;
            while (actual != null)
            {
                actual.Dato.MostrarInformacion();
                actual = actual.Siguiente;
            }
        }

        // Metodo de utilidad para saber el tamano de la lista contando los nodos.
        public int ContarClientes()
        {
            int contador = 0;
            NodoCliente actual = cabeza;
            while (actual != null)
            {
                contador++;
                actual = actual.Siguiente;
            }
            return contador;
        }

        // Recorre todos los nodos sumando los saldos para saber el dinero total del banco.
        public decimal CalcularTotalDinero()
        {
            decimal total = 0;
            NodoCliente actual = cabeza;
            while (actual != null)
            {
                total += actual.Dato.Saldo;
                actual = actual.Siguiente;
            }
            return total;
        }
    }
}