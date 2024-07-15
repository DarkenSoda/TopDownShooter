using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        restartButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));

        quitButton.onClick.AddListener(() => Application.Quit());
    }
}