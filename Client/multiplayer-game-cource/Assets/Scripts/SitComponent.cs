using System;
using Assets.Scripts.Extensions;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Не очень люблю аниамации, поэтому пока через скейл сделаю :) Но если оооочень важно, то переделаю
    /// </summary>
    public class SitComponent : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private float _sitScale = 0.65f;
        [SerializeField] private float _standScale = 1f;

        public bool IsSitting { get; private set; }

        public bool TryGetDown()
        {
            if (IsSitting) return false;
            
            _root.SetScale(_sitScale);

            IsSitting = true;
            return true;
        }

        public bool TryGetUp()
        {
            if (!IsSitting) return false;
            
            _root.SetScale(_standScale);

            IsSitting = false;
            return true;
        }
    }
}