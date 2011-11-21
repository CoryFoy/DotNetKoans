'Imports Xunit

'Public Class AboutHashes
'    Inherits Koan
'#Region "Utilities"
'    Private Function CreateHashTable(ByVal Key1 As String, ByVal Value1 As String, ByVal Key2 As String, ByVal Value2 As String) As Hashtable
'        Dim hashTable As New Hashtable
'        hashTable.Add(Key1, Value1)
'        hashTable.Add(Key2, Value2)
'        Return hashTable
'    End Function
'#End Region

'    <Koan(1)> _
'    Public Sub CreatingHashes()
'        Dim hash = New Hashtable()
'        Assert.Equal(GetType(System.Collections.Hashtable), hash.GetType())
'        Assert.Equal(FILL_ME_IN, hash.Count)
'    End Sub

'    <Koan(2)> _
'    Public Sub HashLiterals()
'        'There are several ways to get similar styles in C# to Ruby
'        'See Haacked's blog here: http://haacked.com/archive/2008/01/06/collection-initializers.aspx
'        'This is one way:
'            Dim hash = New Hashtable() With {With {"one", "uno"}, With {"two", "dos"}}
'        Assert.Equal(FILL_ME_IN, hash.Count)
'    End Sub

'    <Koan(3)> _
'    Public Sub AccessingHashes()
'            Dim hash = New Hashtable() With {With {"one", "uno"}, With {"two", "dos"}}
'        Assert.Equal(FILL_ME_IN, hash("one"))
'        Assert.Equal(FILL_ME_IN, hash("two"))
'        Assert.Equal(FILL_ME_IN, hash("doesntExist"))
'    End Sub


'    <Koan(4)> _
'    Public Sub ChangingHashes()
'        Dim hash = CreateHashTable("one", "uno", "two", "dos")
'        hash("one") = "eins"
'        Dim expected = CreateHashTable("one", FILL_ME_IN, "two", "dos")
'        Assert.Equal(expected, hash)
'    End Sub

'    <Koan(5)> _
'    Public Sub HashIsUnordered()
'        Dim hash1 = CreateHashTable("one", "uno", "two", "dos")
'        Dim hash2 = CreateHashTable("two", "dos", "one", "uno")
'        Assert.Equal(hash1, hash2)
'    End Sub

'    <Koan(6)> _
'    Public Sub HashKeysAndValues()
'            Dim hash = New Hashtable() With {With {"one", "uno"}, With {"two", "dos"}}
'        'Warning: Unfamiliar syntax ahead. Because the hashtable keys
'        'only return an ICollection, there isn't a good way to ask it
'        'if it matches, or contains values. So we are using a trick
'        'from LINQ to cast it over. Note that the casting is not important
'        'for this Koan - it's the value of the keys that is interesting
'            Dim expectedKeys = New List ( Of String )() With {"one", "two"}
'        expectedKeys.Sort()
'        Dim actualKeys = hash.Keys.Cast(Of String)().ToList()
'        actualKeys.Sort()
'        Assert.Equal(expectedKeys, actualKeys)
'            Dim expectedValues = New List ( Of String )() With {FILL_ME_IN.ToString(), FILL_ME_IN.ToString()}
'        expectedValues.Sort()
'        Dim actualValues = hash.Values.Cast(Of String)().ToList()
'        actualValues.Sort()
'        Assert.Equal(expectedValues, actualValues)
'    End Sub

'    <Koan(7)> _
'    Public Sub CombiningHashes()
'            Dim hash = New Hashtable() With {With {"jim", 53}, With {"amy", 20}, With {"dan", 23}}
'        'We can't add the same key:
'        Assert.Throws(GetType(FillMeIn), )
'        'But let's say we wanted to merge two Hashtables? 
'        'We have the following:
'            Dim newHash = New Hashtable() With {With {"jim", 54}, With {"jenny", 26}}
'        'and we want to 'merge' this into our first hashtable. This will do
'        'the trick
'        For Each item As DictionaryEntry In newHash
'            hash(item.Key) = item.Value
'        Next
'        Assert.Equal(FILL_ME_IN, hash("jim"))
'        Assert.Equal(FILL_ME_IN, hash("jenny"))
'        Assert.Equal(FILL_ME_IN, hash("amy"))
'    End Sub
'End Class
