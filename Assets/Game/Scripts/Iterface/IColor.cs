using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColor 
{
    public int GetColorIndexByColor(Color color)
    {
        switch (color)
        {
            case var c when c == Color.red:
                return 0;
            case var c when c == Color.blue:
                return 1;
            case var c when c == Color.green:
                return 2;
            case var c when c == Color.yellow:
                return 3;
            default:
                return -1; // Không kh?p màu nào
        }
    }
}
