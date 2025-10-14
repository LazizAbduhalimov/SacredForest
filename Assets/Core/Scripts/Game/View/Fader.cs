using System;
using UnityEngine;
using PrimeTween;

namespace Core.Scripts.Game.View
{
    public class Fader : MonoBehaviour
    {
        public CanvasGroup CanvasGroup;

        private void Awake()
        {
            CanvasGroup.alpha = 1;
            Tween.Alpha(CanvasGroup, 0, 0.5f, Ease.InSine);
        }
    }
}