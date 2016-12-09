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
    public class ViewModelInheritanceAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "ViewModelInheritanceAnalyzer";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.ViewModelInheritanceAnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.ViewModelInheritanceAnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.ViewModelInheritanceAnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Inheritance";
        
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
            context.RegisterSyntaxNodeAction(AnalyzeClassDeclaration, SyntaxKind.ClassDeclaration);
        }

        private void AnalyzeClassDeclaration(SyntaxNodeAnalysisContext context)
        {
            var classDeclarationNode = (ClassDeclarationSyntax)context.Node;
            if (classDeclarationNode == null)
            {
                return;
            }

            string className = classDeclarationNode.Identifier.ValueText;

            if (className != "BaseViewModel" && className.EndsWith("ViewModel"))
            {
                bool baseViewModelBaseTypeFound = false;
                if (classDeclarationNode.BaseList != null)
                {
                    foreach (SyntaxNode node in classDeclarationNode.BaseList.Types)
                    {
                        SimpleBaseTypeSyntax baseTypeNode = node as SimpleBaseTypeSyntax;
                        if (baseTypeNode != null 
                            && baseTypeNode.Type is IdentifierNameSyntax 
                            && ((IdentifierNameSyntax)baseTypeNode.Type).Identifier.ValueText == "BaseViewModel")
                        {
                            baseViewModelBaseTypeFound = true;
                        }
                    }
                }

                if(!baseViewModelBaseTypeFound)
                {
                    var diagnostic = Diagnostic.Create(Rule, classDeclarationNode.GetLocation(), classDeclarationNode.Identifier.ValueText);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }
}
