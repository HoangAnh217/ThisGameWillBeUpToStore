using UnityEngine;
using System.IO;
using System.Text;

public class CarExporter : MonoBehaviour
{
    public Transform parentTransform; // Gán MapEditor trong Inspector
    public string map;
    [ContextMenu("Export Cars to CSV")]
    public void ExportToCSV()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("x;y;angle;value");

        foreach (Transform child in parentTransform)
        {
            Vector3 pos = child.position;
            float angle = child.eulerAngles.z;

            // Lấy value từ script nếu có, hoặc gán tạm
            int value = Random.Range(0,6);
/*            CarModel model = child.GetComponent<CarModel>();
            if (model != null) value = model.value;*/

            sb.AppendLine($"{pos.x:F2};{pos.y:F2};{angle:F0};{value}");
        }
        string pathString = "Game/Resources/map/" + map + ".csv";
        string path = Path.Combine(Application.dataPath, pathString);
        File.WriteAllText(path, sb.ToString());
        Debug.Log("Exported to: " + path);
    }
}
