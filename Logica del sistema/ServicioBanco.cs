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
    // Agrega un cliente existente a la cola de atencion
        public void AgregarACola(string cedula)
        {
            Cliente cliente = banco.Clientes.BuscarPorCedula(cedula);
            if (cliente != null)
            {
                // Solo si el cliente esta registrado, le permitimos formarse en la fila
                // Ingresa el cliente a la cola.
                banco.Cola.Encolar(cliente);
                Console.WriteLine($"{cliente.NombreCompleto} ha sido agregado a la cola de atencion.");
            }
            else
            {
                Console.WriteLine("No se puede agregar a la cola. El cliente no existe.");
            }
        }

        // Llama al siguiente en la fila para procesarlo.
        public void AtenderSiguiente()
        {
            Cliente cliente = banco.Cola.Desencolar();
            if (cliente != null)
            {
                Console.WriteLine($"Atendiendo en ventanilla a: {cliente.NombreCompleto} (Cuenta: {cliente.NumeroCuenta})");
            }
            else
            {
                Console.WriteLine("No hay clientes en la cola para atender.");
            }
        }

        public void MostrarColaAtencion()
        {
            banco.Cola.MostrarCola();
        }

        // Aumenta el saldo de una cuenta y registra la accion en el historial
        public void RealizarDeposito(string cuenta, decimal monto)
        {
            Cliente cliente = banco.Clientes.BuscarPorCuenta(cuenta);
            if (cliente == null)
            {
                Console.WriteLine("Cuenta no encontrada.");
                return;
            }

            // Validacion de seguridad: evitar montos negativos o ceros
            if (monto <= 0)
            {
                Console.WriteLine("El monto a depositar debe ser mayor a cero.");
                return;
            }

            // Actualizamos la informacion en memoria, aumenta el saldo
            cliente.Saldo += monto;
            
            // Guardamos el paso que acabamos de dar en la Pila por si toca deshacerlo.
            //Registra la operacion en la pila
            banco.Historial.Apilar(new Transaccion("Deposito", cuenta, monto));
            Console.WriteLine($"Deposito exitoso. Nuevo saldo: ${cliente.Saldo}");
        }
        // Disminuye el saldo, siempre y cuando la persona tenga fondos suficientes
        public void RealizarRetiro(string cuenta, decimal monto)
        {
            Cliente cliente = banco.Clientes.BuscarPorCuenta(cuenta);
            if (cliente == null)
            {
                Console.WriteLine("Cuenta no encontrada.");
                return;
            }

            if (monto <= 0)
            {
                Console.WriteLine("El monto a retirar debe ser mayor a cero.");
                return;
            }

            // Verifica que el saldo sea suficiente para el retiro, nunca retirar mas del dinero que se posee
            if (cliente.Saldo < monto)
            {
                Console.WriteLine($"Fondos insuficientes. Saldo actual: ${cliente.Saldo}");
                return;
            }

            cliente.Saldo -= monto;
            
            // Nuevamente, registramos el movimiento en nuestra estructura tipo Pila.
            banco.Historial.Apilar(new Transaccion("Retiro", cuenta, monto));
            Console.WriteLine($"Retiro exitoso. Nuevo saldo: ${cliente.Saldo}");
        }

        public void ConsultarSaldo(string cuenta)
        {
            Cliente cliente = banco.Clientes.BuscarPorCuenta(cuenta);
            if (cliente != null)
            {
                Console.WriteLine($"El saldo actual de la cuenta {cuenta} es: ${cliente.Saldo}");
            }
            else
            {
                Console.WriteLine("Cuenta no encontrada.");
            }
        }

        // Deshace la ultima operacion usando la pila de transacciones.
        
        public void DeshacerUltimaTransaccion()
        {
            if (banco.Historial.EstaVacia())
            {
                Console.WriteLine("No hay transacciones recientes para deshacer.");
                return;
            }

            // Sacamos el ultimo movimiento del historial
            Transaccion ultima = banco.Historial.Desapilar();
            
            // Buscamos a quien le habiamos hecho ese movimiento.
            Cliente cliente = banco.Clientes.BuscarPorCuenta(ultima.NumeroCuenta);

            if (cliente == null)
            {
                Console.WriteLine("Error: La cuenta de la ultima transaccion ya no existe.");
                return;
            }

            // Si antes sumamos (deposito), la forma de deshacer es restar
            // Revierte un deposito restando el monto
            if (ultima.Tipo == "Deposito")
            {
                cliente.Saldo -= ultima.Monto;
                Console.WriteLine($"Se deshizo un deposito de ${ultima.Monto}. Saldo restaurado: ${cliente.Saldo}");
            }
            // Si antes restamos (retiro), la forma de deshacer es sumar devolviendo la plata
            // Revierte un retiro sumando el monto
            else if (ultima.Tipo == "Retiro")
            {
                cliente.Saldo += ultima.Monto;
                Console.WriteLine($"Se deshizo un retiro de ${ultima.Monto}. Saldo restaurado: ${cliente.Saldo}");
            }
        }

        // Calcula reportes usando metodos integrados en nuestra lista enlazada manual
        // Muestra los totales de clientes y dinero almacenado.
        public void MostrarInformacionGeneral()
        {
            int totalClientes = banco.Clientes.ContarClientes();
            decimal totalDinero = banco.Clientes.CalcularTotalDinero();

            Console.WriteLine("--- Informacion General del Banco ---");
            Console.WriteLine($"Total de clientes registrados: {totalClientes}");
            Console.WriteLine($"Total de dinero en la boveda: ${totalDinero}");
        }
    }
}