using Laboratorio5.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Laboratorio5.Web.Areas.Game.Game
{
    [TypeScriptModule("Game.Game.Server")]
    public class GameViewModel
    {
        public GameViewModel()
        {
            this.Messaggi = Array.Empty<string>();
        }

        public Guid IdUtenteCorrente { get; set; }

        public string UrlEseguiAzione { get; set; }
        public string UrlInviaMessaggio { get; set; }
        public string UrlGameHub { get; set; }

        public Guid IdPartita { get; set; }

        public IEnumerable<string> Messaggi { get; set; }

        public IEnumerable<AzioneViewModel> Azioni { get; set; } = new AzioneViewModel[]
        {
            new AzioneViewModel
            {
                IdAzione = 1,
                Nome = "Dolcetto o scherzetto?",
                IdAzioniRisposta = new int[] { 2,3 }
            },
            new AzioneViewModel
            {
                IdAzione = 2,
                Nome = "Dolcetto",
            },
            new AzioneViewModel
            {
                IdAzione = 3,
                Nome = "Scherzetto",
                IdAzioniRisposta = new int[] { 4,5,6 }
            },
            new AzioneViewModel
            {
                IdAzione = 4,
                Nome = "Cambia colori interfaccia"
            },
            new AzioneViewModel
            {
                IdAzione = 5,
                Nome = "Cambia font size interfaccia"
            },
            new AzioneViewModel
            {
                IdAzione = 6,
                Nome = "💩"
            }
        };

        public void SetUrls(IUrlHelper url)
        {
            this.UrlInviaMessaggio = url.Action(MVC.Game.Game.InviaMessaggio());
            this.UrlEseguiAzione = url.Action(MVC.Game.Game.EseguiAzione());
            this.UrlGameHub = "https://localhost:44373/gameHub";
        }

        public string ToJson()
        {
            return Infrastructure.JsonSerializer.ToJsonCamelCase(this);
        }
    }

    [TypeScriptModule("Game.Game.Server")]
    public class AzioneViewModel
    {
        public int IdAzione { get; set; }
        public string Nome { get; set; }
        public IEnumerable<int> IdAzioniRisposta { get; set; }
    }

    [TypeScriptModule("Game.Game.Server")]
    public class InviaMessaggioViewModel
    {
        public Guid IdPartita { get; set; }
        public string Messaggio { get; set; }
    }

    [TypeScriptModule("Game.Game.Server")]
    public class EseguiAzioneViewModel
    {
        public Guid IdPartita { get; set; }
        public int IdAzione { get; set; }
    }
}
