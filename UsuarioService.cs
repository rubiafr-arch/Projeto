public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Usuario> CriarUsuarioAsync(string nome, string email, string senha)
    {
        var senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);

        var usuario = new Usuario
        {
            Nome = nome,
            Email = email,
            SenhaHash = senhaHash,
            CriadoEm = DateTime.UtcNow
        };

        await _usuarioRepository.AddAsync(usuario);
        return usuario;
    }

    public async Task<IEnumerable<Usuario>> ObterTodosAsync() => await _usuarioRepository.GetAllAsync();
}