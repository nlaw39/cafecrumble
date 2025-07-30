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

    private HashSet<string> purchasedUnits = new HashSet<string>();

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

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
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
        

    }

    public void StartCombatPhase()
    {
        EnemyUnitsManager = GameObject.FindGameObjectWithTag("EnemyUnits");
        EnemyUnits = EnemyUnitsManager.GetComponent<BaseUnitController>().unitList;
        foreach (var unit in AllyUnits)
        {
            foreach (Transform child in unit.transform)
            {
                GameObject canvasObj = child.gameObject;
                bool isActive = canvasObj.activeSelf;
                canvasObj.SetActive(!isActive);
            }
        }

        foreach (var unit in EnemyUnits)
        {
            foreach (Transform child in unit.transform)
            {
                GameObject canvasObj = child.gameObject;
                bool isActive = canvasObj.activeSelf;
                canvasObj.SetActive(!isActive);
            }
            unit.GetComponent<UnitSelection>().DeactivateOrderCanvas();
        }

        AllyUnitsManager.GetComponent<AllyUnitController>().PlaceUnitsStart();
        EnemyUnitsManager.GetComponent<EnemyUnitController>().PlaceUnitsStart();
        StartCoroutine(Combat());
    }

    IEnumerator Combat()
    {
        UnityEngine.Debug.Log("Entered the Combat courotine");

        GameObject combatManager = GameObject.FindGameObjectWithTag("CombatManager");
        CombatUIController combatUI = combatManager.GetComponent<CombatUIController>();

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
            combatUI.SetCafeButtonActive();
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
            combatUI.SetCafeButtonActive();
        } else if (!AllyUnits.Any())
        {
            gameState = GameState.EnemyWin;
            UnityEngine.Debug.Log("Combat resulted in Enemy Victory.");
            combatUI.SetCafeButtonActive();
        }

        BattleResults(gameState);
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
            unitScript.UpdateUIText();
            foreach (PassiveAbility passive in unitScript.GetPassives())
            {
                passive.ActivateImmediately(unitScript);
            }
        }


        // Activate all activate immediately passives for enemies
        foreach (var unit in EnemyUnits)
        {
            BaseUnitScript unitScript = unit.GetComponent<BaseUnitScript>();
            unitScript.UpdateUIText();
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
        allyStartingLead.UpdateUIText();
        enemyStartingLead.UpdateUIText();

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
        allyStartingLead.UpdateUIText();
        enemyStartingLead.UpdateUIText();

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
            unitScript.UpdateUIText();
            foreach (PassiveAbility passive in unitScript.GetPassives())
            {
                passive.OnCombatStart(AllyUnits);
            }
        }


        // Activate all combat start passives for enemies
        foreach (var unit in EnemyUnits)
        {
            BaseUnitScript unitScript = unit.GetComponent<BaseUnitScript>();
            unitScript.UpdateUIText();
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

    public void ChangeMoney(int amount)
    {
        currentMoney += amount;
    }

    public int ReturnMoney()
    {
        return currentMoney;
    }

    public void MarkUnitAsPurchased(string unitName)
    {
        purchasedUnits.Add(unitName);
    }

    public bool HasPurchasedUnit(string unitName)
    {
        return purchasedUnits.Contains(unitName);
    }

    public int NumPurchasedUnits() 
    { 
        return purchasedUnits.Count; 
    }

    public void ResetAlliedUnits()
    {
        //AllyUnitsManager = GameObject.FindGameObjectWithTag("AllyUnits");
        AllyUnitsManager.GetComponent<BaseUnitController>().ClearList();

        foreach (Transform child in AllyUnitsManager.transform)
        {
            GameObject unit = child.gameObject;
            unit.SetActive(true);

            var unitScript = unit.GetComponent<BaseUnitScript>();
            unitScript.ResetStats();

            var unitSelectScript = unit.GetComponent<UnitSelection>();
            unitSelectScript.ClearOrderNumber();

            // Move unit to random screen position so they aren't stacked on arrival
            float randomX = UnityEngine.Random.Range(0.3f, 0.7f);
            float randomY = UnityEngine.Random.Range(0.3f, 0.7f);
            Vector3 viewportPos = new Vector3(randomX, randomY, 10f); // z = 10 to be in front of camera
            Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(viewportPos);
            unit.transform.position = spawnPosition;

            foreach (Transform canvas in unit.transform)
            {
                GameObject canvasObj = canvas.gameObject;
                canvasObj.SetActive(false);
            }
        }
    }

    public void BattleResults(GameState gameState)
    {
        UnityEngine.Debug.Log("Getting battle results");
        GameObject CombatManager = GameObject.FindGameObjectWithTag("CombatManager");

        switch (gameState)
        {
            case GameState.AllyWin:
                ChangeMoney(4);
                CombatManager.GetComponent<CombatSceneManager>().SetVictoryText();
                break;
            case GameState.EnemyWin:
                ChangeMoney(0);
                CombatManager.GetComponent<CombatSceneManager>().SetDefeatText();
                break;
            case GameState.Tie:
                ChangeMoney(0);
                CombatManager.GetComponent<CombatSceneManager>().SetTieText();
                break;
        }
    }
}
