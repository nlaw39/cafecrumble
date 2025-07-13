using UnityEngine;

public class AllyUnitController : BaseUnitController
{
    private int allyUnitXOffset = -2;

    protected override void PlaceUnits()
    {
        foreach (GameObject unit in unitList)
        {
            UnityEngine.Debug.Log("Placing " + unit.name);
            unit.transform.position = new Vector3(allyUnitXOffset, transform.position.y,  -1);
            allyUnitXOffset -= 2;
        }
        UnityEngine.Debug.Log("Finished placing ally units");
    }
}
