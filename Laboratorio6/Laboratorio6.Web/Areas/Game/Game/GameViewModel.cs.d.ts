declare module Game.Game.Server {
	interface gameViewModel {
		idUtenteCorrente: any;
		urlEseguiAzione: string;
		urlInviaMessaggio: string;
		urlGameHub: string;
		idPartita: any;
		messaggi: string[];
		azioni: Game.Game.Server.azioneViewModel[];
	}
	interface azioneViewModel {
		idAzione: number;
		nome: string;
		idAzioniRisposta: number[];
	}
	interface inviaMessaggioViewModel {
		idPartita: any;
		messaggio: string;
	}
	interface eseguiAzioneViewModel {
		idPartita: any;
		idAzione: number;
	}
}
