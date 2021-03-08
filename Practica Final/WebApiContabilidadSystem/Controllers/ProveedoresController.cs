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
    public class ProveedoresController : ControllerBase
    {
        private readonly ContabilidadDbContext _db;
        public ProveedoresController(ContabilidadDbContext db)
        {
            _db = db;
        }

        public IEnumerable<PROVEEDOR> Get()
        {
            return _db.Proveedor.ToList();
        }
        public IEnumerable<PROVEEDOR> GetConceptoPagoByEstatus(int Estado)
        {
            var Proveedores = _db.Proveedor.Where(x => x.ESTADO == Estado).ToList();
            return Proveedores;
        }
        [HttpGet("{id}")]
        public ActionResult GetProveedorById(int id)
        {
            var target = _db.Proveedor.FirstOrDefault(x => x.PROVEEDOR_ID == id);

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

        [HttpPut("{id}")]
        public ActionResult EditProveedor([FromBody] PROVEEDOR proveedor)
        {
            try
            {
                var target = _db.Proveedor.FirstOrDefault(x => x.PROVEEDOR_ID == proveedor.PROVEEDOR_ID);
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

        [HttpPut("{id}")]
        public ActionResult ChangeEstadoProveedores(int id, int estado)
        {
            var target = _db.Proveedor.FirstOrDefault(x => x.PROVEEDOR_ID == id);

            if (target != null)
            {
                target.ESTADO = estado;
                return Ok();
            }
            return NotFound();
        }
    }
}
