using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryIEFIBonacci
{
    public partial class frmNuevoSocio : Form
    {
        public frmNuevoSocio()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }

            base.WndProc(ref message);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            clsSocios socio = new clsSocios();
            socio.Nombre = txtNombre.Text;
            socio.IdBarrio = Convert.ToInt32(cboBarrio.SelectedValue);
            socio.IdActividad = Convert.ToInt32(cboActividad.SelectedValue);
            socio.Saldo = Convert.ToDecimal(txtSaldo.Text);
            socio.Direccion = txtDireccion.Text;
            socio.IdSocio = Convert.ToInt32(txtDNI.Text);
            socio.Agregar();
            MessageBox.Show("Socio agregado correctamente");
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtSaldo.Text = "";
            txtDNI.Text = "";
            cboActividad.SelectedValue = 0;
            cboBarrio.SelectedValue = 0;
        }

        private void frmNuevoSocio_Load(object sender, EventArgs e)
        {
            clsBarrio ciudad = new clsBarrio();
            ciudad.Listar(cboBarrio);
            clsActividad actividad = new clsActividad();
            actividad.Listar(cboActividad);

            MinimizeBox = false;
            MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void BtnOn()
        {
            if (txtNombre.Text != "" && txtDireccion.Text != "" && txtSaldo.Text != "" && txtDNI.Text != "" && cboActividad.Text != "" && cboBarrio.Text != "")
            {
                btnAgregar.Enabled = true;
            }
            else
            {
                btnAgregar.Enabled = false;
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            BtnOn();
        }

        private void txtDNI_TextChanged(object sender, EventArgs e)
        {
            BtnOn();
        }

        private void cboBarrio_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnOn();
        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            BtnOn();
        }

        private void cboActividad_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnOn();
        }

        private void txtSaldo_TextChanged(object sender, EventArgs e)
        {
            BtnOn();
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtSaldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
