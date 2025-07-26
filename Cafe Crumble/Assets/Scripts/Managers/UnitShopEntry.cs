using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Diagnostics;

public class UnitShopEntry : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private Button buyButton;

    private BaseUnitScript unitData;

    public void Initialize(BaseUnitScript unit)
    {
        unitData = unit;

        nameText.text = unit.name;
        healthText.text = "HP: " + unit.currentHealthPoints;
        attackText.text = "ATK: " + unit.currentAttackDamage;
        costText.text = unit.unitCost + " ";

        // If you want a click handler:
        buyButton.onClick.AddListener(() => PurchaseUnit(unit));
    }

    private void PurchaseUnit(BaseUnitScript unit)
    {
        UnityEngine.Debug.Log("Purchased " + unit.name + " for " + unit.unitCost + " ");
        // Add unit to player's bench here
    }
}
