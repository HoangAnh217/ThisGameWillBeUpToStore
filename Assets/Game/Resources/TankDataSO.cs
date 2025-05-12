using UnityEngine;

[CreateAssetMenu(fileName = "TankData", menuName = "ScriptableObjects/TurretData", order = 0)]
public class TankDataSO : ScriptableObject
{
    public int id;
    public string nameTurret = "Turret";
    //public int cost;
    public int damage;
    public float maxShootDistance;
   // public float fireRate;

    // Tham chiếu đến TurretDataSO của cấp độ tiếp theo (nếu có)
    //public TankDataSO nextLevel;
}
