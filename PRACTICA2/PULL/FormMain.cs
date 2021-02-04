using PULL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PULL
{
    public partial class FormMain : Form
    {
        string filePath;
        private MySQLDbContext _database = new MySQLDbContext();
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Archivo separado por Pipe (;) | *.csv";
            listView1.Items.Clear();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = this.filePath = openFileDialog1.FileName;
                var empleados = FileService.GetEmpleados(path);
                var empresaDocumento = empleados[0].DocumentoEmpresa;
                var fecha = empleados[0].Fecha;


                var data1 = _database.NominaEmpresas.ToList();
                var data = _database.NominaEmpresas.Where(x => x.DocumentoEmpresa == empresaDocumento && x.Fecha == fecha).ToList();

                if (data.Count >= 1)
                {
                    MessageBox.Show("Este archivo ya sido insertado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                txtCuenta.Text = empleados[0].CuentaEmpresa;
                txtTipoDocumento.Text = empleados[0].TipoDocumento == "P" ? "Pasaporte" : empleados[0].TipoDocumento == "C" ? "Cedula" : "RNC";
                txtDocumento.Text = empresaDocumento;
                textFecha.Text = fecha;

                foreach (var empleado in empleados)
                {
                    var tipoDocumento = empleado.TipoDocumento == "P" ? "Pasaporte" : empleado.TipoDocumento == "C" ? "Cedula" : "RNC";
                    var row = new string[] { empleado.NumCuenta.ToString(), tipoDocumento, empleado.Documento, empleado.Monto.ToString() };

                    var lvi = new ListViewItem(row);
                    lvi.Tag = empleado;

                    listView1.Items.Add(lvi);

                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Desea guardar los registros?",
                                     "Guardar Registros",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                if (listView1.Items.Count == 0)
                {
                    MessageBox.Show("No hay un archivo cargado, debe cargar un archivo", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {

                    var path = this.filePath;
                    var empleados = FileService.GetEmpleados(path);

                    _database.NominaEmpresas.AddRange(empleados);
                    _database.SaveChanges();

                    listView1.Items.Clear();
                    txtCuenta.Clear();
                    txtDocumento.Clear();
                    txtTipoDocumento.Clear();
                    textFecha.Clear();
                    this.filePath = string.Empty;
                    MessageBox.Show("Se guardaron correctamente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            else
            {
                // If 'No', do something here.
            }
        }
    }
}
