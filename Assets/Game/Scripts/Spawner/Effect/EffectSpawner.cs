using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class EffectSpawner : Spawner
{
    public static  EffectSpawner Instance { get; private set; }
    public static string TextFloat = "FloatingText";
    public static string LevelUp = "LevelUp";
    public static string Smoke = "Smoke";

   // [Header("effect")]
  //  [SerializeField] private Canvas canvas;
   /* [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject parentCoin;
*/
    // public static string TextFloat = "DamageText";
    // private TextMeshPro damageTextPrefab;
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }
    public void SpawnEffectText(string effectName, Vector3 position, Quaternion quaternion, bool isCrit,float dame)
    {
        Transform floatingText = base.Spawn(effectName, position, quaternion);
        TextMeshPro textMeshPro = floatingText.GetComponent<TextMeshPro>();
        if (textMeshPro != null)
        {
            textMeshPro.text = dame.ToString("F0"); // Làm tròn số nếu muốn

            // Tuỳ chỉnh màu và kích cỡ nếu là crit
            if (isCrit)
            {
                textMeshPro.color = Color.red;
                textMeshPro.fontSize *= 1.5f;
            }
            else
            {
                textMeshPro.color = Color.yellow;
            }

        }
    }
}
