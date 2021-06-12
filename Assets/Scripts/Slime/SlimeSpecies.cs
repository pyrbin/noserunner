using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "SlimeSpecies", menuName = "Data/SlimeSpecies")]
public class SlimeSpecies : ScriptableObject
{
    public float BaseJumpHeight = 1f;
    public float BaseSpeed = 10f;

    [SerializeField]
    public Modifiers Mods;

    [System.Serializable]
    public struct Modifiers
    {
        public float Scale;
        public float Speed;
        public float Jump;
    }
}

