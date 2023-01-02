using System.Collections;
using UnityEngine;

namespace ToxicFamilyGames.FirstPersonController
{
    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour
    {
        public float MoveHorizontal { get { return gameManager.IsMobile ? joystick.Horizontal : Input.GetAxis("Horizontal"); } }

        public float MoveVertical { get { return gameManager.IsMobile ? joystick.Vertical : Input.GetAxis("Vertical"); } }

        public Vector3 Move { get { return new Vector3(MoveHorizontal, 0, MoveVertical); } }

        public bool IsBrokenNeck { get; set; }

        public bool isLocked;
        [SerializeField]
        private float movementSpeed = 10;
        [SerializeField]
        private float maxUpHead = 30; 
        [SerializeField]
        private float maxDownHead = - 15;
        [SerializeField]
        private Joystick joystick;
        [SerializeField]
        private TouchSystem touchSystem;
        [SerializeField]
        private GameManager gameManager;
        [SerializeField]
        private GeneralSetting generalSetting;
        [SerializeField]
        private GameObject head;
        private Animator animator;
        private CharacterController characterController;
        private float moveMagnitude = 0;
        private float moveHeadX;

        private void Start()
        {
            animator = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
            if (!gameManager.IsMobile)
            {
                Cursor.lockState = CursorLockMode.Locked;
                joystick.gameObject.SetActive(false);
            }
            else touchSystem.OnDragForMove += MoveHead;
        }

        private void OnEnable()
        {
            isLocked = false;
        }

        private void Update()
        {
            if (IsBrokenNeck) StartCoroutine(NeckTwist());
            if (gameManager.IsPause) return;
            if (isLocked) return;
            CameraUpdate();
            characterController.SimpleMove(transform.rotation * Move * movementSpeed);
            if (!gameManager.IsMobile) 
                MoveHead(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * Time.deltaTime);
        }

        private void CameraUpdate()
        {
            float moveMagnitude = Move.magnitude;
            if ((this.moveMagnitude == 0 && moveMagnitude != 0) ||
                (this.moveMagnitude != 0 && moveMagnitude == 0))
            {
                animator.SetBool("isWalking", this.moveMagnitude == 0);
            }
            this.moveMagnitude = moveMagnitude;
        }

        public void MoveHead(Vector2 mouse)
        {
            var directionMove = mouse * generalSetting.TurningSpeed;

            moveHeadX += directionMove.y;
            moveHeadX = Mathf.Clamp(moveHeadX, maxDownHead, maxUpHead);
            head.transform.localEulerAngles = new Vector3(-moveHeadX, head.transform.localEulerAngles.y, 0);

            transform.Rotate(Vector3.up, directionMove.x);
        }

        public IEnumerator NeckTwist()
        {
            IsBrokenNeck = false;
            var monsterTr = GameObject.FindGameObjectWithTag("Monster").transform;
            Quaternion rotationForLookAnMonster;
            isLocked = true;
            while (true)
            {
                rotationForLookAnMonster = Quaternion.LookRotation(- monsterTr.forward + monsterTr.up * 0.3f);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationForLookAnMonster, 0.1f);
                if (Mathf.Abs(transform.rotation.eulerAngles.y - rotationForLookAnMonster.eulerAngles.y) < 1f) break;
                yield return null;
            }
            isLocked = false;
            yield break;
        }
    }
}