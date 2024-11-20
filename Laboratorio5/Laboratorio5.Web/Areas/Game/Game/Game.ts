module Game.Game {
    export class gameViewModel {
        nuovoMessaggio: string;
        azioniDisponibili: Game.Server.azioneViewModel[];
        dolcettiOttenuti: number;
        scherzettiAttivi: number[];

        constructor(public hub: any, public model: Game.Server.gameViewModel) {
            this.nuovoMessaggio = null;
            this.azioniDisponibili = [ this.model.azioni.find(x => x.idAzione == 1) ];
            this.dolcettiOttenuti = 0;
            this.scherzettiAttivi = [];

            // ES2: Sottoscrizione eventi di nostro interesse
            this.hub.on("NuovoMessaggio", async (idUtente: any, email: string, messaggio: string) => {
                this.model.messaggi.push(email + " scrive: " + messaggio);
            });

            this.hub.on("AzioneEseguita", async (idUtente: any, email: string, idAzione: number) => {
                var azione = this.model.azioni.find(x => x.idAzione == idAzione);
                this.model.messaggi.push(email + " esegue azione: " + azione.nome);

                if (!azione.idAzioniRisposta || azione.idAzioniRisposta.length == 0) {
                    this.azioniDisponibili = [this.model.azioni.find(x => x.idAzione == 1)];
                } else if (idUtente != model.idUtenteCorrente) {
                    this.azioniDisponibili = this.model.azioni.filter(x => azione.idAzioniRisposta.includes(x.idAzione));
                }

                if (idUtente != model.idUtenteCorrente) {
                    if (idAzione == 2)
                        this.dolcettiOttenuti++;

                    if (idAzione == 4 || idAzione == 5 || idAzione == 6) {
                        this.scherzettiAttivi.push(idAzione);
                        this.scherzettiAttivi = [...new Set(this.scherzettiAttivi)];
                    }
                }
            });
        }

        public inviaMessaggio = async () => {
            var body = <Game.Server.inviaMessaggioViewModel>{
                idPartita: this.model.idPartita,
                messaggio: this.nuovoMessaggio
            };
            await this.postJson(this.model.urlInviaMessaggio, body);
            this.nuovoMessaggio = null;
        }

        public eseguiAzione = async (idAzione: number) => {
            var body = <Game.Server.eseguiAzioneViewModel>{
                idPartita: this.model.idPartita,
                idAzione: idAzione
            };
            await this.postJson(this.model.urlEseguiAzione, body);
        }

        public async postJson(url: string, body: any): Promise<Response> {
            let res = await fetch(url, {
                method: "POST",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'X-Requested-With': 'XMLHttpRequest',
                },
                credentials: "same-origin",
                body: JSON.stringify(body)
            });

            return res;
        }

        public async getJson(url: string): Promise<Response> {
            let res = await fetch(url, {
                method: "GET",
                headers: {
                    'Accept': 'application/json',
                    'X-Requested-With': 'XMLHttpRequest',
                },
                credentials: "same-origin",
            });

            return res;
        }

        public async getJsonT<T>(url: string): Promise<T> {
            const response = await this.getJson(url);
            return await response.json();
        }
    }
}