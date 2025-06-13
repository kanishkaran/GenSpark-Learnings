export class recipeModel{
    constructor(public id: number, 
        public name: string = "", 
        public cuisine: string = "",
        public ingredients: any[] = [], 
        public image: string = "",
        public instructions: any[] = [],
        public prepTimeMinutes: number,
        public cookTimeMinutes: number
    ){

    }
}