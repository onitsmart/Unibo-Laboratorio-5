module Game.Game {
    export class indexViewModel {
        sfidanti: Game.Server.utenteOptionViewModel[];
        loadingSfidanti: boolean;
        showNoOptions: boolean;

        constructor(public model: Game.Server.indexViewModel) {
            this.sfidanti = [];
            this.loadingSfidanti = false;
            this.showNoOptions = false;
        }

        public cercaSfidanti = async (filtroLibero: string) => {
            try {
                this.loadingSfidanti = true;

                var urlCercaSfidanti = this.model.urlCercaSfidanti;

                if (filtroLibero)
                    urlCercaSfidanti = urlCercaSfidanti + "?filtro=" + filtroLibero;

                await this.getJsonT<Game.Server.utenteOptionViewModel[]>(urlCercaSfidanti).then((sfidanti) => {
                    if (sfidanti.length == 0)
                        this.showNoOptions = true;

                    this.sfidanti = sfidanti;
                    this.loadingSfidanti = false;
                });
            }
            catch (e) {
                this.loadingSfidanti = false;
            }
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