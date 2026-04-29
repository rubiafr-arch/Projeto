[ApiController]
[Route("api/v1/usuarios")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] UsuarioCreateDto usuarioDto)
    {
        var usuario = await _usuarioService.CriarUsuarioAsync(usuarioDto.Nome, usuarioDto.Email, usuarioDto.Senha);
        var resposta = new
        {
            dados_resposta = usuario,
            timestamp_resposta = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss"),
            tempo_da_resposta = "N/A"
        };
        return CreatedAtAction(nameof(BuscarPorId), new { id = usuario.Id }, resposta);
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var usuarios = await _usuarioService.ObterTodosAsync();
        var resposta = new
        {
            dados_resposta = usuarios,
            timestamp_resposta = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss"),
            tempo_da_resposta = "N/A"
        };
        return Ok(resposta);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> BuscarPorId(Guid id)
    {
        var usuario = await _usuarioService.ObterPorIdAsync(id);
        if (usuario == null)
            return NotFound();

        var resposta = new
        {
            dados_resposta = usuario,
            timestamp_resposta = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss"),
            tempo_da_resposta = "N/A"
        };
        return Ok(resposta);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] UsuarioUpdateDto usuarioDto)
    {
        var usuarioAtualizado = await _usuarioService.AtualizarUsuarioAsync(id, usuarioDto);
        if (usuarioAtualizado == null)
            return NotFound();

        var resposta = new
        {
            dados_resposta = usuarioAtualizado,
            timestamp_resposta = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss"),
            tempo_da_resposta = "N/A"
        };
        return Ok(resposta);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Desativar(Guid id)
    {
        var desativado = await _usuarioService.DesativarUsuarioAsync(id);
        if (!desativado)
            return NotFound();

        var resposta = new
        {
            dados_resposta = $"Usuário {id} desativado.",
            timestamp_resposta = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss"),
            tempo_da_resposta = "N/A"
        };
        return Ok(resposta);
    }
}