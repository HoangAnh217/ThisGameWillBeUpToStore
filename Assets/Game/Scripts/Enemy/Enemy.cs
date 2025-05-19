using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Enemy : TriBehaviour, IDamageable
{   

    // component
    private Animator animator;
   // [SerializeField] private List<Sprite> sps;
    private int currentColorIndex;
    public int CurrentColorIndex=> currentColorIndex;
    private SpriteRenderer spr;
    private EnemyDespawn enemyDespawn;
    private EffectSpawner effectSpawner;

    private float maxHealth = 100f;
    public float health = 100f;
    [SerializeField] private Image healthSlider;
    [SerializeField] private Image damageEffectSlider;
    [SerializeField] private Canvas canvasEnemy;
    // attack 
    [SerializeField] private float minY;
    private bool isAttacking = false;

    protected override void Awake()
    {
        spr = transform.Find("Model").GetComponentInChildren<SpriteRenderer>();
        effectSpawner = EffectSpawner.Instance;
    }
    protected override void Start()
    {
        enemyDespawn = GetComponent<EnemyDespawn>();
        animator = transform.Find("Model").GetComponentInChildren<Animator>();
    }
    public override void OnEnable()
    {
        base.OnEnable();
        canvasEnemy.gameObject.SetActive(false);
        health =maxHealth;
    }
    private void Update()
    {
        if (!isAttacking)
        {
            Movement();
            CheckAttackTrigger();
        }
        else
        {
            Attack();
        }
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
    private void CheckAttackTrigger()
    {
        if (transform.position.y <= minY)
        {
            isAttacking = true;
            Debug.Log("Enemy is now attacking!");
        }
    }
    private void Attack()
    {
        // animator.SetBool("Attack", true);
        Debug.Log("Enemy attacks!");
        Player.Instance.TakeDame(30f);

        Die();
    }


    private void Movement()
    {   
        transform.Translate(Vector3.down * Time.deltaTime * 0.8f);
    }
    public void TakeDamage(float damage)
    {
        //DisplayDamageText(damage);
        effectSpawner.SpawnEffectText(EffectSpawner.TextFloat, transform.position, Quaternion.identity, false, damage);
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
