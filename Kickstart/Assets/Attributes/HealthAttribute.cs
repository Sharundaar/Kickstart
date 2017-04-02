using UnityEngine;
using System.Collections;
using System;

namespace DEngine.EntityAttributes
{
    public class HealthAttribute : MonoBehaviour {

        private class HealthChangedEventArgs : EventArgs
        {
            public float OldValue { get; set; }
            public float NewValue { get; set; }
        }
        public event EventHandler HealthChanged;


        private const float DefaultMaxHealth = 100;

        [SerializeField]
        private float m_MaxHealth = DefaultMaxHealth;
        public float MaxHealth
        {
            get { return m_MaxHealth; }
        }

        [SerializeField]
        private float m_InitialHealth = DefaultMaxHealth;
        public float InitialHealth
        {
            get { return m_InitialHealth; }
        }

        private float m_CurrentHealth;
        public float CurrentHealth
        {
            get { return m_CurrentHealth; }
            private set
            {
                float old = m_CurrentHealth;
                m_CurrentHealth = (value >= 0 ? (value <= MaxHealth ? value : MaxHealth) : 0);
                if (old != m_CurrentHealth)
                    OnHealthChanged(old, m_CurrentHealth);
            }
        }

        [SerializeField]
        private bool m_Regenerate;
        public bool Regenerate
        {
            get { return m_Regenerate; }
            set { m_Regenerate = value; }
        }

        [SerializeField]
        private float m_RegenerationRate;
        public float RegenerationRate
        {
            get { return m_RegenerationRate; }
            set { m_RegenerationRate = (value >= 0 ? value : 0); }
        }

        private void Start()
        {
            CurrentHealth = InitialHealth;
        }

        private void Update()
        {
            if(Regenerate)
            {
                CurrentHealth += RegenerationRate * Time.deltaTime;
            }
        }

        private void OnHealthChanged(float _old, float _new)
        {
            if (HealthChanged != null)
                HealthChanged(this, new HealthChangedEventArgs() { OldValue = _old, NewValue = _new });
        }

        public void Damage(float _damage)
        {
            CurrentHealth -= _damage;
        }

        public void Heal(float _value)
        {
            CurrentHealth += _value;
        }

        public void Reset()
        {
            CurrentHealth = InitialHealth;
        }
    }
}
