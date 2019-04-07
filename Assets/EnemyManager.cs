using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
  public bool isBossUnlocked = false;

  int countTrashEnemies;

  private Listener listener;

  public void SetListener(Listener listener)
  {
    this.listener = listener;
  }

  public void IncrementTrashEnemies()
  {
    this.countTrashEnemies++;
  }

  public void DecrementTrashEnemies()
  {
    this.countTrashEnemies--;
    if (countTrashEnemies == 0)
    {
      DoBossUnlock();
    }
  }

  public bool IsBossUnlocked()
  {
    return isBossUnlocked;
  }

  private void DoBossUnlock()
  {
    isBossUnlocked = true;
    listener.OnBossUnlocked();
  }

  public interface Listener
  {
    void OnBossUnlocked();
  }
}
