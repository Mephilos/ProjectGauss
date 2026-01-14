using UnityEngine;

namespace ProjectGauss.Systems
{
    public class HeightSystem
    {
        const float FLOOR_HEIGHT_THRESHOLD = 3f;
        const float LOW_TO_HIGH_MISS_CHANCE = .3f;
        const float HIGH_TO_LOW_DAMAGE_BONUS = 1.2f;

        public int GetFloorLevel(Vector3 position)
        {
            return position.y >= FLOOR_HEIGHT_THRESHOLD ? 1 : 0;
        }

        public (bool isHit, float damageMultiplier) CalculateAttackResult(Vector3 attackerPosition, Vector3 targetPosition)
        {
            int attackerFloor = GetFloorLevel(attackerPosition);
            int targetFloor = GetFloorLevel(targetPosition);

            if (attackerFloor == targetFloor)
            {
                return (true, 1f);
            }

            if (attackerFloor < targetFloor)
            {
                if (Random.value < LOW_TO_HIGH_MISS_CHANCE)
                {
                    return (false, 0f);
                }
                return (true, 1f);
            }

            return (true, HIGH_TO_LOW_DAMAGE_BONUS);
        }
    }
}