using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Objects")]

    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private GameObject _deathMenu;
    [SerializeField]
    private GameObject _victoryMenu;

    private int _score;

    public void UpdateScore(int scoreAdd)
    {
        _score += scoreAdd;
        _scoreText.text = "Score: " + _score;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Death()
    {
        Time.timeScale = 0f;
        _deathMenu.SetActive(true);
    }

    public void Winner()
    {
        Time.timeScale = 0f;
        _victoryMenu.SetActive(true);
    }

    public void ReastartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
