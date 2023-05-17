using UnityEngine;

namespace Asteroids
{
    public class AsteroidManager : MonoBehaviour
    {
        [SerializeField] private Asteroid[] asteroid;
        [SerializeField] float minRange, maxRange;
        [SerializeField] int gridSpacing;
        private GameObject _player;
        void Start()
        {
            _player = GameObject.FindWithTag("Player");
            PlaceInstances();
        }

        void PlaceInstances()
        {
            for (int x = 0; x < gridSpacing; x++)
            {
                InstanceAsteroids(Random.Range(minRange, maxRange), Random.Range(minRange, maxRange), Random.Range(minRange, maxRange), Random.Range(0,2));
            }
        }

        void InstanceAsteroids(float x, float y, float z, int i)
        {
            Instantiate(asteroid[i], _player.transform.position + new Vector3(x, y, z), Quaternion.identity, transform);
        }
    }
}
