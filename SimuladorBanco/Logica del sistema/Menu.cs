using System;

namespace SimuladorBanco
{
    // Clase encargada de la interaccion con el usuario y captura de datos
    // Realiza validaciones basicas antes de enviar la informacion a ServicioBanco
    public class Menu
    {
        private ServicioBanco servicio;

        // Inyectamos el servicio al construir el menu para que sepa a quien dar ordenes
        // Constructor que recibe la instancia del servicio bancario
        public Menu(ServicioBanco servicioBanco)
        {
            servicio = servicioBanco;
        }

        // Metodo principal que mantiene el programa corriendo en un ciclo.
        public void Iniciar()
        {
            bool salir = false;

            // Ciclo de ejecucion del menu principal
            // Este ciclo infinito solo se rompe cuando el usuario elige la opcion de salir
            while (!salir)
            {
                Console.Clear(); 
                Console.WriteLine("========================================");
                Console.WriteLine("       SIMULADOR BANCARIO CONSOLA       ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Registrar cliente");
                Console.WriteLine("2. Listar clientes");
                Console.WriteLine("3. Buscar cliente");
                Console.WriteLine("4. Agregar cliente a la cola de atencion");
                Console.WriteLine("5. Atender siguiente cliente");
                Console.WriteLine("6. Realizar deposito");
                Console.WriteLine("7. Realizar retiro");
                Console.WriteLine("8. Consultar saldo");
                Console.WriteLine("9. Deshacer ultima transaccion");
                Console.WriteLine("10. Mostrar cola de atencion");
                Console.WriteLine("11. Mostrar informacion general del banco");
                Console.WriteLine("12. Salir");
                Console.WriteLine("========================================");
                Console.Write("Seleccione una opcion: ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                // Evaluacion de la opcion seleccionada por el usuario
                // El bloque switch enruta la opcion digitada hacia el metodo correcto
                switch (opcion)
                {
                    case "1":
                        RegistrarNuevoCliente();
                        break;
                    case "2":
                        servicio.ListarClientes();
                        break;
                    case "3":
                        BuscarUnCliente();
                        break;
                    case "4":
                        MandarACola();
                        break;
                    case "5":
                        servicio.AtenderSiguiente();
                        break;
                    case "6":
                        HacerDeposito();
                        break;
                    case "7":
                        HacerRetiro();
                        break;
                    case "8":
                        ConsultarUnSaldo();
                        break;
                    case "9":
                        servicio.DeshacerUltimaTransaccion();
                        break;
                    case "10":
                        servicio.MostrarColaAtencion();
                        break;
                    case "11":
                        servicio.MostrarInformacionGeneral();
                        break;
                    case "12":
                        salir = true; 
                        Console.WriteLine("Gracias por usar el simulador bancario.");
                        break;
                    default:
                        Console.WriteLine("Opcion invalida. Intente de nuevo.");
                        break;
                }

               // Pausa la ejecucion para permitir la lectura de los resultados
                // Pausamos la pantalla antes de limpiarla, para que el usuario alcance a leer
                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        // Metodos de accion del menu 
        //Sub-metodos para mantener el bloque switch corto y ordenado 

        private void RegistrarNuevoCliente()
        {
            string cedula = LeerCadenaNumerica("Ingrese la cedula del cliente (solo numeros): ");
            string nombre = LeerCadenaNoVacia("Ingrese el nombre completo: ");
            string cuenta = LeerCadenaNumerica("Ingrese el numero de cuenta (solo numeros): ");
            decimal saldo = LeerDecimalValido("Ingrese el saldo inicial: ");
            
           // Envio de datos validados al servicio bancario
            // Mandamos los datos ya verificados a la logica del servicio
            servicio.RegistrarCliente(cedula, nombre, cuenta, saldo);
        }

        private void BuscarUnCliente()
        {
            string cedBuscada = LeerCadenaNumerica("Ingrese la cedula a buscar (solo numeros): ");
            servicio.BuscarCliente(cedBuscada);
        }

        private void MandarACola()
        {
            string cedCola = LeerCadenaNumerica("Ingrese la cedula del cliente para agregar a la cola (solo numeros): ");
            servicio.AgregarACola(cedCola);
        }

        private void HacerDeposito()
        {
            string cuentaDep = LeerCadenaNumerica("Ingrese el numero de cuenta (solo numeros): ");
            decimal montoDep = LeerDecimalValido("Ingrese el monto a depositar: ");
            servicio.RealizarDeposito(cuentaDep, montoDep);
        }

        private void HacerRetiro()
        {
            string cuentaRet = LeerCadenaNumerica("Ingrese el numero de cuenta (solo numeros): ");
            decimal montoRet = LeerDecimalValido("Ingrese el monto a retirar: ");
            servicio.RealizarRetiro(cuentaRet, montoRet);
        }

        private void ConsultarUnSaldo()
        {
            string cuentaSal = LeerCadenaNumerica("Ingrese el numero de cuenta (solo numeros): ");
            servicio.ConsultarSaldo(cuentaSal);
        }
// Metodos de validacion contra errores del usuario 

        // Valida que la cadena ingresada no este vacia ni sea solo espacios
        // Atrapa al usuario si presiona Enter sin escribir nada. Se usa para textos como el nombre.
        private string LeerCadenaNoVacia(string mensaje)
        {
            string entrada = "";
            while (string.IsNullOrWhiteSpace(entrada))
            {
                Console.Write(mensaje);
                entrada = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine("Este campo no puede estar vacio. Intente de nuevo.");
                }
            }
            return entrada;
        }

        // Valida que la cadena ingresada contenga unicamente caracteres numericos
        // Lo usamos para que las cedulas y cuentas nunca lleven letras por error
        
        private string LeerCadenaNumerica(string mensaje)
        {
            string entrada = "";
            bool esValido = false;

            while (!esValido)
            {
                Console.Write(mensaje);
                entrada = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine("Este campo no puede estar vacio. Intente de nuevo.");
                }
                else if (!EsSoloNumeros(entrada))
                {
                    Console.WriteLine("Error: Este campo solo permite numeros. Intente de nuevo sin espacios ni letras.");
                }
                else
                {
                    esValido = true; 
                }
            }
            return entrada;
        }

         // Verifica si todos los caracteres de un string son digitos
        // Metodo auxiliar para revisar letra por letra que un texto sea puro numero
        private bool EsSoloNumeros(string texto)
        {
            foreach (char caracter in texto)
            {
                if (caracter < '0' || caracter > '9')
                {
                    return false;
                }
            }
            return true;
        }

        // Valida el ingreso de un valor decimal positivo con un limite de 12 digitos
        // Esta validacion ahora incluye el limite maximo de 12 digitos (999,999,999,999)
        
        private decimal LeerDecimalValido(string mensaje)
        {
            decimal valor;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();
                
                // Intenta la conversion del string a decimal positivo
                // Primero intenta convertirlo a un numero positivo
                if (decimal.TryParse(entrada, out valor) && valor >= 0)
                {
                    // Verifica el limite maximo permitido.
                    if (valor <= 999999999999m)
                    {
                        return valor;
                    }
                    else
                    {
                        Console.WriteLine("Error: El monto ingresado es demasiado grande. El limite es de 12 digitos.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Por favor ingrese un valor numerico valido y positivo. El limite es de 12 digitos");
                }
            }
        }
    }
}
