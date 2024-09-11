using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject store;
    [SerializeField] GameObject moneyCount;
    Tower Tower;
    private string turretTag = "NexusTowerShooting";

    private void Start()
    {
        store.SetActive(true); moneyCount.SetActive(true);
        gameOverPanel.SetActive(false);
    }
    private void Update()
    {
        if (Tower.gameIsOver)
        {
           GameIsOver();
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
    public void Restart()
    {
        Tower.gameIsOver = false;
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
