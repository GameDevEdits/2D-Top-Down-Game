using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WalkerGenerator : MonoBehaviour
{
    public enum Grid
	{
		FLOOR,
		WALL,
		ENEMY,
		EMPTY
	}
	
	//Variables
	public Grid[,] gridHandler;
	public List<WalkerObject> Walkers;
	public Tilemap tileMap;
	public Tile floor;
	public Tile wall;
	public Tile enemy;
	public int enemyCount = 0;
	public int maxEnemyCount;
	public int mapWidth = 30;
	public int mapHeight = 30;
	
	public int maximumWalkers = 10;
	public int tileCount = default;
	public float fillPercentage = 0.4f;
	public float waitTime = 0.05f;
	
	void Start()
	{
		InitializeGrid();
		maxEnemyCount = Random.Range(5, 20);
	}
	
	void InitializeGrid()
	{
		gridHandler = new Grid[mapWidth, mapHeight];
		
		for (int x = 0; x < gridHandler.GetLength(0); x++)
		{
			for(int y = 0; y < gridHandler.GetLength(1); y++)
			{
				gridHandler[x, y] = Grid.EMPTY;
			}
		}
		
		Walkers = new List<WalkerObject>();
		
		Vector3Int tileCenter = new Vector3Int(gridHandler.GetLength(0) / 2, gridHandler.GetLength(1) / 2, 0);
		
		WalkerObject curWalker = new WalkerObject(new Vector2(tileCenter.x, tileCenter.y), GetDirection(), 0.5f);
		gridHandler[tileCenter.x, tileCenter.y] = Grid.FLOOR;
		tileMap.SetTile(tileCenter, floor);
		Walkers.Add(curWalker);
		
		tileCount++;
		
		StartCoroutine(CreateFloors());
	}
	
	Vector2 GetDirection()
	{
		int choice = Mathf.FloorToInt(UnityEngine.Random.value * 3.99f);
		
		switch (choice)
		{
			case 0:
				return Vector2.down;
			case 1:
				return Vector2.left;
			case 2:
				return Vector2.up;
			case 3:
				return Vector2.right;
			default:
				return Vector2.zero;
		}
	}
	
	IEnumerator CreateFloors()
	{
		while ((float)tileCount / (float)gridHandler.Length < fillPercentage)
		{
			bool hasCreatedFloor = false;
			foreach (WalkerObject curWalker in Walkers)
			{
				Vector3Int curPos = new Vector3Int((int)curWalker.position.x, (int)curWalker.position.y, 0);
				
				if(gridHandler[curPos.x, curPos.y] != Grid.FLOOR)
				{
					tileMap.SetTile(curPos, floor);
					tileCount++;
					gridHandler[curPos.x, curPos.y] = Grid.FLOOR;
					hasCreatedFloor = true;
				}
			}
			
			//Walker Methods
			ChanceToRemove();
			ChanceToRedirect();
			ChanceToCreate();
			UpdatePosition();
			
			if(hasCreatedFloor)
			{
				yield return new WaitForSeconds(waitTime);
			}
		}
		
		StartCoroutine(CreateWalls());
	}
	
	void ChanceToRemove()
	{
		int updatedCount = Walkers.Count;
		for(int i = 0; i < updatedCount; i++)
		{
			if(UnityEngine.Random.value < Walkers[i].chanceToChange && Walkers.Count > 1)
			{
				Walkers.RemoveAt(i);
				break;
			}
		}
	}
	
	void ChanceToRedirect()
	{
		for(int i = 0; i < Walkers.Count; i++)
		{
			if(UnityEngine.Random.value < Walkers[i].chanceToChange)
			{
				WalkerObject curWalker = Walkers[i];
				curWalker.direction = GetDirection();
				Walkers[i] = curWalker;
			}
		}
	}
	
	void ChanceToCreate()
	{
		int updatedCount = Walkers.Count;
		for(int i = 0; i < updatedCount; i++)
		{
			if(UnityEngine.Random.value < Walkers[i].chanceToChange && Walkers.Count < maximumWalkers)
			{
				Vector2 newDirection = GetDirection();
				Vector2 newPosition = Walkers[i].position;
				
				WalkerObject newWalker = new WalkerObject(newPosition, newDirection, 0.5f);
				Walkers.Add(newWalker);
			}
		}
	}
	
	void UpdatePosition()
	{
		for(int i = 0; i < Walkers.Count; i++)
		{
			WalkerObject foundWalker = Walkers[i];
			foundWalker.position += foundWalker.direction;
			foundWalker.position.x = Mathf.Clamp(foundWalker.position.x, 1, gridHandler.GetLength(0) - 2);
			foundWalker.position.y = Mathf.Clamp(foundWalker.position.y, 1, gridHandler.GetLength(1) - 2);
			Walkers[i] = foundWalker;
		}
	}
	
	IEnumerator CreateWalls()
	{
		for(int x = 0; x < gridHandler.GetLength(0) - 1; x++)
		{
			for(int y = 0; y < gridHandler.GetLength(1) - 1; y++)
			{
				if(gridHandler[x, y] == Grid.FLOOR)
				{
					bool hasCreatedWall = false;
					
					if(gridHandler[x + 1, y] == Grid.EMPTY)
					{
						tileMap.SetTile(new Vector3Int(x + 1, y, 0), wall);
						gridHandler[x + 1, y] = Grid.WALL;
						hasCreatedWall = true;
					}
					if(gridHandler[x - 1, y] == Grid.EMPTY)
					{
						tileMap.SetTile(new Vector3Int(x - 1, y, 0), wall);
						gridHandler[x - 1, y] = Grid.WALL;
						hasCreatedWall = true;
					}
					if(gridHandler[x, y + 1] == Grid.EMPTY)
					{
						tileMap.SetTile(new Vector3Int(x, y + 1, 0), wall);
						gridHandler[x, y + 1] = Grid.WALL;
						hasCreatedWall = true;
					}
					if(gridHandler[x, y - 1] == Grid.EMPTY)
					{
						tileMap.SetTile(new Vector3Int(x, y - 1, 0), wall);
						gridHandler[x, y - 1] = Grid.WALL;
						hasCreatedWall = true;
					}
					
					if(hasCreatedWall)
					{
						yield return new WaitForSeconds(waitTime);
					}
				}
			}
		}		
		StartCoroutine(CreateEnemies());
	}
	
	IEnumerator CreateEnemies()
	{
		for(int x = 0; x < gridHandler.GetLength(0); x++)
		{
			for(int y = 0; y < gridHandler.GetLength(1); y++)
			{
				if(gridHandler[x, y] == Grid.FLOOR && enemyCount < maxEnemyCount)
				{
					bool hasCreatedEnemy = false;
					tileMap.SetTile(new Vector3Int(x, y, 1), enemy);
					gridHandler[x, y] = Grid.ENEMY;
					hasCreatedEnemy = true;
					enemyCount++;
					
					if(hasCreatedEnemy)
					{
						yield return new WaitForSeconds(waitTime);
					}
				}
			}
		}
	}
}
