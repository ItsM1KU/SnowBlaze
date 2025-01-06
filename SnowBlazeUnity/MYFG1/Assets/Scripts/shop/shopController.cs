using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopController : MonoBehaviour
{
    [SerializeField] GameObject buyMenu;
    [SerializeField] GameObject upgradeMenu;

    [SerializeField] Transform upgradeListPos;
    [SerializeField] GameObject moveItemPrefab;


    [SerializeField] PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        buyMenu.SetActive(true);
        upgradeMenu.SetActive(false);
    }



    public void displayBuyMenu()
    {
        buyMenu.SetActive(true);
        upgradeMenu.SetActive(false);
    }

    public void displayUpgradeMenu() {
        buyMenu.SetActive(false);
        upgradeMenu.SetActive(true);
        setupUpgradeList();
    }

    public void setupUpgradeList()
    {
        foreach ( var move in playerData.moves)
        {
            GameObject item = Instantiate(moveItemPrefab, upgradeListPos);
            item.GetComponent<upgradeItem>().setup(move);
        }
    }
}
