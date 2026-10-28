using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Elements")]
    [SerializeField] GameObject titleText;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject creditsButton;
    [SerializeField] GameObject leaveGameButton;
    [SerializeField] GameObject creditsPanel;

    [SerializeField] GameObject mainMenuElements;
    [SerializeField] GameObject pauseMenuElements;

    [Header("References")]
    [SerializeField] CurtainMove curtainScript;

    [Header("Configurables")]
    [SerializeField] bool isPauseMenu;
    [SerializeField] public bool isPlayMode = false;

    private void Start()
    {
        pauseMenuElements.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPlayMode)
        {
            curtainScript.CloseCurtains();
            StartCoroutine(PauseDelay());
            isPlayMode = false;
        }
    }

    IEnumerator PauseDelay()
    {
        yield return new WaitForSeconds(0.75f);
        pauseMenuElements.SetActive(true);
    }

    public void ResumeGame()
    {
        curtainScript.OpenCurtains();
        pauseMenuElements.SetActive(false);
        isPlayMode = true;
    }

    public void StartGame()
    {
        curtainScript.OpenCurtains();
        mainMenuElements.SetActive(false);
        isPlayMode = true;
    }

    public void Credits()
    {
        creditsPanel.SetActive(true);
        mainMenuElements.SetActive(false);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        creditsPanel.SetActive(false);
        mainMenuElements.SetActive(true);
    }

    public void BackToMenu()
    {
        curtainScript.CloseCurtains();
        mainMenuElements.SetActive(true);
        pauseMenuElements.SetActive(false);
        isPlayMode = false;
    }
}