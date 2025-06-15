using UnityEngine;

public class MatrixGameController : MonoBehaviour
{
    private string mapName = "map1";
    public static MatrixGameController Instance { get; private set; }

    private MatrixGameModel model;

    private void Awake()
    {
        Instance = this;
        model = new MatrixGameModel();
        model.OnChangedMap += HandleMapUpdated;
       // model.OnCarCollision += HandleCarCollision;
    }
    private void Start()
    {   
        mapName = InitLevel.mapName; // Lấy tên bản đồ từ InitLevel
        model.LoadCarMatrixFromCSV_Resources(mapName);
    }

    private void HandleMapUpdated()
    {
        Debug.Log("Level loaded. Number of cars: " + model.Cars.Count);
        GameView.Instance.UpdateMatrix(model.Cars); 
    }

    public bool HandleCarCollision(int carIndex , out Vector2 pos)
    {
        if (model.IsEscape(carIndex,out pos))
        {
            Debug.Log("out");

            return false;
        }
        Debug.Log("cant out");
        return true;
    }   
}
