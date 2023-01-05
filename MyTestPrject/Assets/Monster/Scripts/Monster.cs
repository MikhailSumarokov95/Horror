using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public bool IsGameOver { get { return _level.IsGameOver; } }

    public bool SetMove { get; set; }

    public bool IsMonsterVisible { get; private set; }

    public float VisibilityDistance { get { return visibilityDistance; } set { visibilityDistance = value; } }

    [SerializeField] private float visibilityDistance = 10f;
    [SerializeField] private float distanceSearchPointRandomMove = 4f;
    [SerializeField] private float distanceToPlayerGameOver = 0.5f;
    private float _distanceToPlayer;
    private NavMeshAgent _monsterNMA;
    private CapsuleCollider _monsterCollider;
    private Level _level;

    private void Start()
    {
        _monsterCollider = GetComponent<CapsuleCollider>();
        _monsterNMA = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _level = FindObjectOfType<Level>();
    }

    private void FixedUpdate()
    {
        if (IsGameOver) return;
        DetermineVisibility();
        if (!IsMonsterVisible || SetMove) Move();
        else _monsterNMA.isStopped = true;
        CheckingMonsterHasCaughtUpWithPlayer();
    }

    private void CheckingMonsterHasCaughtUpWithPlayer()
    {
        if (_distanceToPlayer < distanceToPlayerGameOver) NeckTwist();
    }

    private void Move()
    {
        _monsterNMA.isStopped = false;
        _distanceToPlayer = Mathf.Abs((transform.position - Camera.main.transform.position).magnitude);
        if (_distanceToPlayer < visibilityDistance) MoveToPlayer();
        else RandomMove();
    }

    private void MoveToPlayer()
    {
        _monsterNMA.destination = Camera.main.gameObject.transform.position;
    }

    private void RandomMove()
    {
        if (_monsterNMA.remainingDistance > 3) return;
        var targetX = transform.position.x + Random.Range(-distanceSearchPointRandomMove, distanceSearchPointRandomMove);
        var targetZ = transform.position.z + Random.Range(-distanceSearchPointRandomMove, distanceSearchPointRandomMove);
        NavMeshHit hit;
        NavMesh.SamplePosition(new Vector3(targetX, 0, targetZ), out hit, 3, NavMesh.AllAreas);
        _monsterNMA.destination = hit.position;
    }

    private void DetermineVisibility()
    {
        var cameraPosition = Camera.main.gameObject.transform.position;
        var indent = 0.1f;
        var vectorBetweenPlayerAndMonster = transform.position - cameraPosition;
        var vectorViewportParallelAxis = (Quaternion.Euler(0, 90, 0) * vectorBetweenPlayerAndMonster).normalized;

        var topSideMonster = _monsterCollider.transform.up * _monsterCollider.height + transform.position;
        var botSideMonster = transform.position;
        var leftSideMonster = - vectorViewportParallelAxis * _monsterCollider.radius + transform.position + _monsterCollider.transform.up * _monsterCollider.height;
        var rightSideMonster = vectorViewportParallelAxis * _monsterCollider.radius + transform.position + _monsterCollider.transform.up * _monsterCollider.height;

        var topSideMonsterOnViewport = Camera.main.WorldToViewportPoint(topSideMonster);
        var botSideMonsterOnViewport = Camera.main.WorldToViewportPoint(botSideMonster);
        var leftSideMonsterOnViewport = Camera.main.WorldToViewportPoint(leftSideMonster);
        var rightSideMonsterOnViewport = Camera.main.WorldToViewportPoint(rightSideMonster);

        var positionMonsterOnViewport = Camera.main.WorldToViewportPoint(transform.position);

        if (rightSideMonsterOnViewport.x + indent < 0 || leftSideMonsterOnViewport.x - indent > 1 ||
            topSideMonsterOnViewport.y + indent < 0 || botSideMonsterOnViewport.y - indent > 1 ||
            positionMonsterOnViewport.z < 0) IsMonsterVisible = false;
        else
        {
            var indentFromSide = 0.95f;
            var sidesMonster = new Vector3[4];
            sidesMonster[0] = _monsterCollider.transform.up * indentFromSide * _monsterCollider.height + transform.position;
            sidesMonster[1] = _monsterCollider.transform.up * (1 - indentFromSide) + transform.position;
            sidesMonster[2] = - vectorViewportParallelAxis * indentFromSide * _monsterCollider.radius + transform.position +
                _monsterCollider.transform.up * indentFromSide * _monsterCollider.height / 2;
            sidesMonster[3] = vectorViewportParallelAxis * indentFromSide * _monsterCollider.radius + transform.position + 
                _monsterCollider.transform.up * indentFromSide * _monsterCollider.height / 2;
            for (var i = 0; i < sidesMonster.Length; i++)
            {
                Debug.DrawRay(cameraPosition, sidesMonster[i] - cameraPosition, Color.red);
                RaycastHit hit;
                Physics.Raycast(cameraPosition, sidesMonster[i] - cameraPosition, out hit);
                if (hit.collider.gameObject == gameObject)
                {
                    IsMonsterVisible = true;
                    return;
                }
                else IsMonsterVisible = false;
            }
        }
    }

    private void NeckTwist()
    {
        transform.LookAt(Camera.main.transform.position);
        FindObjectOfType<Level>().LossLevel();
        GetComponent<MonsterSound>().PlayNeckTwist();
    }
}