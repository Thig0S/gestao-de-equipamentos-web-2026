using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;

namespace GestaoDeEquipamentosWeb.ConsoleApp.ModuloChamado;

public class RepositorioChamadoEmArquivo : RepositorioBaseEmArquivo<Chamado>, IRepositorioChamado
{
    public RepositorioChamadoEmArquivo(ContextoJson contexto) : base(contexto) { }

    public List<Chamado> SelecionarChamadosConcluidos()
    {
        List<Chamado> chamadosAbertos = new();

        foreach (Chamado c in registros)
        {
            if (c.EstaConcluido)
                chamadosAbertos.Add(c);
        }
        return chamadosAbertos;
    }

    public List<Chamado> SelecionarChamadosEmAberto()
    {
        List<Chamado> chamadosConcluidos = new();

        foreach (Chamado c in registros)
        {
            if (!c.EstaConcluido)
                chamadosConcluidos.Add(c);
        }
        return chamadosConcluidos;
    }

    protected override List<Chamado> CarregarRegistros()
    {
        return contexto.Chamados;
    }
}
