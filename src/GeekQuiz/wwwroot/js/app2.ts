import {bootstrap, Component, View, NgFor, NgClass, AfterViewInit, Inject, OnInit } from 'angular2/angular2';
import {Http, HTTP_BINDINGS, Headers, BaseRequestOptions, ChangeDetectorRef,
    Directive,
    DoCheck,
    IterableDiffer,
    IterableDiffers,
    TemplateRef} from 'angular2/http';
import {Router} from 'angular2/router';
import {MockBackend} from 'angular2/http/testing';



@Component({
    selector: 'geekquiz-app',
    viewBindings: [HTTP_BINDINGS]
})
@View({
    directives: [NgFor, NgClass],
    template: `
       
        <div *ng-if= "!gameend" class="flip-container text-center ">
            
            <div class="back" [ng-class]="{flip: answered, correct: correctAnswer, incorrect:!correctAnswer}">
            
            <div *ng-if="waitinguser"> 
               <p class="lead">{{answer()}}</p>
                <p>
                    <button class="btn btn-info btn-lg next option" (click)="nextQuestion()" [disabled]="working">Next Question</button>
                </p>
            </div>
            <div *ng-if="!waitinguser">
            <p class="lead">{{title}}</p>
            </div>            
            </div>
            <div class="front" [ng-class]="{flip: answered}">
                <p class="lead" *ng-if="showtime">{{timecount}}</p>
                <p class="lead">{{title}}</p>
                <div class="container text-center">
                    <button class="btn btn-info btn-lg option" *ng-for="#option of options" (click)="sendAnswer(option)" [disabled]="working">{{option.title}}</button>
                </div>
                <div>{{num}}</div>
            </div>
        </div>

        <div *ng-if= "gameend" class="flip-container text-center ">
            
            <button class="btn btn-danger btn-lg " (click)="EndGame()"> Press to continue </button>
        </div>


    `
})
class AppComponent implements AfterViewInit {
    public answered = false;
    public title = "loading question...";
    public options = [];
    public correctAnswer = false;
    public working = false;
    public timecount = 40;
    public timer;
    public roomID: number;
   
    
    public status: number;
    public questioncounter = 0;
    public waitinguser = false;
    
   
    public gameend = false;
    public Room: any;
    public showtime: boolean;
    public router: Router;
    public numberofquetion;

    constructor( @Inject(Http) private http: Http) {


    }



    EndGame() {

        window.location.assign("/Home/Summary");

    }


    answer() {
        return this.correctAnswer ? 'correct' : 'incorrect';
    }

    nextQuestion() {


        this.waitinguser = true;
        this.working = true;
        var headers = new Headers();
        headers.append('If-Modified-Since', 'Mon, 27 Mar 1972 00:00:00 GMT');
        this.http.get("/Home/getRoom", { headers: headers }).map(res => res.json()).subscribe(q => {
            this.timecount = q.timeperquestion;
           
           
            
            this.numberofquetion = q.numofquestion;
            this.roomID = q.RoomID;
            this.Room = q;
        },
            err => console.error(err));
        clearInterval(this.timer);
        this.title = "loading question...";
        this.options = [];
        this.answered = false;
        this.http.get("/api/trivia", { headers: headers })
            .map(res => res.json())
            .subscribe(
            question => {
                if (question == "endGame") {
                    this.gameend = true;
                    return;
                }
                this.options = question.options;
                this.title = question.title;
                this.answered = false;
                this.working = false;
                this.showtime = true;
            },
            err => {
                this.gameend = true;
                this.working = false;
            });


        this.timer = setInterval(t => {
            this.timecount--;
            if (this.timecount == 0) {
                this.Timeup();
                clearInterval(this.timer);
            }

        }, 1000);


    }

    sendAnswer(option) {

        this.questioncounter++;



        this.working = true;
        clearInterval(this.timer);
        var answer = { 'questionId': option.questionId, 'optionId': option.id };

        var headers = new Headers();
        headers.append('Content-Type', 'application/json');

        this.http.post('/api/trivia', JSON.stringify(answer), { headers: headers })
            .map(res => res.json())
            .subscribe(
            answerIsCorrect => {
                this.answered = true;
                this.correctAnswer = (answerIsCorrect === true);
                this.working = false;
            },
            err => {
                this.gameend = true;
                this.working = false;
            });

    }


    Timeup() {

        this.questioncounter++;
        clearInterval(this.timer);
        this.working = true;
        var answer = { 'questionId': this.options[0].questionId, 'optionId': this.options[0].id, 'userId': 'wrong' };

        var headers = new Headers();
        headers.append('Content-Type', 'application/json');

        this.http.post('/api/trivia/', JSON.stringify(answer), { headers: headers })
            .map(res => res.json())
            .subscribe(
            answerIsCorrect => {
                this.answered = true;
                this.correctAnswer = false;
                this.working = false;
            },
            err => {
                this.title = "Oops... something went wrong";
                this.working = false;
            });

    }


    afterViewInit() {
        this.nextQuestion();
    }


}





bootstrap(AppComponent);