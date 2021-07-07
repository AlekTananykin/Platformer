using Assets.Code.Configs;
using Assets.Code.Controllers;
using Assets.Code.PlatformGenerator;
using Assets.Code.PlayerInput;
using Assets.Code.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private SpriteRenderer _back;
 
    GameObjectFabric _gameObjectsFabric;

    ControllersStorage _controlersStorage;

    IPlayerInput _playerInput;

    GenerateLevelController _levelGenerator;

    private void Awake()
    {
        var levelView = this.GetComponent<GenerateLevelView>();
        _levelGenerator = new GenerateLevelController(levelView);
        _levelGenerator.Awake();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _controlersStorage = new ControllersStorage();
        _gameObjectsFabric = new GameObjectFabric();
        _playerInput = new PlayerPcInput();

        CameraController camera = new CameraController(_gameObjectsFabric);
        _controlersStorage.Add(camera);

        AddPlatforms(_controlersStorage);

        RepulsiveCrystalController crystal = 
            new RepulsiveCrystalController(_gameObjectsFabric, new Vector2(-35.4f, -1.95f));
        _controlersStorage.Add(crystal);
        
        HeroController hero = new HeroController(_gameObjectsFabric, _playerInput);
        hero.Position += camera.SetPosition;
        _controlersStorage.Add(hero);
        

        CannonController cannon = new CannonController(_gameObjectsFabric);
        _controlersStorage.Add(cannon);


        Vector2 ogrePosition = new Vector2() { x = 15, y = 3 };
        OgreController ogre = new OgreController(
            _gameObjectsFabric, ogrePosition, hero);
        _controlersStorage.Add(ogre);

        _controlersStorage.Initialize();

        cannon.SetAim(hero.Transform);
    }

    private void AddPlatforms(ControllersStorage controlersStorage)
    {
        List<Vector2> platformsPositions =
            new List<Vector2>() {
                new Vector2(-10f, -1.97f),
                new Vector2(-1.48f, -0.77f),
                new Vector2(7.9f, -1f),
                new Vector2(-18f, -2.43f),
                new Vector2(-20.85f, 2.37f),
                new Vector2(-21.46f, -5.82f),
                new Vector2(-26.6f, -2.51f),
                new Vector2(-35.4f, -2.48f)
            };

        for (int i = 0; i < platformsPositions.Count; ++i)
        {
            PlatformController platform = new PlatformController(
                _gameObjectsFabric, platformsPositions[i]);

            _controlersStorage.Add(platform);
        }
    }

    void FixedUpdate()
    {
        float deltaTime = Time.fixedDeltaTime;
        _controlersStorage.Execute(deltaTime);
    }

    private void OnDestroy()
    {
        _controlersStorage.Cleanup();
    }
}
