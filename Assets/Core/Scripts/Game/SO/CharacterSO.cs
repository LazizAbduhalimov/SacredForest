using UnityEngine;

namespace Client.Game
{
    [CreateAssetMenu(fileName = "New Character Config", menuName = "Game/Unit/Character")]
    public class CharacterSO : UnitSO
    {
        public Character Pregab;
    }
}