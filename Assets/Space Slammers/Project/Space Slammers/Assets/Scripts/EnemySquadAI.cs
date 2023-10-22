using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySquadAI : MonoBehaviour
{
    private List<GameObject> Enemies = new List<GameObject>();
    private List<double> EnemyY = new List<double>();
    private bool Entered = false;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform Member in transform)
        {
            if (Member.GetComponent<EnemyAI>() && Member.GetComponent<EnemyAI>().ai_id != 2 && Member.GetComponent<EnemyAI>().ai_id != 3 && Member.GetComponent<EnemyAI>().ai_id != 4)
            Enemies.Add(Member.gameObject);
            EnemyY.Add(0);
        }
        foreach (GameObject Enemy in Enemies)
        {
            Enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-10);
        }
    }

    bool HasDescended()
    {
        foreach (GameObject Enemy in Enemies)
        {
            if (Enemy.transform.position.y > 11)
            {
                return false;
            }
        }
        return true;
    }

    bool NoMoreEnemies()
    {
        foreach (Transform Member in transform)
        {
            if (Member.GetComponent<EnemyAI>())
                return false;
        }
        return true;
    }

    int EdgeCollision()
    {
        foreach (GameObject Enemy in Enemies)
        {
            if (Enemy.transform.position.x >= 15 && Enemy.GetComponent<Rigidbody2D>().velocity.x == 1)
            {
                return 1; // It is colliding with the right! Go Down!
            }
            if (Enemy.transform.position.x <= -15 && Enemy.GetComponent<Rigidbody2D>().velocity.x == -1)
            {
                return 2; // It is colliding with the left! Go Down!
            }
            if (Enemy.transform.position.x >= 0 && Enemy.GetComponent<Rigidbody2D>().velocity.y == -1 && (Mathf.Round(Enemy.transform.position.y * 100)) / 100.0 == EnemyY[Enemies.IndexOf(Enemy)])
            {
                return 3; // It has gone down far enough. Go Left!
            }
            if (Enemy.transform.position.x < 0 && Enemy.GetComponent<Rigidbody2D>().velocity.y == -1 && (Mathf.Round(Enemy.transform.position.y * 100)) / 100.0 == EnemyY[Enemies.IndexOf(Enemy)])
            {
                return 4; // It has gone down far enough. Go Right!
            }
        }
        return 0; // Theres no collisions detected. Keep going!
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = Enemies.Count - 1; i >= 0; i--)
        {
            if(Enemies[i] == null)
            {
                Enemies.Remove(Enemies[i]);
                EnemyY.Remove(EnemyY[i]);
            }
        }
        if (Enemies.Count > 0)
        {
            if (Entered == false)
            {
                if (HasDescended() == true)
                {
                    Entered = true;
                    foreach (GameObject Enemy in Enemies)
                    {
                        Enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
                        EnemyY[Enemies.IndexOf(Enemy)] = Mathf.Round(Enemy.transform.position.y * 100) / 100.0;
                    }
                }
            }
            else
            {
                int Status = EdgeCollision();
                switch (Status)
                {
                    case 1:
                    case 2:
                        foreach (GameObject Enemy in Enemies)
                        {
                            Enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);
                            EnemyY[Enemies.IndexOf(Enemy)] -= 1.5;
                        }
                        break;
                    case 3:
                        foreach (GameObject Enemy in Enemies)
                        {
                            Enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
                        }
                        break;
                    case 4:
                        foreach (GameObject Enemy in Enemies)
                        {
                            Enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            if (NoMoreEnemies() == true)
            {
                Destroy(gameObject);
            }
        }
    }
}
