using System;

namespace SimuladorBanco
{
    // El ServicioBanco contiene las reglas del negocio.
    // Controla que un cliente no retire mas de lo que tiene, que no haya cedulas repetidas, etc.
    public class ServicioBanco
    {
        // El servicio necesita conocer al banco para poder manipular sus listas y colas.
        private Banco banco;  // Referencia al contenedor principal de datos.

        public ServicioBanco(Banco bancoCentral)
        {
            banco = bancoCentral;
        }

        // Logica para crear una cuenta nueva y guardarla en la lista enlazada.
        // Crea un nuevo cliente despues de validar duplicados.
        public void RegistrarCliente(string cedula, string nombre, string cuenta, decimal saldo)
        {
            // Regla de negocio 1: No pueden haber dos clientes con la misma cedula, verifica que el numero de cuenta no exista.
            if (banco.Clientes.BuscarPorCedula(cedula) != null)
            {
                Console.WriteLine("Error: Ya existe un cliente con esa cedula.");
                return; // Cortamos la ejecucion para evitar el duplicado.
            }

            // Regla de negocio 2: Las cuentas deben ser unicas, verifica que el numero de cuenta no exista
            if (banco.Clientes.BuscarPorCuenta(cuenta) != null)
            {
                Console.WriteLine("Error: Ya existe un cliente con ese numero de cuenta.");
                return;
            }

            // Si pasa las validaciones, creamos el cliente y lo metemos a la estructura.
            // Agrega el cliente a la lista.
            Cliente nuevoCliente = new Cliente(cedula, nombre, cuenta, saldo);
            banco.Clientes.Insertar(nuevoCliente);
            Console.WriteLine("Cliente registrado exitosamente.");
        }

        // Pide a la estructura de lista que muestre lo que tiene guardado.
        // Imprime la lista de clientes registrados.
        public void ListarClientes()
        {
            banco.Clientes.Recorrer();
        }

        // Logica para consultar informacion basandose en un identificador unico.
        // Busca e imprime los datos de un cliente por su cedula.
        public void BuscarCliente(string cedula)
        {
            Cliente cliente = banco.Clientes.BuscarPorCedula(cedula);
            if (cliente != null)
            {
                Console.WriteLine("Cliente encontrado:");
                cliente.MostrarInformacion();
            }
            else
            {
                Console.WriteLine("Cliente no encontrado en el sistema.");
            }
        }