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

        private List<RegistroCartaViewModel> LerCartaDoArquivo()
        {
            if (!System.IO.File.Exists(_cartacaminhoArquivo))
            {
                return new List<RegistroCartaViewModel>();
            }

            string json = System.IO.File.ReadAllText(_cartacaminhoArquivo);
            return JsonConvert.DeserializeObject<List<RegistroCartaViewModel>>(json);
        }

        private void EscreverJogosNoArquivo(List<RegistroCartaViewModel> jogos)
        {
            string json = JsonConvert.SerializeObject(jogos);
            System.IO.File.WriteAllText(_cartacaminhoArquivo, json);
        }

        #endregion

        #region POST
        [HttpPost]
        public IActionResult Post(RegistroCartaViewModel registroCartaViewModel)
        {

            var jogosRealizaddos = LerCartaDoArquivo();

            jogosRealizaddos.Add(registroCartaViewModel);

            EscreverJogosNoArquivo(jogosRealizaddos);

            return Ok("Jogo registrado com sucesso");
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
