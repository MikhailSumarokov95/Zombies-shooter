﻿//Copyright 2022, Infima Games. All Rights Reserved.

using UnityEngine;
using System.Collections;
using InfimaGames.LowPolyShooterPack;
using Random = UnityEngine.Random;

namespace InfimaGames.LowPolyShooterPack.Legacy
{
	public class Projectile : MonoBehaviour
	{
		[SerializeField] private int damage = 5;

		[Range(5, 100)]
		[Tooltip("After how long time should the bullet prefab be destroyed?")]
		public float destroyAfter;

		[Tooltip("If enabled the bullet destroys on impact")]
		public bool destroyOnImpact = false;

		[Tooltip("Minimum time after impact that the bullet is destroyed")]
		public float minDestroyTime;

		[Tooltip("Maximum time after impact that the bullet is destroyed")]
		public float maxDestroyTime;

		[Header("Impact Effect Prefabs")]
		public Transform[] bloodImpactPrefabs;

		public Transform[] metalImpactPrefabs;
		public Transform[] dirtImpactPrefabs;
		public Transform[] concreteImpactPrefabs;

		private void Start()
		{
			//Grab the game mode service, we need it to access the player character!
			var gameModeService = ServiceLocator.Current.Get<IGameModeService>();
			//Ignore the main player character's collision. A little hacky, but it should work.
			Physics.IgnoreCollision(gameModeService.GetPlayerCharacter().GetComponent<Collider>(),
				GetComponent<Collider>());

			//Start destroy timer
			StartCoroutine(DestroyAfter());
		}

        //If the bullet collides with anything
        private void OnCollisionEnter(Collision collision)
		{
			//Ignore collisions with other projectiles.
			if (collision.gameObject.GetComponent<Projectile>() != null)
				return;

			// //Ignore collision if bullet collides with "Player" tag
			// if (collision.gameObject.CompareTag("Player")) 
			// {
			// 	//Physics.IgnoreCollision (collision.collider);
			// 	Debug.LogWarning("Collides with player");
			// 	//Physics.IgnoreCollision(GetComponent<Collider>(), GetComponent<Collider>());
			//
			// 	//Ignore player character collision, otherwise this moves it, which is quite odd, and other weird stuff happens!
			// 	Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
			//
			// 	//Return, otherwise we will destroy with this hit, which we don't want!
			// 	return;
			// }
			//
			//If destroy on impact is false, start 
			//coroutine with random destroy timer
			if (!destroyOnImpact)
			{
				StartCoroutine(DestroyTimer());
			}
			//Otherwise, destroy bullet on impact
			else
			{
				Destroy(gameObject);
			}

			if (collision.transform.CompareTag("Enemy"))
			{
				collision.transform.gameObject.GetComponent
					<HealthPoints>().TakeDamage(damage);

				Instantiate(bloodImpactPrefabs[Random.Range
						(0, bloodImpactPrefabs.Length)], transform.position,
					Quaternion.LookRotation(collision.contacts[0].normal));

				Destroy(gameObject);
			}

			//If bullet collides with "Blood" tag
			else if (collision.transform.CompareTag("Blood"))
			{
				//Instantiate random impact prefab from array
				Instantiate(bloodImpactPrefabs[Random.Range
						(0, bloodImpactPrefabs.Length)], transform.position,
					Quaternion.LookRotation(collision.contacts[0].normal));
				//Destroy bullet object
				Destroy(gameObject);
			}

			//If bullet collides with "Metal" tag
			else if (collision.transform.CompareTag("Metal"))
			{
				//Instantiate random impact prefab from array
				Instantiate(metalImpactPrefabs[Random.Range
						(0, bloodImpactPrefabs.Length)], transform.position,
					Quaternion.LookRotation(collision.contacts[0].normal));
				//Destroy bullet object
				Destroy(gameObject);
			}

			//If bullet collides with "Dirt" tag
			else if (collision.transform.CompareTag("Dirt"))
			{
				//Instantiate random impact prefab from array
				Instantiate(dirtImpactPrefabs[Random.Range
						(0, bloodImpactPrefabs.Length)], transform.position,
					Quaternion.LookRotation(collision.contacts[0].normal));
				//Destroy bullet object
				Destroy(gameObject);
			}

			//If bullet collides with "Concrete" tag
			else if (collision.transform.CompareTag("Concrete"))
			{
				//Instantiate random impact prefab from array
				Instantiate(concreteImpactPrefabs[Random.Range
						(0, bloodImpactPrefabs.Length)], transform.position,
					Quaternion.LookRotation(collision.contacts[0].normal));
				//Destroy bullet object
				Destroy(gameObject);
			}

			//If bullet collides with "Target" tag
			else if (collision.transform.CompareTag("Target"))
			{
				//Toggle "isHit" on target object
				collision.transform.gameObject.GetComponent
					<TargetScript>().isHit = true;
				//Destroy bullet object
				Destroy(gameObject);
			}

			//If bullet collides with "ExplosiveBarrel" tag
			else if (collision.transform.CompareTag("ExplosiveBarrel"))
			{
				//Toggle "explode" on explosive barrel object
				collision.transform.gameObject.GetComponent
					<ExplosiveBarrelScript>().explode = true;
				//Destroy bullet object
				Destroy(gameObject);
			}

			//If bullet collides with "GasTank" tag
			else if (collision.transform.CompareTag("GasTank"))
			{
				//Toggle "isHit" on gas tank object
				collision.transform.gameObject.GetComponent
					<GasTankScript>().isHit = true;
				//Destroy bullet object
				Destroy(gameObject);
			}
		}

		private IEnumerator DestroyTimer()
		{
			//Wait random time based on min and max values
			yield return new WaitForSeconds
				(Random.Range(minDestroyTime, maxDestroyTime));
			//Destroy bullet object
			Destroy(gameObject);
		}

		private IEnumerator DestroyAfter()
		{
			//Wait for set amount of time
			yield return new WaitForSeconds(destroyAfter);
			//Destroy bullet object
			Destroy(gameObject);
		}
	}
}