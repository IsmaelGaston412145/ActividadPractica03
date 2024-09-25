using BudgetBack.Domain;
using BudgetBack.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BudgetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private FacturaManager _service;

        public FacturaController()
        {
            _service = new FacturaManager();//lo inyectamos manualmente !
        }

        // GET: api/<BudgetController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_service.GetBudgets());            }
            catch (Exception)
            {
                return StatusCode(500, "Error. Ha ocurrido un error interno!");
            }
        }

     
        // PUT api/<BudgetController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Budget value)
        {
            try
            {
               if(_service.GetBudgetsById(id) == null)
                {
                    return NotFound($"Presupuesto {id} NO ENCONTRADO!");
                }
                if (_service.UpdateBudget(value))
                {
                    return Ok("Presupuesto actualizado con éxito!");
                }
                else
                {
                    return BadRequest("Presupuesto NO actualizado con éxito!");
                }         
            }
            catch (Exception)
            {
                return StatusCode(500, "Error. Ha ocurrido un error interno!");
            }
        }

        [HttpPost]

        public IActionResult Post([FromBody] Budget budget)
        {
            try
            {
                if (budget == null)
                {
                    return BadRequest("Error al insertar la factura");
                }
                if (_service.SaveBudget(budget))
                {
                    return Ok("Factura Insertada con éxito!");
                }
                else
                {
                    return BadRequest("Factura NO insertada con éxito!");
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error. Ha ocurrido un error interno!");
            }
        }
    }
}
