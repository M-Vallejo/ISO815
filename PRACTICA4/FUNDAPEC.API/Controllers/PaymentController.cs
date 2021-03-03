using FUNDAPEC.API.Models;
using FUNDAPEC.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FUNDAPEC.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly MySQLDbContext _dbContext;

        public PaymentController(MySQLDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public Payment GetPayments()
        {
            var details =  _dbContext.PAYMENTS.ToList();
            var payment = new Payment(details);
            return payment;
        }

        [HttpGet]
        public Payment GetPaymentsByStudentId(string matricula)
        {
            var details = _dbContext.PAYMENTS.Where(x => string.Equals(x.Matricula, matricula, StringComparison.OrdinalIgnoreCase));
            var payment = new Payment(details);
            return payment;
        }
    }
}
