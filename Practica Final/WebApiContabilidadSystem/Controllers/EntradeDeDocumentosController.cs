using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class EntradeDeDocumentosController : ControllerBase
    {
        private readonly ContabilidadDbContext _db;
        public EntradeDeDocumentosController(ContabilidadDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IEnumerable<ENTRADA_DOCUMENTO> Get()
        {

            var p = _db.EntradaDocumento
                        .Include("TIPO_DOCUMENTO")
                        .Include("PROVEEDOR")
                        .ToList()
                        .Where(x => x.ESTADO != (int)Estado.Eliminado)
                        .OrderByDescending(x => x.ESTADO);
            return p;
        }

        [HttpGet("{Estado}")]
        public IEnumerable<ENTRADA_DOCUMENTO> GetEntradaDocumentoByEstatus(int Estado)
        {
            var entradaDocumento = _db.EntradaDocumento.Include("TIPO_DOCUMENTO")
                                                       .Include("PROVEEDOR")
                                                       .Where(x => x.ESTADO == Estado);
            return entradaDocumento;
        }

        [HttpGet("{id}")]
        public ActionResult GetEntradaDocumentoById(int id)
        {
            var target = _db.EntradaDocumento.Include("TIPO_DOCUMENTO")
                                             .Include("PROVEEDOR")
                                             .FirstOrDefault(x => x.NUMERO_DOCUMENTO == id && x.ESTADO != (int)Estado.Eliminado);

            if (target != null)
            {
                return Ok(target);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateEntradaDocumento([FromBody] ENTRADA_DOCUMENTO EntradaDocumento)
        {
            try
            {
                EntradaDocumento.ESTADO = (int)Estado.Activo;
                _db.EntradaDocumento.Add(EntradaDocumento);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public ActionResult EditEntradaDocumento([FromBody] ENTRADA_DOCUMENTO EntradaDocumento)
        {
            try
            {
                var target = _db.EntradaDocumento.FirstOrDefault(x => x.NUMERO_DOCUMENTO == EntradaDocumento.NUMERO_DOCUMENTO);
                target.NUMERO_FACTURA = EntradaDocumento.NUMERO_FACTURA;
                target.NUMERO_CHEQUE = EntradaDocumento.NUMERO_CHEQUE;
                target.FECHA_DOCUMENTO = EntradaDocumento.FECHA_DOCUMENTO;
                target.MONTO = EntradaDocumento.MONTO;
                target.TIPO_DOCUMENTO = EntradaDocumento.TIPO_DOCUMENTO;
                target.PROVEEDOR = EntradaDocumento.PROVEEDOR;
                target.CONDICIONES = EntradaDocumento.CONDICIONES;
                target.ESTADO = EntradaDocumento.ESTADO;
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public ActionResult ChangeEstadoEntradaDocumento(int id, int estado)
        {
            var target = _db.EntradaDocumento.FirstOrDefault(x => x.NUMERO_DOCUMENTO == id);

            if (target != null)
            {
                target.ESTADO = estado;
                _db.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
