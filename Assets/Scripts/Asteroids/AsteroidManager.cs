using UnityEngine;

namespace Asteroids
{
    public class AsteroidManager : MonoBehaviour
    {
        [SerializeField] private Asteroid asteroid;
        [SerializeField] int GridSpacing;
        [SerializeField] float minRange, maxRange;
        private GameObject _player;
        void Start()
        {
            _player = GameObject.FindWithTag("Player");
            PlaceInstances();
        }

        void PlaceInstances()
        {
            for (int x = 0; x < GridSpacing; x++)
            {
                InstanceAsteroids(Random.Range(minRange, maxRange), Random.Range(minRange, maxRange), Random.Range(minRange, maxRange));
            }
        }

        void InstanceAsteroids(float x, float y, float z)
        {
            Instantiate(asteroid, _player.transform.position + new Vector3(x, y, z), Quaternion.identity, transform);
        }
    }
}
