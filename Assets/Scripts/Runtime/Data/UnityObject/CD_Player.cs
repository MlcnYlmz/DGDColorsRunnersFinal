using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Player", menuName = "DGDColorsRunnersFinal/CD_Player", order = 0)]
    public class CD_Player : ScriptableObject
    {
        public PlayerData PlayerData;
    }
}