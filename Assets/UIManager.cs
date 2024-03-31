using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public Canvas winCanvas;
    [SerializeField]
    public TextMeshProUGUI Winner;

    //serializable textmeshpro button
    [SerializeField] private TextMeshProUGUI _scoreText;

    //restart button (reload current scene with scenemanager)
    private void Start()
    {
        winCanvas.enabled = false;
    }
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void EndScreen()
    {
        winCanvas.enabled = true;
        Winner.text = "Winner is Player " + GameManager.Instance.Winner;
        winCanvas.gameObject.SetActive(true);
    }

}
