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

        _controlersStorage.Initialize();

        cannon.SetAim(hero.Transform);
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        _controlersStorage.Execute(deltaTime);
    }

    void FixedUpdate()
    {
    }

    private void OnDestroy()
    {
        _controlersStorage.Cleanup();
    }
}
