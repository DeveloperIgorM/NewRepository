using NewRepository.Models;

namespace NewRepository.Services.SessaoService
{
    public interface ISessaoInterface
    {
        UsuarioModel BuscarSessao();
        
            void CriarSessao(UsuarioModel usuario);

            void RemoverSessao();
    }
    
}
