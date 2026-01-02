using UnityEngine;

namespace ProjectGauss.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        void Awake()
        {
            GameSystems gameSystems = new GameSystems();
            IInitializer[] managers = GetComponentsInChildren<IInitializer>();

            foreach (var manager in managers)
            {
                manager.Iniitialize(gameSystems);
            }
        }
    }
}
