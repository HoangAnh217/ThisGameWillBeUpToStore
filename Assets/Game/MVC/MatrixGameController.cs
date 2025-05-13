using UnityEngine;

public class MatrixGameController : MonoBehaviour
{
    [SerializeField] private string mapName = "map1";
    public static MatrixGameController Instance { get; private set; }

    private MatrixGameModel model;

    private void Awake()
    {
        Instance = this;
        model = new MatrixGameModel();
        model.OnChangedMap += HandleMapUpdated;
       // model.OnCarCollision += HandleCarCollision;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            model.LoadCarMatrixFromCSV_Resources(mapName);
        }

       /* if (Input.GetKeyDown(KeyCode.E))
        {
            bool escaped = model.IsEscape(0);
            Debug.Log("Escape result: " + escaped);
        }*/
    }

    private void HandleMapUpdated()
    {
        Debug.Log("Level loaded. Number of cars: " + model.Cars.Count);
        GameView.Instance.UpdateMatrix(model.Cars); 
    }

    public bool HandleCarCollision(int carIndex)
    {
        if (model.IsEscape(carIndex))
        {
            Debug.Log("out");
            return false;
        }
        Debug.Log("cant out");
        return true;
    }   
}
