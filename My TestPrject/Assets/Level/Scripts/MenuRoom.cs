using System.Collections;
using UnityEngine;

public class MenuRoom : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private Transform[] monsterSpawnPoint;
    private GameObject _monster;
    private Coroutine _transferMonsterCoroutine;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        if (!_gameManager.IsMobile) Cursor.lockState = CursorLockMode.None;
        _monster = Instantiate(monsterPrefab, monsterSpawnPoint[0].position, monsterSpawnPoint[0].rotation);
        _monster.transform.SetParent(gameObject.transform);
        _transferMonsterCoroutine = StartCoroutine(TransferMonster());
    }

    private void OnDisable()
    {
        if (!_gameManager.IsMobile) Cursor.lockState = CursorLockMode.Locked;
        Destroy(_monster);
        StopCoroutine(_transferMonsterCoroutine);
    }

    private IEnumerator TransferMonster()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            var numberSpawnPoint = Random.Range(0, monsterSpawnPoint.Length);
            _monster.transform.SetPositionAndRotation(monsterSpawnPoint[numberSpawnPoint].position,
                monsterSpawnPoint[numberSpawnPoint].rotation);
        }
    }
}
