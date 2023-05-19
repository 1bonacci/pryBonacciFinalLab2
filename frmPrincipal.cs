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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
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

        private void agregarNuevosClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNuevoSocio frmNuevoCliente = new frmNuevoSocio();
            frmNuevoCliente.ShowDialog();
        }

        private void buscarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBusquedaSocio frmBusquedaSocio = new frmBusquedaSocio();
            frmBusquedaSocio.ShowDialog();
        }

        private void listadoDeClientesDeudoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListadoSocios frmListadoDeudores = new frmListadoSocios();
            frmListadoDeudores.ShowDialog();
        }

        private void listadoDeClientesDeUnaCiudadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListadoSociosBarrio frmListadoSociosBarrio = new frmListadoSociosBarrio();
            frmListadoSociosBarrio.ShowDialog();
        }

        private void listadoDeTodosLosClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListar frmListar = new frmListar();
            frmListar.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void consultaDeUnClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaSocio frmConsultaSocio = new frmConsultaSocio();
            frmConsultaSocio.ShowDialog();
        }

        private void listadoDeSociosPorActividadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListadoSociosActividad x = new frmListadoSociosActividad();
            x.ShowDialog();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            StartTimer();
            
            lblFecha.BackColor = Color.Transparent;
            lblTime.BackColor = Color.Transparent;
            lblGym.BackColor = Color.Transparent;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            MaximizeBox = false;
        }

        System.Windows.Forms.Timer t = null;
        private void StartTimer()
        {
            t = new System.Windows.Forms.Timer();
            t.Interval = 1000;
            t.Tick += new EventHandler(t_Tick);
            t.Enabled = true;
        }

        void t_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void infoDelDesarrolladorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDatosDesarrollador frmDatosDesarrollador = new frmDatosDesarrollador();
            frmDatosDesarrollador.ShowDialog();
        }
    }
}
