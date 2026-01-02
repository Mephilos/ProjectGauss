using UnityEngine;
using UnityEngine.InputSystem;
using ProjectGauss.Core;
using ProjectGauss.Input;

namespace ProjectGauss.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        GameSystems systems;
        Rigidbody rb;
        GameInput inputs;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            inputs = new GameInput();
        }

        void OnEnable()
        {
            inputs.Enable();
        }

        void OnDisable()
        {
            inputs.Disable();
        }

        public void Initialize(GameSystems systems)
        {
            this.systems = systems;
        }

        void FixedUpdate()
        {

        }
    }
}