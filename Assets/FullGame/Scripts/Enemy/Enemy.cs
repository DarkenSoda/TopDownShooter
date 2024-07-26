using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [SerializeField] private int damage = 50;

    [SerializeField] private AudioClip spawnSound;
    [SerializeField] private AudioClip deathSound;

    private Rigidbody2D rb;
    private Transform player;
    private Vector2 moveDirection;

    public event EventHandler<float> OnHealthChanged;
    public event EventHandler OnDeath;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;

        player.GetComponent<PlayerHealth>().OnDeath += OnPlayerDeath;

        currentHealth = maxHealth;

        if (spawnSound != null)
            AudioSource.PlayClipAtPoint(spawnSound, transform.position);
    }

    private void Update()
    {
        moveDirection = (player.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        rb.velocity = moveDirection * speed;
    }

    private void Rotate()
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        OnHealthChanged?.Invoke(this, (float)currentHealth / maxHealth);

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        if (deathSound != null)
            AudioSource.PlayClipAtPoint(deathSound, transform.position);

        player.GetComponent<PlayerHealth>().OnDeath -= OnPlayerDeath;
        OnDeath?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    public void Heal(int amount) { }

    private void OnPlayerDeath(object sender, EventArgs e)
    {
        enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable player = other.GetComponent<IDamageable>();
            player.TakeDamage(damage);
        }
    }
}
