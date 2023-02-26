using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _sprite_renderer;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private RuntimeAnimatorController _normal_animator;
    [SerializeField]
    private RuntimeAnimatorController _cowboy_animator;
    [SerializeField]
    private RuntimeAnimatorController _king_animator;
    [SerializeField]
    private RuntimeAnimatorController _skater_animator;
    [SerializeField]
    private RuntimeAnimatorController _gentleman_animator;
    [SerializeField]
    private RuntimeAnimatorController _ninja_animator;
    [SerializeField]
    private RuntimeAnimatorController _chinchilla_animator;
    [SerializeField]
    private bool _left;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _sprite_renderer = GetComponent<SpriteRenderer>();
    }

    public void Switch(DiceBuild dice_build)
    {
        switch (dice_build)
        {
            case DiceBuild.NORMAL:
                _animator.runtimeAnimatorController = _normal_animator;
                break;
            case DiceBuild.NINJA:
                _animator.runtimeAnimatorController = _ninja_animator;
                break;
            case DiceBuild.GENTLEMAN:
                _animator.runtimeAnimatorController = _gentleman_animator;
                break;
            case DiceBuild.SKATER:
                _animator.runtimeAnimatorController = _skater_animator;
                break;
            case DiceBuild.COWBOY:
                _animator.runtimeAnimatorController = _cowboy_animator;
                break;
            case DiceBuild.KING:
                _animator.runtimeAnimatorController = _king_animator;
                break;
            case DiceBuild.CHINCHILLA:
                _animator.runtimeAnimatorController = _chinchilla_animator;
                break;
        }
    }

    public void SetAnimationParams(Vector2 movement_vector)
    {
        if (movement_vector.x > 0)
        {
            _left = false;
            _animator.SetBool("right", true);
            _animator.SetBool("left", false);
        }
        if (movement_vector.x < 0)
        {
            _left = true;
            _animator.SetBool("left", true);
            _animator.SetBool("right", false);
        }
        if(movement_vector.y!=0)
        {
            if (!_left)
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
        if (movement_vector.x == 0 && movement_vector.y == 0)
        {
            ResetAnimator();
        }
    }

    private void ResetAnimator()
    {
        _animator.SetBool("left", false);
        _animator.SetBool("right", false);
    }

    public void Die()
    {
        _animator.SetBool("dead", true);
    }

    public void HitFeedback()
    {
        StopAllCoroutines();
        StartCoroutine(HitFeedbackCoroutine());
    }

    private IEnumerator HitFeedbackCoroutine()
    {
        _sprite_renderer.color = new Color(1, 0, 0);
        yield return new WaitForSeconds(0.2f);
        _sprite_renderer.color = new Color(1, 1, 1);
    }
}
