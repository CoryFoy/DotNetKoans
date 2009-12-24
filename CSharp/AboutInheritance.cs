using Xunit;

namespace DotNetKoans.CSharp
{
    public class AboutInheritance : Koan
    {
        public class Dog
        {
            public string Name { get; set; }

            public Dog(string name)
            {
                Name = name;
            }

            // For a method/function to be overidden by sub-classes, it must be virtual.
            public virtual string Bark()
            {
                return "WOOF";
            }
        }

        public class Chihuahua : Dog
        {
            // The only way to "construct" a Dog is to give it a name. Since a 
            // Chihuahua 'is a Dog' it must conform to a public/protected
            // constructor. Since the only public/protected constructor for a 
            // dog requires a name, a public/protected constructor must also
            // require a Name.
            public Chihuahua(string name) : base(name)
            {
            }

            // For a Chihuahua to do something different than a regular "Dog"
            // when called to "Bark", the base class must be virtual and the
            // derived class must declare it as "override".
            public override string Bark()
            {
                return "yip";
            }

            // A derived class can have have methods/functions or properties
            // that are new behaviors altogether.
            public string Wag()
            {
                return "Happy";
            }
        }

        [Koan(1)]
        public void SubclassesHaveTheParentAsAnAncestor()
        {
            Assert.True(typeof(Chihuahua).IsAssignableFrom(FILL_ME_IN.GetType()));
        }

        [Koan(2)]
        public void AllClassesUltimatelyInheritFromAnObject()
        {
            Assert.True(typeof(Chihuahua).IsAssignableFrom(FILL_ME_IN.GetType()));
        }

        [Koan(3)]
        public void SubclassesInheritBehaviorFromParentClass()
        {
            var chico = new Chihuahua("Chico");
            Assert.Equal(FILL_ME_IN, chico.Name);
        }

        [Koan(4)]
        public void SubclassesAddNewBehavior()
        {
            var chico = new Chihuahua("Chico");
            Assert.Equal(FILL_ME_IN, chico.Wag());

            // I'm not quite sure of the best way to represent this concept
            // in .Net since most ways will not even compile.

            //var dog = (Dog)chico;
            //Assert.Throws(FILL_ME_IN.GetType(), delegate() { dog.Wag(); });
        }

        [Koan(5)]
        public void SubclassesCanModifyExistingBehavior()
        {
            var chico = new Chihuahua("Chico");
            Assert.Equal(FILL_ME_IN, chico.Bark());

            var fido = new Dog("Fido");
            Assert.Equal(FILL_ME_IN, fido.Bark());
        }
    }
}