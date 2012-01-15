﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NHibernate.OData.Test.Parser
{
    [TestFixture]
    internal class Logicals : ParserTestFixture
    {
        [Test]
        public void And()
        {
            Verify(
                "true and true",
                new BoolExpression(Operator.And, TrueLiteral, TrueLiteral)
            );

            VerifyThrows("true and");
        }

        [Test]
        public void Or()
        {
            Verify(
                "true or true",
                new BoolExpression(Operator.Or, TrueLiteral, TrueLiteral)
            );

            VerifyThrows("true or");
        }

        [Test]
        public void Nested()
        {
            Verify(
                "true or true and true",
                new BoolExpression(
                    Operator.Or,
                    TrueLiteral,
                    new BoolExpression(Operator.And, TrueLiteral, TrueLiteral)
                )
            );
        }

        [Test]
        public void NestedWithParensLeft()
        {
            Verify(
                "(true and true) or true",
                new BoolExpression(
                    Operator.Or,
                    new BoolParenExpression(
                        new BoolExpression(Operator.And, TrueLiteral, TrueLiteral)
                    ),
                    TrueLiteral
                )
            );
        }

        [Test]
        public void NestedWithParensRight()
        {
            Verify(
                "true or (true and true)",
                new BoolExpression(
                    Operator.Or,
                    TrueLiteral,
                    new BoolParenExpression(
                        new BoolExpression(Operator.And, TrueLiteral, TrueLiteral)
                    )
                )
            );
        }
    }
}