using System;
using System.Collections.Generic;

class Datos
{
    public List<string> clientes;
    public List<bool[]> horarios;
    public List<string> facturas;

    public Datos()
    {
        clientes = new List<string> { "cliente1", "cliente2", "cliente3", "cliente4", "cliente5", "cliente6", "cliente7", "cliente8", "cliente9", "cliente10" };
        horarios = new List<bool[]>();
        facturas = new List<string>();

        for (int i = 0; i < 4; i++)
        {
            bool[] horario = new bool[3];
            Random random = new Random();
            for (int j = 0; j < 3; j++)
            {
                horario[j] = random.Next(2) == 0;
            }
            horarios.Add(horario);
        }
    }
}

class Validaciones
{
    public static bool ValidarNumero(string input, out int numero)
    {
        return int.TryParse(input, out numero);
    }

    public static bool ValidarCampoVacio(string input)
    {
        return !string.IsNullOrEmpty(input);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Datos datos = new Datos();
        int opcion;

        do
        {
            MostrarPantallaPrincipal(datos);
            string input = Console.ReadLine();
            if (Validaciones.ValidarNumero(input, out opcion))
            {
                switch (opcion)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        if (ValidarDisponibilidadHorario(datos.horarios[opcion - 1]))
                        {
                            MostrarPantallaRegistro(datos);
                            RegistroCliente(datos);
                        }
                        else
                        {
                            Console.WriteLine("El horario seleccionado no tiene disponibilidad.");
                            Console.ReadLine();
                        }
                        break;
                    case 5:
                        Console.WriteLine("Gracias por utilizar nuestro servicio. ¡Hasta luego!");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Por favor, ingrese un número del 1 al 5.");
                        Console.ReadLine();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Error: Por favor, ingrese un número válido.");
                Console.ReadLine();
            }
        } while (opcion != 5);
    }

    static void MostrarPantallaPrincipal(Datos datos)
    {
        Console.Clear();
        Console.WriteLine("Pantalla Principal");
        Console.WriteLine("------------------");
        Console.WriteLine("Horarios de Viaje:");
        for (int i = 0; i < datos.horarios.Count; i++)
        {
            Console.WriteLine($"Horario {i + 1}: {(ValidarDisponibilidadHorario(datos.horarios[i]) ? "Tiene espacios disponibles" : "Lleno")}");
        }
        Console.WriteLine();
        Console.WriteLine("Seleccione un horario (1-4) o presione 5 para salir:");
    }

    static bool ValidarDisponibilidadHorario(bool[] horario)
    {
        int espaciosDisponibles = 0;
        foreach (bool disponible in horario)
        {
            if (!disponible)
            {
                espaciosDisponibles++;
            }
        }
        return espaciosDisponibles > 0;
    }

    static void MostrarPantallaRegistro(Datos datos)
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Registro");
        Console.WriteLine("--------------------");
    }

    static void RegistroCliente(Datos datos)
    {
        string nombre, apellidos, cedula, correo;
        Console.Write("Nombre: ");
        nombre = Console.ReadLine();
        if (!Validaciones.ValidarCampoVacio(nombre))
        {
            Console.WriteLine("Error: El campo nombre no puede estar vacío.");
            Console.ReadLine();
            return;
        }
        Console.Write("Apellidos: ");
        apellidos = Console.ReadLine();
        if (!Validaciones.ValidarCampoVacio(apellidos))
        {
            Console.WriteLine("Error: El campo apellidos no puede estar vacío.");
            Console.ReadLine();
            return;
        }
        Console.Write("Cédula: ");
        cedula = Console.ReadLine();
        if (!Validaciones.ValidarCampoVacio(cedula))
        {
            Console.WriteLine("Error: El campo cédula no puede estar vacío.");
            Console.ReadLine();
            return;
        }
        Console.Write("Correo Electrónico: ");
        correo = Console.ReadLine();
        if (!Validaciones.ValidarCampoVacio(correo))
        {
            Console.WriteLine("Error: El campo correo electrónico no puede estar vacío.");
            Console.ReadLine();
            return;
        }

        string cliente = $"{nombre} {apellidos} | Cédula: {cedula} | Correo: {correo}";
        datos.clientes.Add(cliente);

        MostrarPantallaFacturacion(datos, cliente);
    }

    static void MostrarPantallaFacturacion(Datos datos, string cliente)
    {
        Console.Clear();
        Console.WriteLine("Pantalla de Facturación");
        Console.WriteLine("-----------------------");
        Console.WriteLine("Datos del Cliente:");
        Console.WriteLine(cliente);
        Console.WriteLine("Fecha: " + DateTime.Now.ToShortDateString());
        Console.WriteLine("Cantidad de boletos: 1");
        Console.WriteLine("Descripción: Viaje de Puntarenas a Paquera");
        Console.WriteLine();

        decimal montoPago = 100.00m; // Ejemplo: Monto del pago sin impuestos
        decimal subtotal = montoPago;
        decimal impuestoVentas = subtotal * 0.13m; // Ejemplo: Impuesto de ventas (13%)
        decimal total = subtotal + impuestoVentas;

        Console.WriteLine($"Subtotal: {subtotal:C}");
        Console.WriteLine($"Impuesto de Ventas: {impuestoVentas:C}");
        Console.WriteLine($"Total: {total:C}");
        Console.WriteLine();
        Console.WriteLine("Seleccione una opción:");
        Console.WriteLine(" - 'G' para guardar la factura");
        Console.WriteLine(" - Presione Enter para salir");

        string input = Console.ReadLine();
        if (input.ToUpper() == "G")
        {
            GuardarFactura(datos, cliente, subtotal, impuestoVentas, total);
            MostrarRegistrosClientes(datos);
        }
        else
        {
            Console.WriteLine("Gracias por utilizar nuestro servicio. ¡Hasta luego!");
            Console.ReadLine();
        }
    }

    static void GuardarFactura(Datos datos, string cliente, decimal subtotal, decimal impuestoVentas, decimal total)
    {
        string factura = $"Cliente: {cliente} | Subtotal: {subtotal:C} | Impuesto de Ventas: {impuestoVentas:C} | Total: {total:C}";
        datos.facturas.Add(factura);
    }

    static void MostrarRegistrosClientes(Datos datos)
    {
        Console.Clear();
        Console.WriteLine("Registros de Clientes");
        Console.WriteLine("---------------------");
        foreach (string cliente in datos.clientes)
        {
            Console.WriteLine(cliente);
        }
        Console.WriteLine();
        Console.WriteLine("Seleccione una opción:");
        Console.WriteLine(" - Presione Enter para salir");

        Console.ReadLine();
    }
}
