using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Linq;
using UnityEngine;

public class BaseUnitController : MonoBehaviour
{
    // get list of game objects that are set as children to this and have inPlay = true, then align them in a line
    public List<GameObject> unitList;

    public Transform parent;


    // List of Units as Children to This
    // Player clicks on units in order that they want (not happening here)
    // This script reads linePosition and places them into line based on their linePositions


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize()
    {


        unitList = SortLinePositions();

        UnityEngine.Debug.Log("Sorted List of Children:");
        foreach (var go in unitList)
        {
            int pos = go.GetComponent<BaseUnitScript>().linePosition;
            UnityEngine.Debug.Log($"{go.name} -> Position {pos}");
        }

        PlaceUnitsStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> SortLinePositions()
    {
        List<GameObject> sortedLine = parent.Cast<Transform>()
            .Select(t => t.gameObject)
            .Where(go => go.GetComponent<BaseUnitScript>() != null)
            .OrderBy(go => go.GetComponent<BaseUnitScript>().linePosition)
            .ToList();

        return sortedLine;
    }

    public virtual void PlaceUnitsStart()
    {

    }

    public virtual void PlaceUnitsCombat()
    {

    }

    public void TricksterCatSwitch(List<GameObject> enemyTeam)
    {
        if (enemyTeam == null || enemyTeam.Count < 2)
        {
            UnityEngine.Debug.Log("The enemy team is not big enough to swap");
            return; // Nothing to swap
        }

        int firstIndex = 0;
        int lastIndex = enemyTeam.Count - 1;

        GameObject temp = enemyTeam[firstIndex];
        enemyTeam[firstIndex] = enemyTeam[lastIndex];
        enemyTeam[lastIndex] = temp;
        UnityEngine.Debug.Log("The enemy's lead and rear have been swapped!");

        PlaceUnitsCombat();
    }

}
