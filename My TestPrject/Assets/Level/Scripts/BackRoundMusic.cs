using UnityEngine;

public class BackRoundMusic : MonoBehaviour
{
    [SerializeField] private AudioSource[] backGroundMusic;
    private GameObject _backGroundPlaying;
    private int _numberBackgroundPlaying = 0;

    private void Start()
    {
        _numberBackgroundPlaying = Random.Range(0, backGroundMusic.Length);
        _backGroundPlaying = Instantiate(backGroundMusic[_numberBackgroundPlaying].gameObject, transform);
        Destroy(_backGroundPlaying, _backGroundPlaying.GetComponent<AudioSource>().clip.length);
    }

    private void Update()
    {
        if (_backGroundPlaying == null) NextBackGround();
    }

    [ContextMenu("NextBackGround")]
    public void NextBackGround()
    {
        _numberBackgroundPlaying++;
        _numberBackgroundPlaying = (int)Mathf.Repeat(_numberBackgroundPlaying, backGroundMusic.Length);
        _backGroundPlaying = Instantiate(backGroundMusic[_numberBackgroundPlaying].gameObject, transform);
        Destroy(_backGroundPlaying, _backGroundPlaying.GetComponent<AudioSource>().clip.length);
    }
}
