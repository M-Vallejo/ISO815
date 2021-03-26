using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sender
{
    public partial class MainFrm : Form
    {
        string ProcessPath = @"..\..\..\..\Receiver\bin\Debug\net5.0\Receiver.exe";

        public MainFrm()
        {
            InitializeComponent();
        }

        private void btnOpenDocument_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCedula.Text))
                OpenDocument(txtCedula.Text.Replace("-", ""));
            else
                MessageBox.Show("Debes digitar un número de cédula");
        }

        private void OpenDocument(string cedula)
        {
            try
            {
                ProcessStartInfo processInfo = new ProcessStartInfo(ProcessPath, cedula) 
                { 
                    CreateNoWindow = true
                };
                var process = Process.Start(processInfo);
                process.WaitForExit();
                process.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show("Error",e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            cbEstado.SelectedIndex = 0;
        }
    }
}
