using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MethodGenerator
{
    public class ReWriteMethodInfo
    {
        public ReWriteMethodInfo(
            ParameterListSyntax addedParameters,
            IEnumerable<ExpressionSyntax> lambdaParameters,
            TypeParameterListSyntax generics)
        {
            AddedParameters = addedParameters;
            LambdaParameters = lambdaParameters;
            Generics = generics;
        }

        public ParameterListSyntax AddedParameters { get; }

        public IEnumerable<ExpressionSyntax> LambdaParameters { get; }

        public TypeParameterListSyntax Generics { get; set; }
    }
}