using GL4Engine.Core;
using GL4Engine.Graphics;
using GL4Engine.Graphics.Shaders;
using GL4Engine.Scripts;
using OpenTK;

namespace GL4Engine
{
    class TestGame : Game
    {
        public override void OnLoad()
        {
            // Create scene
            Scene scene = new Scene();
            SceneManager.Instance.AddScene("main", scene);
            SceneManager.Instance.SetActiveScene("main");

            // Add shaders
            Resources.Instance.AddShader(new DefaultShader("default"));
            //Resources.Instance.AddShader(new PhongShader("phong"));

            // Create gameObject
            GameObject go1 = new GameObject();
            GameObject go2 = new GameObject();
            GameObject pointLight = new GameObject();
            GameObject spotLight = new GameObject();
            GameObject directionalLight = new GameObject();
            scene.Add(go1);
            scene.Add(go2);
            scene.Add(pointLight);
            scene.Add(spotLight);
            scene.Add(directionalLight);

            // Add Light
            pointLight.AddComponent(new Light(LightType.POINT, new Vector3(0, 0, 1)));
            pointLight.transform.position = new Vector3(0, 5, -18);

            spotLight.AddComponent(new Light(LightType.SPOT, new Vector3(0, 1, 0), 20f, new Vector3(0, -1, 0))).AddComponent(new MoveScript());
            spotLight.transform.position = new Vector3(0, 10, -19);

            //directionalLight.AddComponent(new Light(LightType.DIRECTIONAL, new Vector3(0.5f, 0.5f, 0.5f)));
            //directionalLight.transform.position.Z = 1;

            // Load shaders
            DefaultShader defaultShader = Resources.Instance.LoadShader("default") as DefaultShader;

            // Create materials
            Material dragonMaterial = new Material();
            dragonMaterial.DiffuseColor = new Vector3(1, 1, 1);
            dragonMaterial.Texture = Resources.Instance.LoadTexture("cornflower-blue.png");

            Material stallMaterial = new Material();
            stallMaterial.DiffuseColor = new Vector3(1, 1, 1);
            stallMaterial.Texture = Resources.Instance.LoadTexture("stallTexture_preview.png");

            // Load meshes
            Mesh dragon = Resources.Instance.LoadMesh("dragon.obj");
            dragon.Shader = defaultShader;
            go1.AddComponent(new MeshRenderer(dragon, dragonMaterial)).AddComponent(new RotateScript());
            go1.transform.position.Z = -20;

            Mesh stall = Resources.Instance.LoadMesh("stall.obj");
            stall.Shader = defaultShader;
            go2.AddComponent(new MeshRenderer(stall, stallMaterial)).AddComponent(new RotateScript());
            go2.transform.position.Z = -20;
            go2.transform.position.X = -15;


            Camera.mainCamera.gameObject.AddComponent(new MouseLook());
            Camera.mainCamera.transform.position.Y = 5f;
        }
    }
}
