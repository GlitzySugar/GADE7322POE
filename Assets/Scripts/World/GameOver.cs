using UnityEngine;
public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject nexusTurret;
    private string turretTag = "NexusTowerShooting";
    public static bool gameOver;

    private void Start()
    {
        nexusTurret = GameObject.Find(turretTag);
        gameOverPanel.active = false;
    }
    private void Update()
    {
        if (nexusTurret == null)
        {
           GameIsOver();
        }
    }

    //Called when the nexuxs health is 0
    public void GameIsOver()
    {
        gameOver = true;
        gameOverPanel.active = true;
        Debug.Log("Game Over");
        Time.timeScale = 0.0f;
    }
}
