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
    //phase manager filed
    private PhaseManager _phaseManager;
    

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
        Color targetColor;
        if (PhaseManager.Instance.CurrentPhase == 4)
        {
            // Slide the ambient fully to the player with more points
            if (_gameManager.DreamPlayer.CalculateScore() > _gameManager.NightmarePlayer.CalculateScore())
            {
                targetColor = _dreamColor;
            }
            else
            {
                targetColor = _nightmareColor;
            }
        }
        else
        {
            targetColor = Color.Lerp(_nightmareColor, _dreamColor, (float)CalculateDreamSlider());
        }

        StartTransition(targetColor);
        Debug.Log("Managing transition");
    }

    private void StartTransition(Color targetColor)
    {
        if (_currentColor != targetColor)
        {
            _targetColor = targetColor;
            _transitionTimer = 0f;
            _transitioning = true;
            Debug.Log("Starting transition");
        }
    }

    public void Start()
    {
        _gameManager = GameManager.Instance;
        _phaseManager = PhaseManager.Instance;
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
