Imports Xunit

Public Class AboutInheritance
    Inherits Koan
    Public Class Dog
        Public Property Name() As String

        Public Sub New(ByVal name As String)
            name = name
        End Sub

        ' For a method/function to be overidden by sub-classes, it must be virtual.
        Public Overridable Function Bark() As String
            Return "WOOF"
        End Function
    End Class

    Public Class Chihuahua
        Inherits Dog
        ' The only way to "construct" a Dog is to give it a name. Since a 
        ' Chihuahua 'is a Dog' it must conform to a public/protected
        ' constructor. Since the only public/protected constructor for a 
        ' dog requires a name, a public/protected constructor must also
        ' require a Name.
        Public Sub New(ByVal name As String)
            MyBase.New(name)

        End Sub

        'Unless it doesn't. You have to call the base constructor at some point
        'with a name, but you don't have to have your class conform to that spec:
        Public Sub New()
            MyBase.New("Ima Chihuahua")

        End Sub

        ' For a Chihuahua to do something different than a regular "Dog"
        ' when called to "Bark", the base class must be virtual and the
        ' derived class must declare it as "override".
        Public Overrides Function Bark() As String
            Return "yip"
        End Function

        ' A derived class can have have methods/functions or properties
        ' that are new behaviors altogether.
        Public Function Wag() As String
            Return "Happy"
        End Function
    End Class

    <Koan(1)> _
    Public Sub SubclassesHaveTheParentAsAnAncestor()
        Assert.True(GetType(FillMeIn).IsAssignableFrom(GetType(Chihuahua)))
    End Sub

    <Koan(2)> _
    Public Sub AllClassesUltimatelyInheritFromAnObject()
        Assert.True(GetType(FillMeIn).IsAssignableFrom(GetType(Chihuahua)))
    End Sub

    <Koan(3)> _
    Public Sub SubclassesInheritBehaviorFromParentClass()
        Dim chico = New Chihuahua("Chico")
        Assert.Equal(FILL_ME_IN, chico.Name)
    End Sub

    <Koan(4)> _
    Public Sub SubclassesAddNewBehavior()
        Dim chico = New Chihuahua("Chico")
        Assert.Equal(FILL_ME_IN, chico.Wag())
        'We can search the public methods of an object 
        'instance like this:
        Assert.NotNull(chico.GetType().GetMethod("Wag"))
        'So we can show that the Wag method isn't on Dog. 
        'Proving you can't wag the dog. 
        Dim dog = New Dog("Fluffy")
        Assert.Null(dog.GetType().GetMethod("Wag"))
    End Sub

    <Koan(5)> _
    Public Sub SubclassesCanModifyExistingBehavior()
        Dim chico = New Chihuahua("Chico")
        Assert.Equal(FILL_ME_IN, chico.Bark())
        'Note that even if we cast the object back to a dog
        'we still get the Chihuahua's behavior. It truly
        '"is-a" Chihuahua
        Dim dog As Dog = IIf(TypeOf chico Is Dog, CType(chico, Dog), Nothing)
        Assert.Equal(FILL_ME_IN, dog.Bark())
        Dim fido = New Dog("Fido")
        Assert.Equal(FILL_ME_IN, fido.Bark())
    End Sub

    Public Class ReallyYippyChihuahua
        Inherits Chihuahua
        Public Sub New(ByVal name As String)
            MyBase.New(name)

        End Sub

        'It is possible to redefine behavior for classes where
        'the methods were not marked virtual - but it can really
        'get you if you aren't careful. For example:
        Public Shadows Function Wag() As String
            Return "WAG WAG WAG!!"
        End Function
    End Class

    <Koan(6)> _
    Public Sub SubclassesCanRedefineBehaviorThatIsNotVirtual()
        Dim suzie As ReallyYippyChihuahua = New ReallyYippyChihuahua("Suzie")
        Assert.Equal(FILL_ME_IN, suzie.Wag())
    End Sub

    <Koan(7)> _
    Public Sub NewingAMethodDoesNotChangeTheBaseBehavior()
        'This is vital to understand. In Koan 6, you saw that the Wag
        'method did what we defined in our class. But what happens
        'when we do this?
        Dim bennie As Chihuahua = New ReallyYippyChihuahua("Bennie")
        Assert.Equal(FILL_ME_IN, bennie.Wag())
        'That's right. The behavior of the object is dependent solely
        'on who you are pretending to be. Unlike when you override a
        'virtual method. Remember this in your path to enlightenment.
    End Sub

    Public Class BullDog
        Inherits Dog
        Public Sub New(ByVal name As String)
            MyBase.New(name)

        End Sub

        Public Overrides Function Bark() As String
            Return MyBase.Bark() + ", GROWL"
        End Function
    End Class

    <Koan(8)> _
    Public Sub SubclassesCanInvokeParentBehaviorUsingBase()
        Dim ralph = New BullDog("Ralph")
        Assert.Equal(FILL_ME_IN, ralph.Bark())
    End Sub

    Public Class GreatDane
        Inherits Dog
        Public Sub New(ByVal name As String)
            MyBase.New(name)

        End Sub

        Public Function Growl() As String
            Return MyBase.Bark() + ", GROWL"
        End Function
    End Class

    <Koan(9)> _
    Public Sub YouCanCallBaseEvenFromOtherMethods()
        Dim george = New BullDog("George")
        Assert.Equal(FILL_ME_IN, george.Bark())
    End Sub
End Class
