using UnityEngine;

namespace SpaceGame.Player
{
    [CreateAssetMenu(fileName = "PlayerSpaceshipData", menuName = "Scriptable Objects/Game/Create Player Spaceship Data", order = 1)]
    public class PlayerSpaceshipData : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private float thrustAccelerationSpeed = default;
        [SerializeField] private float thrustMaxSpeed = default;
        [SerializeField] private float steeringSpeed = default;

        [Header("Shooting General")]
        [SerializeField] private float secondsBtwShots = default;

        [Header("Levels")]
        [SerializeField] private LevelData[] levels = default;

        int _currentLevel;
        public int LevelIndex
        {
            get => _currentLevel;
            set { _currentLevel = Mathf.Clamp(value, 0, levels.Length - 1); }
        }
        public LevelData LevelData => levels[LevelIndex];

        public float ThrustAccelerationSpeed => thrustAccelerationSpeed;
        public float ThrustMaxSpeed => thrustMaxSpeed;
        public float SteeringSpeed => steeringSpeed;

        public float SecondsBtwShots => secondsBtwShots;

        public GameObject ProjectilePrefab => LevelData.ProjPrefab;
        public int ProjectileCount => LevelData.ProjCount;
        public float ProjectileSpreading => LevelData.ProjSpreading;

        private void OnValidate()
        {
            thrustAccelerationSpeed = Mathf.Max(0f, thrustAccelerationSpeed);
            thrustMaxSpeed = Mathf.Max(0f, thrustMaxSpeed);
            steeringSpeed = Mathf.Max(0f, steeringSpeed);

            secondsBtwShots = Mathf.Max(0f, secondsBtwShots);
        }
    }


    [System.Serializable]
    public class LevelData
    {
        [Header("Shooting")]
        [SerializeField] private GameObject projPrefab = default;
        [SerializeField] private int projCount = default;
        [SerializeField] private float projSpreading = default;

        public GameObject ProjPrefab => projPrefab;
        public int ProjCount => projCount;
        public float ProjSpreading => projSpreading;
    }
}