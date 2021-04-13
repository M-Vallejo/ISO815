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
    public class TipoDocumentoController : ControllerBase
    {
        private readonly ContabilidadDbContext _db;
        public TipoDocumentoController(ContabilidadDbContext db)
        {
            _db = db;
        }

        public IEnumerable<TIPO_DOCUMENTO> Get()
        {
            return _db.TIPO_DOCUMENTO.Where(x=> x.ESTADO != (int)Estado.Eliminado).OrderByDescending(x => x.ESTADO);
        }

        [HttpGet("{Estado}")]
        public IEnumerable<TIPO_DOCUMENTO> GetTipoDocumentoByEstatus(int Estado)
        {
            var TipoDocumento = _db.TIPO_DOCUMENTO.Where(x => x.ESTADO == Estado).ToList();
            return TipoDocumento;
        }

        [HttpGet("{id}")]
        public ActionResult GetTipoDocumentoById(int id)
        {
            var target = _db.TIPO_DOCUMENTO.FirstOrDefault(x => x.TIPO_DOCUMENTO_ID == id && x.ESTADO != (int)Estado.Eliminado);

            if (target != null)
            {
                return Ok(target);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateTipoDocumento([FromBody] TIPO_DOCUMENTO TipoDocumento)
        {
            try
            {
                TipoDocumento.ESTADO = (int)Estado.Activo;
                _db.TIPO_DOCUMENTO.Add(TipoDocumento);
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public ActionResult EditTipoDocumento([FromBody] TIPO_DOCUMENTO TipoDocumento)
        {
            try
            {
                var target = _db.ConceptoPago.FirstOrDefault(x => x.CONCEPTO_PAGO_ID == TipoDocumento.TIPO_DOCUMENTO_ID);
                target.DESCRIPCION = TipoDocumento.DESCRIPCION;
                target.ESTADO = TipoDocumento.ESTADO;
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public ActionResult ChangeEstadoTipoDocumento(int id, int estado)
        {
            var target = _db.TIPO_DOCUMENTO.FirstOrDefault(x => x.TIPO_DOCUMENTO_ID == id);

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
