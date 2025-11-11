using FluentValidation;
using GerenciadorDeCertificadosApp.Domain.DTOs.Requests;
using GerenciadorDeCertificadosApp.Domain.DTOs.Responses;
using GerenciadorDeCertificadosApp.Domain.Entities;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Repositories;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Services;
using GerenciadorDeCertificadosApp.Domain.Mappers;
using GerenciadorDeCertificadosApp.Domain.Validations;

namespace GerenciadorDeCertificadosApp.Domain.Services
{
    public class AtividadesDomainService : IAtividadesDomainService
    {
        private readonly IAtividadesRepository _atividadesRepository;
        private readonly ICertificadoAtividadesRepository _certificadoAtividadesRepository;

        public AtividadesDomainService(
            IAtividadesRepository atividadesRepository, 
            ICertificadoAtividadesRepository certificadoAtividadesRepository)
        {
            _atividadesRepository = atividadesRepository;
            _certificadoAtividadesRepository = certificadoAtividadesRepository;
        }

        public AtividadeResponseDto? CriarAtividade(AtividadeRequestDto request)
        {
            var validation = new AtividadeValidator().Validate(request);

            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            var atividadeExistente = _atividadesRepository.GetByName(request.Nome);

            if (atividadeExistente != null)
                throw new ApplicationException("Já existe uma atividade com este nome.");

            var atividade = new Atividade(request.Nome);

            _atividadesRepository.Add(atividade);

            return atividade.MapToResponseDto();
        }

        public AtividadeResponseDto? AlterarDadosAtividade(Guid id, AtividadeRequestDto request)
        {
            var validation = new AtividadeValidator().Validate(request);

            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            var atividade = _atividadesRepository.GetById(id);

            if(atividade == null)
                throw new ApplicationException("Atividade não encontrada.");

            atividade = _atividadesRepository.GetByName(request.Nome);

            if (atividade != null && atividade.Id != id)
                throw new ApplicationException("Já existe uma atividade com este nome.");

            atividade.AtualizarDados(request.Nome);

            _atividadesRepository.Update(atividade);

            return atividade.MapToResponseDto();
        }

        public void ExcluirAtividade(Guid id)
        {
            var atividade = _atividadesRepository.GetById(id);

            if (atividade == null)
                throw new ApplicationException("Atividade não encontrada.");

            if(_certificadoAtividadesRepository.IsActivityLinkedToAnyCertificate(atividade.Id))
                throw new ApplicationException("Não é possível excluir esta atividade, pois ela está vinculada a um ou mais certificados.");

            _atividadesRepository.Delete(atividade);
        }

        public AtividadeResponseDto? BuscarAtividadePorId(Guid id)
        {
            var atividade = _atividadesRepository.GetById(id);

            if (atividade == null)
                throw new ApplicationException("Atividade não encontrada.");

            return atividade.MapToResponseDto();
        }

        public List<AtividadeResponseDto>? ListarAtividades()
        {
            var atividades = _atividadesRepository.GetAll();

            return atividades?.Select(a => a.MapToResponseDto()).ToList();
        }
    }
}
