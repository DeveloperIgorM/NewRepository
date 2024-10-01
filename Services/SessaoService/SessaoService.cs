﻿using NewRepository.Models;
using Newtonsoft.Json;

namespace NewRepository.Services.SessaoService
{
    public class SessaoService : ISessaoInterface
    {

        //injeção de depêndendia
        private readonly IHttpContextAccessor _httpAcessor;
        public SessaoService(IHttpContextAccessor httpAccessor)
        {
            _httpAcessor = httpAccessor;
        }

        public UsuarioModel BuscarSessao()
        {
            string sessaoUsuario = _httpAcessor.HttpContext.Session.GetString("UsuarioAtivo");
            if(sessaoUsuario == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }

        public void CriarSessao(UsuarioModel usuario)
        {
            string usuarioJson = JsonConvert.SerializeObject(usuario);
            _httpAcessor.HttpContext.Session.SetString("UsuarioAtivo", usuarioJson);
        }

        public void RemoverSessao()
        {
            _httpAcessor.HttpContext.Session.Remove("UsuarioAtivo");
        }
    }
}