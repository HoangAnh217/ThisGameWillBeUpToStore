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

                var car = new GameObjectModel(new Vector2(x, y), angle, value, new Vector2(0.7f, 0.7f));
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

    public bool IsEscape(int index, out Vector2 pos, out int indexCar)
    {
        pos = Vector2.zero;
        indexCar = -1;

        if (index < 0 || index >= Cars.Count)
        {
            return false;
        }

        GameObjectModel selectedCar = Cars[index];
        Vector2 rayStart = selectedCar._position;
        float halfWidth = selectedCar._size.x / 2 - 0.1f;

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

        Debug.DrawLine(rayStart1, rayEnd1, Color.red, 2f);
        Debug.DrawLine(rayStart2, rayEnd2, Color.red, 2f);

        for (int i = 0; i < Cars.Count; i++)
        {
            GameObjectModel car = Cars[i];
            if (car == selectedCar) continue;

            Vector2 tempPos;
            if (CollisionChecker.CheckRayCollision(car, rayStart1, rayEnd1, out tempPos) ||
                CollisionChecker.CheckRayCollision(car, rayStart2, rayEnd2, out tempPos))
            {
                pos = tempPos;
                indexCar = i; // Lưu lại xe bị va
                Debug.Log($"Xe {index} sắp va vào xe {i} tại {pos}");
                return false;
            }
        }

        // Không có va chạm
        Cars.Remove(selectedCar);
        pos = selectedCar._position;
        return true;
    }

}
