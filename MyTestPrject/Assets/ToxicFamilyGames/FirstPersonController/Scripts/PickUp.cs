using UnityEngine;
using HighlightPlus;

public class PickUp : MonoBehaviour
{
    [SerializeField] private float distaceSearchObject;
    [SerializeField] private GameObject pickUpButton;
    [SerializeField] private GameObject pickUpText;
    [SerializeField] private Energy energy;
    [SerializeField] private Flashlight flashlight;
    [SerializeField] private Coins coins;
    private GameObject searchedObject;
    Reward reward;
    private EscapeMode escapeMode;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        pickUpButton.SetActive(false);
        pickUpText.SetActive(false);
    }

    private void Update()
    {
        SearchObject();
        if (!gameManager.IsMobile && GameInput.Key.GetKeyDown("PickUp")) PickUpObject(); // Input.GetKeyDown(KeyCode.E)
    }

    public void PickUpObject()
    {
        if (reward == null) return;
        reward.Invoke();
        Destroy(searchedObject);
    }

    private void SearchObject()
    {
        RaycastHit hit;
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (!Physics.Raycast(ray, out hit, distaceSearchObject))
        {
            ShowPlayerTheyCanTakeItem(false, searchedObject);
            searchedObject = null;
            reward = null;
            return;
        }
        else if (hit.collider.CompareTag("PowerEngineer"))
        {
            searchedObject = hit.collider.gameObject;
            reward = energy.SetFullEnergy;
            ShowPlayerTheyCanTakeItem(true, searchedObject);
        }
        else if (hit.collider.CompareTag("Batteries"))
        {
            searchedObject = hit.collider.gameObject;
            reward = flashlight.SetFullCharge;
            ShowPlayerTheyCanTakeItem(true, searchedObject);
        }
        else if (hit.collider.CompareTag("Coin"))
        {
            searchedObject = hit.collider.gameObject;
            reward = coins.PickUpCoin;
            ShowPlayerTheyCanTakeItem(true, searchedObject);
        }
        else if (hit.collider.CompareTag("Key"))
        {
            if (escapeMode == null) escapeMode = FindObjectOfType<EscapeMode>();
            searchedObject = hit.collider.gameObject;
            reward = escapeMode.PickUpKey;
            ShowPlayerTheyCanTakeItem(true, searchedObject);
        }
        else
        {
            ShowPlayerTheyCanTakeItem(false, searchedObject);
            searchedObject = null;
            reward = null;
        }
    }

    private void ShowPlayerTheyCanTakeItem(bool value, GameObject searchedObject)
    {
        try
        {
            searchedObject.GetComponent<HighlightEffect>().SetHighlighted(value);
        }
        catch { }
        if (gameManager.IsMobile) pickUpButton.SetActive(value);
        else pickUpText.SetActive(value);
    }

    delegate void Reward();
}
