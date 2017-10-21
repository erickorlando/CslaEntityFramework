using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Csla;
using Csla.Data.EF6;
using Csla.Rules.CommonRules;
using Ucp.Data;
using Ucp.Entidades;

namespace Ucp.LogicaNegocio
{
    [Serializable]
    public class AlumnoRoot : BusinessBase<AlumnoRoot>
    {

        public static readonly PropertyInfo<int> IdProperty = 
            RegisterProperty<int>(c => c.Id);
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> NombresProperty = RegisterProperty<string>(c => c.Nombres);

        [Required]
        public string Nombres
        {
            get { return GetProperty(NombresProperty); }
            set { SetProperty(NombresProperty, value); }
        }

        public static readonly PropertyInfo<string> ApellidosProperty = RegisterProperty<string>(c => c.Apellidos);

        [Required]
        public string Apellidos
        {
            get { return GetProperty(ApellidosProperty); }
            set { SetProperty(ApellidosProperty, value); }
        }

        public static readonly PropertyInfo<int> EdadProperty = RegisterProperty<int>(c => c.Edad);
        public int Edad
        {
            get { return GetProperty(EdadProperty); }
            set { SetProperty(EdadProperty, value); }
        }


        protected override void AddBusinessRules()
        {
            BusinessRules.AddRule(new MinValue<int>(EdadProperty, 18));
        }

        public static AlumnoRoot NewEditableRoot()
        {
            return DataPortal.Create<AlumnoRoot>();
        }

        public static AlumnoRoot GetEditableRoot(int id)
        {
            return DataPortal.Fetch<AlumnoRoot>(id);
        }

        public static void DeleteEditableRoot(int id)
        {
            DataPortal.Delete<AlumnoRoot>(id);
        }


        protected override void DataPortal_Create()
        {
            Edad = 18;
            BusinessRules.CheckRules();
        }

        protected void DataPortal_Fetch(int id)
        {
            using (BypassPropertyChecks)
            {
                using (var contexto = DbContextManager<Colegio>.GetManager())
                {
                    var registro = contexto.DbContext.Alumnos.Find(id);
                    if (registro == null) 
                        throw new InvalidOperationException("El registro no existe");

                    Id = registro.Id;
                    Nombres = registro.Nombres;
                    Apellidos = registro.Apellidos;
                    Edad = registro.Edad;
                }
            }
        }


        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (BypassPropertyChecks)
            {
                using (var ctx = DbContextManager<Colegio>.GetManager())
                {
                    var alumno = new Alumno();
                    alumno.Nombres = Nombres;
                    alumno.Apellidos = Apellidos;
                    alumno.Edad = Edad;

                    ctx.DbContext.Set<Alumno>().Add(alumno);
                    ctx.DbContext.SaveChanges();
                }
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var ctx = DbContextManager<Colegio>.GetManager())
            {
                var entidad = ctx.DbContext.Set<Alumno>().Find(Id);
                if (entidad == null)
                    throw new InvalidOperationException("No se encuentra el registro");

                entidad.Nombres = Nombres;
                entidad.Apellidos = Apellidos;
                entidad.Edad = Edad;

                ctx.DbContext.Set<Alumno>().Attach(entidad);
                ctx.DbContext.Entry(entidad).State = EntityState.Modified;

                ctx.DbContext.SaveChanges();
            }
        }


        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(Id);
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(int id)
        {
            using (var ctx = DbContextManager<Colegio>.GetManager())
            {
                var entidad = ctx.DbContext.Set<Alumno>().Find(id);
                if (entidad == null)
                    throw new InvalidOperationException("No se encuentra el registro");

                ctx.DbContext.Set<Alumno>().Attach(entidad);
                ctx.DbContext.Entry(entidad).State = EntityState.Deleted;

                ctx.DbContext.SaveChanges();
            }
        }
    }
}