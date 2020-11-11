using System.Collections;
using UnityEngine;

public class BuildWalls : MonoBehaviour 
{
	// Public properties for the brick prefab and light prefab.
	public GameObject brickPrefab;
	public GameObject lightPrefab;
	// Public properties for the delays and times associated.
	public float delay;
	public float lightTime;
	public bool lighting = true;

	void Start() 
	{
		// Since a building visual sequence is wanted for each 
		// wall, creating one brick at a time, coroutines are
		// used; first one is to build the north wall.
		StartCoroutine("BuildNorthWall");
	}
	
	IEnumerator BuildNorthWall()
	{
		// The brick prefab has dimensions of (x: 0.5, y: 0.3, z: 0.2),
		// so the values used in this code are pre-calculated according
		// to the wanted width, height and position of the wall.
		float offset = 0f;
		float z = 24.9f;
		// Each iteration of this cycle creates a row of bricks.
		for (float y = 0.15f; y <= 2.05f; y += 0.3f)
		{
			// Each iteration of the cycle creates a single brick in the same row.
			for (float x = -24.75f + offset; x <= 24.75f; x += 0.5f)
			{
				GameObject brick = Instantiate(brickPrefab, new Vector3(x, y, z), Quaternion.identity);
				brick.name = "Brick";
				yield return new WaitForSeconds(delay);
			}
			// Offset defines the position of bricks in one row relative
			// to the adjacent rows (even rows are displaced an offset distance 
			// respect to odd rows).
			offset = (offset == 0f ? 0.25f : 0f);
		}
		// On finish the north wall, next coroutine is to build the east wall.
		yield return StartCoroutine("BuildEastWall");
	}

	IEnumerator BuildEastWall()
	{
		// The brick prefab has dimensions of (x: 0.5, y: 0.3, z: 0.2),
		// so the values used in this code are pre-calculated according
		// to the wanted width, height and position of the wall.
		float offset = 0.25f;
		float x = 24.9f;
		// Each iteration of this cycle creates a row of bricks.
		for (float y = 0.15f; y <= 2.05f; y += 0.3f)
		{
			// Each iteration of the cycle creates a single brick in the same row.
			for (float z = 24.75f - offset; z >= -24.75f; z -= 0.5f)
			{
				GameObject brick = Instantiate(brickPrefab, new Vector3(x, y, z), Quaternion.Euler(new Vector3(0f,90f,0f)));
				brick.name = "Brick";
				yield return new WaitForSeconds(delay);
			}
			// Offset defines the position of bricks in one row relative
			// to the adjacent rows (even rows are displaced an offset distance 
			// respect to odd rows).
			offset = (offset == 0f ? 0.25f : 0f);
		}
		// On finish the east wall, next coroutine is to build the west wall.
		yield return StartCoroutine("BuildWestWall");
	}

	IEnumerator BuildWestWall()
	{
		// The brick prefab has dimensions of (x: 0.5, y: 0.3, z: 0.2),
		// so the values used in this code are pre-calculated according
		// to the wanted width, height and position of the wall.
		float offset = 0.25f;
		float x = -24.9f;
		// Each iteration of this cycle creates a row of bricks.
		for (float y = 0.15f; y <= 2.05f; y += 0.3f)
		{
			// Each iteration of the cycle creates a single brick in the same row.
			for (float z = 24.75f - offset; z >= -24.75f; z -= 0.5f)
			{
				GameObject brick = Instantiate(brickPrefab, new Vector3(x, y, z), Quaternion.Euler(new Vector3(0f,90f,0f)));
				brick.name = "Brick";
				yield return new WaitForSeconds(delay);
			}
			// Offset defines the position of bricks in one row relative
			// to the adjacent rows (even rows are displaced an offset distance 
			// respect to odd rows).
			offset = (offset == 0f ? 0.25f : 0f);
		}
		// On finish the west wall, next coroutine is to handle lighting.
		yield return StartCoroutine("LightsUp");
	}

	IEnumerator LightsUp()
	{
		// Gets an array of all objets tagged as "Brick". 
		GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
		GameObject selected;
		GameObject light;

		// Cycle to handle lighting.
		while (lighting)
		{
			// Selects a random brick from any of the walls.
			selected = bricks[Random.Range(0, bricks.Length)];
			// "Turn off" the selected brick (it becomes invisible).
			selected.SetActive(false);
			// Creates a light from prefab on the selected brick position.
			light = Instantiate(lightPrefab, selected.transform.position, Quaternion.identity);
			// Creates a delay.
			yield return new WaitForSeconds(lightTime);
			// Destroys the light.
			Destroy(light);
			// "Turn on" the selected brick (it becomes visible again).
			selected.SetActive(true);
		}
	}
}
