using UnityEngine;

namespace SirenixPowered.Demo
{
    [ManageableData, CreateAssetMenu(fileName = "DataDemo", menuName = "SirenixPowered/DataDemo")]
    public sealed class DataDemo : ScriptableObject
    {
        public float value1;
        public Vector2 value2;
    }
}
