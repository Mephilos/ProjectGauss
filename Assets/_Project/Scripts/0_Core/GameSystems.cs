using ProjectGauss.Systems;

namespace ProjectGauss.Core
{
    public class GameSystems
    {
        public HeightSystem HeightSystem { get; private set; }
        public GameSystems()
        {
            HeightSystem = new HeightSystem();
        }
    }
}