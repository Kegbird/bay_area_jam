using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Image _shake_bar;
    [SerializeField]
    private Image _build_bar;
    [SerializeField]
    private Image _hp_bar;

    public void UpdateHp(float hp)
    {
        _hp_bar.fillAmount = hp / 100f;
    }

    public void UpdateBuildBar(float percentage)
    {
        _build_bar.fillAmount = percentage;
    }

    public void HideBuildBar()
    {
        _build_bar.gameObject.SetActive(false);
    }

    public void ShowBuildBar()
    {
        _build_bar.gameObject.SetActive(true);
    }

    public void UpdateShakeBar(float percentage)
    {
        _shake_bar.fillAmount = percentage;
    }
}
