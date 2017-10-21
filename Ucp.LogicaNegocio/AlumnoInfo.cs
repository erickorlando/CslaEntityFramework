using System;
using Csla;
using Ucp.Entidades;

namespace Ucp.LogicaNegocio
{
    [Serializable]
    public class AlumnoInfo : ReadOnlyBase<AlumnoInfo>
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }


        public static AlumnoInfo GetReadOnlyChild(Alumno alumno)
        {
            return DataPortal.FetchChild<AlumnoInfo>(alumno);
        }

        protected void Child_Fetch(Alumno alumno)
        {
            Id = alumno.Id;
            Nombres = alumno.Nombres;
            Apellidos = alumno.Apellidos;
            Edad = alumno.Edad;
        }
    }
}