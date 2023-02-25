using System.Collections;
using UnityEngine;
using Utility;

public class PlayerFiring : MonoBehaviour
{
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private float _last_fire_timestamp;
    [SerializeField]
    private float _fire_cd;
    [SerializeField]
    private GameObject _weapon;
    [SerializeField]
    private bool _active;
    [SerializeField]
    private GameObject _normal_bullet;
    [SerializeField]
    private GameObject _ninja_bullet;
    [SerializeField]
    private GameObject _gentleman_bullet;
    [SerializeField]
    private GameObject _cowboy_bullet;
    [SerializeField]
    private GameObject _skater_bullet;
    [SerializeField]
    private GameObject _king_bullet;
    [SerializeField]
    private Sprite _normal_weapon;
    [SerializeField]
    private Sprite _gentleman_weapon;
    [SerializeField]
    private Sprite _cowboy_weapon;
    [SerializeField]
    private Sprite _ninja_weapon;
    [SerializeField]
    private Sprite _skater_weapon;
    [SerializeField]
    private Sprite _king_weapon;
    [SerializeField]
    private PlayerShake _player_shake;

    private void Awake()
    {
        _player_shake = GetComponent<PlayerShake>();
    }

    private void Update()
    {
        Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse_position.z = transform.position.z;

        Vector3 aim_direction = (mouse_position - transform.position).normalized;
        Vector3 weapon_position = Vector3.zero;
        weapon_position.x = transform.position.x + aim_direction.x;
        weapon_position.y = transform.position.y + aim_direction.y;
        weapon_position.z = transform.position.z;

        float angle = Mathf.Atan2(aim_direction.y, aim_direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _weapon.transform.rotation = rotation;
        _weapon.transform.position = weapon_position;

        if (!_active)
            return;

        if (Input.GetMouseButton(0) && Time.time - _last_fire_timestamp >= _fire_cd)
        {
            Vector3 weapon_hole_position = transform.position + aim_direction * 1.2f;
            _last_fire_timestamp = Time.time;

            if (_player_shake.GetCurrentBuild() == DiceBuild.NINJA)
                StartCoroutine(NinjaFireMode(weapon_hole_position, aim_direction, rotation));
            else if (_player_shake.GetCurrentBuild() == DiceBuild.COWBOY)
                StartCoroutine(CowboyFireMode(weapon_hole_position, aim_direction, rotation));
            else
            {
                GameObject bullet = GameObject.Instantiate(_bullet, weapon_hole_position, rotation);
                bullet.SetActive(true);
                bullet.GetComponent<PlayerBullet>().Fire(aim_direction);
            }
        }
    }

    private IEnumerator NinjaFireMode(Vector3 weapon_hole_position, Vector3 aim_direction, Quaternion rotation)
    {
        float time_between_shots = 0.1f;
        for(int i=0; i<3; i++)
        {
            GameObject bullet = GameObject.Instantiate(_bullet, weapon_hole_position, rotation);
            bullet.SetActive(true);
            bullet.GetComponent<PlayerBullet>().Fire(aim_direction);
            yield return new WaitForSeconds(time_between_shots);
        }
        yield return null;
    }

    private IEnumerator CowboyFireMode(Vector3 weapon_hole_position, Vector3 aim_direction, Quaternion rotation)
    {
        float time_between_shots = 0.2f;
        for (int i = 0; i < 2; i++)
        {
            GameObject bullet = GameObject.Instantiate(_bullet, weapon_hole_position, rotation);
            bullet.SetActive(true);
            bullet.GetComponent<PlayerBullet>().Fire(aim_direction);
            yield return new WaitForSeconds(time_between_shots);
        }
        yield return null;
    }

    public void Switch(DiceBuild dice_build)
    {
        switch (dice_build)
        {
            case DiceBuild.NORMAL:
                _weapon.GetComponent<SpriteRenderer>().sprite = _normal_weapon;
                _bullet = _normal_bullet;
                _active = true;
                _fire_cd = 1f;
                break;
            case DiceBuild.NINJA:
                _weapon.GetComponent<SpriteRenderer>().sprite = _ninja_weapon;
                _bullet = _ninja_bullet;
                _active = true;
                _fire_cd = 1f;
                break;
            case DiceBuild.GENTLEMAN:
                _weapon.GetComponent<SpriteRenderer>().sprite = _gentleman_weapon;
                _bullet = _gentleman_bullet;
                _active = true;
                _fire_cd = 3f;
                break;
            case DiceBuild.SKATER:
                _weapon.GetComponent<SpriteRenderer>().sprite = _skater_weapon;
                _bullet = _skater_bullet;
                _active = true;
                _fire_cd = 2f;
                break;
            case DiceBuild.COWBOY:
                _weapon.GetComponent<SpriteRenderer>().sprite = _cowboy_weapon;
                _bullet = _cowboy_bullet;
                _active = true;
                _fire_cd = 2f;
                break;
            case DiceBuild.KING:
                _weapon.GetComponent<SpriteRenderer>().sprite = _king_weapon;
                _bullet = _king_bullet;
                _active = true;
                _fire_cd = 0.2f;
                break;
            case DiceBuild.CHINCHILLA:
                _weapon.GetComponent<SpriteRenderer>().sprite = null;
                _active = false;
                break;
        }
    }
}
