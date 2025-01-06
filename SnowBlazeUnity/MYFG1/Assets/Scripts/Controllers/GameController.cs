using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Gamestates { Freeroam, Dialog, Fight, Cutscene}
public class GameController : MonoBehaviour
{

    public static GameController Instance { get; private set; }

    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerData playerData;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    [SerializeField] enemyUnit enemyunit;

    Gamestates gamestate;

    private ChallengerController currentChallenger;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DialogManager.Instance.onShowDialog += () =>
        {
            gamestate = Gamestates.Dialog;
        };

        DialogManager.Instance.onHideDialog += () =>
        {
            if(gamestate == Gamestates.Dialog)
                gamestate = Gamestates.Freeroam;
        };

        playerMovement.onMobEncounter += startBattle;

        battleSystem.battleEnded += endBattle;

        battleSystem.playerRunaway += stopBattle;

        playerMovement.bossInteraction += (Collider2D bossCollider) =>
        {
            gamestate = Gamestates.Cutscene;
            currentChallenger = bossCollider.GetComponent<ChallengerController>();
            StartCoroutine(startBossBattle(currentChallenger));
        };
    }

    public IEnumerator startBossBattle(ChallengerController chal)
    {
        yield return chal.BattleIntro();
        yield return new WaitForSeconds(2f);
        startBattle();
    }

    public void startBattle()
    {
        gamestate = Gamestates.Fight;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);
        battleSystem.StartBattle();
    }

    public void endBattle(bool won)
    {
        if (won) 
        {
            if (battleSystem.isBossFight)
            {
                currentChallenger.gameObject.SetActive(false);
                playerData.updateExp(5);
                playerData.AddCoins(3, 5);
            }
            else
            {
                //mob fight won so give exp and coins
                playerData.updateExp(1);
                playerData.AddCoins(1, 3);
            }
        }
        else
        {
            if (battleSystem.isBossFight)
            {
                playerData.updateExp(-1);
                playerData.DeductCoins(3);
            }
            //if lost and want to add consequences like losing coins or exp
        }
        gamestate = Gamestates.Freeroam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    public void stopBattle()
    {
        gamestate = Gamestates.Freeroam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
    }

    

    private void Update()
    {
        if(gamestate == Gamestates.Freeroam)
        {
            playerMovement.HandleUpdate();
        }
        else if(gamestate == Gamestates.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
        else if (gamestate == Gamestates.Fight)
        {

        }
        else if (gamestate == Gamestates.Cutscene)
        {

        }
    }

}