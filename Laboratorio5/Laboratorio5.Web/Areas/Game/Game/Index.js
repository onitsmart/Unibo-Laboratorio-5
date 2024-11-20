var Game;
(function (Game_1) {
    var Game;
    (function (Game) {
        class indexViewModel {
            constructor(model) {
                this.model = model;
                this.cercaSfidanti = async (filtroLibero) => {
                    try {
                        this.loadingSfidanti = true;
                        var urlCercaSfidanti = this.model.urlCercaSfidanti;
                        if (filtroLibero)
                            urlCercaSfidanti = urlCercaSfidanti + "?filtro=" + filtroLibero;
                        await this.getJsonT(urlCercaSfidanti).then((sfidanti) => {
                            if (sfidanti.length == 0)
                                this.showNoOptions = true;
                            this.sfidanti = sfidanti;
                            this.loadingSfidanti = false;
                        });
                    }
                    catch (e) {
                        this.loadingSfidanti = false;
                    }
                };
                this.sfidanti = [];
                this.loadingSfidanti = false;
                this.showNoOptions = false;
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
        Game.indexViewModel = indexViewModel;
    })(Game = Game_1.Game || (Game_1.Game = {}));
})(Game || (Game = {}));
//# sourceMappingURL=Index.js.map