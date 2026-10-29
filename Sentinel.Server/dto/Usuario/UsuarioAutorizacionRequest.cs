namespace Sentinel.Server.dto.Usuario
{
    public class UsuarioAutorizacionRequest
    {
        public string Token {  get; set; } = string.Empty;
        public bool Resultado {  get; set; }

        public string Msg { get; set; } = string.Empty;
    }
}
