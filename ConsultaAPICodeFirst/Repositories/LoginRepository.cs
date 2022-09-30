using ConsultaAPICodeFirst.Interfaces;
using ConsultaAPICodeFirst.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using ConsultaAPICodeFirst.Context;
using System.Linq;

namespace ConsultaAPICodeFirst.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        DataContext ctx;

        

        public LoginRepository(DataContext _ctx)
        {
            ctx = _ctx;
        }

        public string Logar(string email, string senha)
        {
            Usuario usuario = ctx.Usuario.Where(u => u.Email == email).FirstOrDefault();

            if (usuario != null && senha != null && usuario.Senha.Contains("$2b$"))
            {
                if (BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
                {
                    using (IUsuarioRepository repoUsuario = new UsuarioRepository(ctx)) {
                        usuario = repoUsuario.FindById(usuario.Id);
                    }


                    //*** Credenciais do JWT para geração do token ***

                    //definição das claims
                    var minhasClaims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                        new Claim(JwtRegisteredClaimNames.Jti,  usuario.Id.ToString()),
                        new Claim(ClaimTypes.Role, usuario.TipoUsuario.Tipo.ToUpper()),
                        new Claim("Tipo", usuario.TipoUsuario.Tipo.ToUpper())
                    };

                    //criação das chaves
                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("cripto-chave-autenticacao"));

                    //criação das credenciais
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    //geração do token
                    var meuToken = new JwtSecurityToken(
                           issuer: "cripto.webAPI",
                           audience: "cripto.webAPI",
                           claims: minhasClaims,
                           expires: DateTime.Now.AddMinutes(30),
                           signingCredentials: creds
                    );

                    //*** Fim das credenciais do JWT ***

                    return new JwtSecurityTokenHandler().WriteToken(meuToken);
                }
            }

            return null;
        }
    }
}
