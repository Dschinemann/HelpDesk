using HelpDesk.Models;
using HelpDesk.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace HelpDesk.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SalvarHelpDesk _service;

        public HomeController(ILogger<HomeController> logger, SalvarHelpDesk service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            Models.HelpDesk model = new Models.HelpDesk();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Nome,Filial,Categoria,Tipo,Modulo,Departamento,Assunto,Severidade,Anexo,Descricao")] Models.HelpDesk hd)
        {
            if (ModelState.IsValid)
            {
                string? message = null; ;
                try
                {
                    message = await _service.SalvarHd(hd);
                    Models.Response? resp = JsonSerializer.Deserialize<Models.Response>(message);
                    return View("Info", new Response { Result = message, Assunto = resp.Assunto, Hd = resp.Hd, Erro = resp.Erro });
                }
                catch(JsonException js)
                {
                    return View(nameof(Error), new ErrorViewModel { Message = "Retorno do servidor", Error = message ?? js.Message});
                }
                catch (Exception ex)
                {
                    return View(nameof(Error), new ErrorViewModel { Message = ex.Message, Error = ex.StackTrace ?? "" }) ;
                }

            }
            return View(nameof(Index), hd);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message });
        }
    }
}