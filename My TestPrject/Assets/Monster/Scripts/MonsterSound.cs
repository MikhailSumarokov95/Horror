using UnityEngine;

public class MonsterSound : MonoBehaviour
{
    [SerializeField] private AudioSource theMonsterSeesThePlayerSound;
    [SerializeField] private AudioSource neckTwistSound;
    private Monster monster;

    private void Start()
    {
        theMonsterSeesThePlayerSound.gameObject.SetActive(false);
        monster = GetComponent<Monster>();
    }

    private void Update()
    {
        if (monster.IsGameOver) return;
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
            if (hit.collider.CompareTag("Player")) theMonsterSeesThePlayerSound.gameObject.SetActive(true);
            else theMonsterSeesThePlayerSound.gameObject.SetActive(false);
        }
        else theMonsterSeesThePlayerSound.gameObject.SetActive(false);
    }
}
