using UnityEngine;

public class TakeDamageEffect : MonoEffect
{
    [SerializeField]
    private MetronomeEvent deathEvent;

    [SerializeField]
    private SpriteRenderer[] hearts;
    [SerializeField]
    private Sprite damagedSprite;

    private int lives;

    private void Awake()
    {
        lives = hearts.Length;
    }

    [ContextMenu("Trigger Damage")]
    protected override void OnEvent()
    {
        lives--;
        for (int i = lives; i < hearts.Length; i++)
            hearts[i].sprite = damagedSprite;
        if (lives <= 0 && deathEvent)
            deathEvent.Trigger();
    }
}
