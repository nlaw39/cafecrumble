using UnityEngine;

public class EnemyUnitController : BaseUnitController
{
    private int enemyUnitXOffset = -2;

    public override void PlaceUnits()
    {
        foreach (GameObject unit in unitList)
        {
            UnityEngine.Debug.Log("Placing " + unit.name);
            unit.transform.position = new Vector3(enemyUnitXOffset, transform.position.y, -1);
            enemyUnitXOffset -= 2;
        }
        UnityEngine.Debug.Log("Finished placing enemy units");
    }
}
