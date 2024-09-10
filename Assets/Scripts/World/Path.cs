using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Path
{
    private List<GameObject> path = new List<GameObject>();
    private List<GameObject> path2 = new List<GameObject>();
    private List<GameObject> path3 = new List<GameObject>();

    private List<GameObject> topTiles = new List<GameObject>();
    private List<GameObject> bottomTiles = new List<GameObject>();
    private List<GameObject> middleTiles = new List<GameObject>();

    private List<GameObject> turretPlacablesPath1 = new List<GameObject>();

    private List<GameObject> leftTiles = new List<GameObject>();
    private List<GameObject> rightTiles = new List<GameObject>();

    private List<GameObject> startingTilesList = new List<GameObject>();

    private int radius, currentTileIndex;
    private bool hasReachedX, hasReachedZ, hasReachedX2, hasReachedZ2, hasReachedX3, hasReachedZ3;
    private GameObject startingTile, startingTile2, startingTile3, endingTile;
    public List<GameObject> GetGeneratedPath => path;
    public List<GameObject> GetGeneratedPath2 => path2;
    public List<GameObject> GetGeneratedPath3 => path3;
    public List<GameObject> GetGeneratedEnding => middleTiles;
    public List<GameObject> GetGeneratedStart => startingTilesList;
    public List<GameObject> GetGeneratedTurret => turretPlacablesPath1;

    public Path(int worldRadius)
    {
        this.radius = worldRadius;
    }
    public void AssignTopAndBottomTiles(int z,int x, GameObject tile)
    {
        if (z == 0)
            topTiles.Add(tile);
        if (z == radius - 1)
            bottomTiles.Add(tile);
        if ((z == 8) && (x == 8))
            middleTiles.Add(tile);
        Debug.Log("Added Tile");
    }

    public void AssignLeftAndRightTiles(int x, GameObject tile)
    {
        if (x == 0) 
            leftTiles.Add(tile);
        if(x == radius - 1)
            rightTiles.Add(tile);
        Debug.Log("AddedTile");
    }

    private bool AssignAndCheckStartingAndEndingTile()
    {
        var xIndex = Random.Range(0, topTiles.Count - 1);
        var xIndex2 = Random.Range(0, leftTiles.Count-1);
        var xIndex3 = Random.Range(0, rightTiles.Count);

        var zIndex = Random.Range(0, bottomTiles.Count);


        startingTile = topTiles[xIndex];
        startingTile2 = leftTiles[xIndex2];
        startingTile3 = rightTiles[xIndex3];
        endingTile = middleTiles[0];

        startingTilesList.Add(startingTile);
        startingTilesList.Add(startingTile2);
        startingTilesList.Add(startingTile3);

        middleTiles.Add(endingTile);
        return startingTile != null && startingTile2 && startingTile3 && endingTile != null;
    }
    public void GeneratePath()
    {
        if (AssignAndCheckStartingAndEndingTile())
        {
            GameObject currentTile = startingTile;
            GameObject currentTile2 = startingTile2;
            GameObject currentTile3 = startingTile3;

            //for (int i = 0; i < 3; i++)
            //    MoveLeft(ref currentTile); MoveUp2(ref currentTile2); MoveDown3(ref currentTile3);


            var safteyBreakX = 0;
            while (!hasReachedX)
            {
                safteyBreakX++;
                if (safteyBreakX > 100)
                    break;
                // We move vertically on our xAxis
                if (currentTile.transform.position.x > endingTile.transform.position.x)
                    MoveDown(ref currentTile);  
                else if (currentTile.transform.position.x < endingTile.transform.position.x)
                    MoveUp(ref currentTile);
                else
                    hasReachedX = true;
            }
            var safteyBreakZ = 0;
            while (!hasReachedZ)
            {
                safteyBreakZ++;
                if (safteyBreakZ > 100)
                    break;
                // We move horizontally on our zAxis
                if (currentTile.transform.position.z > endingTile.transform.position.z)
                    MoveRight(ref currentTile);
                else if (currentTile.transform.position.z < endingTile.transform.position.z)
                    MoveLeft(ref currentTile);
                else
                    hasReachedZ = true;
            }

            var safteyBreakX2 = 0;
            while (!hasReachedX2)
            {
                safteyBreakX2++;
                if (safteyBreakX2 > 100)
                    break;
                // We move vertically on our xAxis
                if (currentTile2.transform.position.x > endingTile.transform.position.x)
                    MoveDown2(ref currentTile2);
                else if (currentTile2.transform.position.x < endingTile.transform.position.x)
                    MoveUp2(ref currentTile2);
                else
                    hasReachedX2 = true;
            }
            var safteyBreakZ2 = 0;
            while (!hasReachedZ2)
            {
                safteyBreakZ2++;
                if (safteyBreakZ2 > 100)
                    break;
                // We move horizontally on our zAxis
                if (currentTile2.transform.position.z > endingTile.transform.position.z)
                    MoveRight2(ref currentTile2);
                else if (currentTile2.transform.position.z < endingTile.transform.position.z)
                    MoveLeft2(ref currentTile2);
                else
                    hasReachedZ2 = true;
            }

            var safteyBreakX3 = 0;
            while (!hasReachedX3)
            {
                safteyBreakX3++;
                if (safteyBreakX3 > 100)
                    break;
                // We move vertically on our xAxis
                if (currentTile3.transform.position.x > endingTile.transform.position.x)
                    MoveDown3(ref currentTile3);
                else if (currentTile3.transform.position.x < endingTile.transform.position.x)
                    MoveUp3(ref currentTile3);
                else
                    hasReachedX3 = true;
            }
            var safteyBreakZ3 = 0;
            while (!hasReachedZ3)
            {
                safteyBreakZ3++;
                if (safteyBreakZ3 > 100)
                    break;
                // We move horizontally on our zAxis
                if (currentTile3.transform.position.z > endingTile.transform.position.z)
                    MoveRight3(ref currentTile3);
                else if (currentTile3.transform.position.z < endingTile.transform.position.z)
                    MoveLeft3(ref currentTile3);
                else
                    hasReachedZ3 = true;
            }
            path.Add(endingTile);
        }
    }
    private void MoveDown(ref GameObject currentTile)
    {
        path.Add(currentTile);
        currentTileIndex = WorldGeneration.GeneratedTiles.IndexOf(currentTile);
        int n = currentTileIndex - radius;
        currentTile = WorldGeneration.GeneratedTiles[n];
    }
    private void MoveUp(ref GameObject currentTile)
    {
        path.Add(currentTile);
        currentTileIndex = WorldGeneration.GeneratedTiles.IndexOf(currentTile);
        int n = currentTileIndex + radius;
        currentTile = WorldGeneration.GeneratedTiles[n];
    }
    private void MoveLeft(ref GameObject currentTile)
    {
        path.Add(currentTile);
        currentTileIndex = WorldGeneration.GeneratedTiles.IndexOf(currentTile);
        currentTileIndex++;
        currentTile = WorldGeneration.GeneratedTiles[currentTileIndex];
    }
    private void MoveRight(ref GameObject currentTile)
    {
        path.Add(currentTile);
        currentTileIndex = WorldGeneration.GeneratedTiles.IndexOf(currentTile);
        currentTileIndex--;
        currentTile = WorldGeneration.GeneratedTiles[currentTileIndex];
    }

    private void MoveDown2(ref GameObject currentTile)
    {
        path2.Add(currentTile);
        currentTileIndex = WorldGeneration.GeneratedTiles.IndexOf(currentTile);
        int n = currentTileIndex - radius;
        currentTile = WorldGeneration.GeneratedTiles[n];
    }
    private void MoveUp2(ref GameObject currentTile)
    {
        path2.Add(currentTile);
        currentTileIndex = WorldGeneration.GeneratedTiles.IndexOf(currentTile);
        int n = currentTileIndex + radius;
        currentTile = WorldGeneration.GeneratedTiles[n];
    }
    private void MoveLeft2(ref GameObject currentTile)
    {
        path2.Add(currentTile);
        currentTileIndex = WorldGeneration.GeneratedTiles.IndexOf(currentTile);
        currentTileIndex++;
        currentTile = WorldGeneration.GeneratedTiles[currentTileIndex];
    }
    private void MoveRight2(ref GameObject currentTile)
    {
        path2.Add(currentTile);
        currentTileIndex = WorldGeneration.GeneratedTiles.IndexOf(currentTile);
        currentTileIndex--;
        currentTile = WorldGeneration.GeneratedTiles[currentTileIndex];
    }

    private void MoveDown3(ref GameObject currentTile)
    {
        path3.Add(currentTile);
        currentTileIndex = WorldGeneration.GeneratedTiles.IndexOf(currentTile);
        int n = currentTileIndex - radius;
        currentTile = WorldGeneration.GeneratedTiles[n];
    }
    private void MoveUp3(ref GameObject currentTile)
    {
        path3.Add(currentTile);
        currentTileIndex = WorldGeneration.GeneratedTiles.IndexOf(currentTile);
        int n = currentTileIndex + radius;
        currentTile = WorldGeneration.GeneratedTiles[n];
    }
    private void MoveLeft3(ref GameObject currentTile)
    {
        path3.Add(currentTile);
        currentTileIndex = WorldGeneration.GeneratedTiles.IndexOf(currentTile);
        currentTileIndex++;
        currentTile = WorldGeneration.GeneratedTiles[currentTileIndex];
    }
    private void MoveRight3(ref GameObject currentTile)
    {
        path3.Add(currentTile);
        currentTileIndex = WorldGeneration.GeneratedTiles.IndexOf(currentTile);
        currentTileIndex--;
        currentTile = WorldGeneration.GeneratedTiles[currentTileIndex];
    }
}