using ControleDeCinema.Dominio.ModuloFilme;
using ControleDeCinema.Dominio.ModuloGenero;
using ControleDeCinema.Dominio.ModuloSessao;
using ControleDeCinema.Dominio.ModulosSala;
using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeCinema.WebApp.Controllers;

public class InicioController : Controller
{
    private readonly IRepositorioSessao repositorioSessao;
    private readonly IRepositorioFilme repositorioFilme;
    private readonly IRepositorioSala repositorioSala;
    private readonly IRepositorioGenero repositorioGenero;

    public InicioController(
        IRepositorioSessao repositorioSessao,
        IRepositorioFilme repositorioFilme,
        IRepositorioSala repositorioSala,
        IRepositorioGenero repositorioGenero
    )
    {
        this.repositorioSessao = repositorioSessao;
        this.repositorioFilme = repositorioFilme;
        this.repositorioSala = repositorioSala;
        this.repositorioGenero = repositorioGenero;
    }
    public IActionResult Index()
    {
        var agrupamentos = repositorioSessao.ObterSessoesAgrupadas();

        var agrupamentosSessoesVm = agrupamentos
            .Select(MapearAgrupamentoSessoes);

        ViewBag.Agrupamentos = agrupamentosSessoesVm;

        ViewBag.QuantidadeFilmes = repositorioFilme.SelecionarTodos().Count;
        ViewBag.QauntidadeGeneros = repositorioGenero.SelecionarTodos().Count;
        ViewBag.QuantidadeSalas = repositorioSala.SelecionarTodos().Count;
        ViewBag.QuantidadeSessoes = repositorioSessao.SelecionarTodos().Count;
        ViewBag.QauntidadeIngressos = repositorioSessao.SelecionarTodosIngressos().Count;

        return View();
    }

    private static AgrupamentoSessoesPorFilmeViewModel MapearAgrupamentoSessoes(IGrouping<string, Sessao> agrupamento)
    {
        return new AgrupamentoSessoesPorFilmeViewModel
        {
            Filme = agrupamento.Key,
            Sessoes = agrupamento.Select(s => new ListarSessaoViewModel
                {
                    Id = s.Id,
                    Filme = agrupamento.Key,
                    Sala = s.Sala.Numero,
                    IngressosDisponiveis = s.ObterQuantidadeIngressosDisponiveis(),
                    Data = s.Data.ToString("dd/MM/yyyy"),
                    Encerrada = s.Encerrada ? "Encerrada" : "Disponível"
                })
                .OrderBy(s => s.Data)
        };
    }
}