using UnityEngine;
using Unity.Cinemachine;
using ProjectGauss.Core;
using ProjectGauss.Input;


namespace ProjectGauss.Player
{
    public class PlayerController : MonoBehaviour
    {
        public GameInput Input { get; private set; }
        public PlayerTargeting Targeting { get; private set; }
        public PlayerStateMachine StateMachine { get; private set; }
        public PlayerMoveState MoveState { get; private set; }
        public PlayerAttackState AttackState { get; private set; }
        public PlayerIdleState IdleState { get; private set; }
        [SerializeField] float moveSpeed = 5f;
        Rigidbody rb;
        GameSystems systems;
        Transform cameraRigPoint;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            Targeting = GetComponent<PlayerTargeting>();
            Input = new GameInput();

            StateMachine = new PlayerStateMachine();
            MoveState = new PlayerMoveState(this);
            AttackState = new PlayerAttackState(this);
            IdleState = new PlayerIdleState(this);
        }

        void OnEnable()
        {
            Input.Enable();
        }

        void OnDisable()
        {
            Input.Disable();
        }

        void Start()
        {
            StateMachine.Initialize(IdleState);
            ConnectToCinemachine();
        }

        void ConnectToCinemachine()
        {
            CinemachineCamera cinemachineCamera = FindFirstObjectByType<CinemachineCamera>();

            cinemachineCamera.Follow = this.transform;
        }

        public void Initialize(GameSystems systems)
        {
            this.systems = systems;
            Targeting.Initialize(Input);
        }

        void Update()
        {
            StateMachine.CurrentState.Execute();
        }

        void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsExecute();
        }

        public void Move(Vector3 dir)
        {
            Vector3 nextPosition = rb.position + dir * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(nextPosition);
        }

        public void LookAt(Vector3 targetPosition)
        {
            Vector3 lookDir = targetPosition - transform.position;
            lookDir.y = 0;

            if (lookDir != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(lookDir);
            }
        }

        public void LookAtMouse()
        {
            Vector2 mouseScreenPosition = Input.Player.Look.ReadValue<Vector2>();
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            if (groundPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                LookAt(hitPoint);
            }
        }
    }
}