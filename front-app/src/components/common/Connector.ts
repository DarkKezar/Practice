import * as signalR from "@microsoft/signalr";

const URL = process.env.HUB_ADDRESS ?? "http://localhost:5052/bills";

class Connector {
    private connection: signalR.HubConnection;

    public events: (onMessageReceived: (username: string, message: string) => void) => void;

    static instance: Connector;

    constructor() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(URL)
            .withAutomaticReconnect()
            .build();
        this.connection.start().catch(err => document.write(err));
        this.events = (onMessageReceived) => {
            this.connection.on("Bills", (username, message) => {
                onMessageReceived(username, message);
            });
        };
    }
    
    public newMessage = (messages: string) => {
        this.connection.send("newMessage", "foo", messages).then(x => console.log("sent"))
    }

    public static getInstance(): Connector {
        if (!Connector.instance)
            Connector.instance = new Connector();
        return Connector.instance;
    }
}

export default Connector.getInstance;