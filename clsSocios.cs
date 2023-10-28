using System;
using System.Collections.Generic;
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

            //leo como si fuera un archivo
            if (lectorBD.HasRows)
            {
                while (lectorBD.Read())
                {
                    datosTabla += "-" + lectorBD[0];
                    grilla.Rows.Add(lectorBD[0], lectorBD[1], lectorBD[2], lectorBD[3], lectorBD[4], lectorBD[6], lectorBD[7]);
                }
            }
        }

        int VarEncontrado = 0;
        public void BuscarPorApellido(string apellido, DataGridView grilla)
        {

            comandoBD = new OleDbCommand();

            comandoBD.Connection = conexionBD;
            //q tipo de operacion quierp hacer y que me traiga TOD la tabla con el tabledirect
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
                        grilla.Rows.Add(lectorBD[0], lectorBD[1], lectorBD[2], lectorBD[3], lectorBD[4], lectorBD[6], lectorBD[7]);

                        break;
                    }

                }
                if (VarEncontrado == 0)
                {

                    MessageBox.Show("NO Existente");

                }
            }
        }
    }

}
