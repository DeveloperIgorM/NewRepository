using NewRepository.Dto;
using NewRepository.Models;

namespace NewRepository.Services.UsuarioService
{
    public interface IUsuarioInterface
    {
        Task<UsuarioModel> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto);
        //Task<UsuarioModel> Login(LoginDto loginDto);
    }
}
