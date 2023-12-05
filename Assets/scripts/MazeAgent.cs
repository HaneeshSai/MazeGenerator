using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeAgent : MonoBehaviour
{
    List<MazeNode> path = new List<MazeNode>();
    bool spawned = false;

    int startIndex;

    void Update()
    {
        if (GameManager.Instance.enemyStart == true && spawned == false)
        {
            transform.position = GameManager.Instance.startNode.transform.position;
            spawned = true;
            startIndex = GameManager.Instance.nodes.IndexOf(GameManager.Instance.startNode);
            StartSearch(GameManager.Instance.nodes[startIndex]);
        }
    }

    void StartSearch(MazeNode currentNode)
    {
        StartCoroutine(findPath(GameManager.Instance.nodes[startIndex]));
        //StartCoroutine(MoveTowards(path));

    }

    IEnumerator findPath(MazeNode currentNode)
    {

        List<MazeNode> VistedNodes = new List<MazeNode>();
        List<MazeNode> Completed = new List<MazeNode>();
        VistedNodes.Add(currentNode);
        while (!VistedNodes.Contains(GameManager.Instance.EndNode))
        {
            startIndex = GameManager.Instance.nodes.IndexOf(VistedNodes[VistedNodes.Count - 1]);
            List<int> possibleMoves = new List<int>();
            if (!VistedNodes[VistedNodes.Count - 1].walls[0].activeSelf && !VistedNodes.Contains(GameManager.Instance.nodes[startIndex + (int)(Mathf.Sqrt(GameManager.Instance.nodes.Count))]) && !Completed.Contains(GameManager.Instance.nodes[startIndex + (int)(Mathf.Sqrt(GameManager.Instance.nodes.Count))]))
            {
                possibleMoves.Add(startIndex + (int)(Mathf.Sqrt(GameManager.Instance.nodes.Count)));
            }
            if (!VistedNodes[VistedNodes.Count - 1].walls[1].activeSelf && !VistedNodes.Contains(GameManager.Instance.nodes[startIndex - (int)(Mathf.Sqrt(GameManager.Instance.nodes.Count))]) && !Completed.Contains(GameManager.Instance.nodes[startIndex - (int)(Mathf.Sqrt(GameManager.Instance.nodes.Count))]))
            {
                possibleMoves.Add(startIndex - (int)(Mathf.Sqrt(GameManager.Instance.nodes.Count)));
            }
            if (!VistedNodes[VistedNodes.Count - 1].walls[2].activeSelf && !VistedNodes.Contains(GameManager.Instance.nodes[startIndex + 1]) && !Completed.Contains(GameManager.Instance.nodes[startIndex + 1]))
            {
                possibleMoves.Add(startIndex + 1);
            }
            if (!VistedNodes[VistedNodes.Count - 1].walls[3].activeSelf && !VistedNodes.Contains(GameManager.Instance.nodes[startIndex - 1]) && !Completed.Contains(GameManager.Instance.nodes[startIndex - 1]))
            {
                possibleMoves.Add(startIndex - 1);
            }
            if (possibleMoves.Count > 0)
            {
                VistedNodes.Add(GameManager.Instance.nodes[possibleMoves[Random.Range(0, possibleMoves.Count)]]);
                VistedNodes[VistedNodes.Count - 1].SetState(NodeState.CorrectPath);
            }
            else
            {
                Completed.Add(VistedNodes[VistedNodes.Count - 1]);
                VistedNodes[VistedNodes.Count - 1].SetState(NodeState.WrongPath);
                VistedNodes.RemoveAt(VistedNodes.Count - 1);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
