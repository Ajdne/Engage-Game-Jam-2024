using UnityEngine;

public class AmbientManager : MonoBehaviour
{
    [Range(0, 1)] public double DreamSlider;
    public Material SkyboxMaterial;
    [SerializeField] private Color _dreamColor;
    [SerializeField] private Color _nightmareColor;
    [SerializeField] private Color _currentColor;
    public float TransitionDuration = 1f;
    private float _transitionTimer = 0f;
    private bool _transitioning = false;
    private Color _targetColor;

    private void Start()
    {
        RenderSettings.skybox = SkyboxMaterial;
        _currentColor = Color.Lerp(_nightmareColor, _dreamColor, (float)DreamSlider);
        // Set the initial sky color
        SkyboxMaterial.SetColor("_Tint", _currentColor);
    }

    private void Update()
    {
        HandleTransition();
        ManageTransition();
    }

    private void HandleTransition()
    {
        if (_transitioning)
        {
            _transitionTimer += Time.deltaTime;
            float t = Mathf.Clamp01(_transitionTimer / TransitionDuration);
            _currentColor = Color.Lerp(_currentColor, _targetColor, t);
            SkyboxMaterial.SetColor("_Tint", _currentColor);
            RenderSettings.skybox.SetColor("_Tint", _currentColor);

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
    }

    private void StartTransition(Color targetColor)
    {
        if (_currentColor != targetColor)
        {
            _targetColor = targetColor;
            _transitionTimer = 0f;
            _transitioning = true;
        }
    }
}
