using UnityEngine;

public class EnemyUnitController : BaseUnitController
{
    private int enemyUnitXOffsetStart = 2;
    private int enemyUnitXOffsetCombat = 2;

    public override void PlaceUnitsStart()
    {
        foreach (GameObject unit in unitList)
        {
            UnityEngine.Debug.Log("Placing " + unit.name);
            unit.transform.position = new Vector3(enemyUnitXOffsetStart, transform.position.y, -1);
            enemyUnitXOffsetStart += 2;
        }
        UnityEngine.Debug.Log("Finished placing enemy units");
    }

    public override void PlaceUnitsCombat()
    {
        foreach (GameObject unit in unitList)
        {
            UnityEngine.Debug.Log("Moving " + unit.name + " in combat.");
            unit.transform.position = new Vector3(enemyUnitXOffsetCombat, transform.position.y, -1);
            enemyUnitXOffsetCombat += 2;
        }
        enemyUnitXOffsetCombat = 2;
        UnityEngine.Debug.Log("Moved enemy units during combat");
    }
}
