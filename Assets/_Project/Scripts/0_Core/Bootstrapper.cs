using UnityEngine;

namespace ProjectGauss.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        void Awake()
        {
            GameSystems gameSystems = new GameSystems();
            IInitializable[] managers = GetComponentsInChildren<IInitializable>();

            foreach (var manager in managers)
            {
                manager.Iniitialize(gameSystems);
            }
        }
    }
}
