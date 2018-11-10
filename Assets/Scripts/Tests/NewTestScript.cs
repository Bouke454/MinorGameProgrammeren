using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestCountDownTimer {
    private float mainTimer = 0;
    private float timer;
    private bool canCount = true;
    private bool doOnce = false;
    private bool FinishedLevel = false;



    [Test]
    public void CountDownTest() {
        timer = mainTimer;
        if (timer >= 0.0f && canCount && FinishedLevel == false) {
            timer -= Time.deltaTime;
            Assert.Fail();
        } else if (timer <= 0.0f && !doOnce) {
            Assert.Pass("Test behaald de tijd is voorbij de speler is af");
        }
    }

    //// A UnityTest behaves like a coroutine in PlayMode
    //// and allows you to yield null to skip a frame in EditMode
    //[UnityTest]
    //public IEnumerator NewTestScriptWithEnumeratorPasses() {
    //    // Use the Assert class to test conditions.
    //    // yield to skip a frame
    //    yield return null;
    //}
}
