namespace Minsk.CodeAnalysis.Syntax
{
    public enum SyntaxKind
    {
        // Tokens
        BadToken,
        EndOfFileToken,
        WhiteSpaceToken,
        NumberToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        BangToken,
        EqualsToken,
        AmpersandAmpersandToken,
        PipePipeToken,
        EqualsEqualsToken,
        BangEqualsToken,
        LessToken,
        LessOrEqualsToken,
        GreatToken,
        GreaterOrEqualsToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        OpenBraceToken,
        CloseBraceToken,
        IdentifierToken,

        // Keyword
        TrueKeyword,
        FalseKeyword,
        IfKeyword,
        ElseKeyword,
        LetKeyword,
        VarKeyword,

        // Nodes
        CompilationUnit,
        ElseClause,

        // Statements
        BlockStatement,
        VariableDeclaration,
        IfStatement,
        ExpressionStatement,

        // Expressions
        LiteralExpression,
        NameExpression,
        UnaryExpression,
        BinaryExpression,
        ParenthesizedExpression,
        AssignmentExpression,
    }
}
