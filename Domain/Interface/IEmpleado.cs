using PruebaTecnicaMaritima.Domain.Entities;

namespace PruebaTecnicaMaritima.Domain.Interface
{
    public interface IEmpleado
    {
        public void CreateEmployee(Empleado nuevoEmpleado);
        IEnumerable<Empleado> RangoSalarial(int rangosSalariales);
        IEnumerable<Empleado>  ObtenerEmpleados();
        IEnumerable<object> EmpleadoAumento();
        IDictionary<string, double> PorcentajeGeneros();

        IEnumerable<Empleado> BorrarEmpleado(string cedula);







    }
}

