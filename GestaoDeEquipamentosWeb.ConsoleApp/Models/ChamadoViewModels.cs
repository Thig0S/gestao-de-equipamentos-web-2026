using System.ComponentModel.DataAnnotations;
namespace GestaoDeEquipamentosWeb.ConsoleApp.ModuloChamado;


public record ListarChamadoViewModel(
    string Id,
    string Titulo,
    string Equipamento,
    DateTime DataAbertura,
    int TempoDecorrido,
    bool EstaConcluido
);
public record CadastrarChamadoViewModel(
    [Required(ErrorMessage = "O campo Titulo deve ser preenchido!")]
    [StringLength(50, MinimumLength = 2)]
    string Titulo,
    [StringLength(500, MinimumLength = 2,ErrorMessage = "O campo Descricao deve ter entre 2 a 500 caracteres")]
    string? Descricao,
    [Required(ErrorMessage = "O campo Equipamento deve ser preenchido!")]
    string EquipamentoId
);

public record EditarChamadoViewModel(

    string Id,

    [Required(ErrorMessage = "O campo Titulo deve ser preenchido!")]
    [StringLength(50, MinimumLength = 2)]
    string Titulo,
    [StringLength(500, MinimumLength = 2,ErrorMessage = "O campo Descricao deve ter entre 2 a 500 caracteres")]
    string? Descricao,
    [Required(ErrorMessage = "O campo Equipamento deve ser preenchido!")]
    string EquipamentoId,
    bool EstaConcluido
);
public record ExcluirChamadoViewModel(
    string Id,
    string Titulo,
    string? Descricao,
    string Equipamento,
    DateTime DataAbertura,
    int TempoDecorrido,
    bool EstaConcluido
);