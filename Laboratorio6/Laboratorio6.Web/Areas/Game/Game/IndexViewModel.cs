using Laboratorio6.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace Laboratorio6.Web.Areas.Game.Game
{
    [TypeScriptModule("Game.Game.Server")]
    public class IndexViewModel
    {
        public IndexViewModel()
        {
        }

        public string UrlCercaSfidanti { get; set; }

        [Display(Name = "Seleziona uno sfidante")]
        public UtenteOptionViewModel UtenteSfidante { get; set; }

        public void SetUrls(IUrlHelper url)
        {
            this.UrlCercaSfidanti = url.Action(MVC.Game.Game.CercaSfidanti());
        }

        public string ToJson()
        {
            return Infrastructure.JsonSerializer.ToJsonCamelCase(this);
        }
    }

    [TypeScriptModule("Game.Game.Server")]
    public class UtenteOptionViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
