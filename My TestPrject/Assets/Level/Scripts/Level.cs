using UnityEngine;
using ToxicFamilyGames.FirstPersonController;

public abstract class Level : MonoBehaviour
{
    [SerializeField] private Transform pointPlayerSpawn;
    [SerializeField] private Transform pointMonsterSpawn;
    [SerializeField] private Transform[] positionSpawnObject;
    protected bool isGameOver;
    public Transform PointPlayerSpawn { get { return pointPlayerSpawn; } }
    public Transform PointMonsterSpawn { get { return pointMonsterSpawn; } }
    public int NumberLevel { get; set; }

    public void WinLevel()
    {
        FindObjectOfType<LevelsProgress>().OpenLevel(NumberLevel + 1);
        FindObjectOfType<GameManager>().OnWin();
    }

    public void LossLevel()
    {
        if (isGameOver) return;
        isGameOver = true;
        FindObjectOfType<GameManager>().OnLoss();
        FindObjectOfType<Character>().IsBrokenNeck = true;
    }

   protected void CreateRandomObjectsOnLevel(GameObject obj, int amount)
    {
        Vector3 positionSpawn;
        int numberPointSpawn;
        for (var i = 0; i < amount; i++)
        {
            numberPointSpawn = Random.Range(0, positionSpawnObject.Length);
            if (positionSpawnObject[numberPointSpawn] == null)
            {
                i--;
                continue;
            }
            positionSpawn = positionSpawnObject[numberPointSpawn].position;
            positionSpawnObject[numberPointSpawn] = null;
            Instantiate(obj, positionSpawn + obj.transform.position, obj.transform.rotation)
                .transform.SetParent(transform);
        }
    }
}
