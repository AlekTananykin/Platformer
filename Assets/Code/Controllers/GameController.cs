using Assets.Code.Configs;
using Assets.Code.Controllers;
using Assets.Code.PlayerInput;
using Assets.Code.Views;
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


    void Start()
    {
        _controlersStorage = new ControllersStorage();
        _gameObjectsFabric = new GameObjectFabric();
        _playerInput = new PlayerPcInput();

        HeroController hero = new HeroController(_gameObjectsFabric, _playerInput);
        _controlersStorage.Add(hero);

        CannonController cannon = new CannonController(_gameObjectsFabric);
        _controlersStorage.Add(cannon);

        PlatformController platform1 = 
            new PlatformController(_gameObjectsFabric, new Vector2(-10f, -1.97f));
        _controlersStorage.Add(platform1);

        PlatformController platform2 = 
            new PlatformController(_gameObjectsFabric, new Vector2(-1.48f, -0.77f));
        _controlersStorage.Add(platform2);

        PlatformController platform3 = 
            new PlatformController(_gameObjectsFabric, new Vector2(7.9f, -1f));
        _controlersStorage.Add(platform3);

        _controlersStorage.Initialize();

        cannon.SetAim(hero.Transform);
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
