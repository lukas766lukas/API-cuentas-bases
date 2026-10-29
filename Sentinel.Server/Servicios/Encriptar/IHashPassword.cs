namespace Sentinel.Server.Servicios.Encriptar
{
    public interface IHashPassword
    {
        void CrearClaveHash(string password, out byte[] hash, out byte[] salt);
        bool VerificarClaveHash(string password, byte[] hash, byte[] salt);
    }
}
