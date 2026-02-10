using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Token
{
    // Class utilitaire pour générer un JWT (Json Web Token)
    // - Token qui permettra d'identifier un utilisateur
    public class TokenTool
    {
        // ↓ Injection de l'outils pour acceder au fichier de config
        private readonly IConfiguration _config;

        public TokenTool(IConfiguration config)
        {
            _config = config;
        }


        // ↓ Classe pour representer les données contenu dans le token
        public class Data
        {
            public required long MemberId { get; set; }
            public required string Role { get; set; }
        }

        // ↓ Méthode pour générer le token
        public string Generate(Data data)
        {
            // Ensemble des données contenu dans le token via « Claim »
            Claim[] claims = [
                new Claim("clef", "La réponse est 42"),
                new Claim(ClaimTypes.NameIdentifier, data.MemberId.ToString()),
                new Claim(ClaimTypes.Role, data.Role)
            ];

            // La signature du token
            byte[] key = Encoding.UTF8.GetBytes(_config["Token:Key"]!);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            // Le token :o
            JwtSecurityToken token = new JwtSecurityToken(
                  issuer: _config["Token:Issuer"],                                          // Identité qui emet le token
                  audience: _config["Token:Audience"],                                      // Context d'utilisation du token
                  expires: DateTime.Now.AddMinutes(_config.GetValue<int>("Token:Expire")),  // Date d'expiration
                  claims: claims,                                                           // Les données contenus
                  signingCredentials: signingCredentials                                    // Signature
            );

            // Renvoi du token sous forme d'une chaine de caractere
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
