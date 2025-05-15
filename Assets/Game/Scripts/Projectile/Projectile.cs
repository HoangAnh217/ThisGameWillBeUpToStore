using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.Translate(-transform.up * speed * Time.deltaTime);
    }
}
