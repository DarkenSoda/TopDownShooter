using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject owner;
    [SerializeField] private int damage = 50;

    public void SetOwner(GameObject owner)
    {
        this.owner = owner;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == owner)
            return;

        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
