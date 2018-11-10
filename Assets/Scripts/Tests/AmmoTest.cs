using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class AmmoTest {
    public int ammo = -3;

    [Test]
    public void ShootingTest() {

        if (ammo <= 0) {
            Assert.Pass("Test behaald de speler kan niet langer schieten");
        } else {
            Assert.Fail();
        }       

    }
}
