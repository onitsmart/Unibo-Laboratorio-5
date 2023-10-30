using Laboratorio6.Services.Utenti;
using Laboratorio6.Web.Features.Base;
using Laboratorio6.Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio6.Web.Areas.Game.Game
{
    [Area("Game")]
    public partial class GameController : BaseController
    {
        private readonly IPublishDomainEvents _publisher;

        public GameController(UtentiService utentiService, IPublishDomainEvents publisher) : base(utentiService)
        {
            _publisher = publisher;
        }

        [HttpGet]
        public virtual IActionResult Index(IndexViewModel model)
        {
            model.SetUrls(Url);

            return View(model);
        }

        // ES2: ACTION CHIAMATA DALLA VUE MULTISELECT
        [HttpGet]
        public virtual async Task<IActionResult> CercaSfidanti(string filtro)
        {
            var utentiDaSfidare = await _utentiService.Query(new UtentiDaSfidareQuery
            {
                // ES2: MANCA ID UTENTE CORRENTE, LO TROVATE NEL BASE CONTROLLER
                IdUtenteCorrente = new Guid(),
                Filtro = filtro,
            });

            return Json(utentiDaSfidare.Select(x => new UtenteOptionViewModel
            {
                Id = x.Id,
                Email = x.Email
            }));
        }

        [HttpPost]
        public virtual async Task<IActionResult> Play(Guid? idUtenteSfidante)
        {
            if (idUtenteSfidante == null)
                return RedirectToAction(Actions.Index());

            return RedirectToAction(Actions.PlayGame(Guid.NewGuid()));
        }

        [HttpPost]
        public virtual async Task<IActionResult> JoinGame(Guid? idPartita)
        {
            if (idPartita == null)
                return RedirectToAction(Actions.Index());

            return RedirectToAction(Actions.PlayGame(idPartita.Value));
        }

        [HttpGet]
        public virtual async Task<IActionResult> PlayGame(Guid idPartita)
        {
            var model = new GameViewModel();
            model.IdPartita = idPartita;
            model.IdUtenteCorrente = UtenteCorrente.Id;
            model.SetUrls(Url);

            return View("Game", model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> InviaMessaggio([FromBody] InviaMessaggioViewModel model)
        {
            // ES3: Esempio pubblicazione evento
            await _publisher.Publish(new NuovoMessaggioEvent
            {
                IdUtente = UtenteCorrente.Id,
                Email = UtenteCorrente.Email,
                IdPartita = model.IdPartita,
                Messaggio = model.Messaggio
            });

            return Ok();
        }

        [HttpPost]
        public virtual async Task<IActionResult> EseguiAzione([FromBody] EseguiAzioneViewModel model)
        {
            await _publisher.Publish(new AzioneEseguitaEvent
            {
                IdUtente = UtenteCorrente.Id,
                Email = UtenteCorrente.Email,
                IdPartita = model.IdPartita,
                IdAzione = model.IdAzione
            });

            return Ok();
        }
    }
}
