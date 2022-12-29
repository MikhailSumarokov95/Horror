using System.Collections;
using UnityEngine;

namespace ToxicFamilyGames.FirstPersonController
{
    [RequireComponent(typeof(CharacterController))]
    public class Character : MonoBehaviour
    {
        public float MoveCharacterHorizontal { get { return gameManager.IsMobile ? joystick.Horizontal : Input.GetAxis("Horizontal"); } }

        public float MoveCharacterVertical { get { return gameManager.IsMobile ? joystick.Vertical : Input.GetAxis("Vertical"); } }

        public Vector2 MoveHead { get { return gameManager.IsMobile ? touchSystem.Delta : new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); } }

        public bool IsBrokenNeck { get; set; }

        public bool isLocked;
        [SerializeField]
        private float movementSpeed = 10;
        [SerializeField]
        private float gravity = 9.81f;
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
        private float _moveX = 0;
        private CharacterController characterController;


        private void Start()
        {
            animator = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
            if (!gameManager.IsMobile)
            {
                Cursor.lockState = CursorLockMode.Locked;
                joystick.gameObject.SetActive(false);
            }
        }

        public Vector2 Mouse
        {
            get
            {
                if (isLocked) return Vector2.zero;
                return MoveHead;
            }
        }

        public Vector3 Move
        {
            get
            {
                if (isLocked) return Vector2.zero;
                Vector3 result = new Vector3(MoveCharacterHorizontal, 0, MoveCharacterVertical);
                return result;
            }
        }

        private float moveMagnitude = 0;
        private Vector3 characterVelocity = Vector3.zero;

        private void Update()
        {
            if (IsBrokenNeck) StartCoroutine(NeckTwist());
            if (gameManager.IsPause) return;
            if (isLocked) return;
            CameraUpdate();
            if (!characterController.isGrounded || moveMagnitude != 0)
            {
                characterVelocity.y -= gravity * Time.deltaTime;
                characterController.Move((transform.rotation * Move * movementSpeed + characterVelocity) * Time.deltaTime);
                if (characterController.isGrounded) characterVelocity.y = 0;
            }
        }

        
        private void CameraUpdate()
        {
            Vector2 mouse = Mouse * generalSetting.TurningSpeed * Time.deltaTime;

            _moveX += mouse.y;
            _moveX = Mathf.Clamp(_moveX, maxDownHead * 2, maxUpHead * 2);
            head.transform.localEulerAngles = new Vector3(-_moveX / 2, head.transform.localEulerAngles.y, 0);
            transform.Rotate(Vector3.up, mouse.x);

            float moveMagnitude = Move.magnitude;
            if ((this.moveMagnitude == 0 && moveMagnitude != 0) ||
                (this.moveMagnitude != 0 && moveMagnitude == 0))
            {
                animator.SetBool("isWalking", this.moveMagnitude == 0);
            }
            this.moveMagnitude = moveMagnitude;
        }

        public IEnumerator NeckTwist()
        {
            IsBrokenNeck = false;
            var monsterPosition = GameObject.FindGameObjectWithTag("Monster").transform;
            Quaternion rotationForLookAnMonster;
            isLocked = true;
            while (true)
            {
                rotationForLookAnMonster = Quaternion.LookRotation(- monsterPosition.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationForLookAnMonster, 0.1f);
                if (Mathf.Abs(transform.rotation.eulerAngles.y - rotationForLookAnMonster.eulerAngles.y) < 1f) break;
                yield return null;
            }
            isLocked = false;
            yield break;
        }
    }
}