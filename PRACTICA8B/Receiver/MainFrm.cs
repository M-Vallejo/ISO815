using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Receiver
{
    public partial class MainFrm : Form
    {
        private readonly string ImagesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DOC");

        public MainFrm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                var cedula = args[1];
                this.Text += ": " + cedula;
                string path = Path.Combine(ImagesPath, cedula + ".png");
                if (File.Exists(path))
                    ShowImage(path);
                else
                {
                    MessageBox.Show($"La imágen solicitada no existe.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.Exit();
                }
            }
            else 
            {
                MessageBox.Show("Cédula no proporcionada.","Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }

        private void ShowImage(string imagePath) 
        {
            pbImage.Image = Image.FromFile(imagePath);
        }
    }
}
