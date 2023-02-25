using Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public static int SCORE;
    [SerializeField]
    private Image _black_screen;
    [SerializeField]
    private AudioSource _audio_source;
    [SerializeField]
    private bool _game_over;
    [SerializeField]
    private PlayerStats _player_stats;
    [SerializeField]
    private PlayerController _player_controller;
    [SerializeField]
    private PlayerFiring _player_firing;
    [SerializeField]
    private PlayerShake _player_shake;
    [SerializeField]
    private TextMeshProUGUI _score_text;

    private void Awake()
    {
        _game_over = false;
        SCORE = 0;
        _score_text.text = SCORE.ToString();
        _audio_source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(HideBlackScreen());
    }

    public void IncreaseScore(int increment)
    {
        SCORE += increment;
        _score_text.text = SCORE.ToString();
    }

    private void Update()
    {
        if (_game_over)
            return;

        if (_player_stats.GetCurrentHp() <= 0)
        {
            _game_over = true;
            _player_shake.Disable();
            _player_controller.Disable();
            _player_firing.Disable();
            StartCoroutine(ShowBlackScreenAndPlayerScore());
        }
    }

    private IEnumerator ShowBlackScreenAndPlayerScore()
    {
        _black_screen.raycastTarget = true;
        float i = 0;
        while (i <= 1f)
        {
            i += Time.deltaTime;
            _audio_source.volume = 1f - i;
            _black_screen.color = new Color(0, 0, 0, i / 1f);
            yield return new WaitForEndOfFrame();
        }
        _black_screen.raycastTarget = false;
    }

    private IEnumerator HideBlackScreen()
    {
        _black_screen.raycastTarget = true;
        float i = 1f;
        while (i >= 0)
        {
            i -= Time.deltaTime;
            _audio_source.volume = 1f - i;
            _black_screen.color = new Color(0, 0, 0, i / 1f);
            yield return new WaitForEndOfFrame();
        }
        _black_screen.raycastTarget = false;
    }
}
