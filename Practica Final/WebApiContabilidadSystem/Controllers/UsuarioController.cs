using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiContabilidadSystem.Data;
using WebApiContabilidadSystem.Models;
using WebApiContabilidadSystem.Utils;

namespace WebApiContabilidadSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ContabilidadDbContext _db;
        public UsuarioController(ContabilidadDbContext db)
        {
            _db = db;
        }

        public IEnumerable<USUARIO> Get()
        {
            return _db.Usuario.Where(x=> x.ESTADO != (int)Estado.Eliminado).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult GetUsuarioById(int id)
        {
            var target = _db.Usuario.FirstOrDefault(x => x.USUARIO_ID == id && x.ESTADO != (int)Estado.Eliminado);

            if (target != null)
            {
                return Ok(target);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateUsuario([FromBody] USUARIO usuario)
        {
            try
            {
                usuario.ESTADO = (int)Estado.Activo;
                usuario.CLAVE = usuario.CLAVE.Encrypt();
                usuario.FECHA_CREACION = DateTime.Now;
                _db.Usuario.Add(usuario);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public ActionResult EditUsuario([FromBody] USUARIO usuario)
        {
            try
            {
                var target = _db.Usuario.FirstOrDefault(x => x.USUARIO_ID == usuario.USUARIO_ID);
                if (target == null)
                {
                    return NotFound();
                }
                target.NOMBRE = usuario.NOMBRE;
                target.NOMBRE_USUARIO = usuario.NOMBRE_USUARIO;
                target.CLAVE = usuario.CLAVE.Encrypt();
                target.CORREO = usuario.CORREO;
                target.APELLIDOS = usuario.APELLIDOS;
                target.ROL = target.ROL;
                target.ESTADO = target.ESTADO;
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public ActionResult ChangeEstadoUsuario([FromQuery] int id, int estado)
        {
            try
            {
                var target = _db.Usuario.FirstOrDefault(x => x.USUARIO_ID == id);

                if (target != null)
                {
                    target.ESTADO = estado;
                    _db.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

    }
}
