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
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(ViewModelInheritanceCodeFixProvider)), Shared]
    public class ViewModelInheritanceCodeFixProvider : CodeFixProvider
    {
        private const string title = "Inherit from BaseViewModel";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(ViewModelInheritanceAnalyzer.DiagnosticId); }
        }

        public sealed override FixAllProvider GetFixAllProvider()
        {
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
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

            var newBaseList = declaration.BaseList != null
                ? declaration.BaseList.AddTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseName("BaseViewModel")))
                : SyntaxFactory.BaseList(SyntaxFactory.SeparatedList(new BaseTypeSyntax[] {
                        SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseName("BaseViewModel")) }));
            var newDeclaration = declaration.WithBaseList(newBaseList);
            var newSyntaxRoot = syntaxRoot.ReplaceNode(declaration, newDeclaration);
            var newDocument = document.WithSyntaxRoot(newSyntaxRoot);

            return newDocument;
        }
    }
}