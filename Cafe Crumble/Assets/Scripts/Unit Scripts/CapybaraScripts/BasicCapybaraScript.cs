using UnityEngine;

public class BasicCapybaraScript : BaseUnitScript
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        baseHealthPoints = 5;
        baseAttackDamage = 1;

        healthGrowth = 2;
        attackGrowth = 0;

        unitCost = 1;
    }

}
