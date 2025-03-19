using CalculatorWPF;
using System.Collections.Generic;

namespace CalculatorTests
{
    [TestClass]
    public sealed class CalculatorTests
    {
        [TestMethod]
        public void InfixExpression_GeneratesValidPostfixExpression()
        {
            //Arrange
            List<string> infixExpr = { "100", "X", "99", "-", "3", "/", "57", "+", "0,543" };
            List<string> expectedPostfixExpr = { "100", "99", "X", "3", "5", "7", "/", "-", "0,543", "+" };
            Calculator TestCalculator = new Calculator();
            TestCalculator.InfixExpression = infixExpr;

            //Act
            TestCalculator.ConvertToPostfix(infixExpr);

            //Assert
            List<string> actualExpr = TestCalculator._postfixExpr;
            Assert.AreEqual(expectedPostfixExpr, actualExpr, "Expression did not generate correctly");


        }
    }
}
