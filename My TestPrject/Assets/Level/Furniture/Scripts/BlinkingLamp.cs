using System.Collections;
using UnityEngine;

public class BlinkingLamp : MonoBehaviour
{
    [SerializeField] float speedblinking;
    [SerializeField] float numberBlinksPerPhase;
    [SerializeField] float lightOffFrequency;
    [SerializeField] float timeLightOff;
    [SerializeField] private Light _lightLamp;
    private float _firstBlink;
    private float _startIntensity;
    private Coroutine _blinkingLight;
    private Renderer _lightRend;

    private void Start()
    {
        _firstBlink = Random.Range(0, lightOffFrequency);
        _startIntensity = _lightLamp.intensity;
        _lightRend = GetComponent<Renderer>();
        _blinkingLight = StartCoroutine(BlinkingLight());
    }

    private void OnDisable()
    {
        StopCoroutine(_blinkingLight);
    }

    private IEnumerator BlinkingLight()
    {
        yield return new WaitForSeconds(_firstBlink);
        while (true)
        {
            for (var i = 0; i < numberBlinksPerPhase; i++)
            {
                SetLighting(false);
                yield return new WaitForSeconds(speedblinking);
                SetLighting(true);
                yield return new WaitForSeconds(speedblinking);
            }

            SetLighting(false);
            yield return new WaitForSeconds(timeLightOff);
            SetLighting(true);
            yield return new WaitForSeconds(lightOffFrequency);
        }
    }

    private void SetLighting(bool value)
    {
        _lightLamp.enabled = value;
        if (value)
        {
            _lightRend.material.SetColor("_EmissionColor", Color.white);
        }
        else
        {
            _lightRend.material.SetColor("_EmissionColor", Color.black);
        }
    }
}
