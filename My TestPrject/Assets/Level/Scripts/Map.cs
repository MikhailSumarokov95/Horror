using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform PointPlayerSpawn { get { return pointPlayerSpawn; } }

    public Transform PointMonsterSpawn { get { return pointMonsterSpawn; } }

    [SerializeField] private Transform pointPlayerSpawn;
    [SerializeField] private Transform pointMonsterSpawn;
    [SerializeField] private Transform[] positionSpawnObject;

    public void CreateRandomObjectsOnLevel(GameObject obj, int amount)
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
