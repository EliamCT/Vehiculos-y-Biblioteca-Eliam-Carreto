using System;
using System.Collections.Generic;

class Autor
{
    public string Nombre { get; set; }
    public string Nacionalidad { get; set; }
    public DateTime FechaNacimiento { get; set; }

    public Autor(string nombre, string nacionalidad, DateTime fechaNacimiento)
    {
        Nombre = nombre;
        Nacionalidad = nacionalidad;
        FechaNacimiento = fechaNacimiento;
    }

    public int GetEdad()
    {
        return DateTime.Now.Year - FechaNacimiento.Year;
    }
}

class Libro
{
    public string Titulo { get; set; }
    public string Isbn { get; set; }
    public int AnioPublicacion { get; set; }
    public Autor Autor { get; set; }

    public Libro(string titulo, string isbn, int anio, Autor autor)
    {
        Titulo = titulo;
        Isbn = isbn;
        AnioPublicacion = anio;
        Autor = autor;
    }

    public void MostrarInformacion()
    {
        Console.WriteLine($"📖 Título: {Titulo}, ISBN: {Isbn}, Año: {AnioPublicacion}");
        Console.WriteLine($"👤 Autor: {Autor.Nombre} ({Autor.Nacionalidad}) - Edad: {Autor.GetEdad()} años\n");
    }
}

class Biblioteca
{
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    private List<Libro> libros = new List<Libro>();

    public Biblioteca(string nombre, string direccion)
    {
        Nombre = nombre;
        Direccion = direccion;
    }

    public void AgregarLibro(Libro libro)
    {
        libros.Add(libro);
    }

    public void BuscarPorAutor(string nombreAutor)
    {
        foreach (var libro in libros)
        {
            if (libro.Autor.Nombre.Equals(nombreAutor, StringComparison.OrdinalIgnoreCase))
            {
                libro.MostrarInformacion();
            }
        }
    }

    public void ListarLibros()
    {
        foreach (var libro in libros)
        {
            libro.MostrarInformacion();
        }
    }

    public void BuscarPorTitulo(string titulo)
    {
        foreach (var libro in libros)
        {
            if (libro.Titulo.IndexOf(titulo, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                libro.MostrarInformacion();
            }
        }
    }

    public void ListarPorAnio(int anio)
    {
        foreach (var libro in libros)
        {
            if (libro.AnioPublicacion == anio)
            {
                libro.MostrarInformacion();
            }
        }
    }
}

class Program
{
    static void Main()
    {
        Biblioteca miBiblioteca = new Biblioteca("Biblioteca Central", "Av. Principal #123");

        // 👉 Creamos autores
        Autor autor1 = new Autor("Gabriel García Márquez", "Colombiano", new DateTime(1927, 3, 6));
        Autor autor2 = new Autor("Mario Vargas Llosa", "Peruano", new DateTime(1936, 3, 28));
        Autor autor3 = new Autor("Isabel Allende", "Chilena", new DateTime(1942, 8, 2));

        // 👉 Creamos libros
        miBiblioteca.AgregarLibro(new Libro("Cien Años de Soledad", "978-3-16-148410-0", 1967, autor1));
        miBiblioteca.AgregarLibro(new Libro("El Amor en los Tiempos del Cólera", "978-84-376-0494-7", 1985, autor1));
        miBiblioteca.AgregarLibro(new Libro("La Ciudad y los Perros", "978-84-663-3709-6", 1963, autor2));
        miBiblioteca.AgregarLibro(new Libro("Conversación en La Catedral", "978-84-663-3799-7", 1969, autor2));
        miBiblioteca.AgregarLibro(new Libro("La Casa de los Espíritus", "978-84-204-8305-6", 1982, autor3));

        int opcion;
        do
        {
            Console.WriteLine("\n📚 --- Menú Biblioteca ---");
            Console.WriteLine("1. Listar todos los libros");
            Console.WriteLine("2. Buscar libros por autor");
            Console.WriteLine("3. Buscar libro por título");
            Console.WriteLine("4. Listar libros por año");
            Console.WriteLine("5. Salir");
            Console.Write("👉 Elige una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("⚠️ Opción inválida.");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    miBiblioteca.ListarLibros();
                    break;
                case 2:
                    Console.Write("Ingrese el nombre del autor: ");
                    string autor = Console.ReadLine();
                    miBiblioteca.BuscarPorAutor(autor);
                    break;
                case 3:
                    Console.Write("Ingrese el título o parte del título: ");
                    string titulo = Console.ReadLine();
                    miBiblioteca.BuscarPorTitulo(titulo);
                    break;
                case 4:
                    Console.Write("Ingrese el año: ");
                    if (int.TryParse(Console.ReadLine(), out int anio))
                        miBiblioteca.ListarPorAnio(anio);
                    else
                        Console.WriteLine("⚠️ Año inválido");
                    break;
                case 5:
                    Console.WriteLine("👋 Saliendo de la biblioteca...");
                    break;
                default:
                    Console.WriteLine("⚠️ Opción no válida.");
                    break;
            }

        } while (opcion != 5);
    }
}
