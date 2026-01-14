using UnityEngine;
using UnityEngine.InputSystem;
using ProjectGauss.Input;
using ProjectGauss.Core;
using ProjectGauss.Systems;
using System;

namespace ProjectGauss.Player
{
    public class PlayerTargeting : MonoBehaviour
    {
        [SerializeField] float scanRange = 10f;
        [SerializeField] LayerMask enemyLayer;
        [SerializeField] LayerMask obstacleLayer;
        [SerializeField] Transform eyeTransform;

        Transform manualTarget;
        Transform nearestTarget;
        GameInput inputs;
        Camera mainCam;
        SightSystem sightSystem;

        public event Action OnTargetingFailed;
        public Transform CurrentTarget
        {
            get
            {
                if (IsTargetValid(manualTarget)) return manualTarget;
                return nearestTarget;
            }
        }

        void Awake()
        {
            mainCam = Camera.main;
        }

        public void Initialize(GameInput inputs, GameSystems systems)
        {
            this.sightSystem = systems.SightSystem;
            this.inputs = inputs;
            this.inputs.Player.Fire.performed += OnFirePerformed;
        }

        void OnDestroy()
        {
            inputs.Player.Fire.performed -= OnFirePerformed;
        }

        void Update()
        {
            UpdateNearestTarget();

            if (manualTarget != null && !IsTargetValid(manualTarget))
            {
                manualTarget = null;
            }
        }

        void OnFirePerformed(InputAction.CallbackContext context)
        {
            Vector2 mousePosition = inputs.Player.Look.ReadValue<Vector2>();

            Ray ray = mainCam.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, enemyLayer))
            {
                Transform targetCondidate = hit.transform;

                if (IsVisible(targetCondidate))
                {
                    manualTarget = targetCondidate;
                    Debug.Log($"타겟 지정 성공{manualTarget}");
                }
                else
                {
                    Debug.LogWarning("타켓팅 불가능 (레이판정)");
                    OnTargetingFailed?.Invoke();
                    manualTarget = null;
                }
            }

            else
            {
                manualTarget = null;
            }
        }

        void UpdateNearestTarget()
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, scanRange, enemyLayer);

            Transform closest = null;
            float minDst = float.MaxValue;

            foreach (var enemy in enemies)
            {
                if (!enemy.gameObject.activeInHierarchy) continue;
                if (!IsVisible(enemy.transform)) continue;

                float dst = Vector3.Distance(transform.position, enemy.transform.position);
                if (dst < minDst)
                {
                    minDst = dst;
                    closest = enemy.transform;
                }
            }

            nearestTarget = closest;
        }

        bool IsTargetValid(Transform target)
        {
            if (target == null) return false;
            if (!target.gameObject.activeInHierarchy) return false;

            float dist = Vector3.Distance(transform.position, target.position);
            return dist <= scanRange + 0.5f && IsVisible(target);
        }

        bool IsVisible(Transform target)
        {
            Vector3 originPosition = eyeTransform.position;
            return sightSystem.SightCheck(originPosition, target.position, obstacleLayer);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, scanRange);

            Gizmos.color = Color.azure;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2f);
            Gizmos.DrawSphere(transform.position + transform.forward * 2f, 0.1f);

            if (manualTarget != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, manualTarget.position);
            }
        }
    }
}