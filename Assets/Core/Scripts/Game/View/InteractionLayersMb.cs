using System;
using UnityEngine;

namespace Game
{
    public class InteractionLayersMb : MonoBehaviour
    {
        public InteractionLayerButtonMb[] Buttons;

        private void OnValidate()
        {
            Buttons = GetComponentsInChildren<InteractionLayerButtonMb>(true);
        }
    }
}
