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
            var mouseCutter = new GameObject("Mouse Cutter", typeof(BoxCutter));
            mouseCutter.transform.position = Vector3.zero;

            // Act
            mouseCutter.GetComponent<BoxCutter>().SetPosition(Vector2.right);

            // Assert
            Assert.AreEqual(Vector2.right, (Vector2) mouseCutter.transform.position);
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
            var boxGO = ArrangeBox(kinematic: true, trigger: true);
            var mouseCollider = ArrangeMouse(new Vector2(-2, 0), true);
            // Act
            yield return boxGO.GetComponent<Box>()
                .StartCoroutine(MoveMouse(1, mouseCollider.transform, new Vector2(-2, 0), new Vector2(2, 0)));

            // Assert
            Assert.IsTrue(boxGO.GetComponent<Box>().wasCut);
        }

        [UnityTest]
        public IEnumerator CutBox_MouseSuccessfulCut()
        {
            // Arrange
            var boxGO = ArrangeBox(5, Vector2.right, kinematic: true, trigger: true);
            var mouseCollider = ArrangeMouse(new Vector2(-2, 0), true);
            // Act
            yield return boxGO.GetComponent<Box>()
                .StartCoroutine(MoveMouse(1, mouseCollider.transform, new Vector2(-2, 0), new Vector2(2, 0)));

            // Assert
            Assert.IsTrue(boxGO.GetComponent<Box>().wasCut);
            // Assert.IsTrue(boxGO.GetComponent<Box>().SuccessfulCut());
        }

        [UnityTest]
        public IEnumerator CutBox_MouseUnsuccessfulCut()
        {
            // Arrange
            var go = ArrangeBox(5, Vector2.left, true, true);
            var mouseCollider = ArrangeMouse(new Vector2(-2, 0), true);
            go.transform.position = Vector2.zero;

            // Act
            yield return go.GetComponent<Box>()
                .StartCoroutine(MoveMouse(1, mouseCollider.transform, new Vector2(-2, 0), new Vector2(2, 0)));

            // Assert
            Assert.IsFalse(go.GetComponent<Box>().SuccessfulCut());
        }

        private IEnumerator MoveMouse(float moveTime = 2f, Transform transform = null, Vector2 start = new Vector2(),
            Vector2 end = new Vector2())
        {
            var movedTime = 0f;
            while (movedTime < moveTime)
            {
                var fraction = movedTime / moveTime;
                transform.position = Vector2.Lerp(start, end, fraction);
                movedTime += Time.deltaTime;
                yield return null;
            }
        }

        private GameObject ArrangeMouse(Vector2 position = new Vector2(), bool trigger = false)
        {
            var mouseCollider = new GameObject("Mouse Collider", typeof(BoxCollider2D));
            mouseCollider.GetComponent<BoxCollider2D>().isTrigger = trigger;
            mouseCollider.transform.position = position;
            return mouseCollider;
        }

        private GameObject ArrangeBox(float threshold = 0f, Vector2 targetDirection = new Vector2(),
            bool kinematic = false, bool trigger = false)
        {
            var boxGO = new GameObject("Box", typeof(Box), typeof(BoxCollider2D), typeof(Rigidbody2D));
            boxGO.GetComponent<Rigidbody2D>().isKinematic = kinematic;
            boxGO.GetComponent<BoxCollider2D>().isTrigger = trigger;
            boxGO.GetComponent<Box>().ThresholdAngle = threshold;
            boxGO.GetComponent<Box>().TargetDirection = targetDirection;
            boxGO.transform.position = Vector2.zero;
            return boxGO;
        }
    }
}