using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private GameObject AllyUnitsManager;
    [SerializeField]
    private GameObject EnemyUnitsManager;

    [SerializeField]
    private List<GameObject> AllyUnits;
    [SerializeField]
    private List<GameObject> EnemyUnits;

    private GameState gameState;

    [SerializeField]
    private int currentMoney;

    public enum GameState
    { 
        Undecided,
        AllyWin,
        EnemyWin,
        Tie
    }



    void Start()
    {
        Faction playerFaction = FactionManager.Instance.SelectedFaction;

        switch (playerFaction)
        {
            case Faction.Cats:
                UnityEngine.Debug.Log("Successfully loaded into Cats");
                break;
            case Faction.Dogs:
                UnityEngine.Debug.Log("Successfully loaded into Dogs");
                break;
            case Faction.Capybaras:
                UnityEngine.Debug.Log("Successfully loaded into Capybaras");
                break;
            case Faction.Hedgehogs:
                UnityEngine.Debug.Log("Successfully loaded into Hedgehogs");
                break;
        }

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

    public void StartCombatPhase()
    {
        StartCoroutine(Combat());
    }

    IEnumerator Combat()
    {
        UnityEngine.Debug.Log("Entered the Combat courotine");

        ActivateImmediatePassives();

        ActivateOnCombatStartPassives();

        ActivateOnLeadPassives();

        yield return new WaitForSeconds(1.0f);

        

        yield return new WaitForSeconds(0.5f);

        while (AllyUnits.Any() && EnemyUnits.Any())
        {
            gameState = GameState.Undecided;
            yield return Attack();
        }

        if (!AllyUnits.Any() && !EnemyUnits.Any())
        {
            gameState = GameState.Tie;
            UnityEngine.Debug.Log("Combat was a tie.");
        } else if (!EnemyUnits.Any())
        {
            foreach (var unit in AllyUnits)
            {
                BaseUnitScript unitScript = unit.GetComponent<BaseUnitScript>();
                foreach (PassiveAbility passive in unitScript.GetPassives())
                { 
                    passive.OnCombatEnd(unitScript);
                }
            }
            gameState = GameState.AllyWin;
            UnityEngine.Debug.Log("Combat resulted in Ally Victory.");
        } else if (!AllyUnits.Any())
        {
            gameState = GameState.EnemyWin;
            UnityEngine.Debug.Log("Combat resulted in Enemy Victory.");
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

        allyUnitScript.Attack(enemyUnitScript);
        enemyUnitScript.Attack(allyUnitScript);


        CheckDeadAlly(allyUnitScript);
        CheckDeadEnemy(enemyUnitScript);

        
        yield return new WaitForSeconds(1.5f);
    }

    // Moves the Ally units to their new positions in line
    IEnumerator MoveUnitsAllies()
    {
        UnityEngine.Debug.Log("Moving ally units");
        AllyUnits[0].SetActive(false);
        AllyUnits.RemoveAt(0);

        yield return new WaitForSeconds(0.5f);

        if (AllyUnits.Any())
        {
            foreach (var GameObject in AllyUnits)
            {
                GameObject.GetComponent<BaseUnitScript>().linePosition--;
            }

            AllyUnitsManager.GetComponent<AllyUnitController>().PlaceUnitsCombat();
            ActivateOnLeadAlly();
        }

        
        yield return new WaitForSeconds(0.5f);
    }

    // Moves the enemy units to their new positions in line
    IEnumerator MoveUnitsEnemies()
    {
        UnityEngine.Debug.Log("Moving enemy units");
        EnemyUnits[0].SetActive(false);
        EnemyUnits.RemoveAt(0);

        yield return new WaitForSeconds(0.5f);

        if (EnemyUnits.Any())
        {
            foreach (var GameObject in EnemyUnits)
            {
                GameObject.GetComponent<BaseUnitScript>().linePosition--;
            }

            EnemyUnitsManager.GetComponent<EnemyUnitController>().PlaceUnitsCombat();

            ActivateOnLeadEnemy();
        }

        
        yield return new WaitForSeconds(0.5f);
    }

    private void ActivateImmediatePassives()
    {
        // Activate all activate immediately passives for allies
        foreach (var unit in AllyUnits)
        {
            BaseUnitScript unitScript = unit.GetComponent<BaseUnitScript>();
            foreach (PassiveAbility passive in unitScript.GetPassives())
            {
                passive.ActivateImmediately(unitScript);
            }
        }


        // Activate all activate immediately passives for enemies
        foreach (var unit in EnemyUnits)
        {
            BaseUnitScript unitScript = unit.GetComponent<BaseUnitScript>();
            foreach (PassiveAbility passive in unitScript.GetPassives())
            {
                passive.ActivateImmediately(unitScript);
            }
        }
    }

    // Activate all OnTakeLead passives for ally and enemy lead
    private void ActivateOnLeadPassives()
    {
        ActivateOnLeadAlly();
        ActivateOnLeadEnemy();
    }

    private void ActivateOnLeadAlly()
    {
        UnityEngine.Debug.Log("I made it to ActivateOnLeadAlly");
        BaseUnitScript allyStartingLead = AllyUnits[0].GetComponent<BaseUnitScript>();
        BaseUnitScript enemyStartingLead = EnemyUnits[0].GetComponent<BaseUnitScript>();
        UnityEngine.Debug.Log(allyStartingLead.name + " is the current lead for allies");
        foreach (PassiveAbility passive in allyStartingLead.GetPassives())
        {
            passive.OnTakeLead(allyStartingLead, enemyStartingLead);
        }

        // Checking in case a passive activated on new lead resulted in killing opposing lead (brainwash hedgehog)
        var enemyUnitScript = EnemyUnits[0].GetComponent<BaseUnitScript>();
        CheckDeadEnemy(enemyUnitScript);
    }

    private void ActivateOnLeadEnemy()
    {
        BaseUnitScript allyStartingLead = AllyUnits[0].GetComponent<BaseUnitScript>();
        BaseUnitScript enemyStartingLead = EnemyUnits[0].GetComponent<BaseUnitScript>();
        foreach (PassiveAbility passive in enemyStartingLead.GetPassives())
        {
            passive.OnTakeLead(enemyStartingLead, allyStartingLead);
        }

        // Checking in case a passive activated on new lead resulted in killing opposing lead (brainwash hedgehog)
        var allyUnitScript = AllyUnits[0].GetComponent<BaseUnitScript>();
        CheckDeadAlly(allyUnitScript);
    }

    private void ActivateOnCombatStartPassives()
    {
        // Activate all combat start passives for allies
        foreach (var unit in AllyUnits)
        {
            BaseUnitScript unitScript = unit.GetComponent<BaseUnitScript>();
            foreach (PassiveAbility passive in unitScript.GetPassives())
            {
                passive.OnCombatStart(AllyUnits);
            }
        }


        // Activate all combat start passives for enemies
        foreach (var unit in EnemyUnits)
        {
            BaseUnitScript unitScript = unit.GetComponent<BaseUnitScript>();
            foreach (PassiveAbility passive in unitScript.GetPassives())
            {
                passive.OnCombatStart(EnemyUnits);
            }
        }
    }

    private void CheckDeadAlly(BaseUnitScript allyUnit)
    {
        if (allyUnit.currentHealthPoints <= 0)
        {
            UnityEngine.Debug.Log("Current ally lead has died");
            foreach (PassiveAbility passive in allyUnit.GetPassives())
            {
                passive.OnDeath(allyUnit);
            }
            StartCoroutine(MoveUnitsAllies());
        }
    }

    private void CheckDeadEnemy(BaseUnitScript enemyUnit)
    {
        if (enemyUnit.currentHealthPoints <= 0)
        {
            UnityEngine.Debug.Log("Current enemy lead has died");
            foreach (PassiveAbility passive in enemyUnit.GetPassives())
            {
                passive.OnDeath(enemyUnit);
            }
            StartCoroutine(MoveUnitsEnemies());
        }

    }

    public void changeMoney(int amount)
    {
        currentMoney += amount;
    }
}
