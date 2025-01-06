using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class PlayerData : MonoBehaviour
{
    [SerializeField] string playerName;

    [SerializeField] int health;
    [SerializeField] int minConcentration;
    [SerializeField] int maxConcentration;
    [SerializeField] Sprite characterSprite;

    [SerializeField] public int exp;
    [SerializeField] public int coins;
    [SerializeField] TMP_Text expText;
    [SerializeField] TMP_Text coinsText;

    [SerializeField] public List<moveBase> moves = new List<moveBase>();

    private int currentHealth;

    public int CurrentHealth => currentHealth;
    public string PlayerName { get { return playerName; } }

    public int Health { get { return health; } }

    public int MinConcentration { get { return minConcentration; } }

    public int MaxConcentration { get {return maxConcentration; } }

    private void Update()
    {
        expText.text = exp.ToString();
        coinsText.text = coins.ToString();
    }


    public void ResetHealth()
    {
        currentHealth = health;
    }


    public bool takeDamage(moveBase move)
    {
        currentHealth -= move.MoveDamage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            return true;
        }
        return false;
    }

    public void heal()
    {
        int amounttoHeal = Random.Range(minConcentration, maxConcentration);
        currentHealth += amounttoHeal;
        if(currentHealth > health)
        {
            currentHealth = health;
        }
    }

    public List<moveBase> pickMoves(int reqMoves)
    {
        if (reqMoves <= 0 || moves.Count == 0)
        {
            return new List<moveBase>();
        }

        reqMoves = Mathf.Min(reqMoves, moves.Count);

        List<moveBase> selectedMoves = new List<moveBase>();
        List<moveBase> availableMoves = new List<moveBase>(moves);

        for (int i = 0; i < reqMoves; i++)
        {
            int randomIndex = Random.Range(0, availableMoves.Count);
            selectedMoves.Add(availableMoves[randomIndex]);
            availableMoves.RemoveAt(randomIndex);
        }
        return selectedMoves;
    }

    public void addMove(moveBase move)
    {
        if (moves.Contains(move))
        {
            Debug.Log("move is already present");
            return;
        }
        else
        {
            moves.Add(move);
            Debug.Log(moves.Count);
        }  
    }

    public void updateExp(int amount)
    {
        exp += amount;
        if(exp <= 0)
        {
            exp = 0;
        }
    }

    public void AddCoins(int minamount, int maxamount)
    {
        int addamount = Random.Range(minamount, maxamount);
        coins += addamount;
        if (coins <= 0) { 
            coins = 0;
        }
    }

    public void DeductCoins(int amount)
    {
        coins -= amount;
        if (coins <= 0)
        {
            coins = 0;
        }
    }
}
