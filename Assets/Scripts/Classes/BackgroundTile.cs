using UnityEngine;

namespace Mathc3Project
{
    public class BackgroundTile : MonoBehaviour
    {
        private int _health;

        private void Start()
        {
            _health = 1;
        }
        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public int Health
        {
            get { return _health; }
            set
            {
                _health = value;
                if(_health <= 0)
                    Destroy(gameObject);
            }
        }
        
    }
}