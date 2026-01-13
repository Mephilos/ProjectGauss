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
        public void Fire(Vector3 origin, Vector3 dir)
        {
            if (Time.time < lastFireTime + fireRate) return;

            Projectile bullet = Instantiate(bulletPrefab, origin, Quaternion.LookRotation(dir));
            bullet.Initialize(dir, bulletSpeed, damage);

            lastFireTime = Time.time;
        }
    }
}