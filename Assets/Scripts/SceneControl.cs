using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    public void LoadCharacterSelection()
    {
        SceneManager.LoadScene("CharacterSelection");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void SelectCharacterAndLoad(string characterName)
    {
        Time.timeScale = 1;
        PlayerPrefs.SetString("SelectedCharacter", characterName);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level_1");
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level_1");
    }
}
