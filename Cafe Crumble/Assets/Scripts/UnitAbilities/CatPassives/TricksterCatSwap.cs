using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TricksterCatSwap", menuName = "Scriptable Objects/TricksterCatSwap")]
public class TricksterCatSwap : PassiveAbility
{

    public override void OnTakeLead(BaseUnitScript self, BaseUnitScript target)
    {
        GameObject enemyManager = GameObject.FindWithTag("EnemyUnits");
        BaseUnitController enemyController = enemyManager.GetComponent<BaseUnitController>();
        List<GameObject> enemyUnits = enemyController.unitList;
        enemyController.TricksterCatSwitch(enemyUnits);
    }
}
