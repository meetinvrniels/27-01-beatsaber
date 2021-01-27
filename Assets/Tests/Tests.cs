using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void MouseCutter_MoveMouse()
        {
            // Arrange
            var mouseCutter = new GameObject("Mouse Cutter", typeof(MouseCutter));
            mouseCutter.transform.position = Vector3.zero;

            // Act
            mouseCutter.GetComponent<MouseCutter>().SetPosition(Vector2.right);

            // Assert
            Assert.AreEqual(Vector2.right, (Vector2)mouseCutter.transform.position);
        }
        [Test]
        public void SpawnBox_ExpectInScene()
        {
            // Arrange
            var spawnerPrefab = Resources.Load("Prefabs/Box Spawner") as GameObject;
            var spawnerGo = GameObject.Instantiate(spawnerPrefab);
            var spawner = spawnerGo.GetComponent<BoxSpawner>();

            // Act
            spawner.SpawnBox();
            var spawnedBox = GameObject.FindObjectOfType<Box>();

            // Assert
            Assert.True(spawnedBox != null);
        }
        [UnityTest]
        public IEnumerator CutBox_MouseDownDraggedThrough()
        {
            // Arrange
            var go = new GameObject("Box", typeof(Box), typeof(BoxCollider2D), typeof(Rigidbody2D));
            go.GetComponent<Rigidbody2D>().isKinematic = true;
            go.GetComponent<BoxCollider2D>().isTrigger = true;
            var mouseCollider = new GameObject("Mouse Collider",  typeof(BoxCollider2D));
            mouseCollider.GetComponent<BoxCollider2D>().isTrigger = true;
            mouseCollider.transform.position = new Vector3(-2, 0);
            go.transform.position = Vector3.zero;

            // Act
            var moveTime = 2f;
            var movedTime = 0f;
            while (movedTime<moveTime)
            {
                var fraction = movedTime / moveTime;
                mouseCollider.transform.position = Vector3.Lerp(new Vector3(-2, 0), new Vector3(2, 0), fraction);
                movedTime += Time.deltaTime;
                yield return null;
            }

            // Assert
            Assert.IsTrue(go.GetComponent<Box>().wasCut);
        }
        [UnityTest]
        public IEnumerator CutBox_MouseSuccessfulCut()
        {
            // Arrange
            var go = new GameObject("Box", typeof(Box), typeof(BoxCollider2D), typeof(Rigidbody2D));
            go.GetComponent<Rigidbody2D>().isKinematic = true;
            go.GetComponent<BoxCollider2D>().isTrigger = true;
            go.GetComponent<Box>().TargetDirection = Vector2.right;
            go.GetComponent<Box>().ThresholdAngle = 5f;
            var mouseCollider = new GameObject("Mouse Collider",  typeof(BoxCollider2D));
            mouseCollider.GetComponent<BoxCollider2D>().isTrigger = true;
            mouseCollider.transform.position = new Vector2(-2, 0);
            go.transform.position = Vector2.zero;

            // Act
            var moveTime = 2f;
            var movedTime = 0f;
            while (movedTime<moveTime)
            {
                var fraction = movedTime / moveTime;
                mouseCollider.transform.position = Vector2.Lerp(new Vector2(-2, 0), new Vector2(2, 0), fraction);
                movedTime += Time.deltaTime;
                yield return null;
            }

            // Assert
            Assert.IsTrue(go.GetComponent<Box>().SuccessfulCut());
        }
        [UnityTest]
        public IEnumerator CutBox_MouseUnsuccessfulCut()
        {
            // Arrange
            var go = new GameObject("Box", typeof(Box), typeof(BoxCollider2D), typeof(Rigidbody2D));
            go.GetComponent<Rigidbody2D>().isKinematic = true;
            go.GetComponent<BoxCollider2D>().isTrigger = true;
            go.GetComponent<Box>().TargetDirection = Vector2.left;
            go.GetComponent<Box>().ThresholdAngle = 5f;
            var mouseCollider = new GameObject("Mouse Collider",  typeof(BoxCollider2D));
            mouseCollider.GetComponent<BoxCollider2D>().isTrigger = true;
            mouseCollider.transform.position = new Vector2(-2, 0);
            go.transform.position = Vector2.zero;

            // Act
            var moveTime = 2f;
            var movedTime = 0f;
            while (movedTime<moveTime)
            {
                var fraction = movedTime / moveTime;
                mouseCollider.transform.position = Vector2.Lerp(new Vector2(-2, 0), new Vector2(2, 0), fraction);
                movedTime += Time.deltaTime;
                yield return null;
            }

            // Assert
            Assert.IsFalse(go.GetComponent<Box>().SuccessfulCut());
        }
    }
}