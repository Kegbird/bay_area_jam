using Enemy;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public static int SCORE;
    [SerializeField]
    public static float SECONDS_OF_LIFE;
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
    private PlayerAnimator _player_animator;
    [SerializeField]
    private PlayerShake _player_shake;
    [SerializeField]
    private TextMeshProUGUI _score_text;
    [SerializeField]
    private GameObject _final_score_panel;
    [SerializeField]
    private TextMeshProUGUI _final_score_text;
    [SerializeField]
    private TextMeshProUGUI _survival_time_text;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;


    private void Awake()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        _game_over = false;
        SCORE = 0;
        SECONDS_OF_LIFE = 0;
        EnemyKamikaze.ACTIVE = true;
        EnemyTank.ACTIVE = true;
        _score_text.text = SCORE.ToString();
        _audio_source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SECONDS_OF_LIFE = Time.time;
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
            SECONDS_OF_LIFE = Time.time - SECONDS_OF_LIFE;
            _game_over = true;
            _player_shake.Disable();
            _player_animator.Die();
            _player_controller.Disable();
            _player_firing.Disable();
            EnemyKamikaze.ACTIVE = false;
            EnemyTank.ACTIVE = false;
            ShowFinalScorePanel();
        }
    }

    public void ShowFinalScorePanel()
    {
        TimeSpan time_span = TimeSpan.FromSeconds(SECONDS_OF_LIFE);
        _survival_time_text.text = string.Format("{1:D2}m:{2:D2}s",
                        time_span.Hours,
                        time_span.Minutes,
                        time_span.Seconds,
                        time_span.Milliseconds);
        _final_score_text.text = GameManager.SCORE.ToString();
        _final_score_panel.SetActive(true);
    }

    public bool IsGameOver()
    {
        return _game_over;
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(ShowBlackScreen());
    }

    private IEnumerator ShowBlackScreen()
    {
        _black_screen.raycastTarget = true;
        float i = 0;
        while (i <= 0.8f)
        {
            i += Time.deltaTime;
            _audio_source.volume = 1f - i - 0.3f;
            _black_screen.color = new Color(0, 0, 0, i / 1f);
            yield return new WaitForEndOfFrame();
        }
        _black_screen.raycastTarget = false;
        SceneManager.LoadScene(GameScenes.MAIN_MENU_SCENE_INDEX);
    }

    private IEnumerator HideBlackScreen()
    {
        _black_screen.raycastTarget = true;
        float i = 1f;
        while (i >= 0)
        {
            i -= Time.deltaTime;
            _audio_source.volume = 1f - i -0.3f;
            _black_screen.color = new Color(0, 0, 0, i / 1f);
            yield return new WaitForEndOfFrame();
        }
        _black_screen.raycastTarget = false;
    }
}
