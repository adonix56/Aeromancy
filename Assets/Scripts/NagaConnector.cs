using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NagaConnector : MonoBehaviour
{
    [SerializeField] private SnakeNaga snakeNagaParent;

    public SnakeNaga GetSnakeNaga() {
        return snakeNagaParent;
    }
}
