using AutoMapper;
using CleanArchitecture.Application.Amigos.Queries.GetAmigos;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Emprestimos.Queries;
using CleanArchitecture.Application.Jogos.Queries.GetJogos;
using CleanArchitecture.Domain.Entities;
using NUnit.Framework;
using System;

namespace CleanArchitecture.Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Test]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }
                
        [Test]
        [TestCase(typeof(Amigo), typeof(AmigoDto))]
        [TestCase(typeof(Jogo), typeof(JogoDto))]
        [TestCase(typeof(Emprestimo), typeof(EmprestimoDto))]
        public void ShouldSupportMappingFromSourceToDestinationEx(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }
    }
}
