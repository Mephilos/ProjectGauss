using UnityEngine;
using ProjectGauss.Core;

namespace ProjectGauss.Player
{
    public class PlayerSpawner : MonoBehaviour, IInitializable
    {
        [SerializeField] PlayerController playerPrefab;
        [SerializeField] Transform spawnPoint;
        PlayerController currentPlayer;

        public void Iniitialize(GameSystems gameSystems)
        {
            SpawnPlayer(gameSystems);
        }

        void SpawnPlayer(GameSystems gameSystems)
        {
            Vector3 spawnPosition = spawnPoint.position;

            currentPlayer = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            currentPlayer.Initialize(gameSystems);
        }
    }
}