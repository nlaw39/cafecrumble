using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopUIController : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Button openShopButton;
    [SerializeField] private Button closeShopButton;
    [SerializeField] private TMP_Text moneyText;

    [SerializeField] private Button goToCombatButton;

    [SerializeField] private Transform unitContainer;
    [SerializeField] private GameObject unitEntryPrefab;

    [SerializeField] private UnitDatabase unitDatabase;

    private Faction currentFaction;
    private List<GameObject> factionUnits;


    void Start()
    {
        SetFaction(FactionManager.Instance.SelectedFaction);

        shopPanel.SetActive(false);

        openShopButton.onClick.AddListener(OpenShop);
        closeShopButton.onClick.AddListener(CloseShop);
        goToCombatButton.onClick.AddListener(GoToCombatScene);

        UpdateMoneyDisplay();

        factionUnits = unitDatabase.GetUnitsForFaction(currentFaction);
    }

    void OpenShop()
    {
        shopPanel.SetActive(true);
        PopulateShop();
    }

    void CloseShop()
    {
        shopPanel.SetActive(false);
    }

    void GoToCombatScene()
    {
        GameObject AllyUnitsManager = GameObject.FindGameObjectWithTag("AllyUnits");
        if (AllyUnitsManager.transform.childCount < 1)
        {
            UnityEngine.Debug.Log("No units owned, cannot go to combat!");
            return;
        } else
        {
            UnityEngine.Debug.Log("Going to combat scene");
            SceneManager.LoadScene("CombatScene");
        }
    }

    private void PopulateShop()
    {
        // Clear existing buttons
        foreach (Transform child in unitContainer)
        {
            Destroy(child.gameObject);
        }


        foreach (GameObject unit in factionUnits)
        {
            BaseUnitScript unitScript = unit.GetComponent<BaseUnitScript>();
            GameObject entry = Instantiate(unitEntryPrefab, unitContainer);
            UnitShopEntry entryScript = entry.GetComponent<UnitShopEntry>();
            entryScript.Initialize(unitScript.GetUnitData());
        }
    }

    public void SetFaction(Faction faction)
    {
        currentFaction = faction;
    }

    public void UpdateMoneyDisplay()
    {
        moneyText.text = "" + GameManager.Instance.ReturnMoney();
    }
}
