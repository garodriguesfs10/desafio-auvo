using DesafioAuvo.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DesafioAuvo.Web.Controllers
{

    public class CsvController : Controller
    {
        private readonly ICsvService _csvService;

        public CsvController(ICsvService csvService)
        {
            _csvService = csvService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Csv/ProcessCsvAsync")]
        public async Task<JsonResult> ProcessCsvAsync([FromQuery] string path)
        {
            try
            {
                var result = await _csvService.ProcesseCsvAsync(path);
                return Json(result);
            }
            catch (Exception ex)
            {
                var erro = new { erro = ex.Message };
                return Json(erro);
            }


        }
    }
}
