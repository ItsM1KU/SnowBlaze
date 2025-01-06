using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobController : MonoBehaviour
{
    public List<MobBase> mobs;

    [SerializeField] enemyUnit enemyUnit;

    public void SpawnMob()
    {
        int currentIndex = Random.Range(0, mobs.Count);
        
        enemyUnit.assignMob(mobs[currentIndex]);
        
    }
}
