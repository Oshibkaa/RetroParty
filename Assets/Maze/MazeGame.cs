using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace qtools.qmaze.example4
{
    public class MazeGame : MonoBehaviour
    {
        public QMazeEngine mazeEngine;

        private Transform _ballController;
        private Rigidbody _ballControllerRB;

        public GameObject FinishTriggerPrefab;
        private bool _needGenerateNewMaze = true;

        private int _currentLevel = 0;

        private void Start()
        {
            _ballController = GameObjectManager.instance.allObjects[0].transform;
            _ballControllerRB = GameObjectManager.instance.allObjects[0].GetComponent<Rigidbody>();
        }

        void LateUpdate()
        {
            if (_needGenerateNewMaze)
            {
                _needGenerateNewMaze = false;
                GenerateNewMaze();
            }
        }

        public void NextGeneration()
        {
            _needGenerateNewMaze = true;
        }

        void GenerateNewMaze()
        {
            mazeEngine.destroyImmediateMazeGeometry();
            mazeEngine.generateMaze();

            List<QVector2IntDir> finishPointList = mazeEngine.getFinishPositionList();
            for (int i = 0; i < finishPointList.Count; i++)
            {
                QVector2IntDir finishPosition = finishPointList[i];
                GameObject finishTriggerInstance = (GameObject)GameObject.Instantiate(FinishTriggerPrefab);
                finishTriggerInstance.transform.parent = mazeEngine.transform;
                finishTriggerInstance.transform.localPosition = new Vector3(
                    finishPosition.x * mazeEngine.getMazePieceWidth(), -0.669f,
                    -finishPosition.y * mazeEngine.getMazePieceHeight());
            }

            FinishTrigger[] finishTriggerArray = FindObjectsOfType<FinishTrigger>();
            if (finishTriggerArray != null)
            {
                for (int i = 0; i < finishTriggerArray.Length; i++)
                    finishTriggerArray[i].triggerHandlerEvent += FinishHandler;
            }

            List<QVector2IntDir> startPointList = mazeEngine.getStartPositionList();

            if (_ballController != null)
            {
                if (startPointList.Count == 0)
                {
                    _ballController.gameObject.transform.position = new Vector3(0, 0.2f, 0);
                }
                else
                {
                    QVector2IntDir startPoint = startPointList[0];
                    _ballController.position = new Vector3(startPoint.x * mazeEngine.getMazePieceWidth(), 0.2f,
                        -startPoint.y * mazeEngine.getMazePieceHeight());
                    _ballControllerRB.isKinematic = false;
                }
            }

            _currentLevel++;
        }


        void FinishHandler()
        {
            _ballControllerRB.isKinematic = true;
        }
    }
}