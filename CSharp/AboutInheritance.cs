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

            //Unless it doesn't. You have to call the base constructor at some point
            //with a name, but you don't have to have your class conform to that spec:
            public Chihuahua() : base("Ima Chihuahua")
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
            Assert.True(typeof(FillMeIn).IsAssignableFrom(typeof(Chihuahua)));
        }

        [Koan(2)]
        public void AllClassesUltimatelyInheritFromAnObject()
        {
            Assert.True(typeof(FillMeIn).IsAssignableFrom(typeof(Chihuahua)));
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

            //We can search the public methods of an object 
            //instance like this:
            Assert.NotNull(chico.GetType().GetMethod("Wag"));

            //So we can show that the Wag method isn't on Dog. 
            //Proving you can't wag the dog. 
            var dog = new Dog("Fluffy");
            Assert.Null(dog.GetType().GetMethod("Wag"));
        }

        [Koan(5)]
        public void SubclassesCanModifyExistingBehavior()
        {
            var chico = new Chihuahua("Chico");
            Assert.Equal(FILL_ME_IN, chico.Bark());

            var fido = new Dog("Fido");
            Assert.Equal(FILL_ME_IN, fido.Bark());
        }

        public class BullDog : Dog
        {
            public BullDog(string name) : base(name) { }
            public override string Bark()
            {
                return base.Bark() + ", GROWL";
            }
        }

        [Koan(6)]
        public void SubclassesCanInvokeParentBehaviorUsingBase()
        {
            var ralph = new BullDog("Ralph");
            Assert.Equal(FILL_ME_IN, ralph.Bark());
        }

        public class GreatDane : Dog
        {
            public GreatDane(string name) : base(name) { }
            public string Growl()
            {
                return base.Bark() + ", GROWL";
            }
        }

        [Koan(7)]
        public void YouCanCallBaseEvenFromOtherMethods()
        {
            var george = new BullDog("George");
            Assert.Equal(FILL_ME_IN, george.Bark());
        }
    }
}