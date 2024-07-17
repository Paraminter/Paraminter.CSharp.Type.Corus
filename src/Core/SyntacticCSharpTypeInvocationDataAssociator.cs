﻿namespace Paraminter.CSharp.Type.Corus;

using Paraminter.Associators.Queries;
using Paraminter.CSharp.Type.Corus.Queries;
using Paraminter.CSharp.Type.Queries.Collectors;
using Paraminter.Queries.Handlers;

using System;

/// <summary>Associates syntactic C# type arguments.</summary>
public sealed class SyntacticCSharpTypeInvocationDataAssociator
    : IQueryHandler<IGetAssociatedInvocationDataQuery<IUnassociatedSyntacticCSharpTypeInvocationData>, IInvalidatingSyntacticCSharpTypeAssociationQueryResponseCollector>
{
    /// <summary>Instantiates a <see cref="SyntacticCSharpTypeInvocationDataAssociator"/>, associating syntactic C# type arguments.</summary>
    public SyntacticCSharpTypeInvocationDataAssociator() { }

    void IQueryHandler<IGetAssociatedInvocationDataQuery<IUnassociatedSyntacticCSharpTypeInvocationData>, IInvalidatingSyntacticCSharpTypeAssociationQueryResponseCollector>.Handle(
        IGetAssociatedInvocationDataQuery<IUnassociatedSyntacticCSharpTypeInvocationData> query,
        IInvalidatingSyntacticCSharpTypeAssociationQueryResponseCollector queryResponseCollector)
    {
        if (query is null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        if (queryResponseCollector is null)
        {
            throw new ArgumentNullException(nameof(queryResponseCollector));
        }

        if (query.UnassociatedInvocationData.Parameters.Count != query.UnassociatedInvocationData.SyntacticArguments.Count)
        {
            queryResponseCollector.Invalidator.Invalidate();

            return;
        }

        for (var i = 0; i < query.UnassociatedInvocationData.Parameters.Count; i++)
        {
            var parameter = query.UnassociatedInvocationData.Parameters[i];
            var argumentData = query.UnassociatedInvocationData.SyntacticArguments[i];

            queryResponseCollector.Associations.Add(parameter, argumentData);
        }
    }
}