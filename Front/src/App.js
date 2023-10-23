import './App.css';
import Catalog from './components/Catalog/Catalog';

function App() {
  const Bills = [
      {
        "id": 1,
        "dateTime": "12:40 12.05.2020",
        "Dishes": [
          { "first" : 12, "second" : 5 },
          { "first" : 2, "second" : 15 },
          { "first" : 15, "second" : 55 }
        ],
        "sale": 0.4
      },
      {
        "id": 2,
        "dateTime": "12:40 12.05.2020",
        "Dishes": [
          { "first" : 12, "second" : 5 },
          { "first" : 2, "second" : 15 },
          { "first" : 15, "second" : 55 }
        ],
        "sale": 0.02
      },
      {
        "id": 3,
        "dateTime": "12:40 12.05.2020",
        "Dishes": [
          { "first" : 12, "second" : 5 },
          { "first" : 2, "second" : 15 },
        ],
        "sale": 0
      }
  ];
  return (
    <div className="App">
     <Catalog items = { Bills } />
    </div>
  );
}

export default App;
