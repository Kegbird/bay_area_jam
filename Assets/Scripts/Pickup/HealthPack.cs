using UnityEngine;

namespace Pickup
{
    public class HealthPack : MonoBehaviour
    {
        [SerializeField]
        private PlayerStats _player_stats;
        [SerializeField]
        private float _recharge_cd;
        [SerializeField]
        private SpriteRenderer _sprite_renderer;
        [SerializeField]
        private float _heal_amount;
        [SerializeField]
        private bool _active;
        [SerializeField]
        private float _last_heal_timestamp;

        private void Awake()
        {
            _active = true;
            _sprite_renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (_active)
                return;

            if (Time.time - _last_heal_timestamp >= _recharge_cd)
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
                _last_heal_timestamp = Time.time;
                _sprite_renderer.enabled = false;
                _player_stats.IncreaseHp(_heal_amount);
                _active = false;
            }
        }
    }
}
