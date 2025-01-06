using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class enemyHUD : MonoBehaviour
{
    [SerializeField] TMP_Text enemyName;
    [SerializeField] Slider hpSlider;

    enemyUnit _enemyUnit;
    public void setupHUD(enemyUnit enemyUnit)
    {
        _enemyUnit = enemyUnit;
        enemyName.text = enemyUnit.mobBase.MobName;
        hpSlider.maxValue = enemyUnit.mobBase.Health;
        hpSlider.value = enemyUnit.mobBase.Health;
    }

    public void updateDHP()
    {
        StartCoroutine(smoothDHP(_enemyUnit.mobBase.CurrentHealth));
    }
    public void updateIHP()
    {
        StartCoroutine(smoothIHP(_enemyUnit.mobBase.CurrentHealth));
    }

    public IEnumerator smoothDHP(int newhp)
    {
        float currHP = hpSlider.value;
        float duration = 1f; // Time in seconds for the health bar to update
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            hpSlider.value = Mathf.Lerp(currHP, newhp, elapsedTime / duration);
            yield return null;
        }

        hpSlider.value = newhp;
    }

    public IEnumerator smoothIHP(int newhp)
    {
        float currHP = hpSlider.value;
        float duration = 1f; // Time in seconds for the health bar to update
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            hpSlider.value = Mathf.Lerp(currHP, newhp, elapsedTime / duration);
            yield return null;
        }
        hpSlider.value = newhp;
    }
}
