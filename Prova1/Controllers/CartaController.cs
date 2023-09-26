using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prova1.Models.Request;

namespace Prova1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartaController : ControllerBase
    {

        private readonly string _cartacaminhoArquivo;

        public CartaController()
        {
            _cartacaminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Carta.json");
        }

        #region Operaçoes arquivo

        private List<RegistraCartaViewModel> LerCartaDoArquivo()
        {
            if (!System.IO.File.Exists(_cartacaminhoArquivo))
            {
                return new List<RegistraCartaViewModel>();
            }

            string json = System.IO.File.ReadAllText(_cartacaminhoArquivo);
            return JsonConvert.DeserializeObject<List<RegistraCartaViewModel>>(json);
        }

        private void EscreverCartasNoArquivo(List<RegistraCartaViewModel> cartas)
        {
            string json = JsonConvert.SerializeObject(cartas);
            System.IO.File.WriteAllText(_cartacaminhoArquivo, json);
        }

        #endregion

        #region POST

        [HttpPost]
        public IActionResult Post(RegistraCartaViewModel registraCartaViewModel)
        {

            var CartasRealizadas = LerCartaDoArquivo();

            CartasRealizadas.Add(registraCartaViewModel);

            EscreverCartasNoArquivo(CartasRealizadas);

            return Ok("Carta registrada com sucesso");
        }

        #endregion

        #region GET 

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(LerCartaDoArquivo());
        }

        #endregion
    }
}
