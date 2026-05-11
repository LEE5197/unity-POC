using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefaultAttack : MonoBehaviour
{
	public float atkTimer = 0f;
	public float coolDown = 10f;
    public float atkRange = 6f;
	public int atkDamage = 1;
	[Space]
	public LayerMask enemyLayer;

	[Space]
	public GameObject elecLinePrefab;
	public GameObject elecParticlePrefab;

	private List<ElectricLine> elecLine;
	private List<ParticleSystem> particle;
	private int maxTarget = 10;
	private int cur = 0;

	private void Awake()
	{
		elecLine = new List<ElectricLine>();
		particle = new List<ParticleSystem>();

		for(int i = 0; i < maxTarget; i++)
		{
			GameObject obj = Instantiate(elecLinePrefab);
			obj.SetActive(false);
			elecLine.Add(obj.GetComponent<ElectricLine>());

			obj = Instantiate(elecParticlePrefab);
			particle.Add(obj.GetComponent<ParticleSystem>());
		}
	}
	private void Update()
	{
		if (atkTimer > coolDown) Attack();
		atkTimer += Time.deltaTime;

	}

	private void Attack()
	{
		atkTimer = 0f;
		Collider2D[] hits = Physics2D.OverlapCircleAll((Vector2)transform.position, atkRange, enemyLayer);
		

		foreach(var hit in hits)	// EnemyController 스크립트 만들고 여기다가 넣어서 데미지 루틴 실행하면 됨
		{
			elecLine[cur].DrawElectric(transform.position, hit.gameObject.transform.position);
			particle[cur].gameObject.transform.position = hit.transform.position;
			particle[cur].Play();

			cur++;
			cur %= maxTarget;
		}
	}


	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		Gizmos.DrawWireSphere(transform.position, atkRange);
	}
}
