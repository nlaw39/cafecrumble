using UnityEngine;
using TMPro;
using System.Diagnostics;
using System.Collections.Generic;

public class BaseUnitScript : MonoBehaviour
{
    public int baseHealthPoints = 5;
    public int baseAttackDamage = 5;

    public int currentHealthPoints;
    public int currentAttackDamage;

    protected int healthGrowth = 1;
    protected int attackGrowth = 2;

    public int unitCost;

    [SerializeField]
    private TMP_Text healthText;
    [SerializeField]
    private TMP_Text attackText;

    public int linePosition;

    public List<PassiveAbility> passives;

    public UnitLevel level;

    public enum UnitLevel
    { 
        Level1 = 1,
        Level2 = 2,
        Level3 = 3
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        int currentLevel = (int)level;
        currentHealthPoints = baseHealthPoints + ((currentLevel - 1) * healthGrowth);
        currentAttackDamage = baseAttackDamage + ((currentLevel - 1) * attackGrowth);

        UpdateHealthValue(0);
        UpdateAttackValue(0);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    // Attack the given target using the current attack value of this unit
    public void Attack(BaseUnitScript target)
    {
        if (target == null)
        {
            UnityEngine.Debug.Log("Target for attack not found");
        } 
        else
        {
            target.UpdateHealthValue(-currentAttackDamage);
            foreach(PassiveAbility passive in  passives)
            {
                passive.OnTakeDamage(this, target);
            }
        }
    }

    // Update this unit's HP (taking damage, being increased/healed)
    public void UpdateHealthValue(int change)
    {
        currentHealthPoints += change;
        healthText.text = "" + currentHealthPoints;
    }

    // Update this unit's ATK (stat increases/decreases)
    public void UpdateAttackValue(int change)
    {
        currentAttackDamage += change;
        attackText.text = "" + currentAttackDamage;
    }

    public List<PassiveAbility> GetPassives()
    {
        return passives;
    }
}
