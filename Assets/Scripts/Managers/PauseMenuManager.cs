using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField]
    private bool _pause;
    [SerializeField]
    private GameObject _pause_panel;
    [SerializeField]
    private Image _black_screen;
    [SerializeField]
    private AudioSource _audio_source;

    private void Awake()
    {
        _audio_source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_pause)
            return;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _pause = true;
            Time.timeScale = 0f;
            EnemyKamikaze.ACTIVE = false;
            EnemyTank.ACTIVE = false;
            _pause_panel.SetActive(true);
        }
    }

    public void ContinueButtonClick()
    {
        _pause = false;
        Time.timeScale = 1f;
        EnemyKamikaze.ACTIVE = true;
        EnemyTank.ACTIVE = true;
        _pause_panel.SetActive(false);
    }

    public void QuitButtonClick()
    {
        Time.timeScale = 1f;
        EnemyKamikaze.ACTIVE = false;
        EnemyTank.ACTIVE = false;
        StartCoroutine(ShowBlackScreen());
    }

    private IEnumerator ShowBlackScreen()
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
        SceneManager.LoadScene(GameScenes.MAIN_MENU_SCENE_INDEX);
    }
}
