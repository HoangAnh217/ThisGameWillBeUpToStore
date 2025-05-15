using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerEscapeCar : MonoBehaviour
{
    public static ControllerEscapeCar instance;
    private void Awake()
    {
        instance = this;
    }
    public void EscapeCar(int index)
    {
        ModelEscapeCar.instance.HandlerLogic(index);
    }

}
