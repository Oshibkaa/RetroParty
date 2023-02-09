using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public int _score;
    public GameObject DeathMenu;
    public GameObject Win;

    public void UpdateScore(int scoreAdd)
    {
        _score += scoreAdd;
        scoreText.text = "Score: " + _score;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Death()
    {
        Time.timeScale = 0f;
        DeathMenu.SetActive(true);
    }

    public void Winner()
    {
        Time.timeScale = 0f;
        Win.SetActive(true);
    }

    public void ReastartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
