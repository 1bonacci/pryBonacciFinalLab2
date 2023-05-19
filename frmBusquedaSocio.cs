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
    public partial class frmBusquedaSocio : Form
    {
        public frmBusquedaSocio()
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

        private void Limpiar()
        {
            txtSaldo.Text = "";
            txtDireccion.Text = "";
            txtDNI.Text = "";
            txtNombre.Text = "";
            txtSaldo.ReadOnly = true;
            btnGuardar.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            txtDireccion.ReadOnly = true;
            txtNombre.ReadOnly = true;
            cboActividad.SelectedValue = 0;
            cboBarrio.SelectedValue = 0;
            cboBarrio.Enabled = false;
            cboActividad.Enabled = false;
        }
        
        private void frmBusquedaSocio_Load(object sender, EventArgs e)
        {
            btnBuscar.Enabled = false;
            txtDireccion.ReadOnly = true;
            txtSaldo.ReadOnly = true;
            txtNombre.ReadOnly = true;
            btnGuardar.Enabled = false;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            cboActividad.SelectedValue = 0;
            cboBarrio.SelectedValue = 0;
            cboActividad.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBarrio.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBarrio.Enabled = false;
            cboActividad.Enabled = false;

            MinimizeBox = false;
            MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            clsBarrio x = new clsBarrio();
            x.Listar(cboBarrio);
            clsActividad s = new clsActividad();
            s.Listar(cboActividad);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Estas seguro que deseas modificar este socio?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Int32 id = Convert.ToInt32(txtDNI.Text);
                clsSocios socio = new clsSocios();
                socio.Saldo = Convert.ToDecimal(txtSaldo.Text);
                socio.Direccion = Convert.ToString(txtDireccion.Text); 
                socio.Nombre = Convert.ToString(txtNombre.Text);
                socio.IdBarrio = Convert.ToInt32(cboBarrio.SelectedValue);
                socio.IdActividad = Convert.ToInt32(cboActividad.SelectedValue);

                socio.Modificar(id);
                MessageBox.Show("Socio modificado correctamente");
                Limpiar();
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            txtSaldo.ReadOnly = false;
            txtDireccion.ReadOnly = false;
            txtNombre.ReadOnly = false;
            btnGuardar.Enabled = true;
            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;

            cboBarrio.Enabled = true;
            cboActividad.Enabled = true;

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Int32 id = Convert.ToInt32(txtDNI.Text);
            clsSocios socio = new clsSocios();
            socio.Buscar(id);
            txtNombre.Text = socio.Nombre;
            txtDireccion.Text = socio.Direccion;
            txtSaldo.Text = socio.Saldo.ToString();
            cboActividad.SelectedValue = socio.IdActividad;
            cboBarrio.SelectedValue = socio.IdBarrio;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Estas seguro que deseas eliminar este socio?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                Int32 id = Convert.ToInt32(txtDNI.Text);
                clsSocios socio = new clsSocios();
                socio.Eliminar(id);
                MessageBox.Show("Socio eliminado correctamente.");
                Limpiar();
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (txtDNI.Text != "")
            {
                btnBuscar.Enabled = true;
            }
            else
            {
                btnBuscar.Enabled = false;
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            TxtOn();
        }

        private void TxtOn()
        {
            if (txtNombre.Text != "" && txtDireccion.Text != "")
            {
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
            {
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
            }
        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {
            TxtOn();
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtSaldo_TextChanged(object sender, EventArgs e)
        {
            TxtOn();
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
