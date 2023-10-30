import DishCollection from "../dishCollection/dishCollection";
import './Bill.css'

const Bill = (props) => {
    console.log(props);
    return(
        <div className="bill">
            <span className="id">{ props.Id }</span>
            <span className="id">{ props.DateTime }</span>
            <span className="id">{ props.Sale * 100 }%</span>
            <DishCollection items = { props.Dishes } />
        </div>
    )
}

export default Bill;