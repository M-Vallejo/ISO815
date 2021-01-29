using PUSH.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PUSH.Services
{
    public class FileService
    {
        internal static void GenerateFile(List<Employee> employees, Header header, string outPath) 
        {
            const string d = "|";
            using (StreamWriter file = new StreamWriter(outPath, false))
            {
                string head = $"{header.Type}{d}{header.DocumentType}{d}{header.DocumentNumber}{d}{header.AccountNumber}{d}{header.TotalAmount}{d}{header.Period.ToString("yyyy-MM-dd")}";
                file.WriteLine(head);
                foreach (var employee in employees)
                {
                    string detail = $"{employee.AccountNumber}{d}{employee.DocumentType}{d}{employee.DocumentNumber}{d}{employee.Amount}";
                    file.WriteLine(detail);
                }
                string footer = $"S{d}{employees.Count}";
                file.WriteLine(footer);
            }
        }
    }
}
