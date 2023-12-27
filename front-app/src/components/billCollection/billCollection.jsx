import Connector from '../common/Connector'
import { useEffect, useState } from 'react';
import Bill from '../bill/Bill';
import BillEntity from '../common/Entities/BillEntity';
import "./billCollection.css";

const BillCollection = (props) => {
    const { newMessage, events } = Connector();
    const [Bills, setBills] = useState([]);

    useEffect(() => {
        events((_, message) => {
            setBills([...Bills, new BillEntity(message)]);
        });
    });

    return (
        <div className="bill_collection">
            <div className='bill'>
                <span>id</span>
                <span>date</span>
                <span>sale</span>
                <span>dishes</span>
            </div>
            { Bills.length == 0 ? 
                <div className='bill'>Пока ничего не добавлено</div> : 
                Bills.map(b => <Bill {...b}/>)}
        </div>
    );
}

export default BillCollection;