using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeCertificadosApp.Domain.Enums
{
    public enum Perfil
    {
        [Description("Administrador")]
        Administrador = 1,
        [Description("Usuário")]
        Usuario = 2
    }
}
