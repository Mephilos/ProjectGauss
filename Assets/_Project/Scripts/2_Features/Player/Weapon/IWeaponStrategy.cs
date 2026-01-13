using UnityEngine;

namespace ProjectGauss.Player
{
    public interface IWeaponStrategy
    {
        void Fire(Vector3 origin, Vector3 dir);
    }
}