using System;
using System.Collections.Generic;

abstract class Vehiculo
{
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public int Anio { get; set; }

    public Vehiculo(string marca, string modelo, int anio)
    {
        Marca = marca;
        Modelo = modelo;
        Anio = anio;
    }

    public abstract double CalcularImpuesto();
    public abstract void MostrarInfo();
}

class Auto : Vehiculo
{
    public int Cilindraje { get; set; }

    public Auto(string marca, string modelo, int anio, int cilindraje)
        : base(marca, modelo, anio)
    {
        Cilindraje = cilindraje;
    }

    public override double CalcularImpuesto()
    {
        return 0.05 * Cilindraje * Anio;
    }

    public override void MostrarInfo()
    {
        Console.WriteLine($"Auto - {Marca} {Modelo} ({Anio}) | Cilindraje: {Cilindraje}");
    }
}

class RegistroVehicular
{
    private List<Vehiculo> vehiculos = new List<Vehiculo>();

    public void AgregarVehiculo(Vehiculo v)
    {
        vehiculos.Add(v);
    }

    public void MostrarImpuestos()
    {
        foreach (var v in vehiculos)
        {
            v.MostrarInfo();
            Console.WriteLine($"Impuesto anual: Q{v.CalcularImpuesto():0.00}\n");
        }
    }

    public void BuscarPorMarca(string marca)
    {
        foreach (var v in vehiculos)
        {
            if (v.Marca.Equals(marca, StringComparison.OrdinalIgnoreCase))
            {
                v.MostrarInfo();
                Console.WriteLine($"Impuesto: Q{v.CalcularImpuesto():0.00}\n");
            }
        }
    }

    public void MostrarPorAnio(int anio)
    {
        foreach (var v in vehiculos)
        {
            if (v.Anio == anio)
            {
                v.MostrarInfo();
            }
        }
    }

    // Extra 1: Mostrar vehículo más nuevo
    public void MostrarMasNuevo()
    {
        if (vehiculos.Count == 0) return;
        Vehiculo masNuevo = vehiculos[0];
        foreach (var v in vehiculos)
        {
            if (v.Anio > masNuevo.Anio)
                masNuevo = v;
        }
        Console.WriteLine("🚀 Vehículo más nuevo:");
        masNuevo.MostrarInfo();
    }

    // Extra 2: Promedio de impuestos
    public void MostrarPromedioImpuesto()
    {
        if (vehiculos.Count == 0) return;
        double suma = 0;
        foreach (var v in vehiculos)
        {
            suma += v.CalcularImpuesto();
        }
        double promedio = suma / vehiculos.Count;
        Console.WriteLine($"📊 Promedio de impuestos: Q{promedio:0.00}");
    }
}

class Program
{
    static void Main()
    {
        RegistroVehicular registro = new RegistroVehicular();

        // Agregamos 5 vehículos mínimo
        registro.AgregarVehiculo(new Auto("Honda", "CRV", 2021, 2000));
        registro.AgregarVehiculo(new Auto("Toyota", "Hilux", 2019, 2400));
        registro.AgregarVehiculo(new Auto("Toyota", "RAV4", 2020, 2200));
        registro.AgregarVehiculo(new Auto("Mazda", "CX-5", 2022, 2100));
        registro.AgregarVehiculo(new Auto("Nissan", "Frontier", 2018, 2500));

        int opcion;
        do
        {
            Console.WriteLine("\n===== 🚗 MENÚ REGISTRO VEHICULAR =====");
            Console.WriteLine("1. Mostrar todos los vehículos e impuestos");
            Console.WriteLine("2. Buscar por marca");
            Console.WriteLine("3. Mostrar por año");
            Console.WriteLine("4. Mostrar vehículo más nuevo");
            Console.WriteLine("5. Mostrar promedio de impuestos");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("❌ Opción inválida");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    registro.MostrarImpuestos();
                    break;
                case 2:
                    Console.Write("Ingrese la marca a buscar: ");
                    string marca = Console.ReadLine();
                    registro.BuscarPorMarca(marca);
                    break;
                case 3:
                    Console.Write("Ingrese el año: ");
                    if (int.TryParse(Console.ReadLine(), out int anio))
                        registro.MostrarPorAnio(anio);
                    else
                        Console.WriteLine("❌ Año inválido");
                    break;
                case 4:
                    registro.MostrarMasNuevo();
                    break;
                case 5:
                    registro.MostrarPromedioImpuesto();
                    break;
                case 0:
                    Console.WriteLine("👋 Saliendo del sistema...");
                    break;
                default:
                    Console.WriteLine("❌ Opción no válida");
                    break;
            }

        } while (opcion != 0);
    }
}
