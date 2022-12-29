using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Eyes : MonoBehaviour
{
    [SerializeField] private Button isOpenButton;
    [SerializeField] private Button isCloseButton;
    [SerializeField] private GameManager gameManager;
    public float length = 1;
    public float delay = 0.5f;
    private Animator _animator;
    private Coroutine _coroutineBlink;

    private bool isForce = false;
    public bool IsForce {
        get { return isForce; }
        set {
            if (!CanControl) return;
            if (value) See(value);
            isForce = value;
            _animator.SetBool("IsForceOpen", isForce);
            isOpenButton.gameObject.SetActive(value);
            isCloseButton.gameObject.SetActive(!value);
            if (value && _coroutineBlink != null) StopCoroutine(_coroutineBlink);
            else _coroutineBlink = StartCoroutine(Blink());
        } 
    }

    public bool CanControl { get; set; } = true;
    public bool IsOpen { get; set; } = true;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        isOpenButton.transform.parent.gameObject.SetActive(FindObjectOfType<GameManager>().IsMobile);
    }

    private void OnEnable()
    {
        IsForce = false;
    }

    private float time = 0;
    private void Update()
    {
        if (!gameManager.IsMobile)
        {
            if (GameInput.Key.GetKeyDown("OpenEyes"))
                IsForce = true;
            if (GameInput.Key.GetKeyUp("OpenEyes"))
                IsForce = false;
        }

        if (!IsOpen) return;
        time += Time.deltaTime;
        if (time >= delay)
        {
            time = 0;
            _coroutineBlink = StartCoroutine(Blink());
        }
    }

    public void Open()
    {
        if (_coroutineBlink != null) StopCoroutine(_coroutineBlink);
        See(true);
        _animator.SetBool("IsOpen", IsOpen = true);
    }

    private IEnumerator Blink()
    {
        _animator.SetBool("IsOpen", IsOpen = false);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0).Length);
        See(false);
        yield return new WaitForSeconds(length);
        See(true);
        _animator.SetBool("IsOpen", IsOpen = true);
    }

    private void See(bool value)
    {
        if (isForce) return;
        Monster[] monsters = FindObjectsOfType<Monster>();
        foreach (Monster monster in monsters) monster.SetMove = !value;
    }
}
