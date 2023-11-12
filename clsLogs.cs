using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace pryDacunteIE1
{
    internal class clsLogs
    {
        OleDbConnection conexion;
        OleDbCommand comando;
        OleDbDataAdapter adaptador;
        DataSet objDataSet = new DataSet();

        public clsLogs()
        {
            //constructor para iniciar lo siguiente
            conexion = new OleDbConnection();
            comando = new OleDbCommand();

        }
        public void ConectarBD()
        {
            try
            {
                string ruta = "Provider = Microsoft.ACE.OLEDB.12.0;" + "Data Source = ..\\..\\Resources\\EL_CLUB.accdb";
                conexion.ConnectionString = ruta;
                conexion.Open();


            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
        }


        public void CargarLog(string varUsuario, DateTime varFecha, string varAccion)
        {
            ConectarBD();
            comando = new OleDbCommand();

            comando.Connection = conexion;
            // Establece el tipo de comando y la tabla
            comando.CommandType = System.Data.CommandType.TableDirect;
            //Que tabla traigo
            comando.CommandText = "Logs";
            // crea el objeto DataAdapter pasando como parámetro el objeto comando que queremos vincular
            adaptador = new OleDbDataAdapter(comando);
            // ejecutar la lectura de la tabla y almacenar su contenido en el dataAdapter
            adaptador.Fill(objDataSet, "Logs");
            // obtenemos una referencia a la tabla


            DataTable dt = objDataSet.Tables["Logs"];

            // creamos el nuevo DataRow con la estructura de campos de la tabla
            DataRow dr = dt.NewRow();
            // asignamos los valores a todos los campos del DataRow
            dr["Usuario"] = varUsuario;
            dr["Fecha"] = varFecha;
            dr["Accion"] = varAccion;

            // agregamos el DataRow a la tabla

            dt.Rows.Add(dr);

            // creamos el objeto OledBCommandBuilder pasando como parámetro el DataAdapter
            OleDbCommandBuilder cb = new OleDbCommandBuilder(adaptador);

            // actualizamos la base con los cambios realizados
            adaptador.Update(objDataSet, "Logs");
            conexion.Close();

        }
    }


}


