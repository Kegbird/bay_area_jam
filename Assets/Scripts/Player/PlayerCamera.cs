using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField]
        private Transform _player_transform;
        [SerializeField]
        private float _smooth_factor;
        [SerializeField]
        private Vector3 _velocity;
        [SerializeField]
        private Vector3 _camera_offset;
        private void Start()
        {
            _player_transform = GameObject.Find("Player").transform;
            SnapCameraOnPlayer();
        }

        public void SnapCameraOnPlayer()
        {
            transform.position = _player_transform.position + _camera_offset;
        }

        private void LateUpdate()
        {
            Vector3 target_position = _player_transform.position + _camera_offset;
            transform.position = Vector3.SmoothDamp(transform.position, target_position, ref _velocity, _smooth_factor);
        }
    }
}