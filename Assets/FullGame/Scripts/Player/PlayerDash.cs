using System.Collections;
using UnityEngine;

namespace Game.FullGame
{
    public class PlayerDash : MonoBehaviour
    {
        [SerializeField] private float dashSpeed = 5f;
        [SerializeField] private float dashDuration = .5f;

        private Rigidbody2D rb;
        private Player player;
        private PlayerHealth playerHealth;

        public bool IsDashing { get; private set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            player = GetComponent<Player>();
            playerHealth = GetComponent<PlayerHealth>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !IsDashing)
                StartCoroutine(DashCoroutine());
        }

        private IEnumerator DashCoroutine()
        {
            IsDashing = true;
            Dash();

            yield return new WaitForSeconds(dashDuration);

            IsDashing = false;
        }

        private void Dash()
        {
            Vector2 dashDirection = player.MoveDirection == Vector2.zero ? player.AimDirection : player.MoveDirection;

            rb.AddForce(dashDirection * dashSpeed, ForceMode2D.Impulse);
        }
    }
}