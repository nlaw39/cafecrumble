using UnityEngine;
using System.Collections.Generic;

public class CombatStartButton : MonoBehaviour
{
    public GameObject allyUnitsHolder;
    public Transform allyFormationTransform;

    public void OnStartCombatPressed()
    {
        allyUnitsHolder = GameObject.FindGameObjectWithTag("AllyUnits");
        List<GameObject> orderedUnits = UnitSelectionManager.Instance.GetOrderedUnits();
        if (orderedUnits.Count < GameManager.Instance.NumPurchasedUnits())
        {
            UnityEngine.Debug.Log("Not all units are given a position in line!");
            return;
        }

        foreach (Transform child in allyUnitsHolder.transform)
        {
            Destroy(child.gameObject); // Clear old lineup
        }

        foreach (GameObject unit in orderedUnits)
        {
            unit.transform.SetParent(allyUnitsHolder.transform);
            allyUnitsHolder.GetComponent<BaseUnitController>().AddUnitToList(unit);
        }



        // Proceed to combat phase...
        GameManager.Instance.StartCombatPhase();
    }
}
