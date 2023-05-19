using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Printing;


namespace pryIEFIBonacci
{
    internal class clsSocios
    {
        #region "Conexion Inicio"

        //Objeto que se usa para conectarse a la base de datos
        private OleDbConnection conexion = new OleDbConnection();

        //Objeto que se usa para configurar el comando u orden que se la pasa a la base de datos
        private OleDbCommand comando = new OleDbCommand();

        //Objeto que nos ayuda a adaptar los datos de ACCESS a un formato entendible por .NET
        private OleDbDataAdapter adaptador = new OleDbDataAdapter();

        private String CadenaConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BD_Clientes.mdb";
        private String Tabla = "Socio";

        #endregion

        #region "Variables"

        private Int32 idsoc;
        private String nom;
        private String dir;
        private Int32 idbar;
        private Int32 idact;
        private Decimal sal;

        private Int32 cant;
        private Decimal total;
        private Int32 may;
        private Int32 men;

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

        public Int32 Cantidad
        {
            get { return cant; }
            set { cant = value; }
        }

        public Decimal Total
        {
            get { return total; }
            set { total = value; }
        }

        public Int32 Mayor
        {
            get { return may; }
            set { may = value; }
        }

        public Int32 Menor
        {
            get { return men; }
            set { men = value; }
        }

        public Decimal PromedioSaldo
        {
            get
            {
                Decimal promedio = 0;
                if (cant != 0) promedio = total / cant;
                return promedio;
            }
        }

        #endregion

        #region "Codigo"

