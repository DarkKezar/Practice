
class DishEntity {
    public Id : string = "default";
    public Count : number = 0;
    public Name : string | null = null;

    constructor(id: string, count : number) {
        this.Id = id;
        this.Count = count;
    }

    public getName = () => {
        //http request
    }
}

export default DishEntity;