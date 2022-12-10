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
    private Animator animator;


    private bool isForce = false;
    public bool IsForce {
        get { return isForce; }
        set {
            if (value) See(!value);
            isForce = value;
            animator.SetBool("IsForceOpen", isForce);
            isOpenButton.gameObject.SetActive(value);
            isCloseButton.gameObject.SetActive(!value);
        } 
    }
    public bool CanControl { get; set; } = true;
    public bool IsOpen { get; private set; } = true;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        isOpenButton.transform.parent.gameObject.SetActive(FindObjectOfType<GameManager>().IsMobile);
    }

    private float time = 0;
    private void Update()
    {
        if (!gameManager.IsMobile && CanControl)
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
            StartCoroutine(Blinck());
        }
    }

    private IEnumerator Blinck()
    {
        animator.SetBool("IsOpen", IsOpen = false);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        See(true);
        yield return new WaitForSeconds(length);
        See(false);
        animator.SetBool("IsOpen", IsOpen = true);
    }

    private void See(bool value)
    {
        if (isForce) return;
        Monster[] monsters = FindObjectsOfType<Monster>();
        foreach (Monster monster in monsters) monster.SetMove = value;
    }
}
