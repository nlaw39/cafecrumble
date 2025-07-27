using UnityEngine;
using TMPro;
using System.Diagnostics;
using System.Collections.Generic;

public abstract class BaseUnitScript : MonoBehaviour
{
    public UnitData unitData;

    public string unitName { get; protected set; }
    public int baseHealthPoints { get; protected set; }
    public int baseAttackDamage { get; protected set; }
    public int healthGrowth { get; protected set; }
    public int attackGrowth { get; protected set; }
    public int unitCost { get; protected set; }
    public Sprite unitSprite { get; protected set; }


    public int currentHealthPoints;
    public int currentAttackDamage;

    // kind of a band-aid fix for Disguised Cat so that it can block attacks.
    public bool isShielded = false;

    // band-aid for dressed up dog because wtf man i cant implement this rn
    public bool doubledStatChanges = false;

    [SerializeField]
    protected TMP_Text healthText;
    [SerializeField]
    protected TMP_Text attackText;

    public int linePosition;

    public List<PassiveAbility> passives;

    public UnitLevel level;

    public enum UnitLevel
    { 
        Level1 = 1,
        Level2 = 2,
        Level3 = 3
    }

    public abstract UnitData GetUnitData();

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
            if (target.isShielded)
            {
                UnityEngine.Debug.Log(target.name + " blocked the incoming attack!");
                target.isShielded = false;
            }
            else
            {
                target.UpdateHealthValue(-currentAttackDamage);
                UnityEngine.Debug.Log(target.name + " took " + currentAttackDamage + " damage from " + this.name + "'s attack");
                foreach (PassiveAbility passive in passives)
                {
                    passive.OnTakeDamage(this, target);
                }
            }
            
        }
    }

    // Update this unit's HP (taking damage, being increased/healed)
    public void UpdateHealthValue(int change)
    {
        if (!doubledStatChanges)
        {
            currentHealthPoints += change;
        } else
        {
            currentHealthPoints += 2 * change;
        }
        UpdateUIText();
    }

    public void UpdateUIText()
    {
        healthText.text = "" + currentHealthPoints;
        attackText.text = "" + currentAttackDamage;
    }

    // Update this unit's ATK (stat increases/decreases)
    public void UpdateAttackValue(int change)
    {
        if (!doubledStatChanges)
        {
            currentAttackDamage += change;
        }
        else
        {
            currentAttackDamage += 2 * change;
        }
        UpdateUIText();
    }

    public List<PassiveAbility> GetPassives()
    {
        return passives;
    }

    public void AddPassive(PassiveAbility passive)
    {
        passives.Add(passive);
        UnityEngine.Debug.Log(this.name + " gained the passive: " + passive.name);
    }
}
