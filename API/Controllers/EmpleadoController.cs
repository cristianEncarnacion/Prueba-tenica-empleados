using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaMaritima.Domain.Entities;
using PruebaTecnicaMaritima.Domain.Interface;
using PruebaTecnicaMaritima.Repository;

namespace PruebaTecnicaMaritima.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleado _empleadoService;
        private readonly IWebHostEnvironment _env;

     

        public EmpleadoController(IEmpleado empleadoService, IWebHostEnvironment env)
        {
            _empleadoService = empleadoService;
            _env = env;

        }
     

        [HttpGet]
        public IActionResult ObtenerEmpleados()
        {
            var empleados = _empleadoService.ObtenerEmpleados();
            return Ok(empleados);
        }

        [HttpPost("CreateEmployee")]
        public IActionResult CrearEmpleado([FromBody] Empleado nuevoEmpleado)
        {
            if (nuevoEmpleado == null ||
                string.IsNullOrWhiteSpace(nuevoEmpleado.Name) ||
                string.IsNullOrWhiteSpace(nuevoEmpleado.LastName) ||
                string.IsNullOrWhiteSpace(nuevoEmpleado.Document) ||
                string.IsNullOrWhiteSpace(nuevoEmpleado.Salary.ToString()) ||
                string.IsNullOrWhiteSpace(nuevoEmpleado.Gender) ||
                string.IsNullOrWhiteSpace(nuevoEmpleado.Position) ||
                string.IsNullOrWhiteSpace(nuevoEmpleado.StartDate))
            {
                return BadRequest("Completa todos los campos obligatorios.");
            }
            var empleadoRepo = new EmpleadoRepository(_env);
            var empleados = empleadoRepo.AllEmployee;
            empleados.Add(nuevoEmpleado);

            return Ok(new { Message = "Empleado creado exitosamente", Empleado = empleados.DistinctBy(e=> e.Document) });
        }

        [HttpGet("rango-salarial/{rangoSalario}")]
        public IActionResult EmpleadosPorSalario(int rangoSalario)
        {
            var empleados = _empleadoService.RangoSalarial(rangoSalario);
            return Ok(empleados);
        }
        [HttpGet("empleados-aumento")]
        public IActionResult EmpleadosConAumento()
        {
            var empleadosConAumento = _empleadoService.EmpleadoAumento();
            return Ok(empleadosConAumento);
        }

        [HttpGet("porcentaje-genero")]
        public IActionResult PorcentajeGenero()
        {
            var empleadosConAumento = _empleadoService.PorcentajeGeneros();
            return Ok(empleadosConAumento);
        }

        [HttpDelete("Borrar-empleado/{cedula}")]
        public IActionResult BorrarEmpleado(string cedula)
        {
            var empleados = _empleadoService.BorrarEmpleado(cedula);
            

            return Ok( empleados);
        }





    }
}
