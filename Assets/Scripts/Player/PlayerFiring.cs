using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFiring : MonoBehaviour
{
    private GameObject _default_bullet;
    [SerializeField]
    private float _last_fire_timestamp;
    [SerializeField]
    private float _fire_cd;
    [SerializeField]
    private GameObject _weapon;


    private void Update()
    {
        Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_position.z = transform.position.z;
        Vector3 aim_direction = (mouse_position - transform.position).normalized;
        Vector3 weapon_position = transform.position + aim_direction;

        _weapon.transform.position = weapon_position;

        if (Input.GetMouseButtonDown(0) && Time.time - _last_fire_timestamp >= _fire_cd)
        {
            _last_fire_timestamp = Time.time;
        }
    }
}
