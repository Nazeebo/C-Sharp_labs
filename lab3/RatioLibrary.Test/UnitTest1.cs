using System;
using RatioLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RatioLibrary.Test {
    [TestClass]
    public class UnitTest1 {
        private Ratio r1,r2;
        [TestMethod]
        public void Test_CreateRatio() {
            r1 = new Ratio(1, 2);
            Assert.AreEqual(0.5, r1.ToDouble());
        }

        [TestMethod]
        public void Test_ZeroDenominatorException_atCreation() {
            Action action = () => r1 = new Ratio(1, 0);

            Assert.ThrowsException<DenominatorException>(action);
        }

        [TestMethod]
        public void Test_ZeroDenominatorException_atExistence() {
            Action action = () => {
                r1 = new Ratio(1, 1);
                r2 = new Ratio(0, 1);
                double result = (r1 / r2).ToDouble();
            };

            Assert.ThrowsException<DenominatorException>(action);
        }

        [TestMethod]
        public void Test_CorrectnessOfReductions() {
            r1 = new Ratio(24,48);
            r2 = new Ratio(1, 2);
            Assert.AreEqual(r2.ToString(), r1.ToString());
        }

        [TestMethod]
        public void Test_CorrectnessOfAddition() {
            r1 = new Ratio(1, 2);
            r2 = new Ratio(1, 4);

            Assert.AreEqual(new Ratio(3,4).ToString(), (r1 + r2).ToString());
        }

        [TestMethod]
        public void Test_CorrectnessOfDeduction() {
            r1 = new Ratio(1, 2);
            r2 = new Ratio(1, 4);

            Assert.AreEqual(new Ratio(1,4).ToString(), (r1 - r2).ToString());
        }

        [TestMethod]
        public void Test_CorrectnessOfMultiply() { 
            r1 = new Ratio(1, 2);
            r2 = new Ratio(1, 4);

            Assert.AreEqual(new Ratio(1,8).ToString(), (r1 * r2).ToString());
        }

        [TestMethod]
        public void Test_CorrectnessOfDivision() {
            r1 = new Ratio(1, 2);
            r2 = new Ratio(1, 4);

            Assert.AreEqual(new Ratio(2,1).ToString(), (r1 / r2).ToString());
        }
    }
}
