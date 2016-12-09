using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.CodeAnalysis.Text;

namespace NetAnalyzersDemo
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ViewModelNamingCodeFixProvider)), Shared]
    public class ViewModelNamingCodeFixProvider : CodeFixProvider
    {
        private const string title = "Add 'ViewModel' to the end of the name of the class";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(ViewModelNamingAnalyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var diagnostic = context.Diagnostics.First();

            // Register a code action that will invoke the fix.
            context.RegisterCodeFix(
                CodeAction.Create(
                    title: title,
                    createChangedDocument: c => GetTransformedDocumentAsync(context.Document, diagnostic, c),
                    equivalenceKey: title),
                diagnostic);
        }
        private static async Task<Document> GetTransformedDocumentAsync(Document document, Diagnostic diagnostic, CancellationToken cancellationToken)
        {
            var syntaxRoot = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
            var declaration = syntaxRoot.FindToken(diagnostic.Location.SourceSpan.Start).Parent.AncestorsAndSelf().OfType<ClassDeclarationSyntax>().First();
            var newDeclaration = declaration.WithIdentifier(SyntaxFactory.Identifier($"{declaration.Identifier.ValueText}ViewModel"));
            var newSyntaxRoot = syntaxRoot.ReplaceNode(declaration, newDeclaration);
            var newDocument = document.WithSyntaxRoot(newSyntaxRoot);

            return newDocument;
        }
    }
}