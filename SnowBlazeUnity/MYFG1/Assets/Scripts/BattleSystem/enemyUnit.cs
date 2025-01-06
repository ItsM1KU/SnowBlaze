using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class enemyUnit : MonoBehaviour
{
    [SerializeField] Image unitSprite;

    [SerializeField] public MobBase mobBase;

    Vector3 originalPos;
    Color originalColor;

    private void Awake()
    {
        originalPos = unitSprite.transform.localPosition;
        originalColor = unitSprite.color;
    }

    public void setup()
    {
        mobBase.ResetHealth();
        unitSprite.sprite = mobBase.MobSprite;
        unitSprite.color = originalColor;
        playEnterAnimation();
    }

    public void assignMob(MobBase mob)
    {
        this.mobBase = mob;
    }

    public void playEnterAnimation()
    {
        unitSprite.transform.localPosition = new Vector3(-1130f, originalPos.y);
        unitSprite.transform.DOLocalMoveX(originalPos.x, 1f);
    }

    public void playAttackAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(unitSprite.transform.DOLocalMoveX(originalPos.x + 30f, 0.25f));
        sequence.Append(unitSprite.transform.DOLocalMoveX(originalPos.x, 0.25f));
    }

    public void playHitAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(unitSprite.DOColor(Color.grey, 0.1f));
        sequence.Append(unitSprite.DOColor(originalColor, 0.1f));
        sequence.Append(unitSprite.DOColor(Color.grey, 0.1f));
        sequence.Append(unitSprite.DOColor(originalColor, 0.1f));
    }

    public void playFaintAnimation()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(unitSprite.transform.DOLocalMoveY(originalPos.y + 150f, 0.5f));
        sequence.Join(unitSprite.DOFade(0f, 0.5f));
    }

}
