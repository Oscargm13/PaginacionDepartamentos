using Microsoft.AspNetCore.Mvc;
using PaginacionDepartamentos.Models;
using PaginacionDepartamentos.Repositories;

namespace PaginacionDepartamentos.Controllers
{
    public class DepartamentosController : Controller
    {
        private RepositoryDepartamentos repo;
        public DepartamentosController(RepositoryDepartamentos repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Departamento> departamentos = await this.repo.GetDepartamentos();
            return View(departamentos);
        }

        public async Task<IActionResult> Empleados(int? posicion, int idDepartamento)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            ModelEmpleados model = await this.repo.GetEmpleadosPaginadosAsync(posicion.Value, idDepartamento);
            ViewData["REGISTROS"] = model.totalRegistros;
            ViewData["IDDEPARTAMENTO"] = idDepartamento;

            return View(model.empleados);
        }
    }
}

//public async Task<IActionResult> PaginarDepartamentos(int? posicion)
//{
//    if(posicion == null)
//    {
//        posicion = 1;
//    }
//    List<Departamento> departamentos = await this.repo.GetDepartamentosAsync(posicion.Value);
//    int numRegistros = await this.repo.GetDepartamentosCountAsync();
//    ViewData["REGISTROS"] = numRegistros;
//    return View(departamentos);
//}

