using System;
using System.Windows.Forms;
using Csla;
using Csla.Rules;
using Ucp.LogicaNegocio;

namespace Ucp.Datos
{
    public partial class FrmMantAlumno : Form
    {
        private AlumnoRoot _alumnoRoot;
        public FrmMantAlumno(AlumnoRoot alumnoRoot)
        {
            InitializeComponent();
            _alumnoRoot = alumnoRoot;
            alumnoRootBindingSource.DataSource = _alumnoRoot;
            alumnoRootBindingSource.ResetBindings(false);
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                alumnoRootBindingSource.EndEdit();

                _alumnoRoot = _alumnoRoot.Save();

                DialogResult = DialogResult.OK;
            }
            catch (ValidationException)
            {
                MessageBox.Show(_alumnoRoot.GetBrokenRules().ToString());
            }
            catch (DataPortalException ex)
            {
                MessageBox.Show(ex.BusinessException.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
