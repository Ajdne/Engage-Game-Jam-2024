using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : NonPersistentSingleton<UIManager>
{
    private void Awake()
    {
        Initialize();
        GameCanvas.enabled = true;
        PauseCanvas.enabled = false;
        EndCanvas.enabled = false;
    }
    [SerializeField]
    private Canvas GameCanvas;
    [SerializeField]
    private Canvas PauseCanvas;
    [SerializeField]
    private Canvas EndCanvas;

    [SerializeField]
    private GameObject PauseMenuPanel;

    [SerializeField]
    private GameObject SettingsPanel;

    public TextMeshProUGUI WinnerText;

    //restart button (reload current scene with scenemanager)
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void EndScreen()
    {
        GameCanvas.enabled = false;
        PauseCanvas.enabled = false;
        EndCanvas.enabled = true;
    }
    public void PauseGame()
    {
        GameCanvas.enabled = false;
        PauseCanvas.enabled = true;
        EndCanvas.enabled = false;
        Time.timeScale = 0f;
    }
    public void UnpauseGame()
    {
        GameCanvas.enabled = true;
        PauseCanvas.enabled = false;
        EndCanvas.enabled = false;
        Time.timeScale = 1f;
    }
    private void OnEnable()
    {
        EventManager.GameOverEvent += EndScreen;
    }
    private void OnDisable()
    {
        EventManager.GameOverEvent -= EndScreen;
    }

}
