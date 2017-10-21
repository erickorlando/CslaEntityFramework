using System;
using System.Linq;
using Csla;
using Csla.Data.EF6;
using Ucp.Data;
using Ucp.Entidades;

namespace Ucp.LogicaNegocio
{
    [Serializable]
    public class AlumnoInfoList : ReadOnlyListBase<AlumnoInfoList, AlumnoInfo>
    {

        public static AlumnoInfoList GetReadOnlyList()
        {
            return DataPortal.Fetch<AlumnoInfoList>();
        }


        protected void DataPortal_Fetch()
        {
            IsReadOnly = false;
            using (var ctx = DbContextManager<Colegio>.GetManager())
            {
                var lista = ctx.DbContext.Set<Alumno>().ToList();

                foreach (var alumno in lista)
                {
                    Add(AlumnoInfo.GetReadOnlyChild(alumno));
                }
            }
            IsReadOnly = true;
        }

    }
}