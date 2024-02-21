using System.Collections;
using UnityEngine;

public class SpawnerEnablerScript : MonoBehaviour
{
    public GameObject wave1SpawnerObject;
    public GameObject wave2SpawnerObject;
    public GameObject archwayBlocker;
    public GameObject specificArchway;

    public GameObject wave1Icon;
    public GameObject wave1Text;
    public GameObject wave2Icon;
    public GameObject wave2Text;
    public GameObject wavesCompletedIcon;
	public static int enemiesNeeded;
	public static int wave = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            wave = 0;
			StartCoroutine(Wave1());
        }
    }
	
	private void Update()
	{
		if(enemiesNeeded == 0 && wave == 1)
		{
			StartCoroutine(Wave2());
			wave++;
		}
		
		if(enemiesNeeded == 0 && wave == 2)
		{
			nextRoom();
		}
	}

    private IEnumerator Wave1()
    {
        // Disable collider
		wave++;
        GetComponent<Collider2D>().enabled = false;

        if (wave1SpawnerObject != null)
        {
            EnemySpawner wave1Spawner = wave1SpawnerObject.GetComponent<EnemySpawner>();
			enemiesNeeded = EnemySpawner.numberOfEnemiesToSpawn;
			
			wavesCompletedIcon.SetActive(false);

            wave1Icon.SetActive(true);
            wave1Text.SetActive(true);
			archwayBlocker.SetActive(true);

            yield return new WaitForSeconds(3f);

            Debug.Log("wave "+wave);

            wave1Spawner.SpawnEnemies();
            wave1SpawnerObject.SetActive(true);
			Debug.Log("Kill "+enemiesNeeded+" enemies");
		}
	}
	
	private IEnumerator Wave2()
	{
		wave1Icon.SetActive(false);
		wave2Icon.SetActive(true);
		wave1Text.SetActive(false);
		EnemySpawner wave2Spawner = wave2SpawnerObject.GetComponent<EnemySpawner>();
		enemiesNeeded = EnemySpawner.numberOfEnemiesToSpawn;
			
		yield return new WaitForSeconds(0.1f);
			
			
        wave2Text.SetActive(true);
        Debug.Log("wave "+wave);
        wave2Spawner.SpawnEnemies();
        wave2SpawnerObject.SetActive(true);
            

        wave1Icon.SetActive(false);
	}
	
	private void nextRoom()
	{
		archwayBlocker.SetActive(false);
		wave2Icon.SetActive(false);
        wave2Text.SetActive(false);
		wavesCompletedIcon.SetActive(true);


            if (specificArchway != null)
            {
                AffectedArchwayController affectedArchwayController = specificArchway.GetComponent<AffectedArchwayController>();
                if (affectedArchwayController != null)
                {
                    affectedArchwayController.SetWavesCompleted(true);
                    affectedArchwayController.OpenArchway();
                }
            }
	}
}
