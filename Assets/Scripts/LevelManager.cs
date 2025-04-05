using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;
    private const string LevelKey = "HighestLevelCompleted";
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
    public void LoadLevel_Name(string level)
    {
        SceneManager.LoadScene(level);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void Start()
    {
        if (levelButtons.Length > 0)
        {
            Levels_completed();
        }
    }
    public void Levels_completed()
    {
        int highestLevel = PlayerPrefs.GetInt(LevelKey, 0);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i <= highestLevel)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }
}
