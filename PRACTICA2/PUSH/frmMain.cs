using PUSH.Models;
using PUSH.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUSH
{
    public partial class frmMain : Form
    {
        private MySQLDbContext _database = new MySQLDbContext();
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "nomina_" + dtpPeriod.Value.ToString("yyyy_MM_dd");
            dialog.Filter = "Archivo separado por pipe | *.csv";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var employees = _database.Employees.ToList();

                string path = dialog.FileName;
                var header = new Header 
                {
                    AccountNumber = txtAccountNumber.Text,
                    TotalAmount = employees.Select(x=> x.Amount).Sum(),
                    DocumentNumber = txtDocumentNumber.Text,
                    DocumentType = cbDocumentType.Text[0],
                    Period = dtpPeriod.Value
                };

                FileService.GenerateFile(employees, header, path);
                MessageBox.Show("Archivo generado satisfactoriamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            cbDocumentType.SelectedIndex = 2;
        }
    }
}
