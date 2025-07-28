using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CombatSceneManager : MonoBehaviour
{
    // assign in Inspector to an empty GameObject positioned near screen bottom
    public Transform allyFormationTransform;
    // The GameObject holding units carried over from shop
    public GameObject allyUnitsHolder; 

    private void Start()
    {
        allyUnitsHolder = GameObject.FindGameObjectWithTag("AllyUnits");

        // To enable selection scripts on all units
        foreach (Transform unit in allyUnitsHolder.transform)
        {
            UnitSelection unitSelectionScript = unit.GetComponent<UnitSelection>();
            if (unitSelectionScript != null)
            {
                unitSelectionScript.enabled = true; // Enable when entering combat scene
            }
        }


        if (allyUnitsHolder == null || allyFormationTransform == null)
        {
            UnityEngine.Debug.LogError("CombatSceneManager is missing references.");
            return;
        }

        List<Transform> allyUnits = new List<Transform>();

        // Grab all unit transforms from the AllyUnits holder
        foreach (Transform unit in allyUnitsHolder.transform)
        {
            Canvas canvas = unit.GetComponentInChildren<Canvas>(true);
            if (canvas != null)
            {
                canvas.gameObject.SetActive(true);
            }
            allyUnits.Add(unit);
        }

        ArrangeUnitsInRow(allyUnits, allyFormationTransform);
    }

    private void ArrangeUnitsInRow(List<Transform> units, Transform parent)
    {
        // adjust based on unit width
        float spacing = 3.0f; 
        float startX = -((units.Count - 1) * spacing) / 2f;

        for (int i = 0; i < units.Count; i++)
        {
            Transform unit = units[i];

            // set new parent
            unit.SetParent(parent);
            unit.localRotation = Quaternion.identity;
            unit.localScale = new Vector3(1.5f, 1.5f, 1.5f);

            // position in a line
            unit.localPosition = new Vector3(startX + i * spacing, -3.0f, 0f);
        }
    }
}
