using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Elements")]
    [SerializeField] private GameObject titleText;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject creditsButton;
    [SerializeField] private GameObject leaveGameButton;
    [SerializeField] private GameObject creditsPanel;

    [SerializeField] private GameObject mainMenuElements;
    [SerializeField] private GameObject pauseMenuElements;

    [Header("References")]
    [SerializeField] private CurtainMove curtainScript;

    [Header("Configurables")]
    [SerializeField] public bool isPlayMode = false;

    private bool isPaused = false;
    private Coroutine pauseCoroutine;

    private void Start()
    {
        if (pauseMenuElements != null)
            pauseMenuElements.SetActive(false);

        if (creditsPanel != null)
            creditsPanel.SetActive(false);

        isPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPlayMode)
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void StartGame()
    {
        if (curtainScript != null)
            curtainScript.OpenCurtains();

        if (mainMenuElements != null)
            mainMenuElements.SetActive(false);

        isPlayMode = true;
        isPaused = false;
    }

    public void PauseGame()
    {
        isPaused = true;

        if (curtainScript != null)
            curtainScript.CloseCurtains();

        if (pauseCoroutine != null)
            StopCoroutine(pauseCoroutine);

        pauseCoroutine = StartCoroutine(PauseDelay());
    }

    public void ResumeGame()
    {
        isPaused = false;

        if (pauseCoroutine != null)
            StopCoroutine(pauseCoroutine);

        if (curtainScript != null)
            curtainScript.OpenCurtains();

        if (pauseMenuElements != null)
            pauseMenuElements.SetActive(false);
    }

    private IEnumerator PauseDelay()
    {
        yield return new WaitForSeconds(0.75f);
        if (isPaused && pauseMenuElements != null)
        {
            pauseMenuElements.SetActive(true);
        }
    }

    public void Credits()
    {
        if (creditsPanel != null) creditsPanel.SetActive(true);
        if (mainMenuElements != null) mainMenuElements.SetActive(false);
    }

    public void Back()
    {
        if (creditsPanel != null) creditsPanel.SetActive(false);
        if (mainMenuElements != null) mainMenuElements.SetActive(true);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        if (pauseCoroutine != null)
            StopCoroutine(pauseCoroutine);

        if (curtainScript != null)
            curtainScript.CloseCurtains();

        if (mainMenuElements != null) mainMenuElements.SetActive(true);
        if (pauseMenuElements != null) pauseMenuElements.SetActive(false);

        isPlayMode = false;
        isPaused = false;
    }
}