using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pickup
{
    public class ScorePack : MonoBehaviour
    {
        [SerializeField]
        private GameManager _game_manager;
        [SerializeField]
        private float _recharge_cd;
        [SerializeField]
        private SpriteRenderer _sprite_renderer;
        [SerializeField]
        private int _score_amount;
        [SerializeField]
        private bool _active;
        [SerializeField]
        private float _last_pickup_timestamp;

        private void Awake()
        {
            _active = true;
            _sprite_renderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            GameObject logic = GameObject.FindWithTag("Logic");
            _game_manager = logic.GetComponent<GameManager>();
        }

        private void Update()
        {
            if (_active)
                return;

            if (Time.time - _last_pickup_timestamp >= _recharge_cd)
            {
                _active = true;
                _sprite_renderer.enabled = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_active)
                return;

            if (collision.gameObject.tag.Equals("Player"))
            {
                _last_pickup_timestamp = Time.time;
                _sprite_renderer.enabled = false;
                _game_manager.IncreaseScore(_score_amount);
                _active = false;
            }
        }
    }
}