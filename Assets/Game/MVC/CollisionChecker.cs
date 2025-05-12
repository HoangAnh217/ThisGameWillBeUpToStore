using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker 
{
    public static bool CheckRayCollision(GameObjectModel car, Vector2 rayStart, Vector2 rayEnd)
    {

        Vector2[] vertices = car.GetVertices();
        for (int i = 0; i < 4; i++)
        {
            Vector2 a = vertices[i];
            Vector2 b = vertices[(i + 1) % 4];
            if (RayIntersectsSegment(rayStart, rayEnd, a, b))
            {
                return true;
            }
        }
        return false;
    }
    private static bool RayIntersectsSegment(Vector2 rayStart, Vector2 rayEnd, Vector2 segmentStart, Vector2 segmentEnd)
    {
        Vector2 rayDirection = rayEnd - rayStart;
        Vector2 segmentDirection = segmentEnd - segmentStart;
        float crossProduct = rayDirection.x * segmentDirection.y - rayDirection.y * segmentDirection.x;
        if (crossProduct == 0)
            return false;
        float qpxr = (segmentStart.x - rayStart.x) * rayDirection.y - (segmentStart.y - rayStart.y) * rayDirection.x;
        float t = ((segmentStart.x - rayStart.x) * segmentDirection.y - (segmentStart.y - rayStart.y) * segmentDirection.x) / crossProduct;
        float u = qpxr / crossProduct;
        return  (t >= 0) && (u >= 0 && u <= 1);
    }
}
