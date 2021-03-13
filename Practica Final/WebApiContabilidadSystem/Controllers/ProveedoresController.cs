using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiContabilidadSystem.Data;
using WebApiContabilidadSystem.Models;

namespace WebApiContabilidadSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProveedoresController : ControllerBase
    {
        private readonly ContabilidadDbContext _db;
        public ProveedoresController(ContabilidadDbContext db)
        {
            _db = db;
        }

        public IEnumerable<PROVEEDOR> Get()
        {
            return _db.Proveedor.Where(x => x.ESTADO != (int)Estado.Eliminado).ToList();
        }

        [HttpGet("{Estado}")]
        public IEnumerable<PROVEEDOR> GetProveedoresByEstatus(int Estado)
        {
            var Proveedores = _db.Proveedor.Where(x => x.ESTADO == Estado).ToList();
            return Proveedores;
        }
        [HttpGet("{id}")]
        public ActionResult GetProveedorById(int id)
        {
            var target = _db.Proveedor.FirstOrDefault(x => x.PROVEEDOR_ID == id && x.ESTADO != (int)Estado.Eliminado);

            if (target != null)
            {
                return Ok(target);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateProveedor([FromBody] PROVEEDOR proveedor)
        {
            try
            {
                proveedor.ESTADO = (int)Estado.Activo;
                _db.Proveedor.Add(proveedor);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public ActionResult EditProveedor([FromBody] PROVEEDOR proveedor)
        {
            try
            {
                var target = _db.Proveedor.FirstOrDefault(x => x.PROVEEDOR_ID == proveedor.PROVEEDOR_ID);
                if (target == null)
                {
                    return NotFound();
                }
                target.NOMBRE = proveedor.NOMBRE;
                target.NUMERO_DOCUMENTO = proveedor.NUMERO_DOCUMENTO;
                target.TIPO_DOCUMENTO = proveedor.TIPO_DOCUMENTO;
                target.BALANCE = proveedor.BALANCE;
                target.ESTADO = proveedor.ESTADO;
                target.TIPO_PERSONA = proveedor.TIPO_PERSONA;
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public ActionResult ChangeEstadoProveedores([FromQuery]int id, int estado)
        {
            try
            {
                var target = _db.Proveedor.FirstOrDefault(x => x.PROVEEDOR_ID == id);

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
