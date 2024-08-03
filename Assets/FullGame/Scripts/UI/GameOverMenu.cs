using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.FullGame
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverMenu;

        [SerializeField] private Button restartButton;
        [SerializeField] private Button optionsButton;
        [SerializeField] private Button quitButton;

        [SerializeField] private PlayerHealth player;

        private void Awake()
        {
            player.OnDeath += (_, _) =>
            {
                Time.timeScale = 0f;
                gameOverMenu.SetActive(true);
            };
        }

        private void Start()
        {
            restartButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1f;
            });

            quitButton.onClick.AddListener(() => Application.Quit());
        }
    }
}
