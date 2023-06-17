using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject LevelSelection;
    [SerializeField] private GameObject SettingsMenu;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void OpenLevelSelect()
    {
        LevelSelection.SetActive(true);
    }
    public void closeLevelSelect()
    {
        LevelSelection.SetActive(false);
    }
    public void OpenSettings()
    {
        SettingsMenu.SetActive(true);
    }
    public void closeSettings()
    {
        SettingsMenu.SetActive(false);
    }
    public void openLevel(string levelName)
    {
        if (levelName != "") SceneManager.LoadScene(levelName);
    }

}
