var Game;
(function (Game_1) {
    var Game;
    (function (Game) {
        class gameViewModel {
            constructor(hub, model) {
                this.hub = hub;
                this.model = model;
                this.inviaMessaggio = async () => {
                    var body = {
                        idPartita: this.model.idPartita,
                        messaggio: this.nuovoMessaggio
                    };
                    await this.postJson(this.model.urlInviaMessaggio, body);
                    this.nuovoMessaggio = null;
                };
                this.eseguiAzione = async (idAzione) => {
                    var body = {
                        idPartita: this.model.idPartita,
                        idAzione: idAzione
                    };
                    await this.postJson(this.model.urlEseguiAzione, body);
                };
                this.nuovoMessaggio = null;
                this.azioniDisponibili = [this.model.azioni.find(x => x.idAzione == 1)];
                this.dolcettiOttenuti = 0;
                this.scherzettiAttivi = [];
                // ES3: Sottoscrizione eventi di nostro interesse
                this.hub.on("NuovoMessaggio", async (idUtente, email, messaggio) => {
                    this.model.messaggi.push(email + " scrive: " + messaggio);
                });
                this.hub.on("AzioneEseguita", async (idUtente, email, idAzione) => {
                    var azione = this.model.azioni.find(x => x.idAzione == idAzione);
                    this.model.messaggi.push(email + " esegue azione: " + azione.nome);
                    if (!azione.idAzioniRisposta || azione.idAzioniRisposta.length == 0) {
                        this.azioniDisponibili = [this.model.azioni.find(x => x.idAzione == 1)];
                    }
                    else if (idUtente != model.idUtenteCorrente) {
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
            async postJson(url, body) {
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
            async getJson(url) {
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
            async getJsonT(url) {
                const response = await this.getJson(url);
                return await response.json();
            }
        }
        Game.gameViewModel = gameViewModel;
    })(Game = Game_1.Game || (Game_1.Game = {}));
})(Game || (Game = {}));
//# sourceMappingURL=Game.js.map