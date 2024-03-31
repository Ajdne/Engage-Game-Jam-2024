using Unity.VisualScripting;
using UnityEngine;

public class AmbientManager : MonoBehaviour
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
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        RenderSettings.skybox = skyboxMaterial;
        _currentColor = Color.Lerp(_nightmareColor, _dreamColor, (float)DreamSlider);
        // Set the initial sky color
        skyboxMaterial.SetColor("_Tint", _currentColor);
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
        HandleTransition();
        ManageTransition();
    }


    private void CalculateDreamSlider()
    {
        double dreamPoints = _gameManager.DreamPlayer.GetTileNumber();
        double nightmarePoints = _gameManager.NightmarePlayer.GetTileNumber();
        double totalPoints = dreamPoints + nightmarePoints;
        Debug.Log("Izracunao dream slider");
        DreamSlider = dreamPoints / totalPoints;
    }

    //singleton‚
    public static AmbientManager Instance;

    //reset skybox material
    public void ResetSkyboxMaterial()
    {
        skyboxMaterial = SkyboxMaterialStateSave;
    }
}
