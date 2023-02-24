using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float _movement_speed;
        [SerializeField]
        private Vector3 _movement_vector;
        [SerializeField]
        private bool _active;
        [SerializeField]
        private bool _alive;
        [SerializeField]
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _active = true;
            _alive = true;
        }

        private void Update()
        {
            _movement_vector.x = Input.GetAxisRaw("Horizontal");
            _movement_vector.y = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            if (!_active || !_alive)
                return;
            _rigidbody.velocity = _movement_vector * _movement_speed;
        }
    }
}
