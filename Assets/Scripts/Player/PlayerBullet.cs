using Enemy;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private float _bullet_speed;
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float _damage;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector3 aim_direction)
    {
        _rigidbody.AddForce(aim_direction * _bullet_speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
            collision.gameObject.GetComponent<EnemyInterface>().ApplyBulletDamage(_damage);
        Destroy(this.gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
