using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class playerUnit : MonoBehaviour
{
    [SerializeField] SpriteRenderer playerSprite;

    Vector3 originalPos;
    Color originalColor;

    private void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        originalPos = playerSprite.transform.localPosition;
        originalColor = playerSprite.color;
    }

    public void unitSetup()
    {
        playerSprite.color = originalColor;
        playEntryAnimation();
    }

    public void playEntryAnimation()
    {
        playerSprite.transform.localPosition = new Vector3(1100f, originalPos.y);
        playerSprite.transform.DOLocalMoveX(originalPos.x, 1f);
    }

    public void playAttackAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(playerSprite.transform.DOLocalMoveX(originalPos.x - 30f, 0.25f));
        sequence.Append(playerSprite.transform.DOLocalMoveX(originalPos.x, 0.25f));
    }

    public void playHitAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(playerSprite.DOColor(Color.grey, 0.1f));
        sequence.Append(playerSprite.DOColor(originalColor, 0.1f));
        sequence.Append(playerSprite.DOColor(Color.grey, 0.1f));
        sequence.Append(playerSprite.DOColor(originalColor, 0.1f));
    }

    public void playFaintAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(playerSprite.transform.DOLocalMoveY(originalPos.y - 150f, 0.5f));
        sequence.Join(playerSprite.DOFade(0f, 0.5f));
    }

}
