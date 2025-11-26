using FluentValidation;
using GerenciadorDeCertificadosApp.Domain.DTOs.Requests;
using GerenciadorDeCertificadosApp.Domain.DTOs.Responses;
using GerenciadorDeCertificadosApp.Domain.Entities;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Repositories;
using GerenciadorDeCertificadosApp.Domain.Interfaces.Services;
using GerenciadorDeCertificadosApp.Domain.Mappers;
using GerenciadorDeCertificadosApp.Domain.Validations;
using System.Linq;

namespace GerenciadorDeCertificadosApp.Domain.Services
{
    public class CertificadosDomainService : ICertificadosDomainService
    {
        private readonly ICertificadosRepository _certificadosRepository;
        private readonly ICertificadoAtividadesRepository _certificadoAtividadesRepository;
        private readonly IAtividadesRepository _atividadesRepository;

        public CertificadosDomainService(ICertificadosRepository certificadosRepository, ICertificadoAtividadesRepository certificadoAtividadesRepository, IAtividadesRepository atividadesRepository)
        {
            _certificadosRepository = certificadosRepository;
            _certificadoAtividadesRepository = certificadoAtividadesRepository;
            _atividadesRepository = atividadesRepository;
        }

        public CertificadoResponseDto? CriarCertificado(CertificadoRequestDto request)
        {
            var validation = new CertificadoValidator().Validate(request);

            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            var certificado = new Certificado(request.Nome, request.UsuarioId);

            _certificadosRepository.Add(certificado);

            List<Atividade> atividades = request.Atividades
                .Select(a => new Atividade(a.Nome))
                .ToList();

            foreach (var atividade in atividades)
            {
                var _atividade = _atividadesRepository.GetByName(atividade.Nome);

                if (_atividade == null)
                {
                    _atividadesRepository.Add(atividade);
                    _atividade = _atividadesRepository.GetById(atividade.Id);
                }

                if (_certificadoAtividadesRepository.IsActivityAlreadyIncludedInTheCertificate(_atividade.Id, certificado.Id))
                    continue;

                _certificadoAtividadesRepository.AddCertificateWithActivities(new CertificadoAtividade
                {
                    IdCertificado = certificado.Id,
                    IdAtividade = _atividade.Id
                });
            }

            certificado = _certificadosRepository.GetById(certificado.Id);

            return certificado!.MapToResponseDto();

            //•	O operador ! (null-forgiving operator) diz ao compilador: “confie em mim, certificado não é null” — impede avisos de nullable durante a compilação.
        }

        public CertificadoResponseDto? AlterarDadosCertificado(Guid id, CertificadoRequestDto request)
        {
            var validation = new CertificadoValidator().Validate(request);

            if (!validation.IsValid)
            {
                throw new ValidationException(validation.Errors);
            }

            var certificado = _certificadosRepository.GetById(id);

            if (certificado is null)
                throw new ApplicationException("Certificado não encontrado.");

            certificado.AtualizarDados(request.Nome, certificado.UsuarioId);

            _certificadosRepository.Update(certificado);

            _certificadoAtividadesRepository.RemoveActivitiesFromCertificate(certificado.Id);

            List<Atividade> atividades = request.Atividades
                .Select(a => new Atividade(a.Nome))
                .ToList();

            foreach (var atividade in atividades)
            {
                var _atividade = _atividadesRepository.GetByName(atividade.Nome);

                if (_atividade == null)
                {
                    _atividadesRepository.Add(atividade);
                    _atividade = _atividadesRepository.GetById(atividade.Id);
                }

                if (_certificadoAtividadesRepository.IsActivityAlreadyIncludedInTheCertificate(_atividade.Id, certificado.Id))
                    continue;

                _certificadoAtividadesRepository.AddCertificateWithActivities(new CertificadoAtividade
                {
                    IdCertificado = certificado.Id,
                    IdAtividade = _atividade.Id
                });
            }

            certificado = _certificadosRepository.GetById(certificado.Id);

            return certificado!.MapToResponseDto();
        }

        public void ExcluirCertificado(Guid id)
        {
            var certificado = _certificadosRepository.GetById(id);

            if (certificado is null)
                throw new ApplicationException("Certificado não encontrado.");

            _certificadoAtividadesRepository.RemoveActivitiesFromCertificate(id);

            _certificadosRepository.Delete(certificado);
        }

        public CertificadoResponseDto? BuscarCertificadoPorId(Guid id)
        {
            var certificado = _certificadosRepository.GetById(id);

            if (certificado is null)
                throw new ApplicationException("Certificado não encontrado.");

            return certificado.MapToResponseDto();
        }

        public List<CertificadoResponseDto>? ListarCertificados(Guid? userId)
        {
            var certificados = userId == null ?
                _certificadosRepository.GetAll() :
                _certificadosRepository.GetByUserId(userId.Value);

            List<CertificadoResponseDto> certificadosDto = new List<CertificadoResponseDto>();

            if (certificados is null || certificados.Count == 0)
                return certificadosDto;
            else
            {
                foreach (var certificado in certificados)
                {
                    certificadosDto.Add(certificado.MapToResponseDto());
                }

                return certificadosDto;
            }
        }
    }
}
