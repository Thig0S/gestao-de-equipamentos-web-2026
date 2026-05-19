
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.Models;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentosWeb.ConsoleApp.Controllers
{
    public class EquipamentoController : Controller
    {
        private readonly IRepositorio<Equipamento> repositorioEquipamento;
        private readonly IRepositorio<Fabricante> repostorioFabricante;
        public EquipamentoController()
        {
            ContextoJson contexto = new();
            contexto.Carregar();

            repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contexto);
            repostorioFabricante = new RepositorioFabricanteEmArquivo(contexto);

        }

        public ActionResult Listar()
        {
            List<Equipamento> equipamentos = repositorioEquipamento.SelecionarTodos();

            List<ListarEquipamentosViewModel> listarVm = new List<ListarEquipamentosViewModel>();

            foreach (Equipamento e in equipamentos)
            {
                ListarEquipamentosViewModel viewModel = new ListarEquipamentosViewModel(
                    e.Id,
                    e.Nome,
                    e.PrecoAquisicao,
                    e.DataFabricacao,
                    e.Fabricante.Nome
                );
                listarVm.Add(viewModel);
            }
            return View(listarVm);
        }
        [HttpGet]
        public ActionResult Cadastrar()
        {
            ViewBag.Fabricantes = CarregarFabricantes();

            return View();
        }
        [HttpPost]
        public ActionResult Cadastrar(CadastrarEquipamentoViewModel cadastrarVm)
        {
            Fabricante? fabricante = repostorioFabricante.SelecionarPorId(cadastrarVm.FabricanteId);

            if (fabricante == null)
                return RedirectToAction(nameof(Listar));

            Equipamento novoEquipamento = new Equipamento(
                cadastrarVm.Nome,
                cadastrarVm.PrecoAquisicao,
                cadastrarVm.DataFabricacao,
                fabricante
            );

            repositorioEquipamento.Cadastrar(novoEquipamento);

            return RedirectToAction(nameof(Listar));
        }

        private List<ListarFabricantesViewModel> CarregarFabricantes()
        {
            List<Fabricante> fabricantes = repostorioFabricante.SelecionarTodos();

            List<ListarFabricantesViewModel> listarVms = new List<ListarFabricantesViewModel>();

            foreach (Fabricante f in fabricantes)
            {
                ListarFabricantesViewModel viewModel = new(
                    f.Id,
                    f.Nome,
                    f.Email,
                    f.Telefone
                );
                listarVms.Add(viewModel);
            }
            return listarVms;
        }
    }
}