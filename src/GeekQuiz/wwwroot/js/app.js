var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") return Reflect.decorate(decorators, target, key, desc);
    switch (arguments.length) {
        case 2: return decorators.reduceRight(function(o, d) { return (d && d(o)) || o; }, target);
        case 3: return decorators.reduceRight(function(o, d) { return (d && d(target, key)), void 0; }, void 0);
        case 4: return decorators.reduceRight(function(o, d) { return (d && d(target, key, o)) || o; }, desc);
    }
};
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
var angular2_1 = require('angular2/angular2');
var http_1 = require('angular2/http');
var AppComponent = (function () {
    function AppComponent(http) {
        this.http = http;
        this.answered = false;
        this.title = "loading question...";
        this.options = [];
        this.correctAnswer = false;
        this.working = false;
        this.timecount = 40;
        this.player1 = "";
        this.player2 = "";
        this.questioncounter = 0;
        this.waitinguser = false;
        this.player1score = 0;
        this.player2score = 0;
        this.gameend = false;
    }
    AppComponent.prototype.EndGame = function () {
        window.location.assign("/Home/Summary");
    };
    AppComponent.prototype.answer = function () {
        return this.correctAnswer ? 'correct' : 'incorrect';
    };
    AppComponent.prototype.nextQuestion = function () {
        var _this = this;
        this.waitinguser = true;
        this.working = true;
        var headers = new http_1.Headers();
        headers.append('If-Modified-Since', 'Mon, 27 Mar 1972 00:00:00 GMT');
        this.http.get("/Home/getRoom", { headers: headers }).map(function (res) { return res.json(); }).subscribe(function (q) {
            _this.timecount = q.timeperquestion;
            _this.player1 = q.players[0].userName;
            _this.player2 = q.players[1].userName;
            _this.player1score = q.players[0].roomScore;
            _this.player2score = q.players[1].roomScore;
            _this.numberofquetion = q.numofquestion;
            _this.roomID = q.RoomID;
            _this.Room = q;
        }, function (err) { return console.error(err); });
        clearInterval(this.timer);
        this.title = "loading question...";
        this.options = [];
        this.answered = false;
        this.http.get("/api/trivia", { headers: headers })
            .map(function (res) { return res.json(); })
            .subscribe(function (question) {
            if (question == "endGame") {
                _this.gameend = true;
                return;
            }
            _this.options = question.options;
            _this.title = question.title;
            _this.answered = false;
            _this.working = false;
            _this.showtime = true;
        }, function (err) {
            _this.gameend = true;
            _this.working = false;
        });
        this.timer = setInterval(function (t) {
            _this.timecount--;
            if (_this.timecount == 0) {
                _this.Timeup();
                clearInterval(_this.timer);
            }
        }, 1000);
    };
    AppComponent.prototype.sendAnswer = function (option) {
        var _this = this;
        this.questioncounter++;
        this.working = true;
        clearInterval(this.timer);
        var answer = { 'questionId': option.questionId, 'optionId': option.id };
        var headers = new http_1.Headers();
        headers.append('Content-Type', 'application/json');
        this.http.post('/api/trivia', JSON.stringify(answer), { headers: headers })
            .map(function (res) { return res.json(); })
            .subscribe(function (answerIsCorrect) {
            _this.answered = true;
            _this.correctAnswer = (answerIsCorrect === true);
            _this.working = false;
        }, function (err) {
            _this.gameend = true;
            _this.working = false;
        });
    };
    AppComponent.prototype.Timeup = function () {
        var _this = this;
        this.questioncounter++;
        clearInterval(this.timer);
        this.working = true;
        var answer = { 'questionId': this.options[0].questionId, 'optionId': this.options[0].id, 'userId': 'wrong' };
        var headers = new http_1.Headers();
        headers.append('Content-Type', 'application/json');
        this.http.post('/api/trivia/', JSON.stringify(answer), { headers: headers })
            .map(function (res) { return res.json(); })
            .subscribe(function (answerIsCorrect) {
            _this.answered = true;
            _this.correctAnswer = false;
            _this.working = false;
        }, function (err) {
            _this.title = "Oops... something went wrong";
            _this.working = false;
        });
    };
    AppComponent.prototype.afterViewInit = function () {
        this.nextQuestion();
    };
    AppComponent = __decorate([
        angular2_1.Component({
            selector: 'geekquiz-app',
            viewBindings: [http_1.HTTP_BINDINGS]
        }),
        angular2_1.View({
            directives: [angular2_1.NgFor, angular2_1.NgClass],
            template: "\n        <div><h1 class=\"userscore\"> {{player1}} score: {{player1score}} </h1></div>\n        <div class=\"userscore\">{{player2}} score: {{player2score}} </div>\n        <div *ng-if= \"!gameend\" class=\"flip-container text-center \">\n            \n            <div class=\"back\" [ng-class]=\"{flip: answered, correct: correctAnswer, incorrect:!correctAnswer}\">\n            \n            <div *ng-if=\"waitinguser\"> \n               <p class=\"lead\">{{answer()}}</p>\n                <p>\n                    <button class=\"btn btn-info btn-lg next option\" (click)=\"nextQuestion()\" [disabled]=\"working\">Next Question</button>\n                </p>\n            </div>\n            <div *ng-if=\"!waitinguser\">\n            <p class=\"lead\">{{title}}</p>\n            </div>            \n            </div>\n            <div class=\"front\" [ng-class]=\"{flip: answered}\">\n                <p class=\"lead\" *ng-if=\"showtime\">{{timecount}}</p>\n                <p class=\"lead\">{{title}}</p>\n                <div class=\"container text-center\">\n                    <button class=\"btn btn-info btn-lg option\" *ng-for=\"#option of options\" (click)=\"sendAnswer(option)\" [disabled]=\"working\">{{option.title}}</button>\n                </div>\n                <div>{{num}}</div>\n            </div>\n        </div>\n\n        <div *ng-if= \"gameend\" class=\"flip-container text-center \">\n            \n            <button class=\"btn btn-danger btn-lg \" (click)=\"EndGame()\"> Press to continue </button>\n        </div>\n\n\n    "
        }),
        __param(0, angular2_1.Inject(http_1.Http))
    ], AppComponent);
    return AppComponent;
})();
angular2_1.bootstrap(AppComponent);
