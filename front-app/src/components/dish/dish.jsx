import DishEntity from "../common/Entities/DishEntity";


const Dish = (props) => {
    
    return (
        <tr>
            <td>{ props.Name != null ? props.Name : props.Id }</td>
            <td>{ props.Count }</td>
        </tr>
    )
}

export default Dish;