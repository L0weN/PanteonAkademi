using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PA.WeaponSystem;

[CreateAssetMenu(menuName = "Attacks/DefaultAttack")]
public class DefaultAttackSO : AttackPatternSO
{
	public GameObject projectile;
	public override void Perform(Transform shootingStartPoint)
	{
		Instantiate(projectile, shootingStartPoint.position, shootingStartPoint.rotation);
	}
}
