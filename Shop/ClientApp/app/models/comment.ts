export class Comment {
    constructor(public id: number, public message: string, public publishedDateTime: Date, public answers: Comment[],
        public userName: string, public userId: string) {

    }
}