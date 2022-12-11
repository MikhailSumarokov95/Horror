using UnityEngine;
using UnityEngine.AI;

public class LevelsCreator : MonoBehaviour
{
    public int NumberCurrentLevel { get; private set; }

    [SerializeField] private GameObject[] modeForLevelNumberPrefabs;
    [SerializeField] private GameObject[] mapPrefabs;
    [SerializeField] private GameObject menuRoom;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject monster;
    
    private GameObject currentLevel;

    public enum Mode
    {
        Survival,
        Escape
    }


    private void Start()
    {
        menuRoom.SetActive(true);
        //foreach (var level in levels)
        //    if (level.GetComponent<Level>() == null) Debug.LogError("Level does not contain script \"Level\"");
    }

    public void CreateLevel(int number)
    {
        menuRoom.SetActive(false);
        Destroy(currentLevel);
        var numberMap = Random.Range(0, mapPrefabs.Length);
        currentLevel = Instantiate(mapPrefabs[numberMap], Vector3.zero, Quaternion.identity);
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
        Instantiate(modeForLevelNumberPrefabs[number], currentLevel.transform);
        var level = FindObjectOfType<Level>();
        level.NumberLevel = NumberCurrentLevel = number;
        player.SetActive(false);
        monster.SetActive(false);
        player.SetActive(true);
        monster.SetActive(true);
        var map = currentLevel.GetComponent<Map>();
        player.transform.SetPositionAndRotation(map.PointPlayerSpawn.position, map.PointPlayerSpawn.rotation);
        monster.transform.SetPositionAndRotation(map.PointMonsterSpawn.position, map.PointMonsterSpawn.rotation);
    }
}