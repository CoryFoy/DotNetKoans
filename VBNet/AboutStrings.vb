Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports Xunit
Imports System.Linq

Public Class AboutStrings
    Inherits Koan
    'Note: This is one of the longest katas and, perhaps, one
    'of the most important. String behavior in .NET is not
    'always what you expect it to be, especially when it comes
    'to concatenation and newlines, and is one of the biggest
    'causes of memory leaks in .NET applications


    <Koan(1)> _
    Public Sub DoubleQuotedStringsAreStrings()
        Dim str = "Hello, World"
        Assert.Equal(GetType(FillMeIn), str.GetType())
    End Sub

    <Koan(2)> _
    Public Sub SingleQuotedStringsAreNotStrings()
        Dim str = "H"c
        Assert.Equal(GetType(FillMeIn), str.GetType())
    End Sub

    <Koan(3)> _
    Public Sub CreateAStringWhichContainsDoubleQuotes()
        Dim str = "Hello, " + Chr(34) + "World" + Chr(34) + ""
        Assert.Equal(FILL_ME_IN, str.Length)
    End Sub

    <Koan(4)> _
    Public Sub AnotherWayToCreateAStringWhichContainsDoubleQuotes()
        'The @ symbol creates a 'verbatim string literal'. 
        'Here's one thing you can do with it:
        Dim str = "Hello, ""World"""
        Assert.Equal(FILL_ME_IN, str.Length)
    End Sub

    <Koan(5)> _
    Public Sub StringsCanHandleFlexibleQuoting()
        Dim strA = "Verbatim Strings can handle both ' and "" characters (when escaped)"
        Dim strB = "Verbatim Strings can handle both ' and " + Chr(34) + " characters (when escaped)"
        Assert.Equal(FILL_ME_IN, strA.Equals(strB))
    End Sub

    '<Koan(6)> _
    'Public Sub StringsCanHandleMultipleLinesToo()
    '    'Tip: What you create for the literal string will have to 
    '    'escape the newline characters. For Windows, that would be
    '    ' \r\n. If you are on non-Windows, that would just be \n.
    '    'We'll show a different way next.
    '    Dim verbatimString = "I"
    '    am(a)
    '        broken line"
    '    Assert.Equal(20, verbatimString.Length)
    '    Dim literalString = FILL_ME_IN
    '    Assert.Equal(literalString, verbatimString)
    'End Sub

    <Koan(6)> _
    Public Sub ACrossPlatformWayToHandleLineEndings()
        'Since line endings are different on different platforms
        '(\r\n for Windows, \n for Linux) you shouldn't just type in
        'the hardcoded escape sequence. A much better way
        '(We'll handle concatenation and better ways of that in a bit)
        Dim literalString = "I" + System.Environment.NewLine + "am a" + System.Environment.NewLine + "broken line"
        Dim vebatimString = FILL_ME_IN
        Assert.Equal(literalString, vebatimString)
    End Sub

    <Koan(7)> _
    Public Sub PlusWillConcatenateTwoStrings()
        Dim str = "Hello, " + "World"
        Assert.Equal(FILL_ME_IN, str)
    End Sub

    <Koan(8)> _
    Public Sub AmpersandWillAlsoConcatenateTwoStrings()
        Dim str = "Hello, " & "World"
        Assert.Equal(FILL_ME_IN, str)
    End Sub


    <Koan(9)> _
    Public Sub ConcatenationWillNotModifyOriginalStrings()
        Dim strA = "Hello, "
        Dim strB = "World"
        Dim fullString = strA + strB
        Assert.Equal(FILL_ME_IN, strA)
        Assert.Equal(FILL_ME_IN, strB)
    End Sub

    <Koan(10)> _
    Public Sub PlusEqualsWillModifyTheTargetString()
        Dim strA = "Hello, "
        Dim strB = "World"
        strA += strB
        Assert.Equal(FILL_ME_IN, strA)
        Assert.Equal(FILL_ME_IN, strB)
    End Sub

    <Koan(11)> _
    Public Sub StringsAreReallyImmutable()
        'So here's the thing. Concatenating strings is cool
        'and all. But if you think you are modifying the original
        'string, you'd be wrong. 
        Dim strA = "Hello, "
        Dim originalString = strA
        Dim strB = "World"
        strA += strB
        Assert.Equal(FILL_ME_IN, originalString)
        'What just happened? Well, the string concatenation actually
        'takes strA and strB and creates a *new* string in memory
        'that has the new value. It does *not* modify the original
        'string. This is a very important point - if you do this kind
        'of string concatenation in a tight loop, you'll use a lot of memory
        'because the original string will hang around in memory until the
        'garbage collector picks it up. Let's look at a better way
        'when dealing with lots of concatenation
    End Sub

    <Koan(12)> _
    Public Sub ABetterWayToConcatenateLotsOfStrings()
        'As shows in the above Koan, concatenating lots of strings
        'is a Bad Idea(tm). If you need to do that, then do this instead
        Dim strBuilder = New System.Text.StringBuilder()
        Dim i As Integer = 0
        While i < 100
            strBuilder.Append("a")
            i += 1
        End While
        Dim str = strBuilder.ToString()
        Assert.Equal(FILL_ME_IN, str.Length)
        'The tradeoff is that you have to create a StringBuilder object, 
        'which is a higher overhead than a string. So the rule of thumb
        'is that if you need to concatenate less than 5 strings, += is fine.
        'If you need more than that, use a StringBuilder. 
    End Sub

    <Koan(13)> _
    Public Sub VBStringsDoNotInterpretEscapeCharacters()
        Dim str = "\n"
        Assert.Equal(FILL_ME_IN, str.Length)
    End Sub

    <Koan(14)> _
    Public Sub VBStringsStillDoNotInterpretEscapeCharacters()
        Dim str = "\"
        Assert.Equal(FILL_ME_IN, str.Length)
    End Sub

    <Koan(15)> _
    Public Sub YouDoNotNeedConcatenationToInsertVariablesInAString()
        Dim world = "World"
        Dim str = String.Format("Hello, {0}", world)
        Assert.Equal(FILL_ME_IN, str)
    End Sub

    <Koan(16)> _
    Public Sub AnyExpressionCanBeUsedInFormatString()
        Dim str = String.Format("The square root of 9 is {0}", Math.Sqrt(9))
        Assert.Equal(FILL_ME_IN, str)
    End Sub

    <Koan(17)> _
    Public Sub YouCanGetASubstringFromAString()
        Dim str = "Bacon, lettuce and tomato"
        Assert.Equal(FILL_ME_IN, str.Substring(19))
        Assert.Equal(FILL_ME_IN, str.Substring(7, 3))
    End Sub

    <Koan(18)> _
    Public Sub YouCanGetASingleCharacterFromAString()
        Dim str = "Bacon, lettuce and tomato"
        Assert.Equal(FILL_ME_IN, str(0))
    End Sub

    <Koan(19)> _
    Public Sub StringsCanBeSplit()
        Dim str = "Sausage Egg Cheese"
        Dim words As String() = str.Split()
        Assert.Equal(New String() {FILL_ME_IN}, words)
    End Sub

    <Koan(20)> _
    Public Sub StringsCanBeSplitUsingCharacters()
        Dim str = "the:rain:in:spain"
        Dim words As String() = str.Split(":"c)
        Assert.Equal(New String() {FILL_ME_IN}, words)
    End Sub

    <Koan(21)> _
    Public Sub StringsCanBeSplitUsingRegularExpressions()
        Dim str = "the:rain:in:spain"
        Dim regex = New System.Text.RegularExpressions.Regex(":")
        Dim words As String() = regex.Split(str)
        Assert.Equal(New String() {FILL_ME_IN}, words)
        'A full treatment of regular expressions is beyond the scope
        'of this tutorial. The book "Mastering Regular Expressions"
        'is highly recommended to be on your bookshelf
    End Sub
End Class