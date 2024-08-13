using ControleDeCinema.WebApp.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace ControleDeCinema.WebApp.Extensions;

public static class TempDataExtensions
{
    public static void SerializarMensagemViewModel(
        this ITempDataDictionary dicionario, MensagemViewModel mensagemVm)
    {
        dicionario["mensagem"] = JsonSerializer.Serialize(mensagemVm);
    }

    public static MensagemViewModel? DesserializarMensagemViewModel(this ITempDataDictionary dicionario)
    {
        var mensagemStr = dicionario["mensagem"]?.ToString();

        if (mensagemStr is null) return null;

        return JsonSerializer.Deserialize<MensagemViewModel>(mensagemStr);
    }
}