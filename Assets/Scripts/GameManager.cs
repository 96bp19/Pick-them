using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int noOfPlatformsTogenerate =5;
    public LevelGenerator levelGeneratorPrefab;
    public Player playerPrefab;
    public InputHandler InputHandlerPrefab;

    private InputHandler _inputHandlerInstance;
    private LevelGenerator _levelGeneratorInstance;
    private Player _PlayerInstance;

    private List<GameObject> playerFollowers = new List<GameObject>();

    private void Start()
    {
        _inputHandlerInstance = Instantiate(InputHandlerPrefab);
        BeginGame();
        HeadFollower.playerAddedListeners += AddPlayerFollower;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }


    void RestartGame()
    {
        Destroy(_levelGeneratorInstance.gameObject);
        Destroy(_PlayerInstance.gameObject);
        RemovePlayerFollowers();
        BeginGame();
       
    }

    void BeginGame()
    {
       
        _levelGeneratorInstance = Instantiate(levelGeneratorPrefab) as LevelGenerator;
        _PlayerInstance = Instantiate(playerPrefab) as Player;
        
        _levelGeneratorInstance.GenerateLevel(noOfPlatformsTogenerate);
        _levelGeneratorInstance.initializePlayerPosition(_PlayerInstance);
    }

    void RemovePlayerFollowers()
    {
        foreach (var item in playerFollowers)
        {
            Destroy(item);
        }
        playerFollowers.Clear();
    }

    void AddPlayerFollower(GameObject addedObj)
    {
        playerFollowers.Add(addedObj);
    }



}
