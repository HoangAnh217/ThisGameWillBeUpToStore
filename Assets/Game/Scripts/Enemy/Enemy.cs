using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Enemy : TriBehaviour, IDamageable
{
    private int currentColorIndex;
    public int CurrentColorIndex=> currentColorIndex;
   // [SerializeField] private List<Sprite> sps;
    private SpriteRenderer spr;
    private EnemyDespawn enemyDespawn;

    private float maxHealth = 100f;
    public float health = 100f;
    [SerializeField] private Image healthSlider;
    [SerializeField] private Image damageEffectSlider;
    [SerializeField] private Canvas canvasEnemy;
    // components
    protected override void Awake()
    {
        spr = transform.Find("Model").GetComponentInChildren<SpriteRenderer>();
    }
    protected override void Start()
    {
        enemyDespawn = GetComponent<EnemyDespawn>();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        canvasEnemy.gameObject.SetActive(false);
        health =maxHealth;
    }
    private void Update()
    {
        Movement();
    }
    public void SetColor(int index)
    {
        Color[] colors =
                {
                    Color.red,
                    Color.blue,
                    Color.green,
                    Color.yellow,
                };
      
        currentColorIndex = index;
        spr.color = colors[index];
    }
    private void Movement()
    {   
        transform.Translate(Vector3.down * Time.deltaTime * 0.8f);
    }
    public void TakeDamage(float damage)
    {
        //DisplayDamageText(damage);
        canvasEnemy.gameObject.SetActive(true);
        damageEffectSlider.fillAmount = health/ maxHealth;
        health -= damage;
        healthSlider.fillAmount = health /maxHealth;
        damageEffectSlider.DOFillAmount(health / maxHealth,0.5f).SetEase(Ease.Linear);
        if (health <= 0)
        {
            Die();
        }
    }
    /*private void DisplayDamageText(int damage)
    {

        Transform obj = EffectSpawner.Instance.Spawn(EffectSpawner.TextFloat, transform.position, Quaternion.identity);
        obj.GetComponent<TextMeshPro>().text = damage.ToString();
    }*/
    void Die()
    {
        enemyDespawn.DeSpawnObj();
       // EnemySpawner.Instance.EnemyDie();
    }
}
