using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdolCatAura", menuName = "Scriptable Objects/IdolCatAura")]
public class IdolCatAura : PassiveAbility
{
    private List<BaseUnitScript> buffedAllies = new List<BaseUnitScript>();

    public override void OnCombatStart(List<GameObject> AllyUnits)
    {
        buffedAllies.Clear();

        foreach (GameObject ally in AllyUnits)
        {
            BaseUnitScript allyScript = ally.GetComponent<BaseUnitScript>();
            if (allyScript != null)
            {
                allyScript.UpdateAttackValue(1);
                buffedAllies.Add(allyScript);
                UnityEngine.Debug.Log(ally.name + " was buffed by " + this.name);
            }
        }
    }


    public override void OnDeath(BaseUnitScript self)
    {
        foreach (BaseUnitScript ally in buffedAllies)
        {
            if (ally != null)
            {
                ally.UpdateAttackValue(-1);
                UnityEngine.Debug.Log(ally.name + " lost the buff provided by " + this.name);
            }
        }

        buffedAllies.Clear();
    }
}
