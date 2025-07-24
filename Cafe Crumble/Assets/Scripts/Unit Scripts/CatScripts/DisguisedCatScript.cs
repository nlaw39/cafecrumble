using UnityEngine;
using TMPro;

public class DisguisedCatScript : BaseUnitScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        unitName = "Disguised Cat";

        baseHealthPoints = 1;
        baseAttackDamage = 3;

        healthGrowth = 1;
        attackGrowth = 2;

        unitCost = 2;

        PassiveAbility passive = Resources.Load<PassiveAbility>("UnitAbilities/PassiveAssets/DisguisedCatPassive");
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
