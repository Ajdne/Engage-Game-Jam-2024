using Unity.VisualScripting;
using UnityEngine;

public class AmbientManager : MonoBehaviour
{
    [SerializeField][Range(0, 1)] public double DreamSlider;
    public Material SkyboxMaterial;
    [SerializeField] private Color _dreamColor;
    [SerializeField] private Color _nightmareColor;
    [SerializeField] private Color _currentColor;
    [SerializeField] private Color _targetColor;
    public float TransitionDuration = 1f;
    private float _transitionTimer = 0f;
    private bool _transitioning = false;
    
    private GameManager _gameManager;

    private void Awake()
    {
        RenderSettings.skybox = SkyboxMaterial;
        _currentColor = Color.Lerp(_nightmareColor, _dreamColor, (float)DreamSlider);
        // Set the initial sky color
        SkyboxMaterial.SetColor("_Tint", _currentColor);
    }

    private void HandleTransition()
    {
        if (_transitioning)
        {
            _transitionTimer += Time.deltaTime;
            float t = Mathf.Clamp01(_transitionTimer / TransitionDuration);
            _currentColor = Color.Lerp(_currentColor, _targetColor, t);
            SkyboxMaterial.SetColor("_SkyColor", _currentColor); // Update the skybox material color using the correct property name
            RenderSettings.skybox = SkyboxMaterial;

            if (_transitionTimer >= TransitionDuration)
            {
                _transitioning = false;
            }
        }
    }


    private void ManageTransition()
    {
        // Interpolate between the dream and nightmare colors based on the DreamSlider value
        Color targetColor = Color.Lerp(_nightmareColor, _dreamColor, (float)DreamSlider);
        StartTransition(targetColor);
        Debug.Log("Managvao transition");
    }

    private void StartTransition(Color targetColor)
    {
        if (_currentColor != targetColor)
        {
            _targetColor = targetColor;
            _transitionTimer = 0f;
            _transitioning = true;
            Debug.Log("Startovao transition");
        }
    }


    //public void start
    public void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void Update()
    {
        HandleTransition();
        ManageTransition();
    }

    private double CalculateDreamSlider()
    {
        double dreamPoints = _gameManager.DreamPlayer.CalculateScore();
        double nightmarePoints = _gameManager.NightmarePlayer.CalculateScore();
        double totalPoints = dreamPoints + nightmarePoints;
        Debug.Log("Izracunao dream slider");
        return dreamPoints / totalPoints;
    }
}
