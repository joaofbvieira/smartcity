using System;
using System.Collections.Generic;
using System.Linq;
using SmartCity.ViewModel;
using SmartCity.ViewModel.Ocorrencias;
using SmartCity.Data.Infrastructure;
using SmartCity.Data.Repositories.Interfaces;
using SmartCity.Domain.Models.Ocorrencias;
using System.Globalization;

namespace SmartCity.Application.Ocorrencias
{
    public class OcorrenciaService : IOcorrenciaService
    {
        IOcorrenciaRepository _ocorrenciaRepo;
        IVotoOcorrenciaRepository _votoOcorrenciaRepo;
        IUnitOfWork _unitOfWork;

        public OcorrenciaService(
            IOcorrenciaRepository ocorrenciaRepo, 
            IUnitOfWork unitOfWork, 
            IVotoOcorrenciaRepository votoOcorrenciaRepo)
        {
            _ocorrenciaRepo = ocorrenciaRepo;
            _unitOfWork = unitOfWork;
            _votoOcorrenciaRepo = votoOcorrenciaRepo;
        }
        
        public ResultBase AdicionarOcorrencia(OcorrenciaViewModel ocorrenciaVm)
        {
            var result = new ResultBase();

            try
            {
                var ocorrencia = new Ocorrencia();
                ocorrencia.DataHoraInclusao = DateTime.Now;
                ocorrencia.Descricao = ocorrenciaVm.Descricao;
                ocorrencia.UsuarioId = ocorrenciaVm.UsuarioId;
                ocorrencia.Latitude = ocorrenciaVm.Latitude;
                ocorrencia.Longitude = ocorrenciaVm.Longitude;
                ocorrencia.EnderecoCompleto = ocorrenciaVm.EnderecoCompleto;
                ocorrencia.Atendida = false;

                ocorrencia.Imagens = new List<OcorrenciaImagem>();
                ocorrencia.Imagens.Add(
                        new OcorrenciaImagem()
                        {
                            Conteudo = ocorrenciaVm.Imagem.Base64ToByteArray(ocorrenciaVm.Imagem.Conteudo),
                            DataHoraInclusao = DateTime.Now,
                            Extensao = ocorrenciaVm.Imagem.Extensao
                        });
                
                if (ocorrencia.PermitirGravacao())
                {
                    _ocorrenciaRepo.Inserir(ocorrencia);
                    _unitOfWork.Commit();
                    result.Sucesso = true;
                }
                else
                {
                    result.Mensagem = "Não é permitir gravar ocorrências sem foto.";
                    result.Sucesso = false;
                }
            }
            catch (Exception ex)
            {
                result.Sucesso = false;
                result.Mensagem = "Não foi possível inserir a ocorrência.";
                result.Excecao = ex;
            }
            
            return result;
        }
        
        public ResultBase BuscarOcorrenciaPorUsuario(int idUsuario)
        {
            var result = new ResultBase();

            try
            {
                var ocorrencias = _ocorrenciaRepo.BuscarOcorrenciasPorUsuario(idUsuario, DateTime.Now.AddDays(-30));
                var ocorrenciasVM = new ConsultaOcorrenciasUsuarioViewModel();

                foreach (var ocorrencia in ocorrencias.Where(o => o.Atendida))
                {
                    var ocorrenciaVm = ConverterOcorrenciaEmViewModel(ocorrencia);
                    ocorrenciaVm.QtdeVotos = _votoOcorrenciaRepo.ObterQtdeVotosDaOcorrencia(ocorrencia.OcorrenciaId);
                    ocorrenciasVM.Atendidas.Add(ocorrenciaVm);
                }

                foreach (var ocorrencia in ocorrencias.Where(o => !o.Atendida))
                {
                    var ocorrenciaVm = ConverterOcorrenciaEmViewModel(ocorrencia);
                    ocorrenciaVm.QtdeDenuncias = _votoOcorrenciaRepo.ObterQtdeDenunciasDaOcorrencia(ocorrencia.OcorrenciaId);
                    ocorrenciaVm.QtdeVotos = _votoOcorrenciaRepo.ObterQtdeVotosDaOcorrencia(ocorrencia.OcorrenciaId);

                    if (ocorrenciaVm.QtdeDenuncias > 0)
                        ocorrenciasVM.Denunciadas.Add(ocorrenciaVm);
                    else
                        ocorrenciasVM.Pendentes.Add(ocorrenciaVm);
                }
                
                result.Sucesso = true;
                result.Dados = ocorrenciasVM;
            }
            catch (Exception ex)
            {
                result.Sucesso = false;
                result.Mensagem = "Não foi possível consultar todas as ocorrências.";
                result.Excecao = ex;
            }

            return result;
        }

        public ResultBase BuscarOcorrenciaPorId(int id)
        {
            var result = new ResultBase();
            
            try
            {
                var ocorrencia = _ocorrenciaRepo.BuscarPorId(id);
                OcorrenciaViewModel ocorrenciaVm = ConverterOcorrenciaEmViewModel(ocorrencia);
                result.Sucesso = true;
                result.Dados = ocorrenciaVm;
            }
            catch (Exception ex)
            {
                result.Sucesso = false;
                result.Mensagem = "Não foi possível consultar todas as ocorrências.";
                result.Excecao = ex;
            }

            return result;
        }

