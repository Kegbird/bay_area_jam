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
        [SerializeField]
        private PlayerAnimator _player_animator;
        [SerializeField]
        private Animator _animator;
        public static bool ACTIVE;
        [SerializeField]
        private float _attack_range;
        [SerializeField]
        private CircleCollider2D _circle_collider;
        [SerializeField]
        private AudioSource _audio_source;

        private void Awake()
        {
            _stun = false;
            _movement_vector = Vector2.zero;
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _sprite_renderer = GetComponent<SpriteRenderer>();
            _circle_collider = GetComponent<CircleCollider2D>();
            _audio_source = GetComponent<AudioSource>();
        }

        private void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            _player_transform = player.gameObject.transform;
            _player_stats = player.GetComponent<PlayerStats>();
            _player_animator = player.GetComponent<PlayerAnimator>();

            GameObject _logic = GameObject.FindGameObjectWithTag("Logic");
            _game_manager = _logic.GetComponent<GameManager>();
        }

        private void Update()
        {
            _movement_vector = (_player_transform.position - transform.position).normalized;
            SetAnimationParams();
        }

        public void SetAnimationParams()
        {
            if (_movement_vector.x > 0)
            {
                _animator.SetBool("right", true);
                _animator.SetBool("left", false);
            }
            else
            {
                _animator.SetBool("left", true);
                _animator.SetBool("right", false);
            }
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                _stun = true;
                _animator.SetBool("attack", true);
            }
        }

        private void Attack()
        {
            float distance = Vector3.Distance(_player_transform.position, transform.position);
            if (distance <= _attack_range)
            {
                _player_animator.HitFeedback();
                _player_stats.DecreaseHp(_damage);
            }
            else
            {
                _stun = false;
                _animator.SetBool("attack", false);
            }
        }

        public void ApplyBulletDamage(float damage)
        {
            _hp -= damage;
            StopAllCoroutines();
            if (_hp <= 0)
            {
                _sprite_renderer.color = new Color(1, 1, 1);
                _stun = true;
                _game_manager.IncreaseScore(points);
                _circle_collider.enabled = false;
                _animator.SetBool("attack", false);
                _animator.SetBool("dead", true);
                _audio_source.Play();
            }
            else
            {
                StartCoroutine(Stun());
                StartCoroutine(HitFeedbackCoroutine());
            }
        }

        public void Die()
        {
            Destroy(this.gameObject);
        }

        private IEnumerator Stun()
        {
            _stun = true;
            yield return new WaitForSeconds(0.2f);
            _stun = false;
        }

        private IEnumerator HitFeedbackCoroutine()
        {
            _sprite_renderer.color = new Color(1, 0, 0);
            yield return new WaitForSeconds(0.5f);
            _sprite_renderer.color = new Color(1, 1, 1);
        }
    }
}
