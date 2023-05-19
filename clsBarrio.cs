using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;

namespace pryIEFIBonacci
{
    internal class clsBarrio
    {
        //Objeto que se usa para conectarse a la base de datos
        private OleDbConnection conexion = new OleDbConnection();

        //Objeto que se usa para configurar el comando u orden que se la pasa a la base de datos
        private OleDbCommand comando = new OleDbCommand();

        //Objeto que nos ayuda a adaptar los datos de ACCESS a un formato entendible por .NET
        private OleDbDataAdapter adaptador = new OleDbDataAdapter();

        private String CadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BD_Clientes.mdb";
        private String Tabla = "Barrio";

        private Int32 idbar;
        private Int32 idsoc;
        private String nom;
        private String dir;
        private Int32 idact;
        private Decimal sal;

        public String Nombre
        {
            get { return nom; }
            set { nom = value; }
        }

        public Int32 IdBarrio
        {
            get { return idbar; }
            set { idbar = value; }
        }

        public Int32 IdSocio
        {
            get { return idsoc; }
            set { idsoc = value; }
        }

        public String Direccion
        {
            get { return dir; }
            set { dir = value; }
        }

        public Int32 IdActividad
        {
            get { return idact; }
            set { idact = value; }
        }

        public Decimal Saldo
        {
            get { return sal; }
            set { sal = value; }
        }



        public void Listar(ComboBox Combo)
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                adaptador = new OleDbDataAdapter(comando);
                DataSet DS = new DataSet();
                adaptador.Fill(DS, Tabla);

                Combo.DataSource = DS.Tables[Tabla];
                Combo.DisplayMember = "Nombre";
                Combo.ValueMember = "idBarrio";

                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        public void LlenarBarrio(TextBox Barrio, Int32 IdSocio)
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;
                
                adaptador = new OleDbDataAdapter(comando);
                DataSet DS = new DataSet();
                adaptador.Fill(DS, Tabla);

                foreach (DataRow fila in DS.Tables[Tabla].Rows)
                {
                    if (Convert.ToInt32(fila["idBarrio"]) == IdSocio)
                    {
                        Barrio.Text = fila["Nombre"].ToString();
                    }
                }

                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        public String Buscar(Int32 IdBarrio)
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                OleDbDataReader DR = comando.ExecuteReader();

                idsoc = 0;
                String nombre = "";

                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        if (DR.GetInt32(0) == IdBarrio)
                        {
                            nombre = (DR.GetString(1));
                        }
                    }
                }
                conexion.Close();
                return nombre;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return "";
            }
        }
    }
}
