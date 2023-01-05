using UnityEngine;

public class SettingMultyplatform : MonoBehaviour
{
    [SerializeField] private GameObject pcSetting;
    [SerializeField] private GameObject mobileSetting;

    private void OnEnable()
    {
        var isMobile = FindObjectOfType<GameManager>().IsMobile;
        mobileSetting.SetActive(isMobile);
        pcSetting.SetActive(!isMobile);
    }
}
