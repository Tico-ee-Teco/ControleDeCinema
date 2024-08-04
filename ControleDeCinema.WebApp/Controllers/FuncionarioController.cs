using ControleDeCinema.Dominio;
using ControleDeCinema.Infra.Compartilhado;
using ControleDeCinema.Infra.ModuloFuncionario;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeCinema.WebApp.Controllers
{
    public class FuncionarioController : Controller
    {
        public ViewResult Listar()
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioFuncionario = new RepositorioFuncionarioEmOrm(db);

            var funcionarios = repositorioFuncionario.SelecionarTodos();

            var listarFuncionariosVm = funcionarios
                .Select(f => new ListarFuncionarioViewModel
                {
                    Id = f.Id,
                    Nome = f.Nome,
                });

            return View(listarFuncionariosVm);
        }

        public ViewResult Inserir()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Inserir(InserirFuncionarioViewModel inserirFuncionarioVm)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioFuncionario = new RepositorioFuncionarioEmOrm(db);

            var funcionario = new Funcionario(inserirFuncionarioVm.Nome, inserirFuncionarioVm.Cpf, inserirFuncionarioVm.Login, inserirFuncionarioVm.Senha);

            repositorioFuncionario.Inserir(funcionario);

            HttpContext.Response.StatusCode = 201;

            var notificacaoVm = new NotificacaoViewModel
            {
                Mensagem = $"O registro com o ID [{funcionario.Id}] foi cadastrado com sucesso!",
                LinkRedirecionamento = "/funcionario/listar"
            };

            return View("mensagens", notificacaoVm);
        }

        public ViewResult Editar(int id)
        {
            var db = new ControleDeCinemaDbContext();
            var repositorioFuncionario = new RepositorioFuncionarioEmOrm(db);

            var funcionario = repositorioFuncionario.SelecionarPorId(id);

            var editarFuncionarioVm = new EditarFuncionarioViewModel
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Cpf = funcionario.CPF,
                Login = funcionario.Login,
                Senha = funcionario.Senha
            };

            return View(editarFuncionarioVm);
        }
        
    }
}
