export class recipeModel{
    constructor(public id: number = 0, 
        public name: string = "", 
        public cuisine: string = "",
        public ingredients: any[] = [], 
        public image: string = "",
        public instructions: any[] = [],
        public prepTimeMinutes: number = 0,
        public cookTimeMinutes: number = 0
    ){

    }
}