using UnityEngine;
using UnityEngine.UI;
using LegendsOfTheArena.Characters;

namespace LegendsOfTheArena.UI
{
    [RequireComponent(typeof(Slider))]
    public sealed class HealthBarView : MonoBehaviour
    {
        [SerializeField] private FighterHealth target;
        [SerializeField] private Slider slider;
        [SerializeField] private Image fill;

        private void Awake()
        {
            if (slider == null) slider = GetComponent<Slider>();
            if (target != null)
            {
                slider.maxValue = target.MaxHealth;
                slider.value = target.CurrentHealth;
                target.HealthChanged += OnHealthChanged;
            }
        }

        private void OnDestroy()
        {
            if (target != null) target.HealthChanged -= OnHealthChanged;
        }

        public void Bind(FighterHealth health)
        {
            if (target != null) target.HealthChanged -= OnHealthChanged;
            target = health;
            if (target == null || slider == null) return;
            slider.maxValue = target.MaxHealth;
            slider.value = target.CurrentHealth;
            target.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(int current, int max)
        {
            slider.maxValue = max;
            slider.value = current;
            if (fill != null)
                fill.color = Color.Lerp(new Color(0.85f, 0.12f, 0.12f), new Color(0.18f, 0.78f, 0.42f), (float)current / max);
        }
    }
}
