using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
  [SerializeField] int damage = 10;

  public int GetDamage(){
      return damage;
  }

  public void Hit(){
      Destroy(gameObject);
  }
}
