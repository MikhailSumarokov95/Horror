using UnityEngine;

public class LampOptimisation : MonoBehaviour
{
    private BlinkingLamp _blinkingLamp;
    
    private void Start()
    {
        _blinkingLamp = GetComponent<BlinkingLamp>();
    }

    private void Update()
    {
        DetermineVisibilityLamp();
    }

    private void DetermineVisibilityLamp()
    {
        Debug.DrawRay(Camera.main.transform.position, transform.position - Camera.main.transform.position, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, transform.position - Camera.main.transform.position, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                SetLightLamp(true);
            }
            else SetLightLamp(false);
        }
        else SetLightLamp(false);
    }

    private void SetLightLamp(bool value)
    {
        if (_blinkingLamp.enabled == value) return; 
        _blinkingLamp.SetLight(value);
        _blinkingLamp.enabled = value;
    }
}
