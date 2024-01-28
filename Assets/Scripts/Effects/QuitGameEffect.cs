using UnityEngine;

public class QuitGameEffect : MonoEffect
{
    protected override void OnEvent()
    {
        Application.Quit();
    }
}
