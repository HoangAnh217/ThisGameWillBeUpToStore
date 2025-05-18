using System;
using System.Collections.Generic;
using UnityEngine;

public class MatrixGameModel
{
    public List<GameObjectModel> Cars { get; private set; } = new List<GameObjectModel>();

    public event Action OnChangedMap;

    public void LoadCarMatrixFromCSV_Resources(string fileNameWithoutExtension)
    {
        Cars.Clear();

        TextAsset csvFile = Resources.Load<TextAsset>("Map/" + fileNameWithoutExtension);
        if (csvFile == null)
        {
            Debug.LogError("Không tìm thấy file: Map/" + fileNameWithoutExtension);
            return;
        }

        string[] lines = csvFile.text.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string rawLine in lines)
        {
            string line = rawLine.Trim();
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("x")) continue;

            string[] parts = line.Split(';');
            if (parts.Length < 4 || string.IsNullOrWhiteSpace(parts[0])) continue;

            try
            {
                float x = float.Parse(parts[0]);
                float y = float.Parse(parts[1]);
                float angle = float.Parse(parts[2]);
                int value = int.Parse(parts[3]);

                /*Color[] colors =
                {
                    Color.red,
                    Color.blue,
                    Color.green,
                    Color.yellow,
                };*/

                var car = new GameObjectModel(new Vector2(x, y), angle, value, new Vector2(0.4f, 0.4f));
                Cars.Add(car);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Lỗi khi parse dòng: {line} => {e.Message}");
            }
        }

        Debug.Log($"Tải thành công: {Cars.Count} xe");
        OnChangedMap?.Invoke();
    }

    public bool IsEscape(int index)
    {
        if (index < 0 || index >= Cars.Count)
        {
            return false;

        }

        GameObjectModel selectedCar = Cars[index];
        Vector2 rayStart = selectedCar._position;
        float halfWidth = selectedCar._size.x/2;

        Vector2 perpendicular = new Vector2(
            Mathf.Cos((selectedCar._angle + 90) * Mathf.Deg2Rad),
            Mathf.Sin((selectedCar._angle + 90) * Mathf.Deg2Rad)
        ) * halfWidth;

        Vector2 rayStart1 = rayStart + perpendicular;
        Vector2 rayStart2 = rayStart - perpendicular;

        Vector2 rayDirection = new Vector2(
            Mathf.Cos(selectedCar._angle * Mathf.Deg2Rad),
            Mathf.Sin(selectedCar._angle * Mathf.Deg2Rad)
        ) * 20;

        Vector2 rayEnd1 = rayStart1 + rayDirection;
        Vector2 rayEnd2 = rayStart2 + rayDirection;

      /*  Debug.Log(rayStart1 +"  " + rayEnd1);
        Debug.Log(rayStart2 +"  " + rayEnd2);
*/
        foreach (GameObjectModel car in Cars)
        {
            if (car == selectedCar) continue;

            if (CollisionChecker.CheckRayCollision(car, rayStart1, rayEnd1) ||
                CollisionChecker.CheckRayCollision(car, rayStart2, rayEnd2))
            {
               // OnCarCollision?.Invoke(Cars.IndexOf(car));
                Debug.Log($"Xe  va chạm với xe {Cars.IndexOf(car)}");
                return false;
            }
        }

        Cars.Remove(selectedCar);
        return true;
    }
}
