using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour {

    public static FadeManager Instance { get; set; }
    public Image fadeImage;
    public Color selectedColor;

    private bool _isInTransition;
    private float _transition;
    private bool _isShowing;
    private float _duration;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null && Instance != this)
            Destroy(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            FadeIn();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            FadeOut();
        }

        if (!_isInTransition)
            return;
        _transition += (_isShowing) ? Time.deltaTime * (1 / _duration) : -Time.deltaTime * (1 / _duration);
        fadeImage.color = Color.Lerp(new Color(1, 1, 1, 0), selectedColor, _transition);

        if (_transition > 1 || _transition < 0)
            _isInTransition = false;
    }

    public void FadeOut()
    {
        Fade(false, 1f);
    }

    public void FadeIn()
    {
        Fade(true, 0.5f);
        Invoke("FadeOut", 0.5f);
        print("invoking");
    }

    private void Fade(bool showing, float duration)
    {
        _isShowing = showing;
        _isInTransition = true;
        _duration = duration;
        _transition = (_isShowing) ? 0 : 1;
    }
}
