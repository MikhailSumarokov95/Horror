using System.Collections;
using UnityEngine;

public class MenuRoom : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private Transform[] monsterSpawmPoint;
    private GameObject monster;
    private Coroutine transferMonsterCoroutine;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        if (!gameManager.IsMobile) Cursor.lockState = CursorLockMode.None;
        monster = Instantiate(monsterPrefab, monsterSpawmPoint[0].position, monsterSpawmPoint[0].rotation);
        monster.transform.SetParent(gameObject.transform);
        transferMonsterCoroutine = StartCoroutine(TransferMonster());
    }

    private void OnDisable()
    {
        if (!gameManager.IsMobile) Cursor.lockState = CursorLockMode.Locked;
        Destroy(monster);
        StopCoroutine(transferMonsterCoroutine);
    }

    private IEnumerator TransferMonster()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            var numberSpawnPoint = Random.Range(0, monsterSpawmPoint.Length);
            monster.transform.SetPositionAndRotation(monsterSpawmPoint[numberSpawnPoint].position,
                monsterSpawmPoint[numberSpawnPoint].rotation);
        }
    }
}
