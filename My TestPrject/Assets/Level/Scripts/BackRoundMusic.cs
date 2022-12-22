using System.Collections;
using UnityEngine;

public class BackRoundMusic : MonoBehaviour
{
    private bool _isPause;
    public bool IsPause 
    { 
        get 
        { 
            return _isPause;
        } 
        set 
        {
            if (value == _isPause) return;
            if (value) _backGroundPlayingAS.Pause();
            else _backGroundPlayingAS.Play();
            _isPause = value;
        } 
    }

    [SerializeField] private AudioSource[] backGroundMusic;
    private GameObject _backGroundPlaying;
    private AudioSource _backGroundPlayingAS;
    private int _numberBackgroundPlaying;

    private void Start()
    {
        _numberBackgroundPlaying = Random.Range(0, backGroundMusic.Length);
        StartCoroutine(PlayBackGround(_numberBackgroundPlaying));
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
        StartCoroutine(PlayBackGround(_numberBackgroundPlaying));
    }

    private IEnumerator PlayBackGround(int number)
    {
        _backGroundPlaying = Instantiate(backGroundMusic[number].gameObject, transform);
        var timerMusic = 0f;
        _backGroundPlayingAS = _backGroundPlaying.GetComponent<AudioSource>();
        while (timerMusic < _backGroundPlayingAS.clip.length)
        {
            if (!IsPause) timerMusic += Time.unscaledDeltaTime;
            yield return null;
        }
        Destroy(_backGroundPlaying);
    }
}
