import { Component, Input, Inject } from "@angular/core";
import { Comment } from "../../models/comment";
import { Http } from "@angular/http";
import { ActivatedRoute } from "@angular/router";
@Component({
    selector: 'comments',
    templateUrl: './comment.component.html'
})
export class CommentComponent {
    @Input() comments: Comment[];
    @Input() productId: string;
    @Input() parentId: number;

    private _http: Http;
    private _baseUrl: string;
    private route: ActivatedRoute;

    public markdown: string;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string, route: ActivatedRoute) {
        this._http = http;
        this._baseUrl = baseUrl;
        this.route = route;
    }

    public message = new Array<string>();
    public error: string;
    answer(data: number, inputNumber: number) {        
        this.error = "";        
        let message = this.message[inputNumber];
        console.log(message);
        this._http.get(this._baseUrl + 'api/SampleData/addcomment?productId=' + this.productId + "&message=" + message + "&=" + data)
            .subscribe(result => {
                console.log(result.json());
                var ans = result.json();
               
                if (ans.result.includes("ok")) {
                    
                    let c = new Comment(ans.id, message, new Date(), new Array<Comment>(), "You", "");
                    var com = this.comments.find(c => c.id == data) as Comment;
                    console.log(com.answers);
                    com.answers.push(c);
                    console.log(com);

                } else {
                    this.error = ans.result;
                }
            });
    }
}