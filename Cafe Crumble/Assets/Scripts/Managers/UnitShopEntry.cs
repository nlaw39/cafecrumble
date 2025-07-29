using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Diagnostics;
using System;

public class UnitShopEntry : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text attackText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private Button buyButton;


    public void Initialize(UnitData unit)
    {
        iconImage.sprite = unit.unitSprite;
        nameText.text = unit.unitName;
        healthText.text = "" + unit.baseHealthPoints;
        attackText.text = "" + unit.baseAttackDamage;
        costText.text = " " + unit.unitCost;
        
        buyButton.onClick.AddListener(() => PurchaseUnit(unit));

        if (GameManager.Instance.HasPurchasedUnit(unit.unitName))
        {
            buyButton.interactable = false;
        }
        else
        {
            buyButton.interactable = true;
        }
    }

    private void PurchaseUnit(UnitData unit)
    {
        if (GameManager.Instance.HasPurchasedUnit(unit.unitName))
        {
            return;
        }

        if (unit.unitCost > GameManager.Instance.ReturnMoney())
        {
            UnityEngine.Debug.Log("Not enough money to purchase this unit!");
            return;
        }
            
        UnityEngine.Debug.Log("Purchased " + unit.unitName + " for " + unit.unitCost + " ");

        if (unit.unitPrefab != null)
        {
            GameObject allyHolder = GameObject.FindGameObjectWithTag("AllyUnits");

            // Random point in screen space (between 30% and 70% of the screen to avoid edges)
            float randomX = UnityEngine.Random.Range(0.3f, 0.7f);
            float randomY = UnityEngine.Random.Range(0.3f, 0.7f);
            Vector3 viewportPos = new Vector3(randomX, randomY, 10f); // z = 10 to be in front of the camera

            // Convert to world position
            Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(viewportPos);

            // Instantiate the unit at that world position
            GameObject newUnit = Instantiate(unit.unitPrefab, spawnPosition, Quaternion.identity, allyHolder.transform);

            GameManager.Instance.MarkUnitAsPurchased(unit.unitName);

            GameManager.Instance.ChangeMoney(-unit.unitCost);

            GameObject shopManagerObject = GameObject.FindGameObjectWithTag("ShopManager");
            ShopUIController shopManagerScript = shopManagerObject.GetComponent<ShopUIController>();
            shopManagerScript.UpdateMoneyDisplay();
        }
        else
        {
            UnityEngine.Debug.LogError("Prefab missing for unit: " + unit.unitName);
        }
    }
}
