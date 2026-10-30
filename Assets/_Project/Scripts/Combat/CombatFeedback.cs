using System.Collections;
using UnityEngine;
using LegendsOfTheArena.Characters;

namespace LegendsOfTheArena.Combat
{
    public sealed class CombatFeedback : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer targetRenderer;
        [SerializeField, Min(0.01f)] private float flashDuration = 0.08f;
        [SerializeField, Range(0f, 1f)] private float hitStopDuration = 0.045f;
        private Color originalColor = Color.white;
        private FighterHealth health;
        private Coroutine flashRoutine;

        private void Awake()
        {
            if (targetRenderer == null) targetRenderer = GetComponentInChildren<SpriteRenderer>();
            if (targetRenderer != null) originalColor = targetRenderer.color;
            health = GetComponent<FighterHealth>();
            if (health != null) health.Damaged += OnDamaged;
        }

        private void OnDestroy()
        {
            if (health != null) health.Damaged -= OnDamaged;
        }

        private void OnDamaged(int amount) => PlayHitFeedback();

        public void PlayHitFeedback()
        {
            if (flashRoutine != null) StopCoroutine(flashRoutine);
            flashRoutine = StartCoroutine(Flash());
        }

        private IEnumerator Flash()
        {
            if (targetRenderer != null) targetRenderer.color = Color.white;
            float previousScale = Time.timeScale;
            Time.timeScale = 0.12f;
            yield return new WaitForSecondsRealtime(hitStopDuration);
            Time.timeScale = previousScale;
            if (targetRenderer != null)
            {
                targetRenderer.color = originalColor;
                yield return new WaitForSeconds(flashDuration);
                targetRenderer.color = originalColor;
            }
            flashRoutine = null;
        }

        private void OnDisable()
        {
            if (flashRoutine != null) StopCoroutine(flashRoutine);
            Time.timeScale = 1f;
            if (targetRenderer != null) targetRenderer.color = originalColor;
        }
    }
}
