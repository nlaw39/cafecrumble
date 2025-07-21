using UnityEngine;

public class SacrificialCapybaraScript : BaseUnitScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        baseHealthPoints = 4;
        baseAttackDamage = 1;

        healthGrowth = 1;
        attackGrowth = 0;

        unitCost = 2;

        PassiveAbility passive = Resources.Load<PassiveAbility>("UnitAbilities/PassiveAssets/SacrificialCapybaraPassive");
        AddPassive(Instantiate(passive));
        base.Start();
    }
}
