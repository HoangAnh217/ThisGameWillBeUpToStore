using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public static GameView Instance;
    List<Transform> tanks = new List<Transform>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
       /* MatrixGameController.Instance.UpdateLevel += UpdateMatrix;
        //   UpdateView();
        MatrixGameController.Instance.UpdateView += UpdateView;
        MatrixGameController.Instance.CarCollison += CarCollision;*/
    }

    public void UpdateMatrix(List<GameObjectModel> Cars)
    {
        GenerateMap(Cars);
    }
     

    private void GenerateMap(List<GameObjectModel> cars)
    {
        tanks.Clear();
        for (int i = 0; i < cars.Count; i++)
        {
            GameObjectModel car = cars[i];
            Vector3 viewPosition = new Vector3(car._position.x, car._position.y,0);
            Transform carTransform = TankSpawner.Instance.Spawn(TankSpawner.TankString, viewPosition, Quaternion.Euler(0, 0, car._angle));
            Tank tank = carTransform.GetComponent<Tank>();
            tank.SetColor(car.colorIndex);
            tanks.Add(carTransform);
        }
    }
    private void CarCollision(int car)
    {

        Transform t = tanks[car];

        Vector3 originalScale = t.localScale;
     //   Color originalColor = Color.white;

      /*  Renderer renderer = t.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            originalColor = renderer.material.color;
        }*/

        Sequence seq = DOTween.Sequence();

        seq.Append(t.DOScale(originalScale * 1.1f, 0.1f).SetEase(Ease.OutBack));

        seq.Append(t.DOShakeRotation(0.2f, new Vector3(0, 10, 0), 10, 90, false, ShakeRandomnessMode.Harmonic));

        /*if (renderer != null)
        {
            seq.Join(renderer.material.DOColor(Color.red, 0.1f));
        }
*/
        seq.Append(t.DOScale(originalScale, 0.1f));
       /* if (renderer != null)
        {
            seq.Join(renderer.material.DOColor(originalColor, 0.1f));
        }*/

        seq.Play();
    }

    private void UpdateView(int index)
    {
        Transform t = tanks[index];
        Destroy(t.gameObject);
        tanks.RemoveAt(index);
    }
    
    
}