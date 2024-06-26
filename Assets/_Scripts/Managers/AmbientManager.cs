using Unity.VisualScripting;
using UnityEngine;

public class AmbientManager : PersistentSingleton<AmbientManager>
{
    [SerializeField][Range(0, 1)] public double DreamSlider;
    [SerializeField] private Material skyboxMaterial;

    private Material SkyboxMaterialStateSave;

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
        Initialize();
    }

    private void HandleTransition()
    {
        if (_transitioning)
        {
            _transitionTimer += Time.deltaTime;
            float t = Mathf.Clamp01(_transitionTimer / TransitionDuration);
            _currentColor = Color.Lerp(_currentColor, _targetColor, t);
            skyboxMaterial.SetColor("_SkyColor", _currentColor); // Update the skybox material color using the correct property name
            RenderSettings.skybox = skyboxMaterial;

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

        targetColor = Color.Lerp(_nightmareColor, _dreamColor, (float)DreamSlider);

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
        SkyboxMaterialStateSave = skyboxMaterial;
    }

    public void Update()
    {
        CalculateDreamSlider(); // Ensure DreamSlider is calculated in real-time
        HandleTransition();
        ManageTransition();
    }

    private void CalculateDreamSlider()
    {
        double dreamPoints = _gameManager.DreamPlayer.GetTileNumber();
        double nightmarePoints = _gameManager.NightmarePlayer.GetTileNumber();
        double totalPoints = dreamPoints + nightmarePoints;
        Debug.Log("Calculated DreamSlider");
        DreamSlider = totalPoints > 0 ? dreamPoints / totalPoints : 0.5; // Avoid division by zero
    }

    //reset skybox material
    public void ResetSkyboxMaterial()
    {
        skyboxMaterial = SkyboxMaterialStateSave;
    }
}
