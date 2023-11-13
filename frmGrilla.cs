using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace pryDacunteIE1
{
    public partial class frmGrilla : Form
    {
        string usuario;
        public frmGrilla(string varUsuario)
        {
            InitializeComponent();
            LlenarTreeView();
            string usuario = varUsuario;
            KeyPreview = true;
            this.KeyDown += CerrarFrm_KeyDown;
            this.usuario = usuario;
        }

        public void CerrarFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit(); // Cierra la aplicación completa
            }
        }

        private void LlenarTreeView()
        {
            TreeNode nodoMadre;

            DirectoryInfo info = new DirectoryInfo(@"../../Resources/Proveedores");
            if (info.Exists == true) 
            {
                nodoMadre = new TreeNode(info.Name);
                nodoMadre.Tag = info;
                AdquirirCarpetas(info.GetDirectories(), nodoMadre);
                treeView1.Nodes.Add(nodoMadre);
            }
        }

        private void AdquirirCarpetas(DirectoryInfo[] subDirs,TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;


            foreach (DirectoryInfo subDir in subDirs)
            {
                aNode = new TreeNode(subDir.Name, 0, 0);
                aNode.Tag = subDir;
                aNode.ImageKey = "folder";

                // Obtener archivos en lugar de solo carpetas
                FileInfo[] files = subDir.GetFiles();
                foreach (FileInfo file in files)
                {
                    TreeNode fileNode = new TreeNode(file.Name, 1, 1);
                    fileNode.Tag = file;
                    aNode.Nodes.Add(fileNode);
                }

                //recursiva - se llama a si mismo para
                //buscar màs carpetas
                subSubDirs = subDir.GetDirectories();
                if (subSubDirs.Length != 0)
                {
                    AdquirirCarpetas(subSubDirs, aNode);
                }
                
                nodeToAddTo.Nodes.Add(aNode);
            }
        }

        private void frmGrilla_Load(object sender, EventArgs e)
        {

        }
        string leerLinea;
        string[] separarDatos;
        private DirectoryInfo info;

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dgv1.Rows.Clear();
            dgv1.Columns.Clear();

            string Archivo = Convert.ToString(treeView1.SelectedNode.FullPath);
            string NombreArchivo = treeView1.SelectedNode.Text;
            info = new DirectoryInfo(@"../../Resources");
            string ruta = info.FullName;
            

            try
            {
                            
                StreamReader sr = new StreamReader(ruta + "\\" + Archivo);

                leerLinea = sr.ReadLine();
                separarDatos = leerLinea.Split(';');

                for (int indice = 0; indice < separarDatos.Length; indice++)
                {
                    dgv1.Columns.Add(separarDatos[indice], separarDatos[indice]);
                }

                while (sr.EndOfStream == false)
                {
                    leerLinea = sr.ReadLine();
                    separarDatos = leerLinea.Split(';');
                    dgv1.Rows.Add(separarDatos);
                }

                sr.Close();
                
                dgv1.Visible = true;
                                              

            }
            catch (Exception)
            {
                
            }
        }

        private void dgv1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            frmMenu frmEntrar = new frmMenu(usuario);
            this.Hide();
            frmEntrar.Show();
        }
    }
}
