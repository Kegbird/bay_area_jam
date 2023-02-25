using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float _movement_speed;
        [SerializeField]
        private float _multiplier;
        [SerializeField]
        private Vector3 _movement_vector;
        [SerializeField]
        private bool _active;
        [SerializeField]
        private bool _alive;
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private PlayerAnimator _player_animator;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _player_animator = GetComponent<PlayerAnimator>();
        }

        private void Start()
        {
            _active = true;
            _alive = true;
        }

        public void Disable()
        {
            _active = false;
        }

        private void Update()
        {
            _movement_vector.x = Input.GetAxisRaw("Horizontal");
            _movement_vector.y = Input.GetAxisRaw("Vertical");
            _player_animator.SetAnimationParams(_movement_vector);
        }

        private void FixedUpdate()
        {
            if (!_active || !_alive)
                return;
            _rigidbody.velocity = _movement_vector * _movement_speed * _multiplier;
        }

        public void Switch(DiceBuild dice_build)
        {
            switch (dice_build)
            {
                case DiceBuild.NORMAL:
                    _multiplier = 1f;
                    break;
                case DiceBuild.NINJA:
                    _multiplier = 3f;
                    break;
                case DiceBuild.GENTLEMAN:
                    _multiplier = 0.3f;
                    break;
                case DiceBuild.SKATER:
                    _multiplier = 2f;
                    break;
                case DiceBuild.COWBOY:
                    _multiplier = 1f;
                    break;
                case DiceBuild.KING:
                    _multiplier = 0.5f;
                    break;
                case DiceBuild.CHINCHILLA:
                    _multiplier = 4f;
                    break;
            }
        }
    }
}
