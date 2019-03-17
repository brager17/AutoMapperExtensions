using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MethodGenerator
{
    public class ReWriteMethodInfo
    {
        public ReWriteMethodInfo(
            ParameterListSyntax addedParameters,
            BlockSyntax Block,
            TypeParameterListSyntax generics)
        {
            AddedParameters = addedParameters;
            this.Block = Block;
            Generics = generics;
        }

        public ParameterListSyntax AddedParameters { get; }

        public BlockSyntax Block { get; }

        public TypeParameterListSyntax Generics { get; }
    }
}