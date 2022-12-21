using UnityEngine;

public class MonsterSound : MonoBehaviour
{
    [SerializeField] private AudioSource theMonsterSeesThePlayerSound;
    [SerializeField] private AudioSource neckTwistSound;
    private Monster _monster;
    private BackRoundMusic _backRoundMusic;

    private void Start()
    {
        _monster = GetComponent<Monster>();
    }

    private void OnEnable()
    {
        _backRoundMusic = FindObjectOfType<BackRoundMusic>();
        SetPlayingMonsterSeesThePlayerSound(false);
    }

    private void Update()
    {
        if (_monster.IsGameOver) return;
        SearchPlayer();
    }

    public void PlayNeckTwist()
    {
        theMonsterSeesThePlayerSound.gameObject.SetActive(false);
        var neckTwist = Instantiate(neckTwistSound.gameObject, transform);
        neckTwist.GetComponent<AudioSource>().Play();
        Destroy(neckTwist, neckTwist.GetComponent<AudioSource>().clip.length);
    }

    private void SearchPlayer()
    { 
        RaycastHit hit;
        if (Physics.Raycast(transform.position,
            Camera.main.transform.position - Camera.main.transform.up * 0.9f - transform.position, out hit))
        {
            if (hit.collider.CompareTag("Player")) SetPlayingMonsterSeesThePlayerSound(true);
            else SetPlayingMonsterSeesThePlayerSound(false);
        }
        else SetPlayingMonsterSeesThePlayerSound(false);
    }

    private void SetPlayingMonsterSeesThePlayerSound(bool value)
    {
        theMonsterSeesThePlayerSound.gameObject.SetActive(value);
        _backRoundMusic.IsPause = value;
    }
}
