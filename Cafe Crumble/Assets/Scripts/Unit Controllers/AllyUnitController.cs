using UnityEngine;

public class AllyUnitController : BaseUnitController
{
    private int allyUnitXOffsetStart = -2;
    private int allyUnitXOffsetCombat = -2;

    private static AllyUnitController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // prevent duplicates
            Destroy(gameObject); 
        }
    }

    public override void PlaceUnitsStart()
    {
        foreach (GameObject unit in unitList)
        {
            unit.transform.position = new Vector3(allyUnitXOffsetStart, transform.position.y,  -1);
            allyUnitXOffsetStart -= 2;
        }
        allyUnitXOffsetStart = -2;
    }

    public override void PlaceUnitsCombat()
    {
        foreach (GameObject unit in unitList)
        {
            //UnityEngine.Debug.Log("Moving " + unit.name + " in combat.");
            unit.transform.position = new Vector3(allyUnitXOffsetCombat, transform.position.y, -1);
            allyUnitXOffsetCombat -= 2;
        }
        allyUnitXOffsetCombat = -2;
    }
}
