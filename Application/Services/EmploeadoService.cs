using PruebaTecnicaMaritima.Domain.Entities;
using PruebaTecnicaMaritima.Domain.Interface;
using PruebaTecnicaMaritima.Repository;
using System.Text.Json;

namespace PruebaTecnicaMaritima.Application.Services
{
    public class EmploeadoService : IEmpleado
    {
        private readonly IWebHostEnvironment _env;

        public EmploeadoService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IEnumerable<Empleado> BorrarEmpleado(string cedula)
        {
            var empleadoRepo = new EmpleadoRepository(_env);
            var empleados = empleadoRepo.AllEmployee;

            // Buscar al empleado por cédula
            var empleadoEliminado = empleados.FirstOrDefault(e => e.Document == cedula);

            if (empleadoEliminado == null)
            {
                // Si no se encuentra, retornar null para que el controlador maneje el caso
                return null;
            }

            empleados.Remove(empleadoEliminado);

            return empleados.DistinctBy(empleado => empleado.Document);
        }

        public void CreateEmployee(Empleado nuevoEmpleado)
        {
            var empleadoRepo = new EmpleadoRepository(_env);
            var empleados = empleadoRepo.AllEmployee;
            empleados.Add(nuevoEmpleado);
            var filePath = Path.Combine(_env.ContentRootPath, "./Repository/data.txt");
            var json = JsonSerializer.Serialize(empleados, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(filePath, json);
        }

        public IEnumerable<object> EmpleadoAumento()
        {
            var empleadoRepo = new EmpleadoRepository(_env);
            var empleados = empleadoRepo.AllEmployee;

            var empleadosConAumento = empleados
                .DistinctBy(empleado => empleado.Document) 
                .Select(empleado =>
                {
                    decimal porcentajeAumento = empleado.Salary > 100000 ? 0.25m : 0.30m;
                    decimal nuevoSalario = empleado.Salary + (empleado.Salary * porcentajeAumento);

                    return new
                    {
                        empleado.Name,
                        empleado.LastName,
                        empleado.Document,
                        SalarioAnterior = empleado.Salary,
                        NuevoSalario = nuevoSalario
                    };
                })
                .ToList();

            return empleadosConAumento;
        }





        public IEnumerable<Empleado> ObtenerEmpleados()
        {
            var empleadoRepo = new EmpleadoRepository(_env);
            var empleados = empleadoRepo.AllEmployee.DistinctBy(e=> e.Document);

            return empleados;
        }

        public IDictionary<string, double> PorcentajeGeneros()
        {
            var empleadoRepo = new EmpleadoRepository(_env);
            var empleados = empleadoRepo.AllEmployee;

            int totalEmpleados = empleados.Count;
            if (totalEmpleados == 0)
            {
                return new Dictionary<string, double>
        {
            { "Femenino", 0 },
            { "Masculino", 0 }
        };
            }

            int totalFemenino = empleados.Count(e => e.Gender.Equals("F", StringComparison.OrdinalIgnoreCase));
            int totalMasculino = empleados.Count(e => e.Gender.Equals("M", StringComparison.OrdinalIgnoreCase));

            double porcentajeFemenino = (totalFemenino / (double)totalEmpleados) * 100;
            double porcentajeMasculino = (totalMasculino / (double)totalEmpleados) * 100;

            return new Dictionary<string, double>
    {
        { "Femenino" , ((int)porcentajeFemenino) },
        { "Masculino", ((int)porcentajeMasculino) }
    };
        }


        public IEnumerable<Empleado> RangoSalarial(int rangoSalario)
        {
            var empleadoRepo = new EmpleadoRepository(_env);
            var empleados = empleadoRepo.AllEmployee;

            var empleadosPorSalario = empleados
                .Where(empleado => empleado.Salary <= rangoSalario)
                .OrderBy(empleado => empleado.Salary) 
                .DistinctBy(empleado => empleado.Document) 
                .ToList();

            return empleadosPorSalario;
        }


    }
}
