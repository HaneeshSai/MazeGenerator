using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int EnemyCount;
    public static GameManager Instance;
    [SerializeField] MazeNode Node;
    public List<MazeNode> nodes = new List<MazeNode>();
    public bool enemyStart = false;
    public MazeNode startNode;
    public MazeNode EndNode;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
