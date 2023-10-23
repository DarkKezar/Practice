import './Bill.css';

function Bill(props) {
    return (
        <div className="bill">
            <span>{ props.data.id }</span>
            
            <span className="nameField">{ props.data.dateTime }</span>
            <span className="suppliesField">{ props.data.sale * 100 }%</span>

            <table>
                <tr>
                    <td>id</td>
                    <td>count</td>
                </tr>
                {props.data.Dishes.map(d => 
                <tr>
                    <td>{d.first}</td>
                    <td>{d.second}</td>
                </tr>)}
            </table>
        </div>
    )
    
}

export default Bill;