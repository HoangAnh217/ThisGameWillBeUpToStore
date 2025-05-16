using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float dame = 50;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(dame);
               // GetComponent<Proj>
            }
        }
        /*else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject); // Destroy the projectile when it hits a wall
        }*/
    }
}
