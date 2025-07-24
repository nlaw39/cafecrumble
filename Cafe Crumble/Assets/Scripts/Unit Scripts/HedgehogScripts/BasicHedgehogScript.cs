using UnityEngine;
using TMPro;

public class BasicHedgehogScript : BaseUnitScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        unitName = "Basic Hedgehog";

        baseHealthPoints = 5;
        baseAttackDamage = 1;

        healthGrowth = 1;
        attackGrowth = 0;

        unitCost = 1;

        Transform child = transform.Find("Canvas/HealthText");
        if (child != null)
        {
            TMP_Text tmp = child.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                healthText = tmp;
            }
        }

        Transform child2 = transform.Find("Canvas/AttackText");
        if (child2 != null)
        {
            TMP_Text tmp2 = child2.GetComponent<TextMeshProUGUI>();
            if (tmp2 != null)
            {
                attackText = tmp2;
            }
        }

        base.Start();
    }
}
