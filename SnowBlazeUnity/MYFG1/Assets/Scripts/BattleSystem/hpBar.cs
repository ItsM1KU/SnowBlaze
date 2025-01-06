using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpBar : MonoBehaviour
{
    [SerializeField] Slider hpSlider;

    public void setBaseHP(PlayerData playerdata)
    {
        hpSlider.maxValue = playerdata.Health;
        hpSlider.value = playerdata.Health;
    }


}
