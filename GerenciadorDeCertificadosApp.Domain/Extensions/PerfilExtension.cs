using GerenciadorDeCertificadosApp.Domain.Enums;
using System.ComponentModel;
using System.Reflection;

namespace GerenciadorDeCertificadosApp.Domain.Extensions
{
    public static class PerfilExtension
    {
        public static string GetDescription(this Perfil perfil)
        {
            //Obtém o campo do enum correspondente ao valor atual.
            var field = perfil.GetType().GetField(perfil.ToString());

            //Busca o atributo [Description] aplicado ao campo.
            var attr = field?.GetCustomAttribute<DescriptionAttribute>();

            //Se o atributo existir, retorna sua descrição; caso contrário, retorna o nome do enum.
            return attr?.Description ?? perfil.ToString();
        }
    }
}
