using UnityEngine;

public class IdolCapybaraScript : BaseUnitScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        baseHealthPoints = 5;
        baseAttackDamage = 0;

        healthGrowth = 2;
        attackGrowth = 0;

        unitCost = 3;

        PassiveAbility passive = Resources.Load<PassiveAbility>("UnitAbilities/PassiveAssets/SacrificialCapybaraPassive");
        AddPassive(Instantiate(passive));
        base.Start();
    }
}
