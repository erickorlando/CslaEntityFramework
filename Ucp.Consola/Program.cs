using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucp.Data;
using Ucp.Entidades;

namespace Ucp.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new Colegio())
            {
                var alumno = new Alumno();
                alumno.Nombres = "Erick";
                alumno.Apellidos = "Orlando";
                alumno.Edad = 32;

                contexto.Alumnos.Add(alumno);
                contexto.SaveChanges();
            }

            Console.WriteLine("Registro Insertado");
            Console.ReadLine();
        }
    }
}
