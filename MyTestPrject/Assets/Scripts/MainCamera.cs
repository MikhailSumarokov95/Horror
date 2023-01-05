using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private GameObject headPlayers;
    [SerializeField] private GameObject pointMenuRoom;
    private Transform target;

    private void LateUpdate()
    {
        if (pointMenuRoom.activeInHierarchy) target = pointMenuRoom.transform;
        else target = headPlayers.transform;
        transform.SetPositionAndRotation(target.position, target.rotation);
    }
}