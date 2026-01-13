using ProjectGauss.Core;
using UnityEngine;

namespace ProjectGauss.Player
{
    public class RifleStrategy : MonoBehaviour, IWeaponStrategy
    {
        [SerializeField] Projectile bulletPrefab;
        [SerializeField] float damage = 10f;
        [SerializeField] float bulletSpeed = 20f;
        [SerializeField] float fireRate = .2f;

        float lastFireTime;
        public void Fire(Vector3 origin, Vector3 dir, Vector3 targetPosition, GameSystems gameSystems)
        {
            if (Time.time < lastFireTime + fireRate) return;

            // 튜플(bool isHit, float damage)
            var result = gameSystems.HeightSystem.CalculateAttackResult(origin, targetPosition);
            Vector3 finalDir = dir;
            float finalDamage = damage * result.damageMultiplier;

            if (!result.isHit)
            {
                float errorfireAngle = Random.Range(10f, 20f);
                if (Random.value > .5f) errorfireAngle = -errorfireAngle;

                finalDir = Quaternion.Euler(0, errorfireAngle, 0) * dir;

                finalDamage = 0;
            }
            else if (result.damageMultiplier > 1f)
            {

            }

            Projectile bullet = Instantiate(bulletPrefab, origin, Quaternion.LookRotation(finalDir));
            bullet.Initialize(dir, bulletSpeed, finalDamage);

            lastFireTime = Time.time;
        }
    }
}