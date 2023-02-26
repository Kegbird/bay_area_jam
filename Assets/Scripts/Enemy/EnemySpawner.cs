using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
        [SerializeField]
        private GameObject _spawns;

        private void Start()
        {
            _game_manager = GetComponent<GameManager>();

            GameObject player = GameObject.FindGameObjectWithTag("Player");

            StartCoroutine(EnemySpawnerCoroutine());
        }

        private IEnumerator EnemySpawnerCoroutine()
        {
            yield return new WaitForSeconds(3f);
            for (int i = 0; i < _initial_number_of_enemies; i++)
            {
                yield return new WaitForSeconds(0.5f);
                GenerateEnemy();
            }

            while (!_game_manager.IsGameOver())
            {
                GenerateEnemy();
                yield return new WaitForSeconds(m_delay_between_enemy_spawn);
            }
            yield return null;
        }

        private void GenerateEnemy()
        {
            for (int j = 0; j < 20; j++)
            {
                int i = Random.Range(0, _spawns.transform.childCount);
                if (!_spawns.transform.GetChild(i).GetComponent<SpriteRenderer>().isVisible)
                {
                    GameObject enemy;
                    float enemy_seed = Random.Range(0, 100);
                    if (enemy_seed <= _kamikaze_max_threshold)
                        enemy = GameObject.Instantiate(_kamikaze_enemy, _spawns.transform.GetChild(i).transform.position, Quaternion.identity);
                    else if (enemy_seed <= _meele_max_threshold)
                        enemy = GameObject.Instantiate(_meele_enemy, _spawns.transform.GetChild(i).transform.position, Quaternion.identity);
                    else
                        enemy = GameObject.Instantiate(_tank_enemy, _spawns.transform.GetChild(i).transform.position, Quaternion.identity);
                    enemy.SetActive(true);
                    return;
                }
            }
        }
    }
}
