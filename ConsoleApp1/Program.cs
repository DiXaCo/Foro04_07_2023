using ConsoleApp1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Reflection.Metadata;


class Program
{
    static void Main(string[] args)
    {
        //agregarEstudiante();
        //consultarEstudiantes();
        //consultarEstudiante();
        //modificarEstudiante();
        //eliminarEstudiante();
        //consultarEstudiantesFunciones();
        //guardarEstudianteYdireccion();
        //guardarEstudianteYdireccionTransaction();
        //consultarDirecciones();
        //consultarDireccion();
        //consultarDireccion2();
        //AgregarCurso();
        Console.WriteLine("Presione Enter para salir...");
        Console.ReadLine();
    }

    public static void guardarEstudianteYdireccion()
    {
        Console.WriteLine("Metodo agregar estudiante y direccion");

        SchoolContext context = new SchoolContext();
        Student std = new Student();
        StudentAddress stdAddress = new StudentAddress();
        
        std.Name = "Ciri";
        context.Students.Add(std);
        context.SaveChanges();

        stdAddress.Address1 = "direccion 1";
        stdAddress.Address2 = "direccion 2";
        stdAddress.StudentID = std.StudentId;
        stdAddress.City = "gye";
        stdAddress.State = "ecu";
        stdAddress.Student= std;
       
        context.StudentAddresses.Add(stdAddress);

        context.SaveChanges();

    }

    public static void AgregarCurso()
    {
        Console.WriteLine("Método agregar curso");

        using (var context = new SchoolContext())
        {
            Course curso = new Course();
            curso.CourseName = "Nombre del Curso";

            context.Courses.Add(curso);
            context.SaveChanges();

            Console.WriteLine("Código: " + curso.CourseId + " Nombre: " + curso.CourseName);
        }
    }

