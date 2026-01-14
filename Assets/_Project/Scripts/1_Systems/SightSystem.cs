using UnityEngine;

namespace ProjectGauss.Systems
{
    public class SightSystem
    {
        public bool SightCheck(Vector3 origin, Vector3 target, LayerMask obstacleLayer)
        {
            Vector3 dir = (target - origin).normalized;
            float dist = Vector3.Distance(origin, target);

            if (Physics.Raycast(origin, dir, out RaycastHit hit, dist, obstacleLayer))
            {
                return false;
            }
            return true;
        }

        public bool FovCheck(Transform eye, Transform target, float viewAngle, float viewDistance, LayerMask obstacleLayer)
        {
            Vector3 dirToTarget = (target.position - eye.position).normalized;
            float dist = Vector3.Distance(eye.position, target.position);

            if (dist > viewDistance) return false;

            if (Vector3.Angle(eye.forward, dirToTarget) > viewAngle * .5f) return false;

            return SightCheck(eye.position, target.position, obstacleLayer);
        }
    }
}