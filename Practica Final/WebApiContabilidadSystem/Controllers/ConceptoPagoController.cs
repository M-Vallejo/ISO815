﻿using Microsoft.AspNetCore.Http;
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
    public class ConceptoPagoController : ControllerBase
    {
        private readonly IDataBaseContext _db;
        public ConceptoPagoController(IDataBaseContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IEnumerable<CONCEPTO_PAGO> Get()
        {
            return _db.ConceptoPago.Where(x=> x.ESTADO != (int) Estado.Eliminado).OrderByDescending(x=> x.ESTADO);
        }

        [HttpGet("{Estado}")]
        public IEnumerable<CONCEPTO_PAGO>GetConceptoPagoByEstatus(int Estado)
        {
            var conceptosPagos = _db.ConceptoPago.Where(x => x.ESTADO == Estado).ToList();
            return conceptosPagos;
        }

        [HttpGet("{id}")]
        public ActionResult GetConceptoPagoById(int id)
        {
            var target = _db.ConceptoPago.FirstOrDefault(x => x.CONCEPTO_PAGO_ID == id && x.ESTADO != (int) Estado.Eliminado);

            if(target != null)
            {
                return Ok(target);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateConceptoPago([FromBody] CONCEPTO_PAGO conceptoPago)
        {
            try
            {
                conceptoPago.ESTADO = (int)Estado.Activo;
                _db.ConceptoPago.Add(conceptoPago);
                _db.SaveChanges();
                return Ok();
            }
            catch(Exception e)
            {
                return StatusCode(500,e.Message);
            }
        }

        [HttpPut]
        public ActionResult EditConceptoPago([FromBody] CONCEPTO_PAGO conceptoPago)
        {
            try
            {
                var target = _db.ConceptoPago.FirstOrDefault(x => x.CONCEPTO_PAGO_ID == conceptoPago.CONCEPTO_PAGO_ID);
                target.DESCRIPCION = conceptoPago.DESCRIPCION;
                target.ESTADO = conceptoPago.ESTADO;
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public ActionResult ChangeEstadoConceptoPago(int id, int estado)
        {
            var target = _db.ConceptoPago.FirstOrDefault(x => x.CONCEPTO_PAGO_ID == id);
            
            if(target !=null)
            {
                target.ESTADO = estado;
                _db.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
