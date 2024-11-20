declare module Game.Game.Server {
	interface indexViewModel {
		urlCercaSfidanti: string;
		utenteSfidante: Game.Game.Server.utenteOptionViewModel;
	}
	interface utenteOptionViewModel {
		id: any;
		email: string;
	}
}
