using System.Collections;

using UnityEngine;
using UnityEngine.UI;

namespace Monzen.Modules.Fade
{
    public sealed class FadeController : MonoBehaviour
    {
        [Header("Auto Start?")]
        [SerializeField] private bool _autoStart = true;

        [Header("Setup Start Fade")]
        [SerializeField] private float _durationTimeStartFade;
        [SerializeField] private float _delayTimeStartFade;
        [SerializeField] private FadeType _fadeType;

        [Header("Color fade")]
        [Tooltip("The Alpha is defined by the FadeType")]
        [SerializeField] private Color _startColor;
        [SerializeField] private Color _endColor;

        private Image _image;

        private void Awake()
        {
            if (_durationTimeStartFade <= 0) _durationTimeStartFade = 3;

            _image = transform.GetChild(0).GetComponent<Image>();
            _image.color = _startColor;
        }

        private void Start()
        {
            if (_autoStart) StartCoroutine(FadeFunction(_durationTimeStartFade, _fadeType));
        }

        private IEnumerator FadeFunction(float tempoFade, FadeType fade)
        {
            ChooseFadeType(fade);
            float t = 0;
            while (_startColor != null)
            {
                t += 1 * Time.unscaledDeltaTime;
                float normalizedTime = t / tempoFade;

                _image.color = Color.Lerp(_startColor, _endColor, normalizedTime);
                yield return null;
            }

            _image.color = _endColor;
        }

        private IEnumerator FadeFunction(float tempoFade, FadeType fade, Color fadeColor)
        {
            ChooseFadeType(fade, fadeColor);
            float t = 0;
            while (_startColor != null)
            {
                t += 1 * Time.unscaledDeltaTime;
                float normalizedTime = t / tempoFade;

                _image.color = Color.Lerp(_startColor, _endColor, normalizedTime);
                yield return null;
            }

            _image.color = _endColor;
        }

        private IEnumerator CompleteFade(float tempoFade)
        {
            StartCoroutine(FadeFunction(tempoFade, FadeType.In));
            yield return new WaitForSecondsRealtime(tempoFade);
            StartCoroutine(FadeFunction(0.5f, FadeType.Out));
            yield return null;
        }

        private IEnumerator CompleteFade(float tempoFade, Color color)
        {
            StartCoroutine(FadeFunction(tempoFade, FadeType.In, color));
            yield return new WaitForSecondsRealtime(tempoFade);
            StartCoroutine(FadeFunction(0.5f, FadeType.Out, color));
            yield return null;
        }

        private void ChooseFadeType(FadeType fadeType)
        {
            _image.color = Color.black;

            if (fadeType == FadeType.In)
            {
                _startColor = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
                _endColor = new Color(_image.color.r, _image.color.g, _image.color.b, 1);
                _image.color = _startColor;
            }
            else if (fadeType == FadeType.Out)
            {
                _startColor = new Color(_image.color.r, _image.color.g, _image.color.b, 1);
                _endColor = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
                _image.color = _startColor;
            }
        }

        private void ChooseFadeType(FadeType fadeType, Color fadeColor)
        {
            if (fadeType == FadeType.In)
            {
                _startColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0);
                _endColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1);
                _image.color = _startColor;
            }
            else if (fadeType == FadeType.Out)
            {
                _startColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 1);
                _endColor = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0);
                _image.color = _startColor;
            }
        }

        #region Methods (To be called in other scripts)
        public void FadeFunctionMethod(float tempoFade, FadeType fade)
        {
            StopAllCoroutines();
            StartCoroutine(FadeFunction(tempoFade, FadeType.In));
        }

        public void CompleteFadeMethod(float time)
        {
            StopAllCoroutines();
            StartCoroutine(CompleteFade(time));
        }

        public void CompleteFadeMethod(float time, Color color)
        {
            StopAllCoroutines();
            StartCoroutine(CompleteFade(time, color));
        }
        #endregion
    }
}