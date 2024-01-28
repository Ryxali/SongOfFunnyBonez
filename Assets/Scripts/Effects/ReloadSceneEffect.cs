using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadSceneEffect : MonoEffect
{
    protected override void OnEvent()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
