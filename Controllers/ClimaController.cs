using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplicationApiClime.Sqlite.Repositories.Interfaces;

namespace WebApplicationApiClime.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClimaController : Controller
    {
        private readonly ILogRepository _logRepository;
        private readonly IClimaAeroportoRepository _climaAeroportoRepository;
        private readonly IClimaCidadeRepository _climaCidadeRepository;

        public ClimaController(ILogRepository logRepository, IClimaAeroportoRepository climaAeroportoRepository, IClimaCidadeRepository climaCidadeRepository)
        {
            _logRepository = logRepository;
            _climaAeroportoRepository = climaAeroportoRepository;
            _climaCidadeRepository = climaCidadeRepository;
        }

        [HttpGet("aeroporto/{codigo}")]
        [ProducesResponseType(typeof(ClimaAeroporto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetClimaAeroporto(string codigo)
        {
            try
            {
                string url = $"https://brasilapi.com.br/api/cptec/v1/clima/aeroporto/{codigo}";

                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = httpClient.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string conteudo = response.Content.ReadAsStringAsync().Result;
                        ClimaAeroporto clima = JsonConvert.DeserializeObject<ClimaAeroporto>(conteudo);
                        _climaAeroportoRepository.InsertClimaAeroporto(clima);
                        return Ok(clima);
                    }
                    else
                    {
                        Log log = new Log()
                        {
                            Rota = "aeroporto",
                            Parametro = codigo,
                            Erro = $"{response.StatusCode} - {response.ReasonPhrase}"
                        };
                        _logRepository.InsertLog(log);
                        return BadRequest($"Erro ao obter o clima do aeroporto {codigo}.");
                    }
                }
            }
            catch (Exception ex) 
            {
                Log log = new Log()
                {
                    Rota = "aeroporto",
                    Parametro = codigo,
                    Erro = ex.Message
                };
                _logRepository.InsertLog(log);
                return BadRequest($"Erro ao obter o clima do aeroporto {codigo}.");
            }
        }

        [HttpGet("cidade/{codigoCidade}")]
        [ProducesResponseType(typeof(ClimaCidade), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetClimaCidade(string codigoCidade)
        {
            try
            {
                string url = $"https://brasilapi.com.br/api/cptec/v1/clima/previsao/{codigoCidade}";

                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = httpClient.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string conteudo = response.Content.ReadAsStringAsync().Result;
                        ClimaCidade clima = JsonConvert.DeserializeObject<ClimaCidade>(conteudo);
                        _climaCidadeRepository.InsertClimaCidade(clima);
                        return Ok(clima);
                    }
                    else
                    {
                        Log log = new Log()
                        {
                            Rota = "cidade",
                            Parametro = codigoCidade,
                            Erro = $"{response.StatusCode} - {response.ReasonPhrase}"
                        };
                        _logRepository.InsertLog(log);
                        return BadRequest($"Erro ao obter o clima da cidade {codigoCidade}.");
                    }
                }
            }
            catch (Exception ex) 
            {
                Log log = new Log()
                {
                    Rota = "cidade",
                    Parametro = codigoCidade,
                    Erro = ex.Message
                };
                _logRepository.InsertLog(log);
                return BadRequest($"Erro ao obter o clima da cidade {codigoCidade}.");
            }
        }
    }
}
