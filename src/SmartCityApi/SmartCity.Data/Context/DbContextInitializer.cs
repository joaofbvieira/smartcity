using SmartCity.Domain.Models.Cidades;
using SmartCity.Domain.Models.Ocorrencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartCity.Data.Context
{
    public class DbContextInitializer
    {
        public static void InicializarDb(SmartCityContext context)
        {
            if (context.Cidades.Any())
                return;

            context.Cidades.Add(new Cidade
            {
                CidadeId = 1,
                Nome = "Joinville",
                CEP = "89200-000",
                IBGE = "4209102",
                UF = "SC"
            });

            context.MotivosDenuncia.Add(new MotivoDenuncia()
            {
                MotivoDenunciaId = 1,
                Descricao = "Não é verdadeira"
            });

            context.MotivosDenuncia.Add(new MotivoDenuncia()
            {
                MotivoDenunciaId = 2,
                Descricao = "Já foi atendida"
            });

            context.MotivosDenuncia.Add(new MotivoDenuncia()
            {
                MotivoDenunciaId = 9,
                Descricao = "Outro motivo"
            });

            context.SaveChanges();
        }

    }
}
