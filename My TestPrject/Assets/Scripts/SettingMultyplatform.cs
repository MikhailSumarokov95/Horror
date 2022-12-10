using UnityEngine;

public class SettingMultyplatform : MonoBehaviour
{
    [SerializeField] private GameObject pcSetting;

    private void Start()
    {
        pcSetting.SetActive(!FindObjectOfType<GameManager>().IsMobile);
    }
}
