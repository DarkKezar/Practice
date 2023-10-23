import Bill from "../Bill/Bill";
import Connector from './../..Connection'
import React, { useEffect, useState } from 'react';

function Catalog(props) {
    const { newMessage, events } = Connector();
    const [message, setMessage] = useState("initial value");
    useEffect(() => {
        events((_, message) => setMessage(message));
    });

    const items = props.items.map((item) => <Bill data = {item}/>);

    return (
        <div className="catalog">
            {items}
        <span>message from signalR: <span style={{ color: "green" }}>{message}</span></span>

        </div>
    )
}

export default Catalog;
