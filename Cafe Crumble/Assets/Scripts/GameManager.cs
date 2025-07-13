using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject AllyUnitsManager;
    [SerializeField]
    private GameObject EnemyUnitsManager;

    private List<GameObject> AllyUnits;
    private List<GameObject> EnemyUnits;

    void Start()
    {
        AllyUnits = AllyUnitsManager.GetComponent<BaseUnitController>().unitList;
        EnemyUnits = EnemyUnitsManager.GetComponent<BaseUnitController>().unitList;
    }

    void Update()
    {
        if (Input.GetKeyDown("Space"))
        {
            StartCoroutine(Combat());
        }
    }

    IEnumerator Combat()
    {
        while (AllyUnits.Any() && EnemyUnits.Any())
        {
            yield return Attack();
        }

        StopCoroutine(Combat());
    }

    // Coroutine for each team's current lead to exchange blows
    // Updates the UI for the units if they are left living after receiving the attack
    // If they die, activates the MoveUnits() coroutine to forward the next lead.
    IEnumerator Attack()
    {
        AllyUnits[0].GetComponent<BaseUnitScript>().UpdateHealthValue(EnemyUnits[0].GetComponent<BaseUnitScript>().currentAttackDamage);
        EnemyUnits[0].GetComponent<BaseUnitScript>().UpdateHealthValue(AllyUnits[0].GetComponent<BaseUnitScript>().currentAttackDamage);


        if (AllyUnits[0].GetComponent<BaseUnitScript>().currentHealthPoints <= 0)
        {
            yield return MoveUnitsAllies();
        }

        if (EnemyUnits[0].GetComponent<BaseUnitScript>().currentHealthPoints <= 0)
        {
            yield return MoveUnitsEnemies();
        }

        yield return new WaitForSeconds(0.75f);
    }

    // Moves the Ally units to their new positions in line
    IEnumerator MoveUnitsAllies()
    {
        AllyUnits.RemoveAt(0);
        foreach (var GameObject in AllyUnits)
        {
            GameObject.GetComponent<BaseUnitScript>().linePosition--;
        }

        AllyUnitsManager.GetComponent<AllyUnitController>().PlaceUnits();
        yield return new WaitForSeconds(0.1f);
    }

    // Moves the enemy units to their new positions in line
    IEnumerator MoveUnitsEnemies()
    {
        EnemyUnits.RemoveAt(0);
        foreach (var GameObject in EnemyUnits)
        {
            GameObject.GetComponent<BaseUnitScript>().linePosition--;
        }

        EnemyUnitsManager.GetComponent<EnemyUnitController>().PlaceUnits();
        yield return new WaitForSeconds(0.1f);
    }
}
