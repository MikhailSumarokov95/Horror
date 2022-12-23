using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LevelsCreator : MonoBehaviour
{
    public int NumberCurrentLevel { get; private set; }

    [SerializeField] private GameObject[] modeForLevelNumberPrefabs;
    [SerializeField] private GameObject[] mapPrefabs;
    [SerializeField] private GameObject menuRoom;
    [SerializeField] private GameObject guadeMobile;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject monster;
    private GameObject _currentLevel;

    private void Start()
    {
        CheckPrefabs();
    }

    public void CreateLevel(int number)
    {
        menuRoom.SetActive(false);
        Destroy(_currentLevel);
        var numberMap = Random.Range(0, mapPrefabs.Length);
        _currentLevel = Instantiate(mapPrefabs[numberMap], Vector3.zero, Quaternion.identity);
        player.SetActive(false);
        monster.SetActive(false);
        StartCoroutine(WaitOneFrameAndInitializationLevel(number));
    }

    public void ReturnMenu()
    {
        if (_currentLevel != null) Destroy(_currentLevel);
        menuRoom.SetActive(true);
        monster.SetActive(false);
        player.SetActive(false);
    }

    public void CreateGuadeLevel()
    {
        menuRoom.SetActive(false);
        _currentLevel = Instantiate(guadeMobile);
        _currentLevel.GetComponent<NavMeshSurface>().BuildNavMesh();
        player.SetActive(true);
        var map = _currentLevel.GetComponent<Map>();
        player.transform.SetPositionAndRotation(map.PointPlayerSpawn.position, map.PointPlayerSpawn.rotation);
        monster.transform.SetPositionAndRotation(map.PointMonsterSpawn.position, map.PointMonsterSpawn.rotation);
    }

    private void InitializationLevel(int number)
    {
        var map = _currentLevel.GetComponent<Map>();
        Instantiate(modeForLevelNumberPrefabs[number], _currentLevel.transform);
        var level = FindObjectOfType<Level>();
        level.NumberLevel = NumberCurrentLevel = number;
        map.GetComponent<NavMeshSurface>().BuildNavMesh();
        player.SetActive(true);
        monster.SetActive(true);
        player.transform.SetPositionAndRotation(map.PointPlayerSpawn.position, map.PointPlayerSpawn.rotation);
        monster.transform.SetPositionAndRotation(map.PointMonsterSpawn.position, map.PointMonsterSpawn.rotation);
    }

    private IEnumerator WaitOneFrameAndInitializationLevel(int number)
    {
        yield return null;
        InitializationLevel(number);
    }

    private void CheckPrefabs()
    {
        foreach (var mode in modeForLevelNumberPrefabs)
            if (mode.GetComponent<Level>() == null) Debug.LogError("No script inherited from \"Level\" script");

        foreach (var map in mapPrefabs)
            if (map.GetComponent<Map>() == null) Debug.LogError("Map does not contain script \"Map\"");
    }

    [ContextMenu("ClearSaveGuade")]
    public void ClearSaveGuade() => PlayerPrefs.SetInt("guade", 0);
}