using System;
using Assets.Scripts.Game.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Game
{

    public class PickUp : MonoBehaviour
    {

        public Action<int> Event;
        public int FoodPoints;

        private void Start()
        {
            Event += Player.Instance.IncreaseFood;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            var itemConsumer = other.GetComponent<IItemConsumer>();
            if (itemConsumer != null)
            {
                Event.Invoke(FoodPoints);
            }
            DestroyObject(gameObject);
        }
    }

}
