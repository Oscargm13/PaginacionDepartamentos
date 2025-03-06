using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PaginacionDepartamentos.Data;
using PaginacionDepartamentos.Models;
#region
/*
 create view V_DEPARTAMENTOS_INDIVIDUAL
as
select cast (ROW_NUMBER() over (order by DEPT_NO)as int) AS POSICION
, DEPT_NO, DNOMBRE, LOC from DEPT
go
 */
#endregion
namespace PaginacionDepartamentos.Repositories
{
    public class RepositoryDepartamentos
    {
        private HospitalContext context;
        public RepositoryDepartamentos(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Departamento>> GetDepartamentos()
        {
            return await this.context.Departamentos.ToListAsync();
        }

        public async Task<ModelEmpleados> GetEmpleadosPaginadosAsync(int posicion, int idDepartamento)
        {
            var registrosParam = new SqlParameter("@registros", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var posicionParam = new SqlParameter("@posicion", posicion);
            var idDepartamentoParam = new SqlParameter("@iddepartamento", idDepartamento);

            try
            {
                var empleados = await context.Empleados.FromSqlRaw("SP_PAGINAR_EMPLEADOS_DEPARTAMENTO @posicion, @iddepartamento, @registros OUT", posicionParam, idDepartamentoParam, registrosParam).ToListAsync(); // Cambiado el nombre del procedimiento almacenado

                int totalRegistros = (int)registrosParam.Value;

                return new ModelEmpleados
                {
                    empleados = empleados,
                    totalRegistros = totalRegistros
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al ejecutar SP_PAGINAR_EMPLEADOS_DEPARTAMENTO: {ex.Message}");
                return new ModelEmpleados
                {
                    empleados = new List<Empleado>(),
                    totalRegistros = 0
                };
            }
        }
    }
}
//public async Task<List<Departamento>> GetDepartamentosAsync(int posicion)
//{
//    string sql = "SP_GRUPO_DEPARTAMENTOS @posicion";
//    SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
//    var consulta = this.context.Departamentos.FromSqlRaw(sql, pamPosicion);
//    return await consulta.ToListAsync();
//}

//public async Task<int> GetDepartamentosCountAsync()
//{
//    return await this.context.Departamentos.CountAsync();
//}