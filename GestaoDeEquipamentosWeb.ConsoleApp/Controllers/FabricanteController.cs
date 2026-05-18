using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado;
using GestaoDeEquipamentosWeb.ConsoleApp.Compartilhado.Arquivos;
using GestaoDeEquipamentosWeb.ConsoleApp.Models;
using GestaoDeEquipamentosWeb.ConsoleApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentosWeb.ConsoleApp.Controllers
{
    public class FabricanteController : Controller
    {
        private readonly IRepositorio<Fabricante> repostorioFabricante;
        public FabricanteController()
        {
            ContextoJson contexto = new();

            contexto.Carregar();

            repostorioFabricante = new RepositorioFabricanteEmArquivo(contexto);
        }
        // GET: FabricanteController
        public ActionResult Listar()
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

            repostorioFabricante.Cadastrar(fabricante);

            return RedirectToAction(nameof(Listar));
        }
        [HttpGet]
        public ActionResult Editar(string id)
        {
            Fabricante? fabricante = repostorioFabricante.SelecionarPorId(id);

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

            repostorioFabricante.Editar(editrVm.Id, fabricanteAtualizado);

            return RedirectToAction(nameof(Listar));
        }
        [HttpGet]
        public ActionResult Excluir(string id)
        {
            Fabricante? fabricante = repostorioFabricante.SelecionarPorId(id);

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
        [ActionName("Excluir")]
        public ActionResult ExlcuirConfirmado(EditarFabricanteViewModel excluirVm)
        {
            Fabricante? fabricante = repostorioFabricante.SelecionarPorId(excluirVm.Id);

            if (fabricante == null)
                RedirectToAction(nameof(Listar));

            repostorioFabricante.Excluir(fabricante);

            return RedirectToAction(nameof(Listar));
        }
    }
}
