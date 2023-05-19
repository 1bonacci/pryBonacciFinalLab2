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
    public partial class frmListar : Form
    {
        public frmListar()
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

        private void btnListar_Click(object sender, EventArgs e)
        {
            clsSocios x = new clsSocios();
            x.Listar(dgvListado);
            lblTot.Text = x.Total.ToString("0.00");
            lblProm.Text = x.PromedioSaldo.ToString("0.00");
            lblMay.Text = x.Mayor.ToString();
            lblMen.Text = x.Menor.ToString();

            btnExportar.Enabled = true;
            btnImprimir.Enabled = true;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            clsSocios Socios = new clsSocios();
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "Archivo CSV (*.csv)|*.csv";
            guardar.FileName = "Lista de Socios";
            if (guardar.ShowDialog() == DialogResult.OK)
            {
                Socios.Reporte(guardar);
                MessageBox.Show("Reporte guardado con exito.", "Guardar", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmListar_Load(object sender, EventArgs e)
        {
            btnExportar.Enabled = false;
            btnImprimir.Enabled = false;

            MinimizeBox = false;
            MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            prtVentana.ShowDialog();
            prtDocumento.PrinterSettings = prtVentana.PrinterSettings;
            prtDocumento.Print();
            MessageBox.Show("Reporte impreso exitosamente.", "Reporte de Socios", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void prtDocumento_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            clsSocios x = new clsSocios();
            x.Imprimir(e);
        }
    }
}
