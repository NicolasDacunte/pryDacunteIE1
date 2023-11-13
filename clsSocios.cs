using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryDacunteIE1
{
    internal class clsSocios
    {
        OleDbConnection conexionBD;
        OleDbCommand comandoBD;
        OleDbDataReader lectorBD;

        public string EstadoConexion = "";
        public string datosTabla;
        //public string ruta;
        
        public void ConectarBD()
        {
            try
            {
                conexionBD = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0;" + "Data Source = C:\\Users\\sistema\\source\\repos\\pryDacunteIE1\\Resources\\EL_CLUB.accdb");
                conexionBD.Open();
                EstadoConexion = "Conectado";
            }
            catch (Exception ex)
            {
                EstadoConexion = "Error" + ex.Message;
            }
        }

        public void TraerDatos(DataGridView grilla)
        {
            //instancia un objeto en la memoria
            comandoBD = new OleDbCommand();

            comandoBD.Connection = conexionBD;
            comandoBD.CommandType = System.Data.CommandType.TableDirect;
            comandoBD.CommandText = "SOCIOS";

            lectorBD = comandoBD.ExecuteReader();
            //crea las columnas de la grilla
            grilla.Columns.Add("ID", "ID");
            grilla.Columns.Add("Nombre", "Nombre");
            grilla.Columns.Add("Apellido", "Apellido");
            grilla.Columns.Add("Pais", "Pais");
            grilla.Columns.Add("Edad", "Edad");
            grilla.Columns.Add("Ingreso", "Ingreso");
            grilla.Columns.Add("Puntaje", "Puntaje");
            grilla.Columns.Add("Estado", "Estado");

            //leo como si fuera un archivo
            if (lectorBD.HasRows)
            {
                while (lectorBD.Read())
                {
                    datosTabla += "-" + lectorBD[0];
                    // Comprueba si lectorBD[8] es true
                    string estado = lectorBD.GetBoolean(8) ? "Activo" : "Inactivo";
                    grilla.Rows.Add(lectorBD[0], lectorBD[1], lectorBD[2], lectorBD[3], lectorBD[4], lectorBD[6], lectorBD[7], estado);
                }
            }
        }

        int VarEncontrado = 0;
        public void BuscarPorApellido(string apellido, DataGridView grilla)
        {

            comandoBD = new OleDbCommand();

            comandoBD.Connection = conexionBD;
            //q tipo de operacion quiero hacer y que me traiga TODA la tabla con el tabledirect
            comandoBD.CommandType = System.Data.CommandType.TableDirect;
            //Que tabla traigo
            comandoBD.CommandText = "SOCIOS";
            //abre la tabla y muestra por renglon
            lectorBD = comandoBD.ExecuteReader();


            //SI TIENE FILAS
            if (lectorBD.HasRows)
            {
                
                while (lectorBD.Read()) //mientras pueda leer, mostrar (leer)
                {
                    if (lectorBD[2].ToString() ==  apellido)
                    {
                        
                        //datosTabla += "-" + lectorBD[0]; //dato d la comlumna 0
                        //MessageBox.Show("El Cliente " + lectorBD[0] + " Existente", "Consulta", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        grilla.Rows.Clear();
                        VarEncontrado = 1;
                        // Comprueba si lectorBD[8] es true
                        string estado = lectorBD.GetBoolean(8) ? "Activo" : "Inactivo";
                        grilla.Rows.Add(lectorBD[0], lectorBD[1], lectorBD[2], lectorBD[3], lectorBD[4], lectorBD[6], lectorBD[7], estado);

                        break;
                    }

                }
                if (VarEncontrado == 0)
                {

                    MessageBox.Show("NO Existente");

                }
            }
        }

        public void ModificarEstado(int id)
        {

            OleDbCommand comando = new OleDbCommand();
            OleDbDataAdapter adaptador;
            DataSet objds = new DataSet();// objeto DataSetas usar

            ConectarBD();
            /*try
            {                
                conexionBD = new OleDbConnection();
                conexionBD.ConnectionString = cadenaDeConexion;
                conexionBD.Open();
               
            }
            catch (Exception ex)
            {
                estadoDeConexion = "Error" + ex.Message;
            }*/

            // establecer las propiedades al objeto comando
            comandoBD.Connection = conexionBD;
            comandoBD.CommandType = CommandType.TableDirect;
            //ELIGE EL AdeD
            comandoBD.CommandText = "SOCIOS";
            // crear el objeto DataAdapter pasando como parámetro el objeto comando que queremos vincular
            adaptador = new OleDbDataAdapter(comandoBD);

            // ejecutar la lectura del AdeD y almacenar su contenido en el dataAdapter

            adaptador.Fill(objds, "SOCIOS");


            // obtenemos una referencia a AdeD de SOCIOS
            DataTable dt = objds.Tables["SOCIOS"];

            // recorrer los registros del AdeD

            foreach (DataRow registro in dt.Rows)
            { // buscar el Socio con el ID ingresado por pantalla

                if ((int)registro["CODIGO_SOCIO"] == id)
                {
                    // establecer el modo de edición del DataRow
                    registro.BeginEdit();

                    // asignamos el nuevo valor al estado del socio 
                    if ((bool)registro["ESTADO"])
                    {
                        registro["ESTADO"] = false;
                    }
                    else
                    {
                        registro["ESTADO"] = true;
                    }

                    // finalizamos el modo edición sobre delDataRow
                    registro.EndEdit();
                    break;// salir del foreach
                }


            }
            // creamos el objeto OledBCommandBuilder pasando como parámetro el DataAdapter
            OleDbCommandBuilder cb = new OleDbCommandBuilder(adaptador);

            // actualizamos la base con los cambios realizados

            adaptador.Update(objds, "SOCIOS");

            MessageBox.Show("Estado cambiado con éxito.");

        }

    }

}
