using System.Collections;
using UnityEngine;

public class BlinkingLamp : MonoBehaviour
{
    [SerializeField] float speedblinking;
    [SerializeField] float numberBlinksPerPhase;
    [SerializeField] float lightOffFrequency;
    [SerializeField] float timeLightOff;
    [SerializeField] private Light lightLamp;
    private float _firstBlink;
    private Coroutine _blinkingLight;
    private Renderer _lightRend;

    private void Awake()
    {
        _firstBlink = Random.Range(0, lightOffFrequency);
        _lightRend = GetComponent<Renderer>();
    }

    public void SetLight(bool value)
    {
        if (value) _blinkingLight = StartCoroutine(BlinkingLight());
        else if (_blinkingLight != null) StopCoroutine(_blinkingLight);
        lightLamp.enabled = value;
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
        lightLamp.enabled = value;
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
