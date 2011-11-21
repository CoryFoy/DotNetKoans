
Public Module ExtensionMethods
    <System.Runtime.CompilerServices.Extension()> _
    Public Function HelloWorld(ByVal koan As Koan) As String
        Return "Hello!"
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function SayHello(ByVal koan As Koan, ByVal name As String) As String
        Return String.Format("Hello, {0}!", name)
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function MethodWithVariableArguments(ByVal koan As Koan, ByVal ParamArray names As String()) As String()
        Return names
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function SayHi(ByVal str As String) As String
        Return "Hi, " + str
    End Function
End Module
