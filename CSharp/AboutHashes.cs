using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace DotNetKoans.CSharp
{
    public class AboutHashes : Koan
    {
        [Koan(1)]
        public void CreatingHashes()
        {
            var hash = new Hashtable();
            Assert.Equal(typeof(System.Collections.Hashtable), hash.GetType());
            Assert.Equal(FILL_ME_IN, hash.Count);
        }

        [Koan(2)]
        public void HashLiterals()
        {
            //There are several ways to get similar styles in C# to Ruby
            //See Haacked's blog here: http://haacked.com/archive/2008/01/06/collection-initializers.aspx
            //This is one way:
            var hash = new Hashtable() { { "one", "uno" }, { "two", "dos" } };
            Assert.Equal(FILL_ME_IN, hash.Count);
        }

        [Koan(3)]
        public void AccessingHashes()
        {
            var hash = new Hashtable() { { "one", "uno" }, { "two", "dos" } };
            Assert.Equal(FILL_ME_IN, hash["one"]);
            Assert.Equal(FILL_ME_IN, hash["two"]);
            Assert.Equal(FILL_ME_IN, hash["doesntExist"]);
        }

        [Koan(4)]
        public void ChangingHashes()
        {
            var hash = new Hashtable() { { "one", "uno" }, { "two", "dos" } };
            hash["one"] = "eins";

            var expected = new Hashtable() { { "one", FILL_ME_IN }, { "two", "dos" } };
            Assert.Equal(expected, hash);
        }

        [Koan(5)]
        public void HashIsUnordered()
        {
            var hash1 = new Hashtable() { { "one", "uno" }, { "two", "dos" } };
            var hash2 = new Hashtable() { { "two", "dos" }, { "one", "uno" } };
            Assert.Equal(hash1, hash2);
        }

        [Koan(6)]
        public void HashKeysAndValues()
        {
            var hash = new Hashtable() { { "one", "uno" }, { "two", "dos" } };

            //Warning: Unfamiliar syntax ahead. Because the hashtable keys
            //only return an ICollection, there isn't a good way to ask it
            //if it matches, or contains values. So we are using a trick
            //from LINQ to cast it over. Note that the casting is not important
            //for this Koan - it's the value of the keys that is interesting

            var expectedKeys = new List<string>() { "one", "two" };
            expectedKeys.Sort();
            var actualKeys = hash.Keys.Cast<string>().ToList();
            actualKeys.Sort();

            Assert.Equal(expectedKeys, actualKeys);

            var expectedValues = new List<string>() { FILL_ME_IN.ToString(), FILL_ME_IN.ToString() };
            expectedValues.Sort();
            var actualValues = hash.Values.Cast<string>().ToList();
            actualValues.Sort();

            Assert.Equal(expectedValues, actualValues);
        }

        [Koan(7)]
        public void CombiningHashes()
        {
            var hash = new Hashtable() { { "jim", 53 }, {"amy", 20}, {"dan", 23}};

            //We can't add the same key:
            Assert.Throws(typeof(FillMeIn), delegate() { hash.Add("jim", 54); });

            //But let's say we wanted to merge two Hashtables? 
            //We have the following:
            var newHash = new Hashtable() { { "jim", 54 }, { "jenny", 26 } };

            //and we want to 'merge' this into our first hashtable. This will do
            //the trick
            foreach (DictionaryEntry item in newHash)
            {
                hash[item.Key] = item.Value;
            }

            Assert.Equal(FILL_ME_IN, hash["jim"]);
            Assert.Equal(FILL_ME_IN, hash["jenny"]);
            Assert.Equal(FILL_ME_IN, hash["amy"]);

        }
    }
}
