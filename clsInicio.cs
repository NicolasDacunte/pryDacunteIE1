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
    internal class clsInicio
    {
        OleDbConnection conexionBD;
        OleDbCommand comandoBD;
        OleDbDataReader lectorBD;
        clsLogs objLog;

        public string EstadoConexion = "";
        

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

        int varContador = 0;
        int varEncontro = 0;
        public void IngresarUsuario(string varNombre, string varContraseña, frmInicio frmInicio)
        {

            comandoBD = new OleDbCommand();

            comandoBD.Connection = conexionBD;


            // Establece el tipo de comando y la tabla
            comandoBD.CommandType = System.Data.CommandType.TableDirect;
            //Que tabla traigo
            comandoBD.CommandText = "Usuarios";
            //abre la tabla y muestra por renglon
            lectorBD = comandoBD.ExecuteReader();


            //SI TIENE FILAS
            if (lectorBD.HasRows)
            {

                while (lectorBD.Read()) //mientras pueda leer, mostrar (leer)
                {
                    if (lectorBD[1].ToString() == varNombre && lectorBD[2].ToString() == varContraseña)
                    {
                        
                        objLog = new clsLogs();
                        string varAccion = "Inicio Sesion";
                        DateTime varFecha = DateTime.Now;

                        objLog.CargarLog(varNombre, varFecha, varAccion);

                        frmInicio.Hide();
                        frmMenu frmCargar = new frmMenu(varNombre);
                        frmCargar.Show();
                        varEncontro++;
                        break;
                    }
                    
                }
                if (varEncontro == 0)
                {
                    MessageBox.Show("Datos de inicio de sesion incorrectos");
                    varContador += 1;

                }

                if (varContador >= 3)
                {
                    MessageBox.Show("Demasiados intentos de inicio de sesion, el sistema se cerrara");
                    Application.Exit();

                }
            }

        }

        public void CrearUsuario(string varUsuario, string contraseñaUs, string TipoUsuario)
        {
            OleDbCommand comandoBD = new OleDbCommand();
            OleDbDataAdapter adaptador;
            DataSet objds = new DataSet(); // objeto DataSet a usar  

            try
            {
                // establecer las propiedades al objeto comando
                comandoBD.Connection = conexionBD;
                comandoBD.CommandType = CommandType.TableDirect;
                // Que tabla traigo
                comandoBD.CommandText = "Usuarios";

                // abrir la tabla y mostrar por renglón
                lectorBD = comandoBD.ExecuteReader();

                // Verificar si TIENE FILAS
                if (lectorBD.HasRows)
                {
                    while (lectorBD.Read()) // mientras pueda leer, mostrar (leer)
                    {
                        if (lectorBD[1].ToString() == varUsuario)
                        {
                            MessageBox.Show("Ya existe este Usuario");
                            return; // Salir del método si ya existe el usuario
                        }
                    }
                }

                // Cerrar el DataReader después de usarlo para evitar conflictos
                lectorBD.Close();

                // Crear el objeto DataAdapter pasando como parámetro el objeto comando que queremos vincular
                adaptador = new OleDbDataAdapter(comandoBD);

                // Ejecutar la lectura de la tabla y almacenar su contenido en el dataAdapter
                adaptador.Fill(objds, "Usuarios");

                // Obtener una referencia a la tabla de Usuarios
                DataTable tabla = objds.Tables["Usuarios"];

                // Crear el nuevo DataRow con la estructura de campos de la tabla Usuarios
                DataRow nuevoRegistro = tabla.NewRow();

                // Asignar los valores a todos los campos del DataRow
                nuevoRegistro["Usuario"] = varUsuario;
                nuevoRegistro["Contraseña"] = contraseñaUs;
                nuevoRegistro["Perfil"] = TipoUsuario;

                // Agregar el DataRow a la tabla Usuarios
                tabla.Rows.Add(nuevoRegistro);

                // Crear el objeto OleDbCommandBuilder pasando como parámetro el DataAdapter
                OleDbCommandBuilder cb = new OleDbCommandBuilder(adaptador);

                // Actualizar la base con los cambios realizados
                adaptador.Update(objds, "Usuarios");

                MessageBox.Show("Usuario creado con éxito");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Asegurarse de cerrar el DataReader en el bloque finally
                if (lectorBD != null && !lectorBD.IsClosed)
                {
                    lectorBD.Close();
                }
            }
        }

    }
}
