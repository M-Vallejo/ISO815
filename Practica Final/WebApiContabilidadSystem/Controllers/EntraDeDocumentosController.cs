using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiContabilidadSystem.AccountingService;
using WebApiContabilidadSystem.Data;
using WebApiContabilidadSystem.Models;
using WebApiContabilidadSystem.Models.DTO;
using WebApiContabilidadSystem.Models.ViewModels;

namespace WebApiContabilidadSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class EntraDeDocumentosController : ControllerBase
    {
        private readonly IDataBaseContext _db;
        private readonly IConfiguration _conf;

        public EntraDeDocumentosController(IDataBaseContext db, IConfiguration conf)
        {
            _db = db;
            _conf = conf;
        }

        [HttpGet]
        public IEnumerable<ENTRADA_DOCUMENTO> Get()
        {

            var p = _db.EntradaDocumento
                        .Include("TIPO_DOCUMENTO")
                        .Include("PROVEEDOR")
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
                                             .FirstOrDefault(x => x.NUMERO_DOCUMENTO == id);

            if (target != null)
            {
                return Ok(target);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateEntradaDocumento([FromBody] EntradaDocumentViewModel EntradaDocumento)
        {
            var entity = EntradaDocumento.ToEntity();
            entity.PROVEEDOR = _db.Proveedor.FirstOrDefault(x => x.PROVEEDOR_ID == entity.PROVEEDOR.PROVEEDOR_ID);
            entity.TIPO_DOCUMENTO = _db.TIPO_DOCUMENTO.FirstOrDefault(x => x.TIPO_DOCUMENTO_ID == entity.TIPO_DOCUMENTO.TIPO_DOCUMENTO_ID);

            _db.EntradaDocumento.Add(entity);
            _db.SaveChanges();
            return Ok(entity.NUMERO_DOCUMENTO);
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

        [HttpPost]
        public IEnumerable<AsientoDTO> Search(AccountingSearchViewModel model) 
        {
            var result = _db.EntradaDocumento
                        .Include("TIPO_DOCUMENTO")
                        .Include("PROVEEDOR")
                        .Where(x => x.ID_ASIENTO == null 
                                    && x.FECHA_DOCUMENTO >= model.Desde.Date 
                                    && x.FECHA_DOCUMENTO <= model.Hasta.Date)
                        .Select(x=> new AsientoDTO(x));
            return result;
        }

        [HttpPost]
        public ActionResult Contabilizar ([FromBody] TransactionsViewModel model)
        {
            try
            {
                var documentos = _db.EntradaDocumento.Include("TIPO_DOCUMENTO")
                        .Include("PROVEEDOR").Where(x => model.Transactions.Contains(x.NUMERO_DOCUMENTO)).ToList();
                var asiento = int.Parse(_conf.GetSection("AccountingSettings").GetSection("IdAuxiliar").Value);
                var credito = int.Parse(_conf.GetSection("AccountingSettings").GetSection("CuentaCredito").Value);

                var contabilidad = new ServicioContabilidad().Contabilizar(documentos, asiento, credito);
                var contabilidadJson = System.Text.Json.JsonSerializer.Serialize(contabilidad);
                var resquestContent = new StringContent(contabilidadJson, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(_conf.GetSection("AccountingSettings").GetSection("Contabilizar").Value);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsync("", resquestContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    var contents = response.Content.ReadAsStringAsync();
                    var mdl = JsonConvert.DeserializeObject<ContabilidadViewModel>(contents.Result);

                    foreach (var documento in documentos)
                    {
                        documento.ID_ASIENTO = mdl.id;
                        documento.FECHA_PROCESO = mdl.entryDate == DateTime.MinValue ? null : mdl.entryDate;
                    }

                    _db.SaveChanges();
                }
                else
                {
                    return BadRequest("El api fallo");
                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
