namespace Game.Core.Services;

/// <summary>
/// Nível de severidade de uma mensagem de log.
/// Veja cada um dos valores para exemplos de quando usá-los.
/// </summary>
/// <remarks>
/// O nome é `LogLvl` para evitar conflitos de nome com o `LogLevel` do unity.
/// </remarks>
public enum LogLvl
{
    /// <summary>
    /// Usado para depuração, tipicamente não estará disponível em Release.<br/>
    /// Útil para desenvolvimento ou investigação.
    /// <example>
    /// Quando usar:
    /// - Estado interno e variáveis: "Player HP antes do cálculo: 42"
    /// - Fluxo detalhado: "Entrando em CalculateDamage() com crit=true"
    /// - Eventos muito frequentes: "Tick de física executado"
    /// </example>
    /// </summary>
    Debug,

    /// <summary>
    /// Mensagens informativas sobre o fluxo relevante.
    /// <example>
    /// Quando usar:
    /// - Inicializações: "Sistema de combate inicializado"
    /// - Ações do jogador: "Carta 'Fireball' jogada"
    /// - Mudanças de estado importantes: "Fase mudou para BattlePhase"
    /// </example>
    /// </summary>
    Info,

    /// <summary>
    /// Situações inesperadas, mas que não impedem a execução do jogo.
    /// <example>
    /// - Fallbacks:"Sprite não encontrado, usando placeholder"
    /// - Dados inconsistentes toleráveis: "HP negativo detectado, normalizando para 0"
    /// - Uso incorreto de API: "Subscribe chamado duas vezes para o mesmo handler"
    /// </example>
    /// </summary>
    /// <remarks>
    /// Em caso de exceções, passar sempre a exceção para contexto.
    /// </remarks>
    Warn,

    /// <summary>
    /// Falhas que comprometem funcionalidade ou indicam bug.<br/>
    /// <example>
    /// - Exceções: "Falha ao carregar deck"
    /// - Estado inválido crítico: "GameState nulo durante execução"
    /// - Recursos obrigatórios ausentes: "Prefab da câmera não configurado"
    /// </example>
    /// </summary>
    /// <remarks>
    /// Em caso de exceções, passar sempre a exceção para contexto.
    /// </remarks>
    Error,
}
