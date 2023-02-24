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

    private void Awake()
    {
        _build = DiceBuild.NORMAL;
    }

    private void Update()
    {
        
    }

}
