using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private int start_level_count = 2;
    private const string LevelKey = "HighestLevelCompleted";
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
    void Start()
    {
        Levels_completed();
    }
    public void Levels_completed()
    {
        int highestLevel = PlayerPrefs.GetInt(LevelKey, 0);
        if (highestLevel < start_level_count)
        {
            PlayerPrefs.SetInt(LevelKey, start_level_count);
        }
        highestLevel = PlayerPrefs.GetInt(LevelKey, 0);
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
