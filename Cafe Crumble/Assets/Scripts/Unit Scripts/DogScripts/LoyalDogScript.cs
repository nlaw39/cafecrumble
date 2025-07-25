using UnityEngine;
using TMPro;

public class LoyalDogScript : BaseUnitScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        baseHealthPoints = 3;
        baseAttackDamage = 2;

        healthGrowth = 0;
        attackGrowth = 1;

        unitCost = 2;

        PassiveAbility passive = Resources.Load<PassiveAbility>("UnitAbilities/PassiveAssets/LoyalDogPassive");
        AddPassive(Instantiate(passive));

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
