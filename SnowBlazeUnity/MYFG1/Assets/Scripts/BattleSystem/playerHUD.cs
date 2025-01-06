using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class playerHUD : MonoBehaviour
{
    [SerializeField] TMP_Text playerName;
    [SerializeField] Slider HPslider;

    PlayerData _playerData;
    public void setupHUD(PlayerData playerData)
    {
        _playerData = playerData;

        playerName.text = playerData.PlayerName;
        HPslider.maxValue = playerData.Health;
        HPslider.value = playerData.Health;
    }

    public void updateDHP()
    {
        StartCoroutine(smoothDHP(_playerData.CurrentHealth));
    }

    public void updateIHP()
    {
        StartCoroutine(smoothIHP(_playerData.CurrentHealth));
    }

    public IEnumerator smoothDHP(int newhp)
    {
        float currHP = HPslider.value;
        float duration = 1f; // Time in seconds for the health bar to update
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            HPslider.value = Mathf.Lerp(currHP, newhp, elapsedTime / duration);
            yield return null;
        }
        HPslider.value = newhp;
    }

    public IEnumerator smoothIHP(int newhp)
    {
        float currHP = HPslider.value;
        float duration = 1f; // Time in seconds for the health bar to update
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            HPslider.value = Mathf.Lerp(currHP, newhp, elapsedTime / duration);
            yield return null;
        }
        HPslider.value = newhp;
    }
}
