using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitDatabase", menuName = "Scriptable Objects/UnitDatabase")]
public class UnitDatabase : ScriptableObject
{
    public List<GameObject> catUnits;
    public List<GameObject> dogUnits;
    public List<GameObject> hedgehogUnits;
    public List<GameObject> capybaraUnits;

    public List<GameObject> GetUnitsForFaction(Faction faction)
    {
        return faction switch
        {
            Faction.Cats => catUnits,
            Faction.Dogs => dogUnits,
            Faction.Hedgehogs => hedgehogUnits,
            Faction.Capybaras => capybaraUnits,
            _ => new List<GameObject>(),
        };
    }
}
