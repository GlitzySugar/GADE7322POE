using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject youWinPanel;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject store;
    [SerializeField] GameObject moneyCount;
    Tower Tower;
    private string turretTag = "NexusTowerShooting";

    private void Start()
    {
        EnemyTargets.score = 0;
        EnemySpawn.spawnScore = 0;
        Time.timeScale = 1;
        store.SetActive(true); moneyCount.SetActive(true);
        youWinPanel.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverPanel.SetActive(false);
    }
    private void Update()
    {
        if (Tower.gameIsOver)
        {
           GameIsOver();
        }
        if (EnemyTargets.score >= 75)
        {
            YouWin();
        }
    }

    //Called when the nexuxs health is 0
    public void GameIsOver()
    {
        gameOverPanel.SetActive(true);
        store.SetActive(false);
        moneyCount.SetActive(false);
        Debug.Log("Game Over");
        Time.timeScale = 0.0f;
    }
    public void YouWin()
    {
        youWinPanel.SetActive(true);
        store.SetActive(false);
        moneyCount.SetActive(false);
        Debug.Log("You Win");
        Time.timeScale = 0.0f;
    }
    public void Restart()
    {
        youWinPanel.SetActive(false);
        pauseMenu.SetActive(false);
        store.SetActive(true);
        moneyCount.SetActive(true);
        Tower.gameIsOver = false;
        EnemySpawn.spawnScore = 0;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        store.SetActive(false);
        moneyCount.SetActive(false);
        Time.timeScale = 0.0f;
    }
    public void Resume()
    {
        store.SetActive(true); moneyCount.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
