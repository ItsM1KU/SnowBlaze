using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy}
public class BattleSystem : MonoBehaviour
{
    [SerializeField] playerHUD playerhud;
    [SerializeField] PlayerData playerData;
    [SerializeField] enemyHUD enemyhud;
    [SerializeField] enemyUnit enemyunit;
    [SerializeField] playerUnit playerUnit;
    [SerializeField] BattleDialogScript dialogScript;

    List<moveBase> selectedMoves;
    [SerializeField] List<Button> moveButtons;
    BattleState state;

    public bool isBossFight;

    public event Action<bool> battleEnded;

    public event Action playerRunaway;

    public void StartBattle()
    {
        state = BattleState.Start;
        StartCoroutine(setup());
    }

    public IEnumerator setup()
    {
        //MobBase mobBase = enemyunit.mobBase;
        playerData.ResetHealth();
        playerhud.setupHUD(playerData);
        enemyhud.setupHUD(enemyunit);
        enemyunit.setup();
        isBossFight = enemyunit.mobBase.IsBoss;
        playerUnit.unitSetup();
        selectedMoves = playerData.pickMoves(4);
        dialogScript.SetMoves(selectedMoves);
        AssignButtonInput(selectedMoves);
        if (isBossFight)
        {
            yield return dialogScript.typeDialog($"{enemyunit.mobBase.MobName} thinks this battle is going to end in a split second... ");
        }
        else
        {
            yield return dialogScript.typeDialog($"A wild {enemyunit.mobBase.MobName} has appeared");
        }       
        yield return new WaitForSeconds(2);
        state = BattleState.PlayerAction;
        playerAction();
    }

    public void playerAction()
    {
        if (state == BattleState.PlayerAction)
        {
            StartCoroutine(dialogScript.typeDialog("Choose an Action"));
            dialogScript.EnableActionSelector(true);
        }
    }

    public void playerMove()
    {
        state = BattleState.PlayerMove;
        dialogScript.EnableDialog(false);
        dialogScript.EnableActionSelector(false);
        dialogScript.EnableMoveSelector(true);
    }

    public IEnumerator onMoveSelected(moveBase move)
    {
        //apply damage first
        dialogScript.EnableMoveSelector(false);
        dialogScript.EnableDialog(true);
        if(UnityEngine.Random.Range(1, 100) < move.MoveAccuracy)
        {
            yield return dialogScript.typeDialog($"Player used {move.MoveName}!!");
            playerUnit.playAttackAnimation();
            bool fainted = enemyunit.mobBase.takeDamage(move);
            enemyunit.playHitAnimation();
            enemyhud.updateDHP();
            yield return new WaitForSeconds(2f);
            if (!fainted)
            {
                state = BattleState.EnemyMove;
                StartCoroutine(enemyTurn());
            }
            else
            {
                if (isBossFight)
                {
                    yield return dialogScript.typeDialog($"{enemyunit.mobBase.MobName} couldn't believe what just happened as they perish... ");
                }
                else
                {
                    yield return dialogScript.typeDialog($"{enemyunit.mobBase.MobName} cries as the player slaughters them...");
                }

                enemyunit.playFaintAnimation();
                yield return new WaitForSeconds(1f);
                battleEnded?.Invoke(true);
                Debug.Log("Battle Ended!!");
                //end the game
            }
        }
        else
        {
            yield return dialogScript.typeDialog($"Player used {move.MoveName} and it missed!!");
            yield return new WaitForSeconds(1f);
            state = BattleState.EnemyMove;
            StartCoroutine(enemyTurn());
        }
    }

    public IEnumerator playerHeal()
    {
        playerData.heal();
        dialogScript.EnableMoveSelector(false);
        dialogScript.EnableDialog(true);
        playerhud.updateIHP();
        StartCoroutine(dialogScript.typeDialog("Player healed!!"));
        yield return new WaitForSeconds(3f);
        state = BattleState.EnemyMove;
        StartCoroutine(enemyTurn());
    }

    public IEnumerator enemyTurn()
    {
        yield return dialogScript.typeDialog($"It's {enemyunit.mobBase.MobName}'s turn");
        yield return new WaitForSeconds(1f);

        if (enemyunit.mobBase.CanHeal && enemyunit.mobBase.CurrentHealth < 0.4f * enemyunit.mobBase.Health && UnityEngine.Random.Range(1, 10) > 6 )
        {
            Debug.Log("enemy heals");
            yield return enemyHeal();
        }
        else
        {
            bool fainted = EnemyAttack(enemyunit.mobBase.Moves);

            yield return new WaitForSeconds(2f);

            if (fainted)
            {
                //end the game
                yield return dialogScript.typeDialog("Player couldn't fight till the end.");
                playerUnit.playFaintAnimation();
                if (isBossFight)
                {
                    yield return dialogScript.typeDialog($"{enemyunit.mobBase.MobName} mocks you for being too weak!! ");
                }
                else
                {
                    yield return dialogScript.typeDialog($"{enemyunit.mobBase.MobName} disappears into the darkness...");
                }
                battleEnded?.Invoke(false);
                Debug.Log("Battle Ended!!");
            }
            else
            {
                state = BattleState.PlayerAction;
                playerAction();
            }
        }    
    }

    public void playerAttack()
    {
        //When player picks attack 
        playerMove();
    }

    public void playerRun()
    {
        //When player wants to run away
        StartCoroutine(playerRunEnum());
    }

    public void Concentration()
    {
        Button healButton = GetComponent<Button>();
        if (state == BattleState.PlayerMove)
        {
            if (playerData.CurrentHealth >= playerData.Health)
            {

            }
            else
            {
                StartCoroutine(playerHeal());
            }
        } 
    }

    public bool EnemyAttack(List<moveBase> enemyMoves)
    {
        int currentIndex = UnityEngine.Random.Range(0, enemyMoves.Count);
        StartCoroutine(dialogScript.typeDialog($"{enemyunit.mobBase.MobName} has used {enemyMoves[currentIndex].MoveName}"));
        bool fainted = playerData.takeDamage(enemyMoves[currentIndex]);
        enemyunit.playAttackAnimation();
        playerUnit.playHitAnimation();
        playerhud.updateDHP();
        return fainted;
    }
    public IEnumerator enemyHeal()
    {
        yield return dialogScript.typeDialog($"{enemyunit.mobBase.MobName} copies your Inner Peace ability and uses it!!");
        yield return new WaitForSeconds(1f);
        enemyunit.mobBase.heal();
        enemyhud.updateIHP();
        yield return new WaitForSeconds(1f);

        state = BattleState.PlayerAction;
        playerAction();
    }

    public void AssignButtonInput(List<moveBase> moves)
    {
        for (int i = 0; i < moveButtons.Count; i++)
        {
            Button button = moveButtons[i];
            if( i < moves.Count)
            {
                moveBase move = moves[i];
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => StartCoroutine(onMoveSelected(move)));
                button.interactable = true;
            }
            else
            {
                button.onClick.RemoveAllListeners();
                button.interactable = false;
            }
        }
    }

    public IEnumerator playerRunEnum()
    {
        dialogScript.EnableActionSelector(false);
        yield return dialogScript.typeDialog("Player is too weak to fight so he runs away...");
        yield return new WaitForSeconds(1f);
        yield return dialogScript.typeDialog($"{enemyunit.mobBase.MobName} laughs at the player...");
        yield return new WaitForSeconds(1f);
        playerRunaway?.Invoke();
        Debug.Log("Player RUns away");
    }

}
