Imports Xunit

Public Class AboutArrays
    Inherits Koan
    <Koan(1)> _
    Public Sub CreatingArrays()
        Dim empty_array = New Object() {}
        Assert.Equal(GetType(FillMeIn), empty_array.GetType())
        'Note that you have to explicitly check for subclasses
        Assert.True(GetType(Array).IsAssignableFrom(empty_array.GetType()))
        Assert.Equal(FILL_ME_IN, empty_array.Length)
    End Sub

    <Koan(2)> _
    Public Sub ArrayLiterals()
        'You don't have to specify a type if the arguments can be inferred
        Dim array = {42,3,2,4}
        Assert.Equal(GetType(Integer()), Array.GetType())
        Assert.Equal(New Integer() {42}, Array)
        'Are arrays 0-based or 1-based?
        Assert.Equal(42, Array(CType(FILL_ME_IN, Integer)))
        'This is important because...
        Assert.True(Array.IsFixedSize)
        '...it means we can't do this: array(1) = 13;
        Assert.Throws(GetType(FillMeIn), Sub() array(1) = 13)
        'This is because the array is fixed at length 1. You could write a function
        'which created a new array bigger than the last, copied the elements over, and
        'returned the new array. Or you could do this:
        Dim dynamicArray As List(Of Integer) = New List(Of Integer)()
        dynamicArray.Add(42)
        Assert.Equal(Array, dynamicArray.ToArray())
        dynamicArray.Add(13)
        Assert.Equal((New Integer() {42, CType(FILL_ME_IN, Integer)}), dynamicArray.ToArray())
    End Sub

    <Koan(3)> _
    Public Sub AccessingArrayElements()
        Dim array = New String() {"peanut", "butter", "and", "jelly"}
        Assert.Equal(FILL_ME_IN, Array(0))
        Assert.Equal(FILL_ME_IN, Array(3))
        'This doesn't work: Assert.Equal(FILL_ME_IN, array(-1));
    End Sub

    <Koan(4)> _
    Public Sub SlicingArrays()
        Dim array = New String() {"peanut", "butter", "and", "jelly"}
        Assert.Equal(New String() {CType(FILL_ME_IN, String), CType(FILL_ME_IN, String)}, Array.Take(2).ToArray())
        Assert.Equal(New String() {CType(FILL_ME_IN, String), CType(FILL_ME_IN, String)}, Array.Skip(1).Take(2).ToArray())
    End Sub

    <Koan(5)> _
    Public Sub PushingAndPopping()
        Dim array = New Integer() {1, 2}
        Dim stack As Stack = New Stack(Array)
        stack.Push("last")
        Assert.Equal(FILL_ME_IN, stack.ToArray())
        Dim poppedValue = stack.Pop()
        Assert.Equal(FILL_ME_IN, poppedValue)
        Assert.Equal(FILL_ME_IN, stack.ToArray())
    End Sub

    <Koan(6)> _
    Public Sub Shifting()
        'Shift == Remove First Element
        'Unshift == Insert Element at Beginning
        'C# doesn't provide this natively. You have a couple
        'of options, but we'll use the LinkedList<T> to implement
        Dim array = New String() {"Hello", "World"}
        Dim list = New LinkedList(Of String)(Array)
        list.AddFirst("Say")
        Assert.Equal(FILL_ME_IN, list.ToArray())
        list.RemoveLast()
        Assert.Equal(FILL_ME_IN, list.ToArray())
        list.RemoveFirst()
        Assert.Equal(FILL_ME_IN, list.ToArray())
        list.AddAfter(list.Find("Hello"), "World")
        Assert.Equal(FILL_ME_IN, list.ToArray())
    End Sub
End Class