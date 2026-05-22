using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.Models;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloEquipamento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestaoDeEquipamentosWeb.ConsoleApp.Controllers;

public class ChamadoController : Controller
{
    private readonly IRepositorio<Chamado> repositorioChamado;
    private readonly IRepositorio<Equipamento> repositorioEquipamento;

    public ChamadoController()
    {
        ContextoJson contexto = new();
        contexto.Carregar();

        repositorioChamado = new RepositorioChamadoEmArquivo(contexto);
        repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contexto);
    }

    [HttpGet]
    public ActionResult Listar()
    {
        List<Chamado> chamados = repositorioChamado.SelecionarTodos();

        List<ListarChamadoViewModel> listarVm = new List<ListarChamadoViewModel>();

        foreach (Chamado c in chamados)
        {
            ListarChamadoViewModel vm = new(
                c.Id,
                c.Titulo,
                c.Equipamento.Nome,
                c.DataAbertura,
                c.TempoDecorrido,
                c.EstaConcluido
            );
            listarVm.Add(vm);
        }
        return View(listarVm);
    }
    [HttpGet]
    public ActionResult Cadastrar()
    {
        ViewBag.Equipamentos = CarregarEquipamentos();

        CadastrarChamadoViewModel cadastrarVm = new CadastrarChamadoViewModel(string.Empty, null, string.Empty);
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarChamadoViewModel chamadoViewModel)
    {
        return View();
    }
    private List<SelectListItem> CarregarEquipamentos()
    {
        List<Equipamento> equipamentos = repositorioEquipamento.SelecionarTodos();

        List<SelectListItem> EquipamentoVm = new();

        foreach (Equipamento e in equipamentos)
        {
            SelectListItem selecionarEquipamentoVm = new(
                e.Nome, e.Id
            );
            EquipamentoVm.Add(selecionarEquipamentoVm);
        }
        return EquipamentoVm;
    }
}
