using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker 
{
    public static bool CheckRayCollision(GameObjectModel car, Vector2 rayStart, Vector2 rayEnd,out Vector2 pos)
    {

        Vector2[] vertices = car.GetVertices();
        for (int i = 0; i < 4; i++)
        {
            Vector2 a = vertices[i];
            Vector2 b = vertices[(i + 1) % 4];
            if (RayIntersectsSegment(rayStart, rayEnd, a, b,out pos))
            {
                return true;
            }
        }
        pos = Vector2.zero;
        return false;
    }
    private static bool RayIntersectsSegment(Vector2 rayStart, Vector2 rayEnd, Vector2 segmentStart, Vector2 segmentEnd , out Vector2 pos)
    {
        Vector2 rayDirection = rayEnd - rayStart;
        Vector2 segmentDirection = segmentEnd - segmentStart;
        float crossProduct = rayDirection.x * segmentDirection.y - rayDirection.y * segmentDirection.x;
        pos = Vector2.zero; // Ray and segment are parallel
        if (crossProduct == 0)
        {
            return false;
        }
        float qpxr = (segmentStart.x - rayStart.x) * rayDirection.y - (segmentStart.y - rayStart.y) * rayDirection.x;
        float t = ((segmentStart.x - rayStart.x) * segmentDirection.y - (segmentStart.y - rayStart.y) * segmentDirection.x) / crossProduct;
        float u = qpxr / crossProduct;
        if (t >= 0 && u >= 0 && u <= 1)
        {
            pos = rayStart + t * rayDirection; // <-- Tính chính xác vị trí va chạm
            return true;
        }
        return false;
    }
}
