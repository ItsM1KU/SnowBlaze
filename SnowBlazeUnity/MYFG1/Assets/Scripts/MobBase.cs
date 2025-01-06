using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MobBase : ScriptableObject
{
    [SerializeField] string mobName;

    [TextArea]
    [SerializeField] string mobDescription;

    [SerializeField] Sprite mobSprite;
    [SerializeField] int health;
    [SerializeField] bool canHeal;
    [SerializeField] int healAmount;
    [SerializeField] List<moveBase> moves = new List<moveBase>();

    [SerializeField] bool isBoss;

    private int currentHealth;


    public void ResetHealth()
    {
        currentHealth = health;
    }

    public void heal()
    {
        if (canHeal)
        {
            int AmountHeal = Random.Range(10, healAmount);
            currentHealth += AmountHeal;
            if(currentHealth > health)
            {
                currentHealth = health;
            }
              
        }
    }


    public bool takeDamage(moveBase move)
    {
        currentHealth -= move.MoveDamage;
        if(currentHealth <= 0) 
        {
            currentHealth = 0;
            return true;
        }
        return false;
    }

    public int CurrentHealth => currentHealth;

    public bool CanHeal => canHeal;

    public bool IsBoss => isBoss;

    public int HealAmount => healAmount;

    public string MobName
    {
        get { return mobName; }
    }

    public Sprite MobSprite
    {
        get { return mobSprite; }
    }

    public int Health
    {
        get { return health; }
    }

    public List<moveBase> Moves
    {
        get { return moves; }
    }
}
