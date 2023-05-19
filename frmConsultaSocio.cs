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
    public partial class frmConsultaSocio : Form
    {
        public frmConsultaSocio()
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            clsSocios socio = new clsSocios();
            socio.Consultar(Convert.ToInt32(cboNombre.SelectedValue));
            
            txtDNI.Text = socio.IdSocio.ToString();
            txtDireccion.Text = socio.Direccion;
            txtSaldo.Text = socio.Saldo.ToString();
            txtActividad.Text = socio.IdActividad.ToString();

            clsBarrio barrio = new clsBarrio();
            barrio.LlenarBarrio(txtBarrio, socio.IdBarrio);

            clsActividad actividad = new clsActividad();
            actividad.LlenarActividad(txtActividad, socio.IdActividad);

        }
        
        private void frmConsultaSocio_Load(object sender, EventArgs e)
        {
            clsSocios x = new clsSocios();
            x.ListarNombre(cboNombre);

            cboNombre.SelectedValue = -1;
            btnBuscar.Enabled = false;

            MinimizeBox = false;
            MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void cboNombre_TextChanged(object sender, EventArgs e)
        {
            if (cboNombre.Text != "")
            {
                btnBuscar.Enabled = true;
            }
            else
            {
                btnBuscar.Enabled = false;
            }
        }
    }
}
