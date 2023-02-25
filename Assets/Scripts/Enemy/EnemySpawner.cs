using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float m_delay_between_enemy_spawn;

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
        while(!_game_manager.IsGameOver())
        {
            yield return new WaitForSeconds(m_delay_between_enemy_spawn);
        }
        yield return null;
    }

    private void GenerateEnemy()
    {

    }
}
