using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance;

    public List<UnitSelection> selectedUnits = new List<UnitSelection>();

    private void Awake()
    {
        Instance = this;
    }

    public void ToggleUnit(UnitSelection unit)
    {
        if (selectedUnits.Contains(unit))
        {
            // Deselect and update all remaining
            selectedUnits.Remove(unit);
            unit.ClearOrderNumber();
            UpdateUnitOrder();
        }
        else
        {
            // Add new selection
            selectedUnits.Add(unit);
            unit.SetOrderNumber(selectedUnits.Count);
        }
    }

    private void UpdateUnitOrder()
    {
        for (int i = 0; i < selectedUnits.Count; i++)
        {
            selectedUnits[i].SetOrderNumber(i + 1);
        }
    }

    public bool AllUnitsSelected(int totalUnits)
    {
        return selectedUnits.Count == totalUnits;
    }

    public List<GameObject> GetOrderedUnits()
    {
        List<GameObject> result = new List<GameObject>();
        foreach (var unit in selectedUnits)
        {
            result.Add(unit.gameObject);
        }
        return result;
    }
}
