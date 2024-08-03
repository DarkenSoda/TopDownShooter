using UnityEngine;

namespace Game.FullGame
{
    public class Shooting : MonoBehaviour
    {
        [Header("Shooting")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireRate = .5f;
        [SerializeField] private float bulletSpeed = 10f;

        [SerializeField] private AudioClip shootingSound;

        private float lastFireTime;

        public bool IsShooting { get; private set; }

        private void Update()
        {
            IsShooting = Input.GetMouseButton(0);

            if (IsShooting && Time.time > fireRate + lastFireTime)
            {
                Shoot();
                lastFireTime = Time.time;
            }
        }

        private void Shoot()
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            bulletComponent.SetOwner(gameObject);

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 firePos = firePoint.position;
            Vector2 aimDirection = (mousePosition - firePos).normalized;

            bulletRb.AddForce(aimDirection * bulletSpeed, ForceMode2D.Impulse);

            AudioSource.PlayClipAtPoint(shootingSound, Camera.main.transform.position);

            Destroy(bullet, 2f);
        }
    }
}
