using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Eyes))]
public class Energy : MonoBehaviour
{
    public float speed;
    public float forcedSpeed;

    [SerializeField, Range(0f,1f)] private float value = 0;
    [SerializeField] private Slider bar;
    [SerializeField] private Image isNotDischargedImage;
    [SerializeField] private Image isDischargedImage;
    [SerializeField] private UnityEvent onZeroEnergy;
    public float Value
    {
        get { return value; }
        set {
            this.value = value;
            if (value <= 0)
            {
                this.value = 0;
                onZeroEnergy.Invoke();
                eyes.IsForce = false;
                eyes.CanControl = false;
            }
            if (value > 1) this.value = 1;
            if (bar != null) bar.value = this.value;
        }
    }

    private Eyes eyes;
    private void Awake()
    {
        eyes = GetComponent<Eyes>();
    }

    private void OnEnable()
    {
        SetFullEnergy();
    }

    private void Update()
    {
        EnergyCheck();
        if (!eyes.IsOpen && !eyes.IsForce) return;
        if (eyes.IsForce)
            Value -= forcedSpeed * Time.deltaTime;
        Value -= speed * Time.deltaTime;
    }

    private void EnergyCheck()
    {
        if (Value < 0.01f)
        {
            isDischargedImage.gameObject.SetActive(true);
            isNotDischargedImage.gameObject.SetActive(false);
        }
        else
        {
            isDischargedImage.gameObject.SetActive(false);
            isNotDischargedImage.gameObject.SetActive(true);
        }
    }

    [ContextMenu("SetFullEnergy")]
    public void SetFullEnergy()
    {
        Value = 1;
        eyes.CanControl = true;
    }
}
