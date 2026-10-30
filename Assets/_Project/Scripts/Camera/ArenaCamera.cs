using UnityEngine;

namespace LegendsOfTheArena.CameraSystem
{
    [RequireComponent(typeof(Camera))]
    public sealed class ArenaCamera : MonoBehaviour
    {
        [SerializeField] private Transform fighterA;
        [SerializeField] private Transform fighterB;
        [SerializeField, Min(1f)] private float minSize = 5f;
        [SerializeField, Min(1f)] private float maxSize = 8.5f;
        [SerializeField, Min(0.1f)] private float padding = 3f;
        [SerializeField, Min(0.1f)] private float smooth = 5f;
        private Camera cam;

        private void Awake() => cam = GetComponent<Camera>();

        private void LateUpdate()
        {
            if (fighterA == null || fighterB == null) return;
            Vector3 midpoint = (fighterA.position + fighterB.position) * 0.5f;
            Vector3 target = new Vector3(midpoint.x, midpoint.y + 1.1f, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * smooth);

            float distance = Mathf.Abs(fighterA.position.x - fighterB.position.x);
            float desired = Mathf.Clamp(minSize + distance * 0.28f + padding * 0.25f, minSize, maxSize);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, desired, Time.deltaTime * smooth);
        }

        public void SetFighters(Transform a, Transform b)
        {
            fighterA = a;
            fighterB = b;
        }
    }
}
