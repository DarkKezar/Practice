import DishEntity from "./DishEntity";

class BillEntity {
    public Id : string = "default";
    public DateTime : Date = new Date();
    public Sale : number = 0;
    public Dishes : DishEntity[] = [];

    constructor(json? : string ){
        let obj;
        if(json != null){
            obj = JSON.parse(json);
        }else{
            obj = {
                "Id": "default",
                "DateTime": "2023-10-23T14:00:09.483793+03:00",
                "Sale": 0,
                "Dishes": [
                    {
                        "First": "default",
                        "Second": 12
                    }
                ]
            }
        }
        this.Id = obj["Id"];
        this.DateTime = obj["DateTime"];
        this.Sale = obj["Sale"];

        for(let i = 0; i < obj["Dishes"].length; i++){
            this.Dishes.push(
                new DishEntity(obj["Dishes"][i].First, obj["Dishes"][i].Second)
            );
        }
    }
}

export default BillEntity;