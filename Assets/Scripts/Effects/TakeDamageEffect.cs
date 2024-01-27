using UnityEngine;

public class TakeDamageEffect : MonoEffect
{
    [SerializeField]
    private MetronomeEvent deathEvent;

    [SerializeField]
    private SpriteRenderer[] hearts;
    [SerializeField]
    private Color damagedColor;

    private int lives;

    private void Awake()
    {
        lives = hearts.Length;
    }

    [ContextMenu("Trigger Damage")]
    protected override void OnEvent()
    {
        for (int i = lives; i < hearts.Length; i++)
            hearts[i].color = damagedColor;
        lives--;
        if (lives <= 0 && deathEvent)
            deathEvent.Trigger();
    }
}
