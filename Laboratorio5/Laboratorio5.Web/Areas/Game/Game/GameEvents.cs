using System;

namespace Laboratorio5.Web.Areas.Game.Game
{
    public class NuovoMessaggioEvent
    {
        public Guid IdUtente { get; set; }
        public string Email { get; set; }
        public Guid IdPartita { get; set; }
        public string Messaggio { get; set; }
    }

    public class AzioneEseguitaEvent
    {
        public Guid IdUtente { get; set; }
        public string Email { get; set; }
        public Guid IdPartita { get; set; }
        public int IdAzione { get; set; }
    }
}
