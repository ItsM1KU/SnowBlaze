using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class moveBase : ScriptableObject
{
    [SerializeField] string moveName;

    [TextArea]
    [SerializeField] string moveDescription;

    [SerializeField] int moveDamage;
    [SerializeField] int moveAccuracy;

    //only for player moves
    [SerializeField] int damageUpgradeAmount;
    [SerializeField] int currentUpgrade = 0;
    int maxUpgrade = 3;

    public string MoveName
    {
        get { return moveName; }
    }

    public string MoveDescription
    {
        get { return moveDescription; }
    }

    public int MoveDamage
    {
        get { return moveDamage; }
    }

    public int MoveAccuracy
    {
        get { return moveAccuracy; }
    }

    public int DamageUpgradeAmount => damageUpgradeAmount;
    public int CurrentUpgrade => currentUpgrade;
    public int MaxUpgrade => maxUpgrade;

    public int GetUpgradedDamage()
    {
        //return moveDamage + ((currentUpgrade + 1) * damageUpgradeAmount);
        return moveDamage + damageUpgradeAmount;
    }

    public bool canUpgrade()
    {
        return currentUpgrade < maxUpgrade;
    }

    public void upgrade()
    {
        if (canUpgrade())
        {
            int newDamage = moveDamage + damageUpgradeAmount;
            moveDamage = newDamage;
            currentUpgrade++;
        }
    }

}
