using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PolyNav;
using System.Linq;


//example. moving between some points at random
[RequireComponent(typeof(PolyNavAgent))]
public class PatrolRandomWaypoints : MonoBehaviour
{
    public List<Vector2> WPoints = new List<Vector2>();
    public float delayBetweenPoints = 0f;

    private PolyNavAgent _agent;
    private PolyNavAgent agent
    {
        get { return _agent != null ? _agent : _agent = GetComponent<PolyNavAgent>(); }
    }
    void OnEnable()
    {
        agent.OnDestinationReached += MoveRandom;
        agent.OnDestinationInvalid += MoveRandom;
    }

    void OnDisable()
    {
        agent.OnDestinationReached -= MoveRandom;
        agent.OnDestinationInvalid -= MoveRandom;
    }

    void Start()
    {
        TileData[] allTiles = FindObjectsOfType<TileData>();

        foreach (var tile in allTiles)
        {
            if (tile.position.x != 0 && tile.position.y != 0)
            {
                WPoints.Add(tile.position);
            }
        }
        if (WPoints.Count > 0)
        {
            MoveRandom();
        }
    }

    public void MoveRandom()
    {
        StartCoroutine(WaitAndMove());
    }

    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(delayBetweenPoints);
        TileData[] tiles = FindObjectsOfType<TileData>();
        List<TileData> validTiles = tiles
            .Where(t => t != null && t.gameObject.activeInHierarchy)
            .ToList();
        if (validTiles.Count == 0)
            yield break;
        Vector2 currentPos = transform.position;
        TileData closestTile = validTiles[0];
        float minDist = Vector2.Distance(currentPos, closestTile.transform.position);

        foreach (var tile in validTiles)
        {
            float dist = Vector2.Distance(currentPos, tile.transform.position);
            if (dist < minDist)
            {
                closestTile = tile;
                minDist = dist;
            }
        }
        agent.SetDestination(closestTile.transform.position);
    }
    public void MoveRandomExternally()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(WaitAndMove());
        }
    }
    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < WPoints.Count; i++)
        {
            Gizmos.DrawSphere(WPoints[i], 0.05f);
        }
    }
}
