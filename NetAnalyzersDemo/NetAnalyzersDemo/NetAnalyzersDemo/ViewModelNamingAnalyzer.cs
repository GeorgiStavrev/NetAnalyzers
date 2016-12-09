using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace NetAnalyzersDemo
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ViewModelNamingAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "ViewModelNamingAnalyzer";

        // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
        // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Localizing%20Analyzers.md for more on localization
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.ViewModelNamingAnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.ViewModelNamingAnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.ViewModelNamingAnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";

        private static DiagnosticDescriptor Rule = 
            new DiagnosticDescriptor(
                DiagnosticId, 
                Title, 
                MessageFormat, 
                Category,
                DiagnosticSeverity.Error, 
                isEnabledByDefault: true, 
                description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            // TODO: Consider registering other actions that act on syntax instead of or in addition to symbols
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Analyzer%20Actions%20Semantics.md for more information
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ClassDeclaration);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var classDeclarationNode = (ClassDeclarationSyntax)context.Node;
            if (classDeclarationNode == null)
            {
                return;
            }

            string className = classDeclarationNode.Identifier.ValueText;

            if (classDeclarationNode != null && !className.EndsWith("ViewModel") && classDeclarationNode.BaseList != null)
            {
                foreach(SyntaxNode node in classDeclarationNode.BaseList.Types)
                {
                    SimpleBaseTypeSyntax baseTypeNode = node as SimpleBaseTypeSyntax;
                    if (baseTypeNode != null && ((IdentifierNameSyntax)baseTypeNode.Type).Identifier.ValueText == "BaseViewModel")
                    {
                        // For all such symbols, produce a diagnostic.
                        var diagnostic = Diagnostic.Create(Rule, classDeclarationNode.GetLocation(), classDeclarationNode.Identifier.ValueText);

                        context.ReportDiagnostic(diagnostic);
                    }
                }
            }
        }
    }
}
