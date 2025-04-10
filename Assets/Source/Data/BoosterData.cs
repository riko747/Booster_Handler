using System.Collections.Generic;
using UnityEngine;

namespace Source.Data
{
    [CreateAssetMenu(fileName = "BoosterData", menuName = "Boosters/BoosterDataScriptableObject")]
    public class BoosterData : ScriptableObject
    {
        
        public List<Booster> boosters;
    }
}
