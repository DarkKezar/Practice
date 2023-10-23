import Dish from "../dish/dish";
import './dishCollection.css'

const DishCollection = (props) => {
    return (
        <table>
            <thead>
                <tr>
                    <td>Name</td>
                    <td>Count</td>
                </tr>
            </thead>
            <tbody>
                { props.items.length === 0 ? 
                    <tr>no items</tr> : 
                    props.items.map(d => <Dish key={d.Id} {...d} />) 
                }
            </tbody>
        </table>
    )
}

export default DishCollection;