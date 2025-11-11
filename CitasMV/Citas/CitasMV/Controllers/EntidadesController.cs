 using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CitasMV.Controllers
{
    public class EntidadesController : Controller
    {
        public IActionResult EmpresasTerceros()
        {
            return View();
        }
    }
}