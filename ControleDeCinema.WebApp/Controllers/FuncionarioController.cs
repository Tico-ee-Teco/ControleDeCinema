using ControleDeCinema.Dominio;
using ControleDeCinema.WebApp.Extensions;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeCinema.WebApp.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly IRepositorioFuncionario repositorioFuncionario;

        public FuncionarioController(IRepositorioFuncionario repositorioFuncionario)
        {
            this.repositorioFuncionario = repositorioFuncionario;
        }
        public IActionResult Listar()
        {
            var funcionarios = repositorioFuncionario.SelecionarTodos();

            var listarFuncionariosVm = funcionarios
                .Select(f => new ListarFuncionarioViewModel
                {
                    Id = f.Id,
                    Nome = f.Nome,
                });

            ViewBag.Mensagem = TempData.DesserializarMensagemViewModel();

            return View(listarFuncionariosVm);
        }

        public IActionResult Inserir()
        {
            return RedirectToAction(nameof(Listar));
        }

        [HttpPost]
        public IActionResult Inserir(InserirFuncionarioViewModel inserirFuncionarioVm)
        {
            var funcionario = new Funcionario(inserirFuncionarioVm.Nome, inserirFuncionarioVm.Cpf, inserirFuncionarioVm.Login, inserirFuncionarioVm.Senha);

            repositorioFuncionario.Inserir(funcionario);

            HttpContext.Response.StatusCode = 201;

            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{funcionario.Id}] foi inserido com sucesso!"
            });

            return RedirectToAction(nameof(Listar));
        }

        public IActionResult Editar(int id)
        {
            var funcionario = repositorioFuncionario.SelecionarPorId(id);

            var editarFuncionarioVm = new EditarFuncionarioViewModel
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Cpf = funcionario.CPF,
                Login = funcionario.Login,
                Senha = funcionario.Senha
            };

            return RedirectToAction(nameof(Listar));
        }

        [HttpPost]
        public IActionResult Editar(EditarFuncionarioViewModel editarFuncionarioVm)
        {
            var funcionarioOriginal = repositorioFuncionario.SelecionarPorId(editarFuncionarioVm.Id);

            funcionarioOriginal.Nome = editarFuncionarioVm.Nome;
            funcionarioOriginal.CPF = editarFuncionarioVm.Cpf;
            funcionarioOriginal.Login = editarFuncionarioVm.Login;
            funcionarioOriginal.Senha = editarFuncionarioVm.Senha;

            repositorioFuncionario.Editar(funcionarioOriginal);

            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{funcionarioOriginal.Id}] foi editado com sucesso!"
            });

            return RedirectToAction(nameof(Listar));
        }
        
        public IActionResult Excluir(int id)
        {
            var funcionario = repositorioFuncionario.SelecionarPorId(id);

            var excluirFuncionarioVm = new ExcluirFuncionarioViewModel()
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Cpf = funcionario.CPF,
                Login = funcionario.Login,
                Senha = funcionario.Senha
            };

            return RedirectToAction(nameof(Listar));
        }
        
        [HttpPost]
        public IActionResult Excluirconfirmado(ExcluirFuncionarioViewModel excluirFuncionarioVm)
        {
            var funcionario = repositorioFuncionario.SelecionarPorId(excluirFuncionarioVm.Id);

            repositorioFuncionario.Excluir(funcionario);

            TempData.SerializarMensagemViewModel(new MensagemViewModel()
            {
                Titulo = "Sucesso",
                Mensagem = $"O registro ID [{funcionario.Id}] foi excluído com sucesso!"
            });

            return RedirectToAction(nameof(Listar));
        }
    }
}
