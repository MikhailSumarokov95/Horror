using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ToxicFamilyGames.FirstPersonController;

public class GuadeMobile : MonoBehaviour
{
    [SerializeField] private Button goPauseButton;
    [SerializeField] private Flashlight flashlight;
    [SerializeField] private Energy energy;
    [SerializeField] private GameObject battery;
    [SerializeField] private GameObject energyDrink;
    [SerializeField] private Image handForEyesButtonImage;
    [SerializeField] private TMP_Text forEyesButtonText;
    [SerializeField] private Button isCloseEyesButton;
    [SerializeField] private TMP_Text energyLevelText;
    [SerializeField] private Image handEnergyLevelImage;
    [SerializeField] private TMP_Text energyLevelLookForEnergyText;
    [SerializeField] private Image handTutorialPickUpImage;
    [SerializeField] private Button pickUpButton;
    [SerializeField] private TMP_Text flashlightButtonText;
    [SerializeField] private Image handFlashlightButtonImage;
    [SerializeField] private Button isOffFlashlightButton;
    [SerializeField] private TMP_Text batteriesText;
    [SerializeField] private TMP_Text batteriesFindText;
    [SerializeField] private GameObject monster;
    [SerializeField] private Character player;
    [SerializeField] private GameObject head;

    private void OnEnable()
    {
        StartCoroutine(GuadeScenario());
        flashlight.gameObject.SetActive(false);
        goPauseButton.gameObject.SetActive(false);
        flashlight.OnDisable();
    }

    private void OnDisable()
    {
        goPauseButton.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (energy.Value < 0.05) energy.SetFullEnergy();
    }

    private IEnumerator GuadeScenario()
    {
        handForEyesButtonImage.gameObject.SetActive(true);
        forEyesButtonText.gameObject.SetActive(true);
        
        yield return new WaitForButtonClick(isCloseEyesButton);

        handForEyesButtonImage.gameObject.SetActive(false);
        forEyesButtonText.gameObject.SetActive(false);

        energyLevelText.gameObject.SetActive(true);
        handEnergyLevelImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(5f);

        energyLevelText.gameObject.SetActive(false);
        handEnergyLevelImage.gameObject.SetActive(false);

        energyLevelLookForEnergyText.gameObject.SetActive(true);
        energyDrink.SetActive(true);

        while(energyDrink != null)
        {
            if (pickUpButton.gameObject.activeInHierarchy) handTutorialPickUpImage.gameObject.SetActive(true);
            else handTutorialPickUpImage.gameObject.SetActive(false);
            yield return null;
        }

        handTutorialPickUpImage.gameObject.SetActive(false);
        energyLevelLookForEnergyText.gameObject.SetActive(false);

        flashlight.gameObject.SetActive(true);
        flashlight.GetComponent<Flashlight>().SetActiveFlashlight(false);

        flashlightButtonText.gameObject.SetActive(true);
        handFlashlightButtonImage.gameObject.SetActive(true);

        yield return new WaitForButtonClick(isOffFlashlightButton);

        flashlightButtonText.gameObject.SetActive(false);
        handFlashlightButtonImage.gameObject.SetActive(false);

        batteriesText.gameObject.SetActive(true);
        battery.SetActive(true);

        yield return new WaitForSeconds(1f);

        batteriesFindText.gameObject.SetActive(true);

        while(battery != null)
        {
            yield return null;
        }

        batteriesFindText.gameObject.SetActive(false);
        batteriesText.gameObject.SetActive(false);

        head.transform.localRotation = Quaternion.identity;
        player.isLocked = true;
        monster.SetActive(true);
        var monsterPosition = head.transform.position - head.transform.transform.forward * 2 + new Vector3(0, - 0.6f, 0);
        var monsterRotation = player.transform.rotation * monster.transform.rotation;
        monster.transform.SetPositionAndRotation(monsterPosition, monsterRotation);

        while (true)
        {
            var rotationForLookAnMonster = Quaternion.LookRotation(- monster.transform.forward);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, rotationForLookAnMonster, 0.1f);
            if (Mathf.Abs(player.transform.rotation.eulerAngles.y - rotationForLookAnMonster.eulerAngles.y) < 1f) break;
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        player.isLocked = false;

        PlayerPrefs.SetInt("guade", 1);
        FindObjectOfType<GameManager>().StartMenu();
    }
}
