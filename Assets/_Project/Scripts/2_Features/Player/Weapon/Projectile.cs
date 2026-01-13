
using UnityEngine;

namespace ProjectGauss.Player
{
    public class Projectile : MonoBehaviour
    {
        Rigidbody rb;
        float damage;
        float speed;
        float lifeTime;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.useGravity = false;
        }

        public void Initialize(Vector3 dir, float speed, float damage, float lifeTime = 3f)
        {
            this.damage = damage;
            this.speed = speed;
            this.lifeTime = lifeTime;

            rb.linearVelocity = dir * this.speed;
            Destroy(this.gameObject, this.lifeTime);
        }
        void OnCollisionEnter(Collision collision)
        {
            ContactPoint contact = collision.contacts[0];

            Debug.Log($"총알 타격 대상: {collision.gameObject.name}, " +
                      $"위치: {contact.point}, " +
                      $"데미지: {damage}");

            Destroy(gameObject);
        }
    }
}