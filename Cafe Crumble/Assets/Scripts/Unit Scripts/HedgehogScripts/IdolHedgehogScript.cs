using UnityEngine;
using TMPro;

public class IdolHedgehogScript : BaseUnitScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        unitName = "Hatsune Prick-u";

        baseHealthPoints = 4;
        baseAttackDamage = 0;

        healthGrowth = 2;
        attackGrowth = 0;

        unitCost = 3;

        PassiveAbility passive = Resources.Load<PassiveAbility>("UnitAbilities/PassiveAssets/IdolCatAura");
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
