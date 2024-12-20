using PruebaTecnicaMaritima.Domain.Entities;
using System.Text.Json;

namespace PruebaTecnicaMaritima.Repository
{
    public class EmpleadoRepository
    {
        public List<Empleado> AllEmployee { get; private set; }
        public EmpleadoRepository(IWebHostEnvironment env) {
            var filePath = Path.Combine(env.ContentRootPath, "./Repository/data.txt");
            var json = File.ReadAllText(filePath);
            AllEmployee = JsonSerializer.Deserialize<List<Empleado>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Empleado>();
        }
    }
}
