
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.Models;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloEquipamento;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentosWeb.ConsoleApp.Controllers
{
    public class EquipamentoController : Controller
    {
        private readonly IRepositorio<Equipamento> repositorioEquipamento;

        public EquipamentoController()
        {
            ContextoJson contexto = new();
            contexto.Carregar();

            repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contexto);
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
    }
}