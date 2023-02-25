using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyTank : MonoBehaviour, EnemyInterface
    {
        [SerializeField]
        private bool _stun;
        [SerializeField]
        private Transform _player_transform;
        [SerializeField]
        private PlayerStats _player_stats;
        [SerializeField]
        private float _hp;
        [SerializeField]
        private float _damage;
        [SerializeField]
        private int points;
        [SerializeField]
        private Vector2 _movement_vector;
        [SerializeField]
        private float _movement_speed;
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private GameManager _game_manager;
        [SerializeField]
        private SpriteRenderer _sprite_renderer;
        public static bool ACTIVE;

        private void Awake()
        {
            _stun = false;
            _movement_vector = Vector2.zero;
            _rigidbody = GetComponent<Rigidbody2D>();
            _sprite_renderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            _player_transform = player.gameObject.transform;
            _player_stats = player.GetComponent<PlayerStats>();

            GameObject _logic = GameObject.FindGameObjectWithTag("Logic");
            _game_manager = _logic.GetComponent<GameManager>();
        }

        private void Update()
        {
            _movement_vector = (_player_transform.position - transform.position).normalized;
        }

        private void FixedUpdate()
        {
            if (_stun || !ACTIVE)
            {
                _rigidbody.velocity = Vector2.zero;
                return;
            }
            _rigidbody.velocity = _movement_vector * _movement_speed;
        }

        /*private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                //Kaboom
                _player_stats.DecreaseHp(_damage);
                Destroy(this.gameObject);
            }
        }*/

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Player"))
                Attack();
        }

        private void Attack()
        {

        }

        public void ApplyBulletDamage(float damage)
        {
            _hp -= damage;
            StopAllCoroutines();
            StartCoroutine(Stun());
            StartCoroutine(HitFeedbackCoroutine());
            if (_hp <= 0)
            {
                _game_manager.IncreaseScore(points);
                Destroy(this.gameObject);
            }
        }

        private IEnumerator Stun()
        {
            _stun = true;
            yield return new WaitForSeconds(1f);
            _stun = false;
        }

        private IEnumerator HitFeedbackCoroutine()
        {
            _sprite_renderer.color = new Color(1, 1, 1);
            yield return new WaitForSeconds(0.5f);
            _sprite_renderer.color = new Color(1, 0, 0);
        }
    }
}
