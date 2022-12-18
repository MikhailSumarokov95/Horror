using System.Collections;
using UnityEngine;

public class MenuRoom : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private Transform[] monsterSpawnPoint;
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
        monster = Instantiate(monsterPrefab, monsterSpawnPoint[0].position, monsterSpawnPoint[0].rotation);
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
            yield return new WaitForSeconds(2);
            var numberSpawnPoint = Random.Range(0, monsterSpawnPoint.Length);
            monster.transform.SetPositionAndRotation(monsterSpawnPoint[numberSpawnPoint].position,
                monsterSpawnPoint[numberSpawnPoint].rotation);
        }
    }
}