        public void Agregar()
        {
            try
            {
                String sql = "";
                sql = "INSERT INTO Socio (IdSocio, Nombre, Direccion, IdBarrio, IdActividad, Saldo)";
                sql = sql + " VALUES (" + idsoc + ", '" + nom + "', '" + dir + "', " + idbar + ", " + idact + ", " + sal + ")";

                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql;
                comando.ExecuteNonQuery();

                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        public void Eliminar(Int32 IdSocio)
        {
            try
            {
                String sql = "DELETE * FROM Socio WHERE IdSocio = " + IdSocio.ToString();
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql;
                comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        public void Modificar(Int32 IdSocio)
        {
            try
            {
                String sql = "";
                sql = "UPDATE Socio SET Nombre = '" + nom + "', Direccion = '" + dir + "', Saldo = " + sal.ToString() + ", idBarrio = " + idbar + ", idActividad = " + idact + " WHERE IdSocio = " + IdSocio.ToString();

                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = sql;
                comando.ExecuteNonQuery();

                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        public void Buscar(Int32 IdSocio)
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
                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        if (DR.GetInt32(0) == IdSocio)
                        {
                            idsoc = DR.GetInt32(0);
                            nom = DR.GetString(1);
                            dir = DR.GetString(2);
                            sal = DR.GetDecimal(5);
                            idbar = DR.GetInt32(3);
                            idact = DR.GetInt32(4);
                        }
                    }
                }
                conexion.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void Consultar(Int32 Id)
        {
            try
            {
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;
                OleDbDataReader DR = comando.ExecuteReader();
                
                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        if (DR.GetInt32(0) == Id)
                        {
                            IdSocio = DR.GetInt32(0);
                            Direccion = DR.GetString(2);
                            IdBarrio = DR.GetInt32(3);
                            IdActividad = DR.GetInt32(4);
                            Saldo = DR.GetDecimal(5);
                        }
                    }
                }
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        public void ListarNombre(ComboBox Combo)
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
                Combo.ValueMember = "IdSocio";

                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }
    
        #endregion

        #region "Listar Reporte Imprimir"

        public void Listar(DataGridView Grilla)
        {
            try
            {
                men = 999999999;
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                OleDbDataReader DR = comando.ExecuteReader();

                Grilla.Rows.Clear();

                clsBarrio bar = new clsBarrio();
                clsActividad act = new clsActividad();
                String nombar = "";
                String nomact = "";

                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        nombar = bar.Buscar(DR.GetInt32(3));
                        nomact = act.Buscar(DR.GetInt32(4));
                        sal = DR.GetDecimal(5);
                        Grilla.Rows.Add(DR.GetInt32(0), DR.GetString(1), DR.GetDecimal(5), nombar, nomact, DR.GetString(2));

                        cant++;
                        total = total + sal;

                        if (may <= sal)
                        {
                            may = Convert.ToInt32(sal);
                        }
                        if (men >= sal)
                        {
                            men = Convert.ToInt32(sal);
                        }
                    }
                }
                

                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        public void Reporte(SaveFileDialog guardar)
        {
            try
            {
                men = 999999999;

                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                OleDbDataReader DR = comando.ExecuteReader();
                StreamWriter Ad = new StreamWriter(guardar.FileName, false, Encoding.UTF8);

                Ad.WriteLine("Listado de Socios\n");
                Ad.WriteLine("DNI;Nombre;Direccion;Barrio;Actividad;Saldo");

                clsBarrio bar = new clsBarrio();
                clsActividad act = new clsActividad();
                String nombar = "";
                String nomact = "";

                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        if (DR.GetDecimal(5) > 0)
                        {
                            nombar = bar.Buscar(DR.GetInt32(3));
                            nomact = act.Buscar(DR.GetInt32(4));

                            Ad.Write(DR.GetInt32(0));
                            Ad.Write(";");
                            Ad.Write(DR.GetString(1)); 
                            Ad.Write(";");
                            Ad.Write(DR.GetString(2));
                            Ad.Write(";");
                            Ad.Write(nombar); 
                            Ad.Write(";");
                            Ad.Write(nomact); 
                            Ad.Write(";");
                            Ad.WriteLine(DR.GetDecimal(5)); 
                            
                            cant++;
                            sal = DR.GetDecimal(5);
                            total = total + sal;

                            if (may <= sal)
                            {
                                may = Convert.ToInt32(sal);
                            }
                            if (men >= sal)
                            {
                                men = Convert.ToInt32(sal);
                            }
                        }
                    }
                }
                Ad.WriteLine();
                Ad.Write("Cantidad:;;");
                Ad.WriteLine(cant);
                Ad.Write("Total Saldo:;;");
                Ad.WriteLine(total);
                Ad.Write("Promedio Deuda:;;");
                Ad.WriteLine(total / cant);
                Ad.Write("Mayor saldo:;;");
                Ad.WriteLine(may);
                Ad.Write("Menor saldo:;;");
                Ad.WriteLine(men);

                conexion.Close();
                Ad.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        public void Imprimir(PrintPageEventArgs reporte) // Imprimir el reporte de Socios

        {
            try
            {
                Font LetraDelTitulo = new Font("Arial", 20);
                Font LetraDelTitulo2 = new Font("Arial", 11);
                Font LetraTexto = new Font("Arial", 8);
                Int32 l = 205;

                reporte.Graphics.DrawString("Listado de Socios", LetraDelTitulo, Brushes.DarkGray, 250, 100);
                reporte.Graphics.DrawString("DNI", LetraDelTitulo2, Brushes.Red, 100, 180);
                reporte.Graphics.DrawString("Nombre", LetraDelTitulo2, Brushes.Red, 200, 180);
                reporte.Graphics.DrawString("Saldo", LetraDelTitulo2, Brushes.Red, 300, 180);
                reporte.Graphics.DrawString("Barrio", LetraDelTitulo2, Brushes.Red, 400, 180);
                reporte.Graphics.DrawString("Actividad", LetraDelTitulo2, Brushes.Red, 500, 180);
                reporte.Graphics.DrawString("Direccion", LetraDelTitulo2, Brushes.Red, 600, 180);

                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                adaptador = new OleDbDataAdapter(comando);
                DataSet DS = new DataSet();
                adaptador.Fill(DS, Tabla);

                if (DS.Tables[Tabla].Rows.Count > 0)
                {
                    foreach (DataRow fila in DS.Tables[Tabla].Rows)
                    {
                        reporte.Graphics.DrawString(fila["IdSocio"].ToString(), LetraTexto, Brushes.Black, 100, l);
                        reporte.Graphics.DrawString(fila["Nombre"].ToString(), LetraTexto, Brushes.Black, 200, l);
                        reporte.Graphics.DrawString(fila["Saldo"].ToString(), LetraTexto, Brushes.Black, 300, l);
                        reporte.Graphics.DrawString(fila["idBarrio"].ToString(), LetraTexto, Brushes.Black, 400, l);
                        reporte.Graphics.DrawString(fila["idActividad"].ToString(), LetraTexto, Brushes.Black, 500, l);
                        reporte.Graphics.DrawString(fila["Direccion"].ToString(), LetraTexto, Brushes.Black, 600, l);
                        cant++;
                        sal = Convert.ToDecimal(fila["Saldo"]);
                        total = total + sal;
                        l = l + 15;
                    }
                }
                reporte.Graphics.DrawString("Cantidad de socios: " + cant, LetraDelTitulo2, Brushes.Black, 100, 750);
                reporte.Graphics.DrawString("Saldo total: " + total, LetraDelTitulo2, Brushes.Black, 100, 800);
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        public void ListarSocios(DataGridView Grilla)
        {
            try
            {
                men = 999999999;
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                OleDbDataReader DR = comando.ExecuteReader();

                Grilla.Rows.Clear();

                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        if (DR.GetDecimal(5) > 0)
                        {
                            sal = DR.GetDecimal(5);
                            Grilla.Rows.Add(DR.GetInt32(0), DR.GetString(1), DR.GetDecimal(5));

                            cant++;
                            total = total + DR.GetDecimal(5);

                            if (may <= sal)
                            {
                                may = Convert.ToInt32(sal);
                            }
                            if (men >= sal)
                            {
                                men = Convert.ToInt32(sal);
                            }
                        }
                    }
                }

                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        public void ReporteSocios(SaveFileDialog guardar)
        {
            try
            {
                men = 999999999;
                
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                OleDbDataReader DR = comando.ExecuteReader();
                StreamWriter Archivo = new StreamWriter(guardar.FileName, false, Encoding.UTF8);
                Archivo.WriteLine("Reporte de los saldos de los Socios\n");
                Archivo.WriteLine();
                Archivo.WriteLine("IdSocio;Nombre;Saldo");

                if (DR.HasRows) // si hay filas para leer entonces
                {
                    while (DR.Read())
                    {
                        if (DR.GetDecimal(5) > 0)
                        {
                            sal = (DR.GetDecimal(5));
                            Archivo.Write(DR.GetInt32(0));
                            Archivo.Write(";;");
                            Archivo.Write(DR.GetString(1));
                            Archivo.Write(";;");
                            Archivo.WriteLine(DR.GetDecimal(5));
                            cant++;
                            total = total + DR.GetDecimal(5);

                            if (may <= sal)
                            {
                                may = Convert.ToInt32(sal);
                            }
                            if (men >= sal)
                            {
                                men = Convert.ToInt32(sal);
                            }
                        }
                    }
                }
                Archivo.WriteLine();
                Archivo.Write("Cantidad:;;");
                Archivo.WriteLine(cant);
                Archivo.Write("Total Saldo:;;");
                Archivo.WriteLine(total);
                Archivo.Write("Promedio Deuda:;;");
                Archivo.WriteLine(total / cant);
                Archivo.Write("Mayor saldo:;;");
                Archivo.WriteLine(may);
                Archivo.Write("Menor saldo:;;");
                Archivo.WriteLine(men);

                Archivo.Close();
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        public void ImprimirSocios(PrintPageEventArgs reporte) // Imprimir el reporte de Socios

        {
            try
            {
                Font LetraDelTitulo = new Font("Arial", 20);
                Font LetraDelTitulo2 = new Font("Arial", 11);
                Font LetraTexto = new Font("Arial", 8);
                Int32 l = 205;

                reporte.Graphics.DrawString("Listado de Saldos de los Socios", LetraDelTitulo, Brushes.DarkGray, 220, 100);
                reporte.Graphics.DrawString("DNI", LetraDelTitulo2, Brushes.Red, 100, 180);
                reporte.Graphics.DrawString("Nombre", LetraDelTitulo2, Brushes.Red, 200, 180);
                reporte.Graphics.DrawString("Saldo", LetraDelTitulo2, Brushes.Red, 310, 180);

                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                adaptador = new OleDbDataAdapter(comando);
                DataSet DS = new DataSet();
                adaptador.Fill(DS, Tabla);

                if (DS.Tables[Tabla].Rows.Count > 0)
                {
                    foreach (DataRow fila in DS.Tables[Tabla].Rows)
                    {
                        reporte.Graphics.DrawString(fila["IdSocio"].ToString(), LetraTexto, Brushes.Black, 100, l);
                        reporte.Graphics.DrawString(fila["Nombre"].ToString(), LetraTexto, Brushes.Black, 200, l);
                        reporte.Graphics.DrawString(fila["Saldo"].ToString(), LetraTexto, Brushes.Black, 310, l);
                        cant++;
                        sal = Convert.ToDecimal(fila["Saldo"]);
                        total = total + sal;
                        l = l + 15;
                    }
                }
                reporte.Graphics.DrawString("Cantidad de socios: " + cant, LetraDelTitulo2, Brushes.Black, 100, 750);
                reporte.Graphics.DrawString("Saldo total: " + total, LetraDelTitulo2, Brushes.Black, 100, 800);
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        public void ListarSociosDeUnBarrio(DataGridView Grilla, Int32 IdBarrio)
        {
            try
            {
                men = 999999999;
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                OleDbDataReader DR = comando.ExecuteReader();
                Grilla.Rows.Clear();

                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        if (DR.GetInt32(3) == IdBarrio)
                        {
                            sal = DR.GetDecimal(5);
                            Grilla.Rows.Add(DR.GetInt32(0), DR.GetString(1), DR.GetDecimal(5));

                            cant++;
                            total = total + DR.GetDecimal(5);

                            if (may <= sal)
                            {
                                may = Convert.ToInt32(sal);
                            }
                            if (men >= sal)
                            {
                                men = Convert.ToInt32(sal);
                            }
                        }
                    }
                }

                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        public void ReporteBarrio(SaveFileDialog guardar)
        {
            try
            {
                men = 999999999;

                conexion.ConnectionString = CadenaConexion;
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                adaptador = new OleDbDataAdapter(comando);
                DataSet Ds = new DataSet();
                adaptador.Fill(Ds, Tabla);

                StreamWriter archivo = new StreamWriter(guardar.FileName, false, Encoding.UTF8);
                archivo.WriteLine("Listado de socios por barrio\n");
                archivo.WriteLine();
                archivo.WriteLine("IdSocio;Nombre;Saldo");
                
                if (Ds.Tables[Tabla].Rows.Count > 0)
                {
                    foreach (DataRow fila in Ds.Tables[Tabla].Rows)
                    {
                        if (Convert.ToInt32(fila["idBarrio"]) == IdBarrio)
                        {
                            archivo.Write(fila["idSocio"]);
                            archivo.Write(";;");
                            archivo.Write(fila["Nombre"]);
                            archivo.Write(";;");
                            archivo.WriteLine(fila["Saldo"]);

                            cant++;
                            total = total + (decimal)fila["Saldo"];
                            sal = Convert.ToDecimal(fila["Saldo"]);

                            if (may <= sal)
                            {
                                may = Convert.ToInt32(sal);
                            }
                            if (men >= sal)
                            {
                                men = Convert.ToInt32(sal);
                            }
                        }
                    }
                }

                archivo.WriteLine();
                archivo.Write("Cantidad:;;");
                archivo.WriteLine(cant);
                archivo.Write("Total de saldos:;;");
                archivo.WriteLine(total);
                archivo.Write("Promedio de saldos:;;");
                archivo.WriteLine(PromedioSaldo);
                archivo.Write("Mayor saldo:;;");
                archivo.WriteLine(may);
                archivo.Write("Menor saldo:;;");
                archivo.WriteLine(men);
                archivo.Close();

                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        public void ImprimirBarrios(PrintPageEventArgs reporte)
        {
            try
            {
                Font LetraDelTitulo = new Font("Arial", 20);
                Font LetraDelTitulo2 = new Font("Arial", 11);
                Font LetraTexto = new Font("Arial", 8);
                Int32 l = 205;

                conexion.ConnectionString = CadenaConexion;
                conexion.Open();
                
                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                adaptador = new OleDbDataAdapter(comando);
                DataSet DS = new DataSet();
                adaptador.Fill(DS, Tabla);

                reporte.Graphics.DrawString("Listado de Socios de un Barrio", LetraDelTitulo, Brushes.DarkGray, 220, 100);
                reporte.Graphics.DrawString("DNI", LetraDelTitulo2, Brushes.Red, 100, 180);
                reporte.Graphics.DrawString("Nombre", LetraDelTitulo2, Brushes.Red, 200, 180);
                reporte.Graphics.DrawString("Saldo", LetraDelTitulo2, Brushes.Red, 310, 180);

                if (DS.Tables[Tabla].Rows.Count > 0)
                {
                    foreach (DataRow fila in DS.Tables[Tabla].Rows)
                    {
                        if (Convert.ToInt32(fila["idBarrio"]) == IdBarrio)
                        {
                            reporte.Graphics.DrawString(fila["IdSocio"].ToString(), LetraTexto, Brushes.Black, 100, l);
                            reporte.Graphics.DrawString(fila["Nombre"].ToString(), LetraTexto, Brushes.Black, 200, l);
                            reporte.Graphics.DrawString(fila["Saldo"].ToString(), LetraTexto, Brushes.Black, 310, l);
                            l += 15;
                            cant++;
                            sal = Convert.ToDecimal(fila["Saldo"]);
                            total = total + sal;
                        }
                    }
                }

                reporte.Graphics.DrawString("Cantidad de socios: " + cant, LetraDelTitulo2, Brushes.Black, 100, 750);
                reporte.Graphics.DrawString("Saldo total: " + total, LetraDelTitulo2, Brushes.Black, 100, 800);
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }    
        }

        public void ListarSociosActividad(DataGridView Grilla, Int32 IdActividad)
        {
            try
            {
                men = 999999999;
                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                OleDbDataReader DR = comando.ExecuteReader();
                Grilla.Rows.Clear();

                if (DR.HasRows)
                {
                    while (DR.Read())
                    {
                        if (DR.GetInt32(4) == IdActividad)
                        {
                            sal = DR.GetDecimal(5);
                            Grilla.Rows.Add(DR.GetInt32(0), DR.GetString(1), DR.GetDecimal(5));

                            cant++;
                            total = total + DR.GetDecimal(5);

                            if (may <= sal)
                            {
                                may = Convert.ToInt32(sal);
                            }
                            if (men >= sal)
                            {
                                men = Convert.ToInt32(sal);
                            }
                        }
                    }
                }

                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        public void ReporteActividad(SaveFileDialog guardar)
        {
            try
            {
                men = 999999999;

                conexion.ConnectionString = CadenaConexion;
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                adaptador = new OleDbDataAdapter(comando);
                DataSet Ds = new DataSet();
                adaptador.Fill(Ds, Tabla);

                StreamWriter archivo = new StreamWriter(guardar.FileName, false, Encoding.UTF8);
                archivo.WriteLine("Listado de Socios segun Actividad\n");
                archivo.WriteLine();
                archivo.WriteLine("IdSocio;Nombre;Saldo");

                if (Ds.Tables[Tabla].Rows.Count > 0)
                {
                    foreach (DataRow fila in Ds.Tables[Tabla].Rows)
                    {
                        if (Convert.ToInt32(fila["idActividad"]) == IdActividad)
                        {
                            archivo.Write(fila["idSocio"]);
                            archivo.Write(";;");
                            archivo.Write(fila["Nombre"]);
                            archivo.Write(";;");
                            archivo.WriteLine(fila["Saldo"]);

                            cant++;
                            total = total + (decimal)fila["Saldo"];
                            sal = Convert.ToDecimal(fila["Saldo"]);


                            if (may <= sal)
                            {
                                may = Convert.ToInt32(sal);
                            }
                            if (men >= sal)
                            {
                                men = Convert.ToInt32(sal);
                            }
                        }
                    }
                }

                archivo.WriteLine();
                archivo.Write("Cantidad:;");
                archivo.WriteLine(cant);
                archivo.Write("Total de saldos:;");
                archivo.WriteLine(total);
                archivo.Write("Promedio de saldos:;");
                archivo.WriteLine(PromedioSaldo);
                archivo.Write("Mayor saldo:;");
                archivo.WriteLine(may);
                archivo.Write("Menor saldo:;");
                archivo.WriteLine(men);
                archivo.Close();
                conexion.Close();

                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        public void ImprimirActividad(PrintPageEventArgs reporte)
        {
            try
            {
                Font LetraDelTitulo = new Font("Arial", 20);
                Font LetraDelTitulo2 = new Font("Arial", 11);
                Font LetraTexto = new Font("Arial", 8);
                Int32 l = 205;

                conexion.ConnectionString = CadenaConexion;
                conexion.Open();

                comando.Connection = conexion;
                comando.CommandType = CommandType.TableDirect;
                comando.CommandText = Tabla;

                adaptador = new OleDbDataAdapter(comando);
                DataSet DS = new DataSet();
                adaptador.Fill(DS, Tabla);

                reporte.Graphics.DrawString("Listado de Socios segun Actividad", LetraDelTitulo, Brushes.DarkGray, 220, 100);
                reporte.Graphics.DrawString("DNI", LetraDelTitulo2, Brushes.Red, 100, 180);
                reporte.Graphics.DrawString("Nombre", LetraDelTitulo2, Brushes.Red, 200, 180);
                reporte.Graphics.DrawString("Saldo", LetraDelTitulo2, Brushes.Red, 310, 180);

                if (DS.Tables[Tabla].Rows.Count > 0)
                {
                    foreach (DataRow fila in DS.Tables[Tabla].Rows)
                    {
                        if (Convert.ToInt32(fila["idActividad"]) == IdActividad)
                        {
                            reporte.Graphics.DrawString(fila["IdSocio"].ToString(), LetraTexto, Brushes.Black, 100, l);
                            reporte.Graphics.DrawString(fila["Nombre"].ToString(), LetraTexto, Brushes.Black, 200, l);
                            reporte.Graphics.DrawString(fila["Saldo"].ToString(), LetraTexto, Brushes.Black, 310, l);
                            l += 15;
                            cant++;
                            sal = Convert.ToDecimal(fila["Saldo"]);
                            total = total + sal;
                        }
                    }
                }

                reporte.Graphics.DrawString("Cantidad de socios: " + cant, LetraDelTitulo2, Brushes.Black, 100, 750);
                reporte.Graphics.DrawString("Saldo total: " + total, LetraDelTitulo2, Brushes.Black, 100, 800);
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }
        
        #endregion
    }
}
