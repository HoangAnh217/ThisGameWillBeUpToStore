using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelEscapeCar : MonoBehaviour
{
    public struct matrixEscapeCar
    {
        public int value;   // 0: ô trống, != 0: ô có xe
        public Vector2Int direction; // Hướng di chuyển của xe (-1,0), (1,0), (0,-1), (0,1)
        public Vector3 transform;
    }

    public static ModelEscapeCar instance;
    private void Awake()
    {
        instance = this;
    }

    public matrixEscapeCar[,] matrix;
    private int rows = 4;
    private int cols = 4;

    void Start()
    {
        matrix = new matrixEscapeCar[rows, cols];

        matrix[0, 0] = new matrixEscapeCar { value = 1, direction = new Vector2Int(0, 1) };
        matrix[0, 1] = new matrixEscapeCar { value = 2, direction = new Vector2Int(1, 0) };
        matrix[0, 2] = new matrixEscapeCar { value = 3, direction = new Vector2Int(0, 1) };
        matrix[0, 3] = new matrixEscapeCar { value = 0, direction = new Vector2Int(0, 0) };

        matrix[1, 0] = new matrixEscapeCar { value = 0, direction = new Vector2Int(0, 0) };
        matrix[1, 1] = new matrixEscapeCar { value = 1, direction = new Vector2Int(1, 0) };
        matrix[1, 2] = new matrixEscapeCar { value = 2, direction = new Vector2Int(0, 1) };
        matrix[1, 3] = new matrixEscapeCar { value = 3, direction = new Vector2Int(1, 0) };

        matrix[2, 0] = new matrixEscapeCar { value = 3, direction = new Vector2Int(1, 0) };
        matrix[2, 1] = new matrixEscapeCar { value = 2, direction = new Vector2Int(0, 1) };
        matrix[2, 2] = new matrixEscapeCar { value = 1, direction = new Vector2Int(1, 0) };
        matrix[2, 3] = new matrixEscapeCar { value = 0, direction = new Vector2Int(0, 0) };

        matrix[3, 0] = new matrixEscapeCar { value = 0, direction = new Vector2Int(0, 0) };
        matrix[3, 1] = new matrixEscapeCar { value = 1, direction = new Vector2Int(1, 0) };
        matrix[3, 2] = new matrixEscapeCar { value = 2, direction = new Vector2Int(0, 1) };
        matrix[3, 3] = new matrixEscapeCar { value = 3, direction = new Vector2Int(1, 0) };
    }

    public bool CanEscape(int i, int j)
    {
        if (matrix[i, j].value == 0)
            return false;

        Vector2Int direction = matrix[i, j].direction;

        return CanMove(i, j, direction.x, direction.y) || CanMove(i, j, -direction.x, -direction.y);
    }

    private bool CanMove(int i, int j, int di, int dj)
    {
        while (true)
        {
            i += di;
            j -= dj;

            if (i < 0 || i >= rows || j < 0 || j >= cols)
            {
                return true;
            }

            if (matrix[i, j].value != 0)
            {
                return false;
            }
        }
    }

    public void HandlerLogic(int index)
    {
        int i = index / cols;
        int j = index % cols;

        if (CanEscape(i, j))
        {
            matrix[i, j].value = 0;
          //  GameView.Instance.UpdateMatrix();
            Debug.Log("Escape");
        }
        else
        {
            Debug.Log("Cannot Escape");
        }
    }
}
