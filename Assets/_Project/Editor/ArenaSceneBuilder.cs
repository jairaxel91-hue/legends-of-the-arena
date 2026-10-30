#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using LegendsOfTheArena.Characters;
using LegendsOfTheArena.Combat;
using LegendsOfTheArena.Core;
using LegendsOfTheArena.CameraSystem;

namespace LegendsOfTheArena.Editor
{
    public static class ArenaSceneBuilder
    {
        private const string SceneFolder = "Assets/_Project/Scenes";
        private const string ScenePath = SceneFolder + "/ArenaPrototype.unity";

        [MenuItem("Legends of the Arena/Create Prototype Arena")]
        public static void CreatePrototypeArena()
        {
            if (!AssetDatabase.IsValidFolder("Assets/_Project"))
                AssetDatabase.CreateFolder("Assets", "_Project");
            if (!AssetDatabase.IsValidFolder(SceneFolder))
                AssetDatabase.CreateFolder("Assets/_Project", "Scenes");

            Scene scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            CreateBackdrop();
            GameObject floor = CreateBox("Ground", new Vector2(0f, -2.1f), new Vector2(24f, 1f), new Color(0.12f, 0.14f, 0.18f));
            floor.layer = LayerMask.NameToLayer("Default");

            GameObject p1 = CreateFighter("Fighter One — TEMP", new Vector2(-3f, -1.3f), new Color(0.18f, 0.55f, 0.95f), false);
            GameObject p2 = CreateFighter("Fighter Two — TEMP", new Vector2(3f, -1.3f), new Color(0.88f, 0.3f, 0.22f), true);

            GameObject cameraObject = new GameObject("Arena Camera");
            Camera cam = cameraObject.AddComponent<Camera>();
            cam.orthographic = true;
            cam.orthographicSize = 5.5f;
            cameraObject.tag = "MainCamera";
            cameraObject.transform.position = new Vector3(0f, 1f, -10f);
            ArenaCamera arenaCamera = cameraObject.AddComponent<ArenaCamera>();
            arenaCamera.SetFighters(p1.transform, p2.transform);

            GameObject manager = new GameObject("Match Manager");
            MatchManager match = manager.AddComponent<MatchManager>();
            match.Configure(p1.GetComponent<FighterHealth>(), p2.GetComponent<FighterHealth>());

            EditorSceneManager.SaveScene(scene, ScenePath);
            AssetDatabase.SaveAssets();
            Selection.activeObject = AssetDatabase.LoadAssetAtPath<SceneAsset>(ScenePath);
            EditorUtility.DisplayDialog("Arena prototype created",
                "Created ArenaPrototype.unity. This scene uses clearly temporary blockout art. Replace it with production character and arena assets before calling the visuals final.",
                "OK");
        }

        private static GameObject CreateFighter(string name, Vector2 position, Color color, bool playerTwo)
        {
            GameObject root = new GameObject(name);
            root.transform.position = position;
            Rigidbody2D rb = root.AddComponent<Rigidbody2D>();
            rb.gravityScale = 3f;
            rb.freezeRotation = true;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            CapsuleCollider2D capsule = root.AddComponent<CapsuleCollider2D>();
            capsule.size = new Vector2(0.85f, 1.8f);

            SpriteRenderer renderer = root.AddComponent<SpriteRenderer>();
            renderer.sprite = CreateTemporarySprite(color);
            renderer.color = color;
            renderer.sortingOrder = 5;

            FighterController controller = root.AddComponent<FighterController>();
            FighterHealth health = root.AddComponent<FighterHealth>();
            FighterCombat combat = root.AddComponent<FighterCombat>();

            controller.ConfigurePlayerTwo(playerTwo);
            combat.ConfigurePlayerTwo(playerTwo);

            GameObject check = new GameObject("GroundCheck");
            check.transform.SetParent(root.transform);
            check.transform.localPosition = new Vector3(0f, -0.94f, 0f);
            controller.SetGroundCheck(check.transform);

            GameObject hit = new GameObject("HitOrigin");
            hit.transform.SetParent(root.transform);
            hit.transform.localPosition = new Vector3(0.85f, 0.15f, 0f);
            combat.SetHitOrigin(hit.transform);
            combat.SetFighterMask(1 << root.layer);
            return root;
        }

        private static GameObject CreateBox(string name, Vector2 position, Vector2 size, Color color)
        {
            GameObject obj = new GameObject(name);
            obj.transform.position = position;
            BoxCollider2D collider = obj.AddComponent<BoxCollider2D>();
            collider.size = size;
            SpriteRenderer sr = obj.AddComponent<SpriteRenderer>();
            sr.sprite = CreateTemporarySprite(color);
            sr.color = color;
            sr.transform.localScale = new Vector3(size.x, size.y, 1f);
            sr.sortingOrder = 1;
            return obj;
        }

        private static void CreateBackdrop()
        {
            GameObject bg = new GameObject("Backdrop — TEMP");
            bg.transform.position = new Vector3(0f, 1.5f, 2f);
            SpriteRenderer sr = bg.AddComponent<SpriteRenderer>();
            sr.sprite = CreateTemporarySprite(new Color(0.055f, 0.07f, 0.11f));
            sr.color = new Color(0.055f, 0.07f, 0.11f);
            sr.transform.localScale = new Vector3(24f, 11f, 1f);
            sr.sortingOrder = -10;
        }

        private static Sprite CreateTemporarySprite(Color color)
        {
            const string generatedFolder = "Assets/_Project/Generated";
            if (!AssetDatabase.IsValidFolder(generatedFolder))
                AssetDatabase.CreateFolder("Assets/_Project", "Generated");

            string hex = ColorUtility.ToHtmlStringRGBA(color);
            string path = generatedFolder + "/Blockout_" + hex + ".png";
            if (!File.Exists(path))
            {
                Texture2D texture = new Texture2D(32, 32, TextureFormat.RGBA32, false);
                Color[] pixels = new Color[32 * 32];
                for (int i = 0; i < pixels.Length; i++) pixels[i] = Color.white;
                texture.SetPixels(pixels);
                texture.Apply();
                File.WriteAllBytes(path, texture.EncodeToPNG());
                Object.DestroyImmediate(texture);
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceSynchronousImport);
                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
                if (importer != null)
                {
                    importer.textureType = TextureImporterType.Sprite;
                    importer.spritePixelsPerUnit = 32f;
                    importer.filterMode = FilterMode.Bilinear;
                    importer.SaveAndReimport();
                }
            }
            return AssetDatabase.LoadAssetAtPath<Sprite>(path);
        }
    }
}
#endif
