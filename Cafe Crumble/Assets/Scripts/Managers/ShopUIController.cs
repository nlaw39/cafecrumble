using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIController : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Button openShopButton;
    [SerializeField] private Button closeShopButton;

    [SerializeField] private Transform unitContainer;
    [SerializeField] private GameObject unitEntryPrefab;

    private Faction currentFaction;
    private List<BaseUnitScript> factionUnits;

    void Start()
    {
        shopPanel.SetActive(false);

        openShopButton.onClick.AddListener(OpenShop);
        closeShopButton.onClick.AddListener(CloseShop);
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

    private void PopulateShop()
    {
        // Clear existing buttons
        foreach (Transform child in unitContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (BaseUnitScript unit in factionUnits)
        {
            GameObject entry = Instantiate(unitEntryPrefab, unitContainer);
            UnitShopEntry entryScript = entry.GetComponent<UnitShopEntry>();
            entryScript.Initialize(unit);
        }
    }

    public void SetFaction(Faction faction)
    {
        currentFaction = faction;
    }
}
