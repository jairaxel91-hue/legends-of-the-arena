using System.Collections;
using UnityEngine;
using LegendsOfTheArena.Characters;

namespace LegendsOfTheArena.Combat
{
    [RequireComponent(typeof(FighterController))]
    public sealed class FighterCombat : MonoBehaviour
    {
        [Header("Attack")]
        [SerializeField] private Transform hitOrigin;
        [SerializeField] private Vector2 hitBox = new Vector2(1.1f, 0.8f);
        [SerializeField, Min(1)] private int lightDamage = 45;
        [SerializeField, Min(1)] private int heavyDamage = 90;
        [SerializeField, Min(0.05f)] private float lightRecovery = 0.28f;
        [SerializeField, Min(0.05f)] private float heavyRecovery = 0.62f;
        [SerializeField] private LayerMask fighterMask;

        [Header("Input")]
        [SerializeField] private KeyCode lightKey = KeyCode.J;
        [SerializeField] private KeyCode heavyKey = KeyCode.K;
        [SerializeField] private KeyCode blockKey = KeyCode.L;
        [SerializeField] private bool playerTwo;

        private FighterController controller;
        private bool recovering;
        private float lastAttackTime;
        public bool IsBlocking { get; private set; }
        public bool IsAttacking { get; private set; }

        private void Awake()
        {
            controller = GetComponent<FighterController>();
            if (playerTwo)
            {
                lightKey = KeyCode.Keypad1;
                heavyKey = KeyCode.Keypad2;
                blockKey = KeyCode.Keypad3;
            }
        }

        private void Update()
        {
            IsBlocking = Input.GetKey(blockKey) && controller.IsGrounded && !recovering;
            if (recovering) return;

            if (Input.GetKeyDown(lightKey)) StartCoroutine(Attack(lightDamage, lightRecovery));
            else if (Input.GetKeyDown(heavyKey)) StartCoroutine(Attack(heavyDamage, heavyRecovery));
        }

        private IEnumerator Attack(int damage, float recovery)
        {
            recovering = true;
            IsAttacking = true;
            lastAttackTime = Time.time;
            yield return new WaitForSeconds(0.09f);

            Vector2 center = hitOrigin != null
                ? (Vector2)transform.TransformPoint(new Vector3(Mathf.Abs(hitOrigin.localPosition.x) * controller.Facing, hitOrigin.localPosition.y, 0f))
                : (Vector2)transform.position + Vector2.right * controller.Facing * 0.9f;
            Collider2D[] hits = Physics2D.OverlapBoxAll(center, hitBox, 0f, fighterMask);
            foreach (Collider2D hit in hits)
            {
                if (hit.transform == transform || hit.transform.IsChildOf(transform)) continue;
                FighterHealth health = hit.GetComponentInParent<FighterHealth>();
                FighterCombat targetCombat = hit.GetComponentInParent<FighterCombat>();
                if (health == null || health.IsKnockedOut) continue;

                bool blocked = targetCombat != null && targetCombat.IsBlocking &&
                               Mathf.Sign(targetCombat.transform.position.x - transform.position.x) ==
                               Mathf.Sign(targetCombat.GetComponent<FighterController>().Facing) * -1f;
                if (!blocked) health.TakeDamage(damage);
                break;
            }

            IsAttacking = false;
            yield return new WaitForSeconds(Mathf.Max(0.01f, recovery - 0.09f));
            recovering = false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Vector3 center = hitOrigin != null ? hitOrigin.position :
                transform.position + Vector3.right * 0.9f;
            Gizmos.DrawWireCube(center, hitBox);
        }
    }
}
