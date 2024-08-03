using UnityEngine;

namespace Game.FullGame
{
    public class PlayerAnimation : MonoBehaviour
    {
        [Header("Animations")]
        [SerializeField] private Animator bodyAnimator;
        [SerializeField] private Animator legAnimator;

        private Player player;
        private Shooting shooting;

        private void Awake()
        {
            player = GetComponent<Player>();
            shooting = GetComponent<Shooting>();
        }

        private void Update()
        {
            legAnimator.SetBool("IsMoving", player.IsMoving);
            bodyAnimator.SetBool("IsMoving", player.IsMoving);
            bodyAnimator.SetBool("IsShooting", shooting.IsShooting);
        }

        private void OnDisable()
        {
            legAnimator.SetBool("IsMoving", false);
            bodyAnimator.SetBool("IsMoving", false);
            bodyAnimator.SetBool("IsShooting", false);
        }
    }
}
