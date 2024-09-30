using NewRepository.Dto;
using NewRepository.Models;

namespace NewRepository.Services.UsuarioService
{
    public class UsuarioService
    {
        private readonly Contexto _context;
    }
}
        /*
        public UsuarioService(Contexto contexto)
        {
            _context = contexto;
        }

        public Task<UsuarioModel> Cadastrar(UsuarioCriacaoDto usuarioCriacaoDto)
        {
            try
            {
                CriarSenhaHash(usuarioCriacaoDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                var usuario = new UsuarioModel()
                {
                    Usuario = usuarioCriacaoDto.Usuario,
                    Email = usuarioCriacaoDto.Email,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt
                };

                _context.Add(usuario);
                await _context.SaveChangesAsync();

                return usuario;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
*/