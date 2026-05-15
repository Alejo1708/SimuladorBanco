using System;

namespace SimuladorBanco
{
    // Esta clase es un modelo de datos puro. Su unica responsabilidad es almacenar
    // las caracteristicas de un cliente. No realiza calculos ni logica compleja, 
    // solo guarda informacion para que el resto del sistema la pueda usar.
    public class Cliente
    {
        public string Cedula { get; set; }
        public string NombreCompleto { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }

        // El constructor obliga a dar todos los datos necesarios cuando se crea
        // un cliente nuevo, asi se evita tener clientes a medias en el sistema.
        public Cliente(string cedula, string nombreCompleto, string numeroCuenta, decimal saldoInicial)
        {
            Cedula = cedula;
            NombreCompleto = nombreCompleto;
            NumeroCuenta = numeroCuenta;
            Saldo = saldoInicial;
        }

        // metodo para imprimir los datos rapido en la consola 
        public void MostrarInformacion()
        {
            Console.WriteLine($"Cedula: {Cedula} | Nombre: {NombreCompleto} | Cuenta: {NumeroCuenta} | Saldo: ${Saldo}");
        }
    }
}