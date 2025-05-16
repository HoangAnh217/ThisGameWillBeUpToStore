using UnityEngine;

public class GameObjectModel
{
    public Vector2 _position;
    public Vector2 _size;
    public float _angle;
    public int colorIndex;

    public GameObjectModel(Vector2 position, float angle,int colorIndex, Vector2 _size)
    {
        this._position = position;
        this._size = _size;
        this._angle = angle;
        this.colorIndex = colorIndex;
    }

    public Vector2[] GetVertices()
    {
        float rad = Mathf.Deg2Rad * _angle;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        Vector2 halfSize = _size / 2;
        Vector2[] localCorners = new Vector2[]
        {
            new Vector2(-halfSize.x, -halfSize.y),
            new Vector2( halfSize.x, -halfSize.y),
            new Vector2( halfSize.x,  halfSize.y),
            new Vector2(-halfSize.x,  halfSize.y)
        };

        Vector2[] worldCorners = new Vector2[4];
        for (int i = 0; i < 4; i++)
        {
            float x = localCorners[i].x;
            float y = localCorners[i].y;
            worldCorners[i] = new Vector2(
                _position.x + x * cos - y * sin,
                _position.y + x * sin + y * cos
            );
        }
        return worldCorners;
    }

    public Vector2 GetForwardRay(float length)
    {
        float rad = Mathf.Deg2Rad * _angle;
        return new Vector2(
            _position.x + Mathf.Cos(rad) * length,
            _position.y + Mathf.Sin(rad) * length
        );
    }
}
