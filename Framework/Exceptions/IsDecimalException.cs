﻿using System;
using System.Linq.Expressions;
using JJ.Framework.Reflection;

namespace JJ.Framework.Exceptions
{
    public class IsDecimalException : Exception
    {
        private const string MESSAGE = "{0} should not be a Decimal.";

        public IsDecimalException(Expression<Func<object>> expression)
            : base(String.Format(MESSAGE, ExpressionHelper.GetText(expression)))
        { }
    }
}