    public static void guardarEstudianteYdireccionTransaction()
    {
        Console.WriteLine("Metodo agregar estudiante y direccion");

        using (var context = new SchoolContext())
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Crear el estudiante
                    Student std = new Student();
                    std.Name = "Karina";
                    context.Students.Add(std);

                    // Crear la dirección del estudiante
                    StudentAddress stdAddress = new StudentAddress();
                    stdAddress.Address1 = "direccion 1";
                    stdAddress.Address2 = "direccion 2";
                    stdAddress.City = "gye";
                    stdAddress.State = "ecu";

                    // Relacionar la dirección con el estudiante
                    std.StudentAddress = stdAddress;

                    // Crear el curso
                    Course course = new Course();
                    course.CourseName = "Nombre del Curso";

                    // Agregar el curso al estudiante
                   // course.CourseId = new List<Course> { course };
                    // Guardar los cambios en la base de datos
                    context.SaveChanges();

                    // Confirmar la transacción
                    dbContextTransaction.Commit();

                    Console.WriteLine("Datos guardados con éxito");
                }
                catch (Exception e)
                {
                    // Revertir la transacción en caso de error
                    dbContextTransaction.Rollback();
                    Console.WriteLine("Error: " + e.ToString());
                }
            }
        }
    }

    public static void consultarDirecciones()
    {
        Console.WriteLine("Consultar direcciones");
        //Console.WriteLine("Metodo consultar estudiante por Id");
        SchoolContext context = new SchoolContext();
        List<StudentAddress> listaDirecciones;
        listaDirecciones = context.StudentAddresses
            .Include(x=> x.Student)
            .ToList();
        
        foreach (var item in listaDirecciones)
        {
            Console.WriteLine("Codigo:"+ item.Student.StudentId +
                " Nombre: " + item.Student.Name + 
                " Direccion:" + item.Address1);
        }
        

    }

    public static void consultarDireccion()
    {
        Console.WriteLine("Consultar direccion por Id");
        //Console.WriteLine("Metodo consultar estudiante por Id");
        SchoolContext context = new SchoolContext();
        StudentAddress address = new StudentAddress();
        address = context.StudentAddresses
            .Where(x =>x.StudentID==16)
            .Include(x => x.Student)
            .ToList()[0];

        
        Console.WriteLine("Codigo: " + address.Student.StudentId +
                " Nombre: " + address.Student.Name +
                " Direccion: " + address.Address1);


    }

    public static void consultarDireccion2()
    {
        Console.WriteLine("Consultar direccion por Id, metodo 2");
        
        SchoolContext context = new SchoolContext();
        StudentAddress address = new StudentAddress();
        address = context.StudentAddresses
            .Single(x => x.StudentID == 16);
           

        context.Entry(address)
            .Reference(x => x.Student)
            .Load();

        /*
        context.Entry(address)
          .Collection(x => x.Student)
          .Load();
        */

        Console.WriteLine("Codigo: " + address.Student.StudentId +
                " Nombre: " + address.Student.Name +
                " Direccion: " + address.Address1);


    }
    //agregar estudiante
    public static void agregarEstudiante()
    {
        Console.WriteLine("Metodo agregar estudiante");
        
        SchoolContext context = new SchoolContext();
        Student std = new Student();
        std.Name = "Pedro";
        context.Students.Add(std);
        context.SaveChanges();
      
        Console.WriteLine("Codigo: "+ std.StudentId + " Nombre: "+ std.Name);

    }

    public static void consultarEstudiantes()
    {
        Console.WriteLine("Metodo consultar estudiantes");
        SchoolContext context = new SchoolContext();
        List<Student> listEstudiantes= context.Students.ToList() ;

        foreach (var item in listEstudiantes)
        {
            Console.WriteLine("Codigo: " + item.StudentId + " Nombre: " + item.Name);
        }
        
    }

    public static void consultarEstudiante()
    {
        Console.WriteLine("Metodo consultar estudiante por Id");
        SchoolContext context = new SchoolContext();
        Student std = new Student();
        std = context.Students.Find(11);

       Console.WriteLine("Codigo: " + std.StudentId + " Nombre: " + std.Name);
      
    }

    public static void modificarEstudiante()
    {
        Console.WriteLine("Metodo modificar estudiante");
        SchoolContext context = new SchoolContext();
        Student std = new Student();
        std = context.Students.Find(1);

        std.Name = "Anahi";
        context.SaveChanges();
        Console.WriteLine("Codigo: " + std.StudentId + " Nombre: " + std.Name);

    }

    public static void eliminarEstudiante()
    {
        Console.WriteLine("Metodo eliminar estudiante");
        SchoolContext context = new SchoolContext();
        Student std = new Student();
        std = context.Students.Find(5);
        context.Remove(std);
        context.SaveChanges();
        Console.WriteLine("Codigo: " + std.StudentId + " Nombre: " + std.Name);

    }
    public static void consultarEstudiantesFunciones()
    {
        Console.WriteLine("Metodo consultar estudiantes con el uso de funciones");
        SchoolContext context = new SchoolContext();
        List<Student> listEstudiantes;

        Console.WriteLine("Cantidad de registros: " + context.Students.Count());
        Student std = context.Students.First();

        Console.WriteLine("Primer elemento de la tabla:" +  std.StudentId +"-" +std.Name);

        //listEstudiantes = context.Students.Where(s => s.StudentId > 2 && s.Name == "Anita").ToList();

        //listEstudiantes = context.Students.Where(s => s.Name == "Patty" || s.Name == "Anita").ToList();

        listEstudiantes = context.Students.Where(s => s.Name.StartsWith("A")).ToList();

        foreach (var item in listEstudiantes)
        {
            Console.WriteLine("Codigo: " + item.StudentId + " Nombre: " + item.Name);
        }


        /*
        var query = context.Students.GroupBy( s => s.Name) 
        .Select(g => new
        {
            Nombre = g.Key,
            Cantidad = g.Count()
        }). ToList();

        foreach (var result in query)
        {
            Console.WriteLine($"Nombre: {result.Nombre}, Cantidad: {result.Cantidad}");
        }
        */





    }
}