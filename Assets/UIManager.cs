using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField]
    public Canvas winCanvas;

    [SerializeField]
    public TextMeshProUGUI Winner;

    //restart button (reload current scene with scenemanager)

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void EndScreen()
    {
        winCanvas.gameObject.SetActive(true);
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
