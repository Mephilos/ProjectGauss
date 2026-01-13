using UnityEngine;

namespace ProjectGauss.Player
{
    public interface IWeaponStratagy
    {
        void Fire(Vector3 origin, Vector3 dir);
    }
}