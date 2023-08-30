using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private AudioMixer audioMixer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale > 0)
        {
            if(!settingsPanel.activeSelf) openSettings();
        }
    }

    public void openSettings()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        settingsPanel.SetActive(true);
    }
    public void closeSettings()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        settingsPanel.SetActive(false);
    }

    public void GameWon()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        victoryPanel.SetActive(true);
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        defeatPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
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

    private void OnEnable()
    {
        PlayerScore.gameWon += GameWon;
        PlayerScore.gameOver += GameOver;
    }
    private void OnDisable()
    {
        PlayerScore.gameWon -= GameWon;
        PlayerScore.gameOver -= GameOver;
    }

}
