using Microsoft.AspNetCore.Mvc;
using SmartCity.Application.Ocorrencias;
using SmartCity.ViewModel;
using SmartCity.ViewModel.Ocorrencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCity.WepAPI.Controllers
{
    [Route("api/[controller]")]
    public class OcorrenciaController : Controller
    {
        IOcorrenciaService _ocorrenciaService;

        public OcorrenciaController(IOcorrenciaService ocorrenciaService)
        {
            _ocorrenciaService = ocorrenciaService;
        }

        [HttpPost("Adicionar")]
        public ResultBase Adicionar([FromBody] OcorrenciaViewModel ocorrencia)
        {
            return _ocorrenciaService.AdicionarOcorrencia(ocorrencia);
        }

        [HttpGet("BuscarPorId")]
        public ResultBase BuscarPorId(int id)
        {
            return _ocorrenciaService.BuscarOcorrenciaPorId(id);
        }

        [HttpGet("BuscarImagensPorOcorrencia")]
        public ResultBase BuscarImagensPorOcorrencia(int idOcorrencia)
        {
            return _ocorrenciaService.BuscarImagensPorOcorrencia(idOcorrencia);
        }

        [HttpGet("BuscarOcorrenciasPorUsuario")]
        public ResultBase BuscarOcorrenciasPorUsuario(int idUsuario)
        {
            return _ocorrenciaService.BuscarOcorrenciaPorUsuario(idUsuario);
        }

        [HttpPost("VotarEmOcorrencia")]
        public ResultBase VotarEmOcorrencia([FromBody] VotoOcorrenciaViewModel voto)
        {
            return _ocorrenciaService.VotarEmOcorrencia(voto);
        }

        [HttpPost("BuscarOcorrenciasProximas")]
        public ResultBase BuscarOcorrenciasProximas([FromBody] ConsultaOcorrenciasProximasViewModel coordenadas)
        {
            return _ocorrenciaService.BuscarOcorrenciasProximas(coordenadas);
        }

    }
}
