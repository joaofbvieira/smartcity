using Autofac;
using Autofac.Extensions.DependencyInjection;
using SmartCity.Data.Infrastructure;
using SmartCity.Data.Repositories.Interfaces;
using SmartCity.Domain.Models.Ocorrencias;
using SmartCity.IoC;
using System;
using System.Linq;
using Xunit;

namespace SmartCity.Data.XUnitTest
{
    public class OcorrenciaRepositoryTest
    {
        [Fact]
        public void InserirOcorrencia_SemImagens_Sucesso_Test()
        {
            var di = new AutofacContainerGenerator();
            di.CarregarDependenciasTestesUnitarios();
            
            using (var dbFactory = di.Container.Resolve<IDbFactory>())
            {
                var repo = di.Container.Resolve<IOcorrenciaRepository>();
                var uoW = di.Container.Resolve<IUnitOfWork>();

                var ocorrencia = new Ocorrencia();
                ocorrencia.DataHoraInclusao = DateTime.Now;
                ocorrencia.Descricao = "Teste unitário";
                ocorrencia.UsuarioId = 1;

                repo.Inserir(ocorrencia);

                uoW.Commit();
            }

            Assert.True(true);
        }

        [Fact]
        public void BuscarOcorrenciaPorId_ComFKs_Sucesso_Test()
        {
            var di = new AutofacContainerGenerator();
            di.CarregarDependenciasTestesUnitarios();

            using (var dbFactory = di.Container.Resolve<IDbFactory>())
            {
                var repo = di.Container.Resolve<IOcorrenciaRepository>();

                var ocorrencia = repo.BuscarPorId(4);
            }
            
            Assert.True(true);
        }




        [Fact]
        public void OcorrenciasProximas_Test()
        {
            double RAIO_TERRESTRE = 6371;
            double PARAMETRO_LATITUDE = -26.343946214607882;
            double PARAMETRO_LONGITUDE = -48.84473219339356;


            var di = new AutofacContainerGenerator();
            di.CarregarDependenciasTestesUnitarios();

            using (var dbFactory = di.Container.Resolve<IDbFactory>())
            {
                var dbContext = dbFactory.Inicializar();

                foreach (var o in dbContext.Ocorrencias)
                {
                    var calc = 
                        RAIO_TERRESTRE *
                        Math.Acos(
                            Math.Cos(DegreeToRadian(PARAMETRO_LATITUDE)) *
                            Math.Cos(DegreeToRadian(o.Latitude)) *
                            Math.Cos(DegreeToRadian(PARAMETRO_LONGITUDE) - DegreeToRadian(o.Longitude)) +
                            Math.Sin(DegreeToRadian(PARAMETRO_LATITUDE)) *
                            Math.Sin(DegreeToRadian(o.Latitude))
                    );
                    
                    //var calc = (RAIO_TERRESTRE * Math.Acos(Math.Cos(DegreeToRadian(PARAMETRO_LATITUDE)) * Math.Cos(DegreeToRadian(o.Latitude)) * Math.Cos(DegreeToRadian(PARAMETRO_LONGITUDE) - DegreeToRadian(o.Longitude)) + Math.Sin(DegreeToRadian(PARAMETRO_LATITUDE)) * Math.Sin(DegreeToRadian(o.Latitude))));
                    bool mais5Km = calc <= 5;
                }

                /*
                var ocorrencias = dbContext.Ocorrencias
                    .Where(o =>  <= 5)
                    .ToList();
                    */
                //Assert.True(ocorrencias != null);
            }
            
            Assert.True(true);
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private double DegreeToRadian(string angle)
        {
            double dblAngle = Convert.ToDouble(angle);
            return Math.PI * dblAngle / 180.0;
        }

    }
}
