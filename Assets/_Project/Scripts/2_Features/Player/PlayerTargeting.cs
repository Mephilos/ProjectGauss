using UnityEngine;
using UnityEngine.InputSystem;
using ProjectGauss.Input;

namespace ProjectGauss.Player
{
    public class PlayerTargeting : MonoBehaviour
    {
        [SerializeField] float scanRange = 10f;
        [SerializeField] LayerMask enemyLayer;

        Transform manualTarget;
        Transform nearestTarget;
        GameInput inputs;
        Camera mainCam;

        public Transform CurrentTarget
        {
            get
            {
                if (IsTargetValid(manualTarget)) return manualTarget;
                return nearestTarget;
            }
        }

        private void Awake()
        {
            mainCam = Camera.main;
        }

        public void Initialize(GameInput inputs)
        {
            this.inputs = inputs;
            this.inputs.Player.Fire.performed += OnFirePerformed;
        }

        private void OnDestroy()
        {
            if (inputs != null)
            {
                inputs.Player.Fire.performed -= OnFirePerformed;
            }
        }

        private void Update()
        {
            UpdateNearestTarget();

            if (manualTarget != null && !IsTargetValid(manualTarget))
            {
                manualTarget = null;
            }
        }

        private void OnFirePerformed(InputAction.CallbackContext context)
        {
            Vector2 mousePosition = inputs.Player.Look.ReadValue<Vector2>();

            Ray ray = mainCam.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, enemyLayer))
            {
                manualTarget = hit.transform;
            }

            else
            {
                manualTarget = null;
            }
        }

        private void UpdateNearestTarget()
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, scanRange, enemyLayer);

            Transform closest = null;
            float minDst = float.MaxValue;

            foreach (var enemy in enemies)
            {
                if (!enemy.gameObject.activeInHierarchy) continue;

                float dst = Vector3.Distance(transform.position, enemy.transform.position);
                if (dst < minDst)
                {
                    minDst = dst;
                    closest = enemy.transform;
                }
            }

            nearestTarget = closest;
        }

        private bool IsTargetValid(Transform target)
        {
            if (target == null) return false;
            if (!target.gameObject.activeInHierarchy) return false;

            float dist = Vector3.Distance(transform.position, target.position);
            return dist <= scanRange + 0.5f;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, scanRange);

            if (manualTarget != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, manualTarget.position);
            }
        }
    }
}