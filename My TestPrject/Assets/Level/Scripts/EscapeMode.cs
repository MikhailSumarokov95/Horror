using UnityEngine;
using TMPro;

public class EscapeMode : Level
{
    [SerializeField] GameObject key;
    [SerializeField] int amountKey;
    [SerializeField] float numberOfKeysToWin;
    private int numberFoundKeys;
    private TMP_Text numberFoundKeysText;
    

    private void Awake()
    {
        numberFoundKeysText = GameObject.FindGameObjectWithTag("NumberFoundKeysText").GetComponent<TMP_Text>();
        
    }

    private void Start()
    {
        base.Start();
        numberFoundKeysText.text = "0";
        _map.CreateRandomObjectsOnLevel(key, amountKey);
    }

    private void OnDestroy()
    {
        numberFoundKeysText.text = "";
    }

    [ContextMenu("PickUpKey")]
    public void PickUpKey()
    {
        numberFoundKeys++;
        numberFoundKeysText.text = numberFoundKeys.ToString();
        if (numberFoundKeys >= numberOfKeysToWin) WinLevel(); 
    }
}
