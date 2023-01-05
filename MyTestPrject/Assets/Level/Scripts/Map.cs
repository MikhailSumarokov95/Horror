using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Transform[] positionSpawnObject;
    [SerializeField] private Transform[] pointSpawn;

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

    public Transform GetSpawnPoint()
    {
        Transform spawnPoint;
        int numberSpawnPoint;
        while (true)
        {
            numberSpawnPoint = Random.Range(0, pointSpawn.Length);
            if (pointSpawn[numberSpawnPoint] == null) continue;
            else spawnPoint = pointSpawn[numberSpawnPoint];
            break;
        }
        pointSpawn[numberSpawnPoint] = null;
        return spawnPoint;
    }
}
