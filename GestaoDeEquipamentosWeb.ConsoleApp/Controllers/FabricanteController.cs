using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.Models;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentosWeb.ConsoleApp.Controllers
{
    public class FabricanteController : Controller
    {
        private readonly IRepositorio<Fabricante> repositorioFabricante;

        public FabricanteController()
        {
            ContextoJson contexto = new ContextoJson();
            contexto.Carregar();

            repositorioFabricante =
                new RepositorioFabricanteEmArquivo(contexto);
        }
        // GET: FabricanteController
        public ActionResult Listar()
        {
            List<Fabricante> fabricantes = repositorioFabricante.SelecionarTodos();

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

            return View(listarVms);
        }
        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Cadastrar(CadastrarFabricanteViewModel cadastrarVm)
        {
            Fabricante fabricante = new(
                cadastrarVm.Nome,
                cadastrarVm.Email,
                cadastrarVm.Telefone
                );

            repositorioFabricante.Cadastrar(fabricante);

            return RedirectToAction(nameof(Listar));
        }
        [HttpGet]
        public ActionResult Editar(string id)
        {
            Fabricante? fabricante = repositorioFabricante.SelecionarPorId(id);

            if (fabricante == null)
                RedirectToAction(nameof(Listar));

            EditarFabricanteViewModel viewModel = new(
                id,
                fabricante.Nome,
                fabricante.Email,
                fabricante.Telefone
            );
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Editar(EditarFabricanteViewModel editrVm)
        {
            Fabricante fabricanteAtualizado = new(editrVm.Nome, editrVm.Email, editrVm.Telefone);

            repositorioFabricante.Editar(editrVm.Id, fabricanteAtualizado);

            return RedirectToAction(nameof(Listar));
        }
        [HttpGet]
        public ActionResult Excluir(string id)
        {
            Fabricante? fabricante = repositorioFabricante.SelecionarPorId(id);

            if (fabricante == null)
                RedirectToAction(nameof(Listar));

            ExcluirFabricanteViewModel viewModel = new(
            id,
            fabricante.Nome,
            fabricante.Email,
            fabricante.Telefone
        );

            return View(viewModel);
        }

        [HttpPost]
        [ActionName("Excluir")]
        public ActionResult ExlcuirConfirmado(ExcluirFabricanteViewModel excluirVm)
        {
            Fabricante? fabricante = repositorioFabricante.SelecionarPorId(excluirVm.Id);

            if (fabricante == null)
                return RedirectToAction(nameof(Listar));

            repositorioFabricante.Excluir(fabricante);

            return RedirectToAction(nameof(Listar));
        }
    }
}
