using UnityEngine;
using System.Collections.Generic;
using System;
using System.Diagnostics;

public class EnemyGroupManager : MonoBehaviour
{
    public GameObject catStage1Group;
    public GameObject catStage2Group;
    public GameObject catStage3Group;

    public GameObject dogStage1Group;
    public GameObject dogStage2Group;
    public GameObject dogStage3Group;

    public GameObject capybaraStage1Group;
    public GameObject capybaraStage2Group;
    public GameObject capybaraStage3Group;

    public GameObject hedgehogStage1Group;
    public GameObject hedgehogStage2Group;
    public GameObject hedgehogStage3Group;

    public Transform spawnPoint;

    void Start()
    {
        Stage stage = GameManager.Instance.currentStage;
        Faction playerFaction = FactionManager.Instance.SelectedFaction;

        // generate a list of factions that does not include the player's
        List<Faction> enemyFactions = new List<Faction> { Faction.Cats, Faction.Dogs, Faction.Capybaras, Faction.Hedgehogs };
        enemyFactions.Remove(playerFaction);

        // pick a random enemy faction
        Faction enemyFaction = enemyFactions[UnityEngine.Random.Range(0, enemyFactions.Count)];

        GameObject groupToSpawn = null;

        switch (enemyFaction)
        {
            case Faction.Cats:
                if (stage == Stage.Stage1) groupToSpawn = catStage1Group;
                else if (stage == Stage.Stage2) groupToSpawn = catStage2Group;
                else if (stage == Stage.Stage3) groupToSpawn = catStage3Group;
                break;

            case Faction.Dogs:
                if (stage == Stage.Stage1) groupToSpawn = dogStage1Group;
                else if (stage == Stage.Stage2) groupToSpawn = dogStage2Group;
                else if (stage == Stage.Stage3) groupToSpawn = dogStage3Group;
                break;

            case Faction.Capybaras:
                if (stage == Stage.Stage1) groupToSpawn = capybaraStage1Group;
                else if (stage == Stage.Stage2) groupToSpawn = capybaraStage2Group;
                else if (stage == Stage.Stage3) groupToSpawn = capybaraStage3Group;
                break;

            case Faction.Hedgehogs:
                if (stage == Stage.Stage1) groupToSpawn = hedgehogStage1Group;
                else if (stage == Stage.Stage2) groupToSpawn = hedgehogStage2Group;
                else if (stage == Stage.Stage3) groupToSpawn = hedgehogStage3Group;
                break;
        }

        if (groupToSpawn != null)
        {
            Instantiate(groupToSpawn, new Vector3(0f, 0f, -10f), Quaternion.identity);
            groupToSpawn.GetComponent<EnemyUnitController>().PlaceUnitsStart();
        }
        else
        {
            UnityEngine.Debug.LogError("Failed to select enemy group prefab!");
        }
    }
}