        public ResultBase BuscarImagensPorOcorrencia(int idOcorrencia)
        {
            var result = new ResultBase();

            try
            {
                var imagens = _ocorrenciaRepo.BuscarImagensPorIdOcorrencia(idOcorrencia);

                IEnumerable<OcorrenciaImagemViewModel> imagensVm = 
                    imagens.Select(i => ConverterImagemOcorrenciaEmViewModel(i));

                result.Sucesso = true;
                result.Dados = imagensVm;
            }
            catch (Exception ex)
            {
                result.Sucesso = false;
                result.Mensagem = "Não foi possível consultar todas as ocorrências.";
                result.Excecao = ex;
            }

            return result;
        }

        public ResultBase VotarEmOcorrencia(VotoOcorrenciaViewModel votoVm)
        {
            var result = new ResultBase();

            try
            {
                var ocorrencia = _ocorrenciaRepo.BuscarPorId(votoVm.OcorrenciaId);

                if (ocorrencia.UsuarioId == votoVm.UsuarioId)
                {
                    result.Sucesso = false;
                    result.Mensagem = "Não pode votar na sua própria ocorrência.";
                    return result;
                }

                int qtdeVotos = _votoOcorrenciaRepo.ObterQtdeVotosPorUsuarioEOcorrencia(votoVm.UsuarioId, votoVm.OcorrenciaId);
                if (qtdeVotos > 0)
                {
                    result.Sucesso = false;
                    result.Mensagem = "Não pode votar novamente na mesma ocorrência.";
                    return result;
                }

                var voto = new VotoOcorrencia();
                voto.OcorrenciaId = votoVm.OcorrenciaId;
                voto.DataHoraInclusao = DateTime.Now;
                voto.Positivo = votoVm.Positivo;
                voto.UsuarioId = votoVm.UsuarioId;

                if (!voto.Positivo)
                {
                    voto.Comentario = votoVm.Comentario;
                    voto.MotivoDenunciaId = votoVm.MotivoDenunciaId;
                }

                _votoOcorrenciaRepo.Inserir(voto);
                _unitOfWork.Commit();

                result.Sucesso = true;
            }
            catch (Exception ex)
            {
                result.Sucesso = false;
                result.Mensagem = "Não foi possível votar na ocorrência.";
                result.Excecao = ex;
            }
            
            return result;
        }

        public ResultBase BuscarOcorrenciasProximas(ConsultaOcorrenciasProximasViewModel coordenadas)
        {
            var result = new ResultBase();

            try
            {
                var ocorrencias = _ocorrenciaRepo.BuscarOcorrenciasNoRaioDeKm(
                    coordenadas.RaioKm, 
                    Double.Parse(coordenadas.Latitude, CultureInfo.InvariantCulture), 
                    Double.Parse(coordenadas.Longitude, CultureInfo.InvariantCulture));

                var ocorrenciasVM = new ConsultaOcorrenciasProximasResultViewModel();

                foreach (var ocorrencia in ocorrencias)
                    ocorrenciasVM.Ocorrencias.Add(ConverterOcorrenciaEmViewModel(ocorrencia));
                
                result.Sucesso = true;
                result.Dados = ocorrenciasVM;
            }
            catch (Exception ex)
            {
                result.Sucesso = false;
                result.Mensagem = "Não foi possível consultar as ocorrências próximas a você.";
                result.Excecao = ex;
            }

            return result;
        }

        private OcorrenciaViewModel ConverterOcorrenciaEmViewModel(Ocorrencia ocorrencia)
        {
            var ocorrenciaVm = new OcorrenciaViewModel()
            {
                OcorrenciaId = ocorrencia.OcorrenciaId,
                Descricao = ocorrencia.Descricao,
                UsuarioId = ocorrencia.UsuarioId,
                Latitude = ocorrencia.Latitude,
                Longitude = ocorrencia.Longitude,
                EnderecoCompleto = ocorrencia.EnderecoCompleto,
                Data = ocorrencia.DataHoraInclusao.ToString("dd/MM/yyyy"),
                Hora = ocorrencia.DataHoraInclusao.ToString("HH:mm")
            };
            
            if (ocorrencia.Imagens != null)
            {
                foreach (var imagem in ocorrencia.Imagens)
                {
                    ocorrenciaVm.Imagem = ConverterImagemOcorrenciaEmViewModel(imagem);
                    break;
                }
            }

            return ocorrenciaVm;
        }

        private OcorrenciaImagemViewModel ConverterImagemOcorrenciaEmViewModel(OcorrenciaImagem imagem)
        {
            var imagemVM = new OcorrenciaImagemViewModel();
            imagemVM.Extensao = imagem.Extensao;
            imagemVM.Conteudo = imagemVM.ByteArrayToBase64(imagem.Conteudo);
            return imagemVM;
        }
    }
}
