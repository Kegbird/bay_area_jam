using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class PlayerShake : MonoBehaviour
{
    [SerializeField]
    private DiceBuild _build;
    [SerializeField]
    private float _build_reset_cd = 10f;
    [SerializeField]
    private float _shake_cd = 1f;
    [SerializeField]
    private float _last_shake_timestamp;
    [SerializeField]
    private float _last_buld_reset_timestamp;
    [SerializeField]
    private PlayerController _player_controller;
    [SerializeField]
    private PlayerFiring _player_firing;

    private void Awake()
    {
        _build = DiceBuild.NORMAL;
        _player_controller = GetComponent<PlayerController>();
        _player_firing = GetComponent<PlayerFiring>();
    }

    private void Start()
    {
        _player_firing.Switch(_build);
        _player_controller.Switch(_build);
    }



    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - _last_shake_timestamp >= _shake_cd)
        {
            //shake
            List<DiceBuild> dice_builds = new List<DiceBuild>();
            for(int i=1; i<=6; i++)
            {
                if (_build != (DiceBuild)(i))
                    dice_builds.Add((DiceBuild)(i));
            }
            _build = dice_builds[Random.Range(0, dice_builds.Count)];
            _player_firing.Switch(_build);
            _player_controller.Switch(_build);
            _last_shake_timestamp = Time.time;
            _last_buld_reset_timestamp = Time.time;
        }

        if (_build != DiceBuild.NORMAL)
        {
            if (Time.time - _last_buld_reset_timestamp >= _build_reset_cd)
            {
                _build = DiceBuild.NORMAL;
                _player_firing.Switch(_build);
                _player_controller.Switch(_build);
            }
            else
            {

            }
        }
    }

    public DiceBuild GetCurrentBuild()
    {
        return _build;
    }
}
