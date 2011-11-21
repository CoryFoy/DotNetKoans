Imports Xunit

Public Class AboutMethods
    Inherits Koan
    'Extension Methods allow us to "add" methods to any class
    'without having to recompile. You only have to reference the
    'namespace the methods are in to use them. Here, since both the
    'ExtensionMethods class and the AboutMethods class are in the
    'DotNetKoans.CSharp namespace, AboutMethods can automatically
    'find them
    <Koan(1)> _
    Public Sub ExtensionMethodsShowUpInTheCurrentClass()
        Assert.Equal(FILL_ME_IN, Me.HelloWorld())
    End Sub

    <Koan(2)> _
    Public Sub ExtensionMethodsWithParameters()
        Assert.Equal(FILL_ME_IN, Me.SayHello("Cory"))
    End Sub

    <Koan(3)> _
    Public Sub ExtensionMethodsWithVariableParameters()
        Assert.Equal(FILL_ME_IN, Me.MethodWithVariableArguments("Cory", "Will", "Corey"))
    End Sub

    'Extension methods can extend any class my referencing 
    'the name of the class they are extending. For example, 
    'we can "extend" the string class like so:
    <Koan(4)> _
    Public Sub ExtendingCoreClasses()
        Assert.Equal(FILL_ME_IN, "Cory".SayHi())
    End Sub

    'Of course, any of the parameter things you can do with 
    'extension methods you can also do with local methods
    Private Function LocalMethodWithVariableParameters(ByVal ParamArray names As String()) As String()
        Return names
    End Function

    <Koan(5)> _
    Public Sub LocalMethodsWithVariableParams()
        Assert.Equal(FILL_ME_IN, Me.LocalMethodWithVariableParameters("Cory", "Will", "Corey"))
    End Sub

    'Note how we called the method by saying "this.LocalMethodWithVariableParameters"
    'That isn't necessary for local methods
    <Koan(6)> _
    Public Sub LocalMethodsWithoutExplicitReceiver()
        Assert.Equal(FILL_ME_IN, LocalMethodWithVariableParameters("Cory", "Will", "Corey"))
    End Sub

    'But it is required for Extension Methods, since it needs
    'an instance variable. So this wouldn't work, giving a
    'compile-time error:
    'Assert.Equal(FILL_ME_IN, MethodWithVariableArguments("Cory", "Will", "Corey"));
    Class InnerSecret
        Public Shared Function Key() As String
            Return "Key"
        End Function

        Public Function Secret() As String
            Return "Secret"
        End Function

        Protected Function SuperSecret() As String
            Return "This is secret"
        End Function

        Private Function SooperSeekrit() As String
            Return "No one will find me!"
        End Function
    End Class

    Class StateSecret
        Inherits InnerSecret
        Public Function InformationLeak() As String
            Return SuperSecret()
        End Function
    End Class

    'Static methods don't require an instance of the object
    'in order to be called. 
    <Koan(7)> _
    Public Sub CallingStaticMethodsWithoutAnInstance()
        Assert.Equal(FILL_ME_IN, InnerSecret.Key())
    End Sub

    'In fact, you can't call it on an instance variable
    'of the object. So this wouldn't compile:
    'InnerSecret secret = new InnerSecret();
    'Assert.Equal(FILL_ME_IN, secret.Key());
    <Koan(8)> _
    Public Sub CallingPublicMethodsOnAnInstance()
        Dim secret As InnerSecret = New InnerSecret()
        Assert.Equal(FILL_ME_IN, secret.Secret())
    End Sub

    'Protected methods can only be called by a subclass
    'We're going to call the public method called
    'InformationLeak of the StateSecret class which returns
    'the value from the protected method SuperSecret
    <Koan(9)> _
    Public Sub CallingProtectedMethodsOnAnInstance()
        Dim secret As StateSecret = New StateSecret()
        Assert.Equal(FILL_ME_IN, secret.InformationLeak())
    End Sub

    'But, we can't call the private methods of InnerSecret
    'either through an instance, or through a subclass. It
    'just isn't available.
    'Ok, well, that isn't entirely true. Reflection can get
    'you just about anything, and though it's way out of scope
    'for this...
    <Koan(10)> _
    Public Sub SubvertPrivateMethods()
        Dim secret As InnerSecret = New InnerSecret()
        Dim superSecretMessage As String = IIf(TypeOf secret.GetType().GetMethod("SooperSeekrit", System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance).Invoke(secret, Nothing) Is String, CType(secret.GetType().GetMethod("SooperSeekrit", System.Reflection.BindingFlags.NonPublic Or System.Reflection.BindingFlags.Instance).Invoke(secret, Nothing), String), Nothing)
        Assert.Equal(FILL_ME_IN, superSecretMessage)
    End Sub

    'Up till now we've had explicit return types. It's also
    'possible to create methods which dynamically shift
    'the type based on the input. These are referred to
    'as generics
    Public Shared Function GiveMeBack(Of T)(ByVal p1 As T) As T
        Return p1
    End Function

    <Koan(11)> _
    Public Sub CallingGenericMethods()
        Assert.Equal(GetType(FillMeIn), GiveMeBack(Of Integer)(1).GetType())
        Assert.Equal(FILL_ME_IN, GiveMeBack(Of String)("Hi!"))
    End Sub
End Class