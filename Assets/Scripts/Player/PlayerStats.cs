using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float _hp;
    [SerializeField]
    private PlayerUI _player_ui;

    private void Awake()
    {
        _player_ui = GetComponent<PlayerUI>();
        _hp = 100f;
    }

    public float GetCurrentHp()
    {
        return _hp;
    }

    public void IncreaseHp(float increment)
    {
        _hp = Mathf.Clamp(_hp+increment, 0, 100);
        _player_ui.UpdateHp(_hp);
    }

    public void DecreaseHp(float decrement)
    {
        _hp = Mathf.Clamp(_hp - decrement, 0, 100);
        _player_ui.UpdateHp(_hp);
    }
}
