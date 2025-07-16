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

    [SerializeField]
    private List<GameObject> AllyUnits;
    [SerializeField]
    private List<GameObject> EnemyUnits;

    void Start()
    {
        AllyUnitsManager.GetComponent<BaseUnitController>().Initialize();
        EnemyUnitsManager.GetComponent<BaseUnitController>().Initialize();

        AllyUnits = AllyUnitsManager.GetComponent<BaseUnitController>().unitList;
        EnemyUnits = EnemyUnitsManager.GetComponent<BaseUnitController>().unitList;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEngine.Debug.Log("Combat initiated via space bar");
            StartCoroutine(Combat());
        }
    }

    IEnumerator Combat()
    {
        UnityEngine.Debug.Log("Entered the Combat courotine");
        while (AllyUnits.Any() && EnemyUnits.Any())
        {
            yield return Attack();
        }
    }

    // Coroutine for each team's current lead to exchange blows
    // Updates the UI for the units if they are left living after receiving the attack
    // If they die, activates the MoveUnits() coroutine to forward the next lead.
    IEnumerator Attack()
    {
        UnityEngine.Debug.Log("Units are exchanging blows");
        var allyUnitScript = AllyUnits[0].GetComponent<BaseUnitScript>();
        var enemyUnitScript = EnemyUnits[0].GetComponent<BaseUnitScript>();

        allyUnitScript.UpdateHealthValue(-enemyUnitScript.currentAttackDamage);
        enemyUnitScript.UpdateHealthValue(-allyUnitScript.currentAttackDamage);

        if (allyUnitScript.currentHealthPoints <= 0)
        {
            UnityEngine.Debug.Log("Current ally lead has died");
            yield return MoveUnitsAllies();
        }

        if (enemyUnitScript.currentHealthPoints <= 0)
        {
            UnityEngine.Debug.Log("Current enemy lead has died");
            yield return MoveUnitsEnemies();
        }

        yield return new WaitForSeconds(0.75f);
    }

    // Moves the Ally units to their new positions in line
    IEnumerator MoveUnitsAllies()
    {
        UnityEngine.Debug.Log("Moving ally units");
        AllyUnits[0].SetActive(false);
        AllyUnits.RemoveAt(0);
        foreach (var GameObject in AllyUnits)
        {
            GameObject.GetComponent<BaseUnitScript>().linePosition--;
        }

        AllyUnitsManager.GetComponent<AllyUnitController>().PlaceUnitsCombat();
        yield return new WaitForSeconds(0.1f);
    }

    // Moves the enemy units to their new positions in line
    IEnumerator MoveUnitsEnemies()
    {
        UnityEngine.Debug.Log("Moving enemy units");
        EnemyUnits[0].SetActive(false);
        EnemyUnits.RemoveAt(0);
        foreach (var GameObject in EnemyUnits)
        {
            GameObject.GetComponent<BaseUnitScript>().linePosition--;
        }

        EnemyUnitsManager.GetComponent<EnemyUnitController>().PlaceUnitsCombat();
        yield return new WaitForSeconds(0.1f);
    }
}
