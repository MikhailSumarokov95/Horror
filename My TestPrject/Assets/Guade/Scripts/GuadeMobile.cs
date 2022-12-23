using System.Collections;
using UnityEngine;

public class GuadeMobile : MonoBehaviour
{
    [SerializeField] private GameObject battery;
    [SerializeField] private GameObject powerEngineer;


    private void Start()
    {
        StartCoroutine(GuadeScenario());
    }

    private IEnumerator GuadeScenario()
    {
        battery.SetActive(true);
        while (battery != null)
        {
            yield return null;
        }
        powerEngineer.SetActive(true);
        while (powerEngineer != null)
        {
            yield return null;
        }

        PlayerPrefs.SetInt("guade", 1);
        FindObjectOfType<GameManager>().StartMenu();
    }
}
