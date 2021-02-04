using PULL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PULL.Services
{
    public static class FileService
    {
        public static List<nominaEmpresas> GetEmpleados(string path)
        {
            var empleados = new List<nominaEmpresas>();

            try
            {
                int counter = 0;
                string empresa_tipo_documento = "";
                string empresa_documento = "";
                string cuenta = "";
                string total = "";
                string fecha = "";
                string line;
                int totaldata = 0;
                using (StreamReader file = new StreamReader(path, false))
                {
                    char separador = '|';

                    while ((line = file.ReadLine()) != null)
                    {
                        var data = line.Split(separador);
                        if (data[0] == "E")
                        {
                            empresa_tipo_documento = data[1];
                            empresa_documento = data[2];
                            cuenta = data[3];
                            total = data[4];
                            fecha = data[5];
                        }

                        else if (data[0] != "S")
                        {
                            var empleado = new nominaEmpresas();
                            empleado.TipoDocumentoEmpresa = empresa_tipo_documento;
                            empleado.CuentaEmpresa = cuenta;
                            empleado.DocumentoEmpresa = empresa_documento;
                            empleado.Fecha = fecha;
                            empleado.NumCuenta = data[0];
                            empleado.TipoDocumento = data[1];
                            empleado.Documento = data[2];
                            empleado.Monto = decimal.Parse(data[3]);
                            
                            empleados.Add(empleado);
                        }
                        else
                        {
                            totaldata = int.Parse(data[1]);
                        }
                        counter++;
                    }
                }
                if (empleados.Count != (totaldata))
                {
                    MessageBox.Show("Error al leer el archivo, la cantidad de registros leidos es diferente a la cantidad total", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return new List<nominaEmpresas>();
                }
                return empleados;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Archivo no valido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return empleados;
            }
        }
    }
}
