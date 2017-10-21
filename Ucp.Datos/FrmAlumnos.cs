using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ucp.LogicaNegocio;

namespace Ucp.Datos
{
    public partial class FrmAlumnos : Form
    {
        public FrmAlumnos()
        {
            InitializeComponent();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            alumnoInfoListBindingSource.DataSource = AlumnoInfoList.GetReadOnlyList();
            alumnoInfoListBindingSource.ResetBindings(false);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmMantAlumno(AlumnoRoot.NewEditableRoot()))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    btnCargar.PerformClick();
               
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                var seleccionado = alumnoInfoListBindingSource.Current as AlumnoInfo;
                if (seleccionado == null) return;

                using (var frm = new FrmMantAlumno(AlumnoRoot.GetEditableRoot(seleccionado.Id)))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                        btnCargar.PerformClick();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var seleccionado = alumnoInfoListBindingSource.Current as AlumnoInfo;
                if (seleccionado == null) return;

                if (MessageBox.Show("¿Está seguro que desea eliminar el registro?", "Confirme", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    AlumnoRoot.DeleteEditableRoot(seleccionado.Id);
                    btnCargar.PerformClick();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
