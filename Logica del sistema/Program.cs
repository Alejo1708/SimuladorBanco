using System;

namespace SimuladorBanco
{
    // Clase principal de la aplicacion de consola
    class Program
    {
        // Punto de entrada principal donde se inicializa y ejecuta la aplicacion
        static void Main(string[] args)
        {
            // Instancia del contenedor principal de las estructuras de datos
            Banco miBanco = new Banco();

           // Instancia del servicio que maneja la logica de negocio y opera sobre los datos
            ServicioBanco miServicio = new ServicioBanco(miBanco);

         // Instancia del menu de consola, inyectando el servicio para ejecutar las ordenes
            Menu menuPrincipal = new Menu(miServicio);

// Inicia el ciclo principal de interaccion con el usuario
            menuPrincipal.Iniciar();
        }
    }
}