using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private int _initial_number_of_enemies;
        [SerializeField]
        private float _spawn_distance;
        [SerializeField]
        private GameManager _game_manager;
        [SerializeField]
        private Transform _player_transform;
        [SerializeField]
        private GameObject _kamikaze_enemy;
        [SerializeField]
        private GameObject _tank_enemy;
        [SerializeField]
        private GameObject _meele_enemy;
        [SerializeField]
        private float m_delay_between_enemy_spawn;
        [SerializeField]
        private float _meele_max_threshold;
        [SerializeField]
        private float _kamikaze_max_threshold;


        private void Awake()
        {

        }

        private void Start()
        {
            _game_manager = GetComponent<GameManager>();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            _player_transform = player.transform;

            for (int i = 0; i < _initial_number_of_enemies; i++)
                GenerateEnemy();

            StartCoroutine(EnemySpawnerCoroutine());
        }

        private IEnumerator EnemySpawnerCoroutine()
        {
            while (!_game_manager.IsGameOver())
            {
                GenerateEnemy();
                yield return new WaitForSeconds(m_delay_between_enemy_spawn);
            }
            yield return null;
        }

        private void GenerateEnemy()
        {
            Vector3 enemy_position = Vector3.zero;
            enemy_position.z = _player_transform.position.z;

            float angle = Random.Range(0, 360f) * Mathf.Deg2Rad;

            enemy_position.x = _player_transform.position.x + Mathf.Cos(angle) * _spawn_distance;
            enemy_position.y = _player_transform.position.y + Mathf.Sin(angle) * _spawn_distance;

            GameObject enemy;
            float enemy_seed = Random.Range(0, 100);
            if (enemy_seed <= _kamikaze_max_threshold)
                 enemy = GameObject.Instantiate(_kamikaze_enemy, enemy_position, Quaternion.identity);
            else if (enemy_seed <= _meele_max_threshold)
                enemy = GameObject.Instantiate(_meele_enemy, enemy_position, Quaternion.identity);
            else
                enemy = GameObject.Instantiate(_tank_enemy, enemy_position, Quaternion.identity);

            enemy.SetActive(true);
        }
    }
}
