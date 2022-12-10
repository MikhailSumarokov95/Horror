using UnityEngine;
using UnityEngine.AI;

public class LevelsCreator : MonoBehaviour
{
    public int NumberCurrentLevel { get; private set; } 

    [SerializeField] private GameObject menuRoom;
    [SerializeField] private GameObject[] levels;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject monster;
    [SerializeField] private Flashlight flashlight;
    private GameObject currentLevel;

    private void Start()
    {
        menuRoom.SetActive(true);
        foreach (var level in levels)
            if (level.GetComponent<Level>() == null) Debug.LogError("Level does not contain script \"Level\"");
    }

    public void CreateLevel(int number)
    {
        menuRoom.SetActive(false);
        Destroy(currentLevel);
        currentLevel = Instantiate(levels[number], Vector3.zero, Quaternion.identity);
        InitializationLevel(number);
        
    }

    public void ReturnMenu()
    {
        Destroy(currentLevel);
        menuRoom.SetActive(true);
        monster.SetActive(false);
        player.SetActive(false);
    }

    private void InitializationLevel(int number)
    {
        var level = currentLevel.GetComponent<Level>();
        level.NumberLevel = NumberCurrentLevel = number;
        player.SetActive(false);
        monster.SetActive(false);
        player.SetActive(true);
        monster.SetActive(true);
        player.transform.SetPositionAndRotation(level.PointPlayerSpawn.position, level.PointPlayerSpawn.rotation);
        monster.transform.SetPositionAndRotation(level.PointMonsterSpawn.position, level.PointMonsterSpawn.rotation);


    }
}