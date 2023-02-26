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
    [SerializeField]
    private PlayerAnimator _player_animator;
    [SerializeField]
    private PlayerUI _player_ui;
    [SerializeField]
    private GameObject _shake_gameobject;
    [SerializeField]
    private bool _active;

    private void Awake()
    {
        _active = true;
        _build = DiceBuild.NORMAL;
        _player_controller = GetComponent<PlayerController>();
        _player_firing = GetComponent<PlayerFiring>();
        _player_animator = GetComponent<PlayerAnimator>();
        _player_ui = GetComponent<PlayerUI>();
    }

    private void Start()
    {
        _player_firing.Switch(_build);
        _player_controller.Switch(_build);
        _player_animator.Switch(_build);
    }

    public void Disable()
    {
        _active = false;
    }

    private void Update()
    {
        if (!_active)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && Time.time - _last_shake_timestamp >= _shake_cd)
        {
            List<DiceBuild> dice_builds = new List<DiceBuild>();
            for(int i=1; i<=6; i++)
            {
                if (_build != (DiceBuild)(i))
                    dice_builds.Add((DiceBuild)(i));
            }
            _build = dice_builds[Random.Range(0, dice_builds.Count)];
            _player_firing.Switch(_build);
            _player_controller.Switch(_build);
            _player_animator.Switch(_build);
            StartCoroutine(ShakeAnimation());
            _last_shake_timestamp = Time.time;
            _last_buld_reset_timestamp = Time.time;
            _player_ui.ShowBuildBar();
            _player_ui.SetBuildText(_build.ToString());
        }
        _player_ui.UpdateShakeBar((Time.time - _last_shake_timestamp) / _shake_cd);

        if (_build != DiceBuild.NORMAL)
        {
            if (Time.time - _last_buld_reset_timestamp >= _build_reset_cd)
            {
                _build = DiceBuild.NORMAL;
                _player_firing.Switch(_build);
                _player_controller.Switch(_build);
                _player_animator.Switch(_build);
                StartCoroutine(ShakeAnimation());
                _player_ui.HideBuildBar();
            }
            _player_ui.UpdateBuildBar(1 - (Time.time - _last_buld_reset_timestamp) / _build_reset_cd);
        }
        else
        {
            _player_ui.UpdateBuildBar(1f);
        }
    }

    IEnumerator ShakeAnimation()
    {
        _shake_gameobject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _shake_gameobject.SetActive(false);
    }

    public DiceBuild GetCurrentBuild()
    {
        return _build;
    }
}
