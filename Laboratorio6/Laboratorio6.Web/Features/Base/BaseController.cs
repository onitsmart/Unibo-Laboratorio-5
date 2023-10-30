using Laboratorio6.Services.Utenti;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Laboratorio6.Web.Features.Base
{
    // ES1: ATTRIBUTO AUTHORIZE E FILTRO
    [Authorize]
    public partial class BaseController : Controller
    {
        public static string UtenteViewDataKey = "ViewDataUtenteCorrente";

        internal readonly UtentiService _utentiService;

        internal UtenteViewModel UtenteCorrente { get; set; }

        public BaseController()
        {
        }

        public BaseController(UtentiService utentiService)
        {
            _utentiService = utentiService;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idUtenteString = HttpContext.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (Guid.TryParse(idUtenteString, out var idUtente))
            {
                var utente = await _utentiService.Query(new UtenteInfoQuery { Id = idUtente });

                UtenteCorrente = new UtenteViewModel
                {
                    Id = utente.Id,
                    Email = utente.Email,
                    FirstName = utente.FirstName,
                    LastName = utente.LastName,
                    NickName = utente.NickName
                };
                ViewData[UtenteViewDataKey] = UtenteCorrente;
            }
            else
            {
                this.SignOut();
                await HttpContext.SignOutAsync();

                context.Result = new RedirectResult(context.HttpContext.Request.GetEncodedUrl());
            }

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
