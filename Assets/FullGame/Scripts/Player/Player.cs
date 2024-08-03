using UnityEngine;

namespace Game.FullGame
{
    public class Player : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float speed = 5f;

        private Rigidbody2D rb;
        private PlayerHealth playerHealth;
        private PlayerDash playerDash;

        public Vector2 MoveDirection { get; private set; }
        public Vector2 AimDirection { get; private set; }

        public bool IsMoving { get; private set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            playerHealth = GetComponent<PlayerHealth>();
            playerDash = GetComponent<PlayerDash>();

            playerHealth.OnDeath += (_, _) =>
            {
                Destroy(gameObject);
            };
        }

        private void Update()
        {
            ProcessInputs();

            IsMoving = MoveDirection != Vector2.zero;
        }

        private void FixedUpdate()
        {
            Move();
            Rotate();
        }

        private void ProcessInputs()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            MoveDirection = new Vector2(moveX, moveY).normalized;

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            AimDirection = (mousePosition - rb.position).normalized;
        }

        private void Move()
        {
            if (playerDash.IsDashing)
                return;

            rb.velocity = MoveDirection * speed;
        }

        private void Rotate()
        {
            float angle = Mathf.Atan2(AimDirection.y, AimDirection.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }
}
