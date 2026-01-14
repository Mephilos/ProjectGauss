using ProjectGauss.Systems;

namespace ProjectGauss.Core
{
    public class GameSystems
    {
        public HeightSystem HeightSystem { get; private set; }
        public SightSystem SightSystem { get; private set; }
        public GameSystems()
        {
            this.HeightSystem = new HeightSystem();
            this.SightSystem = new SightSystem();
        }
    }
}