using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class GlobalStats : ScriptableObject
{
    public float scrollSpeed;
    public int playerLifes;
    public Vector2 playerPosition;

    public void SmallEnemyDead(GameObject enemy)
    {
        Debug.Log(enemy);
    }

    public void AnimacaoDeMorte(Vector2 posicao)
    {
        //criar um objeto vazio que passe a animação de morte
        Debug.Log(posicao.x);
    }

}
