using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiContabilidadSystem.Models
{
    public enum Estado
    {
        Inactivo = 0,
        Activo =1,
        Eliminado = 2
    }

    public enum TipoDocumento
    {
        Cedula = 0,
        RNC = 1,
        Pasaporte = 2
    }
    public enum Rol
    {
        Usuario = 0,
        Administrador = 1
    }
    public enum TipoPersona
    {
        Fisica = 0,
        Juridica = 1,
    }
}
