using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject LevelSelection;
    [SerializeField] private GameObject SettingsMenu;
    [SerializeField] private GameObject guideBook;
    [SerializeField] private AudioMixer audioMixer;

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
    public void OpenGuideBook()
    {
        guideBook.SetActive(true);
    }
    public void CloseGuideBook()
    {
        guideBook.SetActive(false);
    }

    public void changeMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }
    public void changeMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }
    public void changeFXVolume(float volume)
    {
        audioMixer.SetFloat("fxVolume", volume);
    }

}